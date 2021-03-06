using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        //Repo for Business Layer
        private readonly IDiscountRepository _repository;

        //Constructor
        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            //Get discount and return with OK Status code
            var discount = await _repository.GetDiscount(productName);

            //If New default coupon was created, return
            if (discount.ProductName == "No Discount")
                return StatusCode(201, discount);
            return Ok(discount);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.NotModified)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            //Create discount and return with the GET endpoint
            var success = await _repository.CreateDiscount(coupon);

            //Return bad request if success is false
            if (!success)
                return StatusCode(304, success);
            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.NotModified)]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
        {
            //Update discount and return OK
            var success = await _repository.UpdateDiscount(coupon);

            //Return bad request if success is false
            if (!success)
                return StatusCode(304, success);
            return Ok(success);
        }

        [HttpDelete("{productName}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotModified)]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            //Updaet discount and return OK
            var success = await _repository.DeleteDiscount(productName);

            //Return bad request if success is false
            if (!success)
                return StatusCode(304, success);
            return Ok(success);
        }
    }
}
