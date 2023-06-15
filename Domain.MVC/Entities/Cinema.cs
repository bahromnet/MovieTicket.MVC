using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.MVC.Entities;

[Table("cinema")]
public class Cinema
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Cinema Logo")]
    [Required(ErrorMessage = "Cinema logo is required")]
    public string Logo { get; set; }

    [Display(Name = "Cinema Name")]
    [Required(ErrorMessage = "Cinema name is required")]
    public string Name { get; set; }

    [Display(Name = "Description")]
    [Required(ErrorMessage = "Cinema description is required")]
    public string Description { get; set; }

    //Relationships
    public List<Movie> Movies { get; set; }
}
