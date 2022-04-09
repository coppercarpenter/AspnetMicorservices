using Dapper;
using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    internal class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _config;

        public DiscountRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using (var connection = new NpgsqlConnection(_config.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                var affected = await connection.ExecuteAsync($"INSERT INTO coupon(productname, description, amount) VALUES({coupon.ProductName}, {coupon.Description}, {coupon.Amount})");
                if (affected == 0)
                    return false;

                return true;
            }
        }

        public async Task<bool> DeleteDiscount(string product_name)
        {
            using (var connection = new NpgsqlConnection(_config.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                var affected = await connection.ExecuteAsync($"DELETE FROM coupon WHERE productname={product_name}");
                if (affected == 0)
                    return false;

                return true;
            }
        }

        public async Task<Coupon> GetDiscount(string product_name)
        {
            using (var connection = new NpgsqlConnection(_config.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>($"select * from Coupon where productname = {product_name}");
                if (coupon == null)
                    return new Coupon() { Amount = 0, Description = "No Discount Desc", ProductName = "No Discount" };
                return coupon;
            }
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using (var connection = new NpgsqlConnection(_config.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                var affected = await connection.ExecuteAsync($"UPDATE coupon SET productname ={coupon.ProductName}, description ={coupon.Description}, amount ={coupon.Amount} WHERE id = {coupon.Id} ");
                if (affected == 0)
                    return false;

                return true;
            }
        }
    }
}
