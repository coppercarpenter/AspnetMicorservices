using Ordering.Application.Contracts.Presistance;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Presistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            return await GetAsync(g => g.UserName.Equals(userName));
        }
    }
}
