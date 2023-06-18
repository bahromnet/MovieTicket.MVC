using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Application.MVC.Common.Services.ServiceMovie;

public class MovieService : IMovieService
{
    private readonly IApplicationDbContext _context;

    public MovieService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
    {
        var response = new NewMovieDropdownsVM()
        {
            Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
            Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
            Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
        };

        return response;
    }
}
