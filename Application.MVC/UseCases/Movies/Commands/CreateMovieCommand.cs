using Application.MVC.Common.Interfaces;
using Domain.MVC.Entities;
using Domain.MVC.Enums;
using MediatR;

namespace Application.MVC.UseCases.Movies.Commands;

public record CreateMovieCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string ImageURL { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public MovieCategory MovieCategory { get; set; }
}

public class CreateMovieCommandHandle : IRequestHandler<CreateMovieCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateMovieCommandHandle(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var entity = new Movie
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            ImageURL = request.ImageURL,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            MovieCategory = request.MovieCategory
        };

        _context.Movies.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }
}
