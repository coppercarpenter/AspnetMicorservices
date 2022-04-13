using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        #region Private Fields
        private readonly IDiscountRepository _repo;


        #endregion

        #region Private Methods

        #endregion

        #region Constructor
        public DiscountController(IDiscountRepository repo)
        {
            _repo = repo;
        }
        #endregion

        #region Properties

        #endregion

        #region Fields

        #endregion

        #region Methods

        #region EndPoints

        #region POST
        [HttpPost("")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<bool>> CreateDiscount([FromBody] Coupon coupon)
        {
            await _repo.CreateDiscount(coupon);
            return CreatedAtRoute("GetDiscount", new { product_name = coupon.ProductName }, coupon);
        }
        #endregion

        #region GET
        [HttpGet("{product_name}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string product_name)
        {
            return Ok(await _repo.GetDiscount(product_name));
        }
        #endregion

        #region PUT
        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateDiscount([FromBody] Coupon coupon)
        {
            return Ok(await _repo.UpdateDiscount(coupon));
        }
        #endregion

        #region DELETE
        [HttpDelete("{product_name}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteDiscount(string product_name)
        {
            return Ok(await _repo.DeleteDiscount(product_name));
        }
        #endregion

        #endregion

        #endregion
    }
}
