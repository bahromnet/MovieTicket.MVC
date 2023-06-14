using System.ComponentModel.DataAnnotations;

namespace Domain.MVC.Entities;

public class ApplicationUser
{
    [Display(Name = "Full name")]
    public string FullName { get; set; }
}
