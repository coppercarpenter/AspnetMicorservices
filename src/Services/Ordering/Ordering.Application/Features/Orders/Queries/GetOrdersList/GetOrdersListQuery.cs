using MediatR;
using System.Collections.Generic;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{

    public class GetOrdersListQuery : IRequest<List<OrderVm>>
    {
        public GetOrdersListQuery(string username)
        {
            Username = username;
        }

        public string Username { get; set; }

    }
}
