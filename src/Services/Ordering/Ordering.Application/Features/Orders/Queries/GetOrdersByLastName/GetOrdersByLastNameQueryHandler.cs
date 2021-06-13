using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersByLastName
{
    public class GetOrdersByLastNameQueryHandler : IRequestHandler<GetOrdersByLastNameQuery, List<OrdersVm>>
    {
        //Objects
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        //Constructor
        public GetOrdersByLastNameQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        //Handle exposed API Endpoint for GetOrdersByLastNameQuery
        public async Task<List<OrdersVm>> Handle(GetOrdersByLastNameQuery request, CancellationToken cancellationToken)
        {
            //Get order's entity list
            var ordersList = await _orderRepository.GetOrdersByLastName(request.LastName);
            return _mapper.Map<List<OrdersVm>>(ordersList); //Map IEnumerable<Order> to LIst<OrdersVm>
        }
    }
}
