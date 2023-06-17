using Domain.MVC.Entities;

namespace Application.MVC.Common.Models.DtoModels;

public class NewMovieDropdownsDto
{
    public List<Producer> Producers { get; set; }
    public List<Cinema> Cinemas { get; set; }
    public List<Actor> Actors { get; set; }
    public NewMovieDropdownsDto()
    {
        Producers = new List<Producer>();
        Cinemas = new List<Cinema>();
        Actors = new List<Actor>();
    }   
}
