using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersByCountry
{
    public class GetOrdersByCountryQueryHandler : IRequestHandler<GetOrdersByCountryQuery, List<OrdersVm>>
    {
        //Objects
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        //Constructor
        public GetOrdersByCountryQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<OrdersVm>> Handle(GetOrdersByCountryQuery request, CancellationToken cancellationToken)
        {
            //Get order's entity list
            var ordersList = await _orderRepository.GetOrdersByCountry(request.Country);
            return _mapper.Map<List<OrdersVm>>(ordersList); //Map IEnmumberal<Order> to List<OrdersVm>
        }
    }
}
