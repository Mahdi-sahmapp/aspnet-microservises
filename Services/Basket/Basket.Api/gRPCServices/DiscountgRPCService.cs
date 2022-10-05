using Discount.gRPC.Protos;
using System.Threading.Tasks;

namespace Basket.Api.gRPCServices
{
    public class DiscountgRPCService
    {
        #region Constructor
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountgRPCService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }
        #endregion

        public async Task<CouponModel> GetDiscount(string productname)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productname };

            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }

    }
}
