using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Exceptions;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrders;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersByCountry;
using Ordering.Application.Features.Orders.Queries.GetOrdersByEmail;
using Ordering.Application.Features.Orders.Queries.GetOrdersByFirstName;
using Ordering.Application.Features.Orders.Queries.GetOrdersByLastName;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        //Objects
        private readonly IMediator _mediator;

        //Constructor
        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{userName}", Name = "GetOrder")]
        [ProducesResponseType(typeof(IEnumerable<OrdersVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersVm>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrdersListQuery(userName); //Set query and get orders
            var orders = await _mediator.Send(query); //Handle
            return Ok(orders);
        }

        [Route("[action]/{firstName}", Name = "ByFirstName")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrdersVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersVm>>> ByFirstName(string firstName)
        {
            var query = new GetOrdersByFirstNameQuery(firstName); //Set query and get orders
            var orders = await _mediator.Send(query); //Handle
            return Ok(orders);
        }

        [Route("[action]/{lastName}", Name = "ByLastName")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrdersVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersVm>>> ByLastName(string lastName)
        {
            var query = new GetOrdersByLastNameQuery(lastName); //Set query and get orders
            var orders = await _mediator.Send(query); //Handle
            return Ok(orders);
        }

        [Route("[action]/{email}", Name = "ByEmail")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrdersVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersVm>>> ByEmail(string email)
        {
            var query = new GetOrdersByEmailQuery(email); //Set query and get orders
            var orders = await _mediator.Send(query); //Handle
            return Ok(orders);
        }

        [Route("[action]/{country}", Name = "ByCountry")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrdersVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersVm>>> ByCountry(string country)
        {
            var query = new GetOrdersByCountryQuery(country); //SEt query and get orders
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        // testing purpose (not publically posted)
        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            //Get result from command [FromBody]
            var result = await _mediator.Send(command); //Handle command
            return Ok(result); //Return
        }

        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            try
            {
                //Get result from command [FromBody]
                await _mediator.Send(command); //Handle command
            } catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            try
            {
                //Get command
                var command = new DeleteOrderCommand() { Id = id };
                await _mediator.Send(command); //Handle
            } catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }

        [Route("[action]", Name = "DeleteOrders")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrders([FromBody] List<int> ids)
        {
            try
            {
                //Get command
                var command = new DeleteOrdersCommand() { Ids = ids };
                await _mediator.Send(command); //Handle
            } catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }
    }
}
