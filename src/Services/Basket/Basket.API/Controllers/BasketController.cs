using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountGrpcServices;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        //Constructor
        public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcServices, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountGrpcServices = discountGrpcServices ?? throw new ArgumentNullException(nameof(discountGrpcServices));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            //Get basket from Redis database
            var basket = await _repository.GetBasket(userName);
            //If empty, return new Database
            if (basket == null)
                return StatusCode(201, new ShoppingCart(userName));
            return Ok(basket);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            //Communicate & Consume Discount.Grpc for every item in the basket
            //Calculate latest prices of products into the shopping cart
            foreach(var item in basket.Items)
            {
                //item.ProductName
                //Get coupon for coupon
                //Dapper and GRPC speed allows for this call to be made very quickly
                var coupon = await _discountGrpcServices.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            //Return updated basket
            return Ok(await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            //Delete the basket and return
            await _repository.DeleteBasket(userName);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {

            // Get existing basket with total price
            var basket = await _repository.GetBasket(basketCheckout.UserName);
            if(basket == null) return BadRequest(); //Basket doesn't exist

            // Create basketcheckoutevent -- set totalprice on basketcheckout eventmessage
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);

            // Send checkout event to rabbitmq
            eventMessage.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish(eventMessage);

            // Remove the basket
            await _repository.DeleteBasket(basket.UserName);
            return Accepted();
        }
    }
}
