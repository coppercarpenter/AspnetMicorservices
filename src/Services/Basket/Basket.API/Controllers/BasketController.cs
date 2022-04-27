using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Message.Events;
using MassTransit;
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
        private readonly  IMapper _mapper;
        private readonly  IPublishEndpoint _publishEndpoint;
        private readonly DiscountGrpcService _grpc;

        #endregion Private Fields

        #region Constructor

        public BasketController(IBasketRepository repo, DiscountGrpcService grpc, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repo = repo;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
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
            return Ok(await _repo.UpdateBasket(basket));
        }


        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckOut model)
        {
            var basket = await _repo.GetBasket(model.UserName);
            if (basket == null)
                return BadRequest();


            var message = _mapper.Map<BasketCheckOutEvent>(model);
            message.TotalPrice = basket.TotalPrice;

            await _publishEndpoint.Publish(message);

            await _repo.DeleteBasket(model.UserName);

            return Ok();
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