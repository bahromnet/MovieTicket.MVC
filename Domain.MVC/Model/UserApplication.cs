using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.MVC.Entities;

public class UserApplication : IdentityUser
{
    [Display(Name = "Full name")]
    public string FullName { get; set; }
}
