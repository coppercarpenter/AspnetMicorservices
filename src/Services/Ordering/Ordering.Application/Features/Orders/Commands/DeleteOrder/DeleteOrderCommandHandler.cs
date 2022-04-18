using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Presistance;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        public readonly IOrderRepository _order;
        public readonly IMapper _mapper;
        public readonly ILogger<DeleteOrderCommandHandler> _logger;


        public DeleteOrderCommandHandler(IOrderRepository order, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
        {
            _order = order;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _order.GetByIdAsync(request.Id);
            if (order == null)
            {
                _logger.LogError("Order not exist in database");
                throw new NotFoundException(nameof(Order), request.Id);
            }

            await _order.DeleteAsync(order);

            _logger.LogInformation($"Order {order.Id} is successfully deleted");

            return Unit.Value;
        }
    }
}
