using Application.MVC.Common.Models.DtoModels;
using AutoMapper;
using Domain.MVC.Entities;

namespace Application.MVC.Common.Mappings;

public class ProducerMapping : Profile
{
    public ProducerMapping()
    {
        CreateMap<Producer, ProducerDto>().ReverseMap();
    }
}
