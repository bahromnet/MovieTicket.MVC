using Application.MVC.Common.Models.ViewModels;

namespace Application.MVC.Common.Interfaces;

public interface IMovieService
{
    Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
}
