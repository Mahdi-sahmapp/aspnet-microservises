using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using Oredering.Application.Contracts.Infrasteucture;
using Oredering.Application.Contracts.Persistence;
using Oredering.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oredering.Application.Features.Orders.Command.CheckoutOrder
{
    class CheckoutOrderCammandHandler : IRequestHandler<CheckoutOrderCammand, int>
    {
        private readonly IOrderRepository _orderRepository; 
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCammandHandler> _logger;

        public CheckoutOrderCammandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderCammandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCammand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var neworder = await _orderRepository.AddAsync(orderEntity);

            _logger.LogInformation($"New Order with ID {neworder.Id} has been Registerd successfully");

            await SendEmail(neworder);

            return neworder.Id;
        }

        private async Task SendEmail(Order ordere)
        {
            try
            {
                await _emailService.SendEmail(
                    new Email
                    {
                        To = "Mahdi@Gmail.com",
                        Body = "Hello Brother!",
                        Subject = "...."
                    });
            }
            catch (Exception)
            {
                _logger.LogError("Email has not been send ");
            }
        }
    }
}
