using Discount.Grpc.Protos;
using System;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        //Constructor
        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
        }

        //Get discount method
        public async Task<CouponModel> GetDiscount(string productName)
        {
            //Create discount request object
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            //Return data
            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}
