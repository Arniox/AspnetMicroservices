using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository repository, IMapper mapper, ILogger<DiscountService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //GET GRPC Service
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            //Get coupon
            var coupon = await _repository.GetDiscount(request.ProductName);
            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            }
            _logger.LogInformation("Discount is retrieved for ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            //Returned couponModel protobuff message type class
            //Convert from Entityframework Core Model to GRPC Model
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        //POST GRPC Service
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            //Get coupon
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            //Create coupon
            await _repository.CreateDiscount(coupon);
            _logger.LogInformation("Discount is successfully created. ProductName: {ProductName}", coupon.ProductName);

            //Remap back into couponModel and return
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        //PUT GRPC Service
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            //Get coupon
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            //Update coupon
            await _repository.UpdateDiscount(coupon);
            _logger.LogInformation("Discount is successfully updated. ProductName: {ProductName}", coupon.ProductName);

            //Remap back into couponModel and return
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        //DELETE GRPC Service
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            //get deleted satus
            var deleted = await _repository.DeleteDiscount(request.ProductName);
            //Create response
            var response = new DeleteDiscountResponse
            {
                Success = deleted
            };
            return response;
        }
    }
}
