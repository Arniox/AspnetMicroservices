using MediatR;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System;
using System.Collections.Generic;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersByLastName
{
    public class GetOrdersByLastNameQuery: IRequest<List<OrdersVm>>
    {
        public string LastName { get; set; }

        //Constructor
        public GetOrdersByLastNameQuery(string lastName)
        {
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }
    }
}
