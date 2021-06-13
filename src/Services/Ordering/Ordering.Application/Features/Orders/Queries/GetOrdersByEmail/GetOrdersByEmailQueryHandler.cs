using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersByEmail
{
    public class GetOrdersByEmailQueryHandler : IRequestHandler<GetOrdersByEmailQuery, List<OrdersVm>>
    {
        //Objects
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        //Constructor
        public GetOrdersByEmailQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<OrdersVm>> Handle(GetOrdersByEmailQuery request, CancellationToken cancellationToken)
        {
            //Get order's entity list
            var ordersList = await _orderRepository.GetOrdersByEmail(request.Email);
            return _mapper.Map<List<OrdersVm>>(ordersList); //Map IEnumerable<Order> to List<OrdersVm>
        }
    }
}
