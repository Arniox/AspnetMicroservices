using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Set up mapping profiles for visa versa Order to OrdersVm
            CreateMap<Order, OrdersVm>().ReverseMap();
            //Set up mapping profiles for visa versa Order to CheckoutOrderCommand
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            //Set up mapping profiles for visa versa Order to UpdateOrderCommand
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
