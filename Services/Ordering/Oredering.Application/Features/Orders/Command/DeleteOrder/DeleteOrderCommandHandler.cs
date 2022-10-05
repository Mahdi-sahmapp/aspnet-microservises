using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using Oredering.Application.Contracts.Persistence;
using Oredering.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oredering.Application.Features.Orders.Command.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var Orderfordelete = await _orderRepository.GetByIdAsync(request.Id);

            if (Orderfordelete == null) 
            {
                _logger.LogError("Order not exsits!");
                throw new NotFoundEx(nameof(Order), request.Id);
            } 
            else
            {
                await _orderRepository.DeleteAsync(Orderfordelete);
                _logger.LogInformation($"Order {Orderfordelete.Id} deleted!");
            }           

            return Unit.Value;
        }
    }
}
