using Application.MVC.Common.Models.DtoModels;
using AutoMapper;
using Domain.MVC.Entities;

namespace Application.MVC.Common.Mappings;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
    }
}
