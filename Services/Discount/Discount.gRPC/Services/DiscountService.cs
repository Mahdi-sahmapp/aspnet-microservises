using AutoMapper;
using Discount.gRPC.Entities;
using Discount.gRPC.Protos;
using Discount.gRPC.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.gRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {

        #region Constructor
        private readonly IDiscountRepository _discoutRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discoutRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discoutRepository = discoutRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discoutRepository.GetDiscount(request.ProductName);

            if (coupon == null) throw new RpcException(new Status(StatusCode.NotFound,
                  $"Discount with product name {request.ProductName} is not found"));

            _logger.LogInformation("Discount reterived.....!");
            return _mapper.Map<CouponModel>(coupon);
            // کد بالا همون کد پایی است با این تفاوت که از اتو مپر استفاده شده
            //return new CouponModel
            //{
            //    ProductName = coupon.ProductName,
            //    Id = coupon.Id,
            //    Amount = coupon.Amount,
            //    Description = coupon.Description
            //};
        }

        public override async Task<CouponModel> CreatDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            // CouponModel convert to Coupon
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _discoutRepository.CreateDiscount(coupon);
            _logger.LogInformation("Discount Created Successfully");

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {

            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _discoutRepository.UpdateDiscount(coupon);
            _logger.LogInformation("Update successfully.....!");

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var resuslt = await _discoutRepository.DeleteDiscount(request.ProductName);
            var response = new DeleteDiscountResponse { Success = resuslt };
            return response;
        }
    }
}
