using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersByFirstName
{
    public class GetOrdersByFirstNameQueryHandler : IRequestHandler<GetOrdersByFirstNameQuery, List<OrdersVm>>
    {
        //Objects
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        //Constructor
        public GetOrdersByFirstNameQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //Handle exposed API Endpoint for GetOrdersByFirstNameQuery
        public async Task<List<OrdersVm>> Handle(GetOrdersByFirstNameQuery request, CancellationToken cancellationToken)
        {
            //Get order's entity list
            var orderList = await _orderRepository.GetOrdersByFirstName(request.FirstName);
            return _mapper.Map<List<OrdersVm>>(orderList); //Map IEnumuerable<Order> to List<OrdersVm>
        }
    }
}
