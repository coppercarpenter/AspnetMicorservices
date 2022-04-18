using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Presistance;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckOutOrder
{

    public class CheckOutOrderCommandHandler : IRequestHandler<CheckOutOrderCommand, long>
    {
        public readonly IOrderRepository _order;
        public readonly IMapper _mapper;
        //public readonly IEmailService _email;
        public readonly ILogger<CheckOutOrderCommandHandler> _logger;

        public CheckOutOrderCommandHandler(IOrderRepository order, IMapper mapper,  ILogger<CheckOutOrderCommandHandler> logger)
        {
            _order = order;
            _mapper = mapper;
            //_email = email;
            _logger = logger;
        }

        public async Task<long> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            var newOrder = await _order.AddAsync(order);

            _logger.LogInformation($"Order {newOrder.Id} is successfully Created");

            //await SendMail(newOrder);

            return newOrder.Id;
        }

        //private async Task SendMail(Order order)
        //{
        //    var email = new Email() { To = "sheheryar.sajid921@gmail.com", Body = $"Order was created.", Subject = "Order was created" };

        //    try
        //    {
        //        await _email.SendEmail(email);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
        //    }
        //}
    }
}
