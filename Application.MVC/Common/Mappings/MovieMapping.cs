using Application.MVC.Common.Models.DtoModels;
using AutoMapper;
using Domain.MVC.Entities;

namespace Application.MVC.Common.Mappings;

public class MovieMapping : Profile
{
    public MovieMapping()
    {
        CreateMap<Movie, MovieDto>().ReverseMap();
    }
}
