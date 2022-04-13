using System;
using System.Collections.Generic;
using System.Linq;
using Discount.Grpc.Protos;
using System.Threading.Tasks;
using Grpc.Core;
using Discount.Grpc.Repositories;
using Microsoft.Extensions.Logging;
using Discount.Grpc.Entities;
using AutoMapper;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        #region Private Fields
        private readonly IDiscountRepository _repo;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Private Methods

        #endregion

        #region Constructor
        public DiscountService(IDiscountRepository repo, ILogger<DiscountService> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Properties

        #endregion

        #region Fields

        #endregion

        #region Methods

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _repo.UpdateDiscount(coupon);

            _logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);


            return _mapper.Map<CouponModel>(coupon);
        }
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repo.GetDiscount(request.ProductName);
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product name ={request.ProductName} not found"));

            _logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);
            var res = _mapper.Map<CouponModel>(coupon);
            return res;

        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _repo.DeleteDiscount(request.ProductName);
            var response = new DeleteDiscountResponse
            {
                Success = deleted
            };

            return response;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _repo.CreateDiscount(coupon);

            _logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

            return _mapper.Map<CouponModel>(coupon);
        }
        #endregion


    }
}
