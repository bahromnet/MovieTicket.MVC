using Domain.MVC.Entities;
using Domain.MVC.Enums;

namespace Application.MVC.Common.Models.DtoModels;

public class MovieDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string ImageURL { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public MovieCategory MovieCategory { get; set; }
    public Cinema Cinema { get; set; }
    public Producer Producer { get; set; }
    public List<ActorMovie> ActorsMovies { get; init; }
}
