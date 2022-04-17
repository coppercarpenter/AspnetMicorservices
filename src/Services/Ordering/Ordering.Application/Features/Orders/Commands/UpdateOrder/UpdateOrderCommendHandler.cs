using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Presistance;
using Ordering.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{

    public class UpdateOrderCommendHandler : IRequestHandler<UpdateOrderCommand>
    {
        public readonly IOrderRepository _order;
        public readonly IMapper _mapper;
        public readonly ILogger<UpdateOrderCommendHandler> _logger;

        public UpdateOrderCommendHandler(IOrderRepository order, IMapper mapper, ILogger<UpdateOrderCommendHandler> logger)
        {
            _order = order;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _order.GetByIdAsync(request.Id);
            if (order == null)
            {
                _logger.LogError("Order not exist in database");
                //throw new NotFoundException(nameof(Order), request.Id);
            }

            _mapper.Map(request, order, typeof(UpdateOrderCommand), typeof(Order));
            await _order.UpdateAsync(order);

            _logger.LogInformation($"Order {order.Id} is successfully updated");

            return Unit.Value;

        }
    }
}
