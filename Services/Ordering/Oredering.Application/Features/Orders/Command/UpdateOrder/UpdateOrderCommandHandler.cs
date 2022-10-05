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

namespace Oredering.Application.Features.Orders.Command.UpdateOrder
{
    class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, ILogger<UpdateOrderCommandHandler> logger, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForUpdate = await _orderRepository.GetByIdAsync(request.Id);
            if (orderForUpdate == null)
            {
                _logger.LogInformation("order is not exists");
                throw new NotFoundEx(nameof(Order), request.Id);
            }
            
            _mapper.Map(request, orderForUpdate, typeof(UpdateOrderCommand), typeof(Order));

            await _orderRepository.UpdateAsync(orderForUpdate);
            _logger.LogInformation($"order {orderForUpdate.Id} is successfully updated");

            return Unit.Value;
        }
    }
}
