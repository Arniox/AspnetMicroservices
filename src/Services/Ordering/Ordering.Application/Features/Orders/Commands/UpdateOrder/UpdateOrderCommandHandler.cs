using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        //Objects
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        //Constructor
        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            //Update order
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
            //Check if order was not found/not updated
            if(orderToUpdate == null)
            {
                _logger.LogError("Order does not exist on database");
                //throw new NotFoundException(nameof(Order), request.Id);
            }

            //Map object
            _mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommandHandler), typeof(Order));
            //Update
            await _orderRepository.UpdateAsync(orderToUpdate);

            //Log
            _logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated.");
            return Unit.Value; //Return null
        }
    }
}
