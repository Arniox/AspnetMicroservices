using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrders
{
    public class DeleteOrdersCommandHandler : IRequestHandler<DeleteOrdersCommand>
    {
        //Objects
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrdersCommandHandler> _logger;

        //Constructor
        public DeleteOrdersCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrdersCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //Handle
        public async Task<Unit> Handle(DeleteOrdersCommand request, CancellationToken cancellationToken)
        {
            //Get orders
            var ordersToDelete = await _orderRepository.GetByIdListAsync(request.Ids);
            //Check if orders was not found
            if(ordersToDelete == null || ordersToDelete.Count == 0)
            {
                _logger.LogError("Order's do not exist on the database.");
                throw new NotFoundException(nameof(Order), string.Join(", ", request.Ids));
            }

            //Delete orders
            await _orderRepository.DeleteManyAsync(ordersToDelete);
            _logger.LogInformation($"Orders {string.Join(", ", ordersToDelete.Select(i => i.Id))} where successfully deleted.");
            return Unit.Value; //Return null
        }
    }
}
