﻿using Basket.API.Entities;
using Basket.API.Repositories;
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

        //Constructor
        public BasketController(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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
            // TODO : Communicate with Discount.Grpc & Calculate latest prices of products into the shopping cart

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
    }
}
