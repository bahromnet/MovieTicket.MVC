using Application.MVC.Common.Models.DtoModels;
using AutoMapper;
using Domain.MVC.Entities;

namespace Application.MVC.Common.Mappings;

public class ActorMapping : Profile
{
    public ActorMapping()
    {
        CreateMap<Actor, ActorDto>().ReverseMap();
    }
}
