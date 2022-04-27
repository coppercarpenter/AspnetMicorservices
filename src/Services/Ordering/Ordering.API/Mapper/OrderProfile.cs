using AutoMapper;
using EventBus.Message.Events;
using Ordering.Application.Features.Orders.Commands.CheckOutOrder;

namespace Ordering.API.Mapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CheckOutOrderCommand, BasketCheckOutEvent>().ReverseMap();
        }
    }
}
