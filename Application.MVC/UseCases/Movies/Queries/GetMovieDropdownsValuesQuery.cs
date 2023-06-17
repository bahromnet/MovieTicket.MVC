using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models.DtoModels;
using Domain.MVC.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MVC.UseCases.Movies.Queries;

public record GetMovieDropdownsValuesQuery : IRequest<NewMovieDropdownsDto>
{
    public List<Producer> Producers { get; set; }
    public List<Cinema> Cinemas { get; set; }
    public List<Actor> Actors { get; set; }
    public GetMovieDropdownsValuesQuery()
    {
        Producers = new List<Producer>();
        Cinemas = new List<Cinema>();
        Actors = new List<Actor>();
    }
}

public class GetMovieDropdownsValuesQueryHandler : IRequestHandler<GetMovieDropdownsValuesQuery, NewMovieDropdownsDto>
{
    private readonly IApplicationDbContext _context;

    public GetMovieDropdownsValuesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<NewMovieDropdownsDto> Handle(GetMovieDropdownsValuesQuery request, CancellationToken cancellationToken)
    {
        var response = new NewMovieDropdownsDto()
        {
            Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
            Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
            Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
        };
        return response;
    }
}
