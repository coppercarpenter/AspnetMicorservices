using AutoMapper;
using EventBus.Message.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Features.Orders.Commands.CheckOutOrder;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckOutEvent>
    {
        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        #endregion Private Fields

        #region Constructor

        public BasketCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        #endregion Constructor

        #region Methods

        public async Task Consume(ConsumeContext<BasketCheckOutEvent> context)
        {
            var order = _mapper.Map<CheckOutOrderCommand>(context.Message);
            var result = await _mediator.Send(order);

            _logger.LogInformation($"BasketCheckoutEvent consumed successfully. Order id {result}");
        }

        #endregion Methods
    }
}