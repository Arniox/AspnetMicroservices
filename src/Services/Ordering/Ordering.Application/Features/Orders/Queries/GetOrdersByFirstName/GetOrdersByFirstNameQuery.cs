using MediatR;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System;
using System.Collections.Generic;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersByFirstName
{
    public class GetOrdersByFirstNameQuery : IRequest<List<OrdersVm>>
    {
        public string FirstName { get; set; }

        //Constructor
        public GetOrdersByFirstNameQuery(string firstName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        }
    }
}
