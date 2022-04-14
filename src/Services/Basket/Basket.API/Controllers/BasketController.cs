using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        #region Private Fields

        private readonly IBasketRepository _repo;
        private readonly DiscountGrpcService _grpc;

        #endregion Private Fields

        #region Constructor

        public BasketController(IBasketRepository repo, DiscountGrpcService grpc)
        {
            _repo = repo;
            _grpc = grpc;
        }

        #endregion Constructor

        #region Methods

        #region EndPoints

        #region POST

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            foreach (var item in basket.Items)
            {
                var coupon = await _grpc.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            var res = await _repo.UpdateBasket(basket);
            return Ok(res);
        }

        #endregion POST

        #region GET

        [HttpGet("{username}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
        {
            var res = await _repo.GetBasket(username);
            return Ok(res ?? new ShoppingCart(username));
        }

        #endregion GET

        #region DELETE

        [HttpDelete("{username}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> DeleteBasket(string username)
        {
            await _repo.DeleteBasket(username);
            return Ok();
        }

        #endregion DELETE

        #endregion EndPoints

        #endregion Methods
    }
}