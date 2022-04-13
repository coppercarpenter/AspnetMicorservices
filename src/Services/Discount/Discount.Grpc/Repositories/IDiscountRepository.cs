using Discount.Grpc.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{

    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string product_name);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string product_name);
    }
}
