using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Mapper
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            //Create map from Coupon to CouponModel and visa versa
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
