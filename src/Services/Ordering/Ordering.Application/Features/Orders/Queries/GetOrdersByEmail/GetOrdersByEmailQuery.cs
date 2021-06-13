using MediatR;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System;
using System.Collections.Generic;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersByEmail
{
    public class GetOrdersByEmailQuery: IRequest<List<OrdersVm>>
    {
        public string Email { get; set; }
        
        //Constructor
        public GetOrdersByEmailQuery(string email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }
    }
}
