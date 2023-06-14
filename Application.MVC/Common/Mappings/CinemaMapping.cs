using Application.MVC.Common.Models.DtoModels;
using AutoMapper;
using Domain.MVC.Entities;

namespace Application.MVC.Common.Mappings;

public class CinemaMapping : Profile
{
    public CinemaMapping()
    {
        CreateMap<Cinema, CinemaDto>().ReverseMap();
    }
}
