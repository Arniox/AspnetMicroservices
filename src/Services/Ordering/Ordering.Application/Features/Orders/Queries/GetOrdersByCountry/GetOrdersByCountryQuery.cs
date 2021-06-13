using MediatR;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System;
using System.Collections.Generic;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersByCountry
{
    public class GetOrdersByCountryQuery : IRequest<List<OrdersVm>>
    {
        public string Country { get; set; }

        //Constructor
        public GetOrdersByCountryQuery(string country)
        {
            Country = country ?? throw new ArgumentNullException(nameof(country));
        }
    }
}
