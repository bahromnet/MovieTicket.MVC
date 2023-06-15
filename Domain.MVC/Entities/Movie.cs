using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.MVC.Enums;

namespace Domain.MVC.Entities;

[Table("movie")]
public class Movie
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string ImageURL { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public MovieCategory MovieCategory { get; set; }

    //Relationships
    public List<ActorMovie> ActorsMovies { get; set; }

    //Cinema
    public int CinemaId { get; set; }
    [ForeignKey("CinemaId")]
    public Cinema Cinema { get; set; }

    //Producer
    public int ProducerId { get; set; }
    [ForeignKey("ProducerId")]
    public Producer Producer { get; set; }
}
