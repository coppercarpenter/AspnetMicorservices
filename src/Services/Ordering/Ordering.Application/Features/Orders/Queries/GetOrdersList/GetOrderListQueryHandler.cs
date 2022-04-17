using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Presistance;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderVm>>
    {
        public readonly IOrderRepository _order;
        public readonly IMapper _mapper;

        public GetOrderListQueryHandler(IOrderRepository order, IMapper mapper)
        {
            _order = order;
            _mapper = mapper;
        }


        public async Task<List<OrderVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _order.GetOrdersByUserName(request.Username);
            return _mapper.Map<List<OrderVm>>(orderList);
        }
    }
}
