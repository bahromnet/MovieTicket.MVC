using Application.MVC.Common.Interfaces;
using Domain.MVC.Entities;
using Domain.MVC.Enums;
using MediatR;

namespace Application.MVC.UseCases.Movies.Commands;

public record CreateMovieCommand : IRequest<int>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public double Price { get; init; }
    public string ImageURL { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public MovieCategory MovieCategory { get; init; }
    public Cinema Cinema { get; init; }
    public Producer Producer { get; init; }
    public List<int> ActorIds { get; init; }
    //public List<ActorMovie> ActorsMovies { get; init; }
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
            MovieCategory = request.MovieCategory,
            CinemaId = request.Cinema.Id,
            ProducerId = request.Producer.Id
        };

        _context.Movies.Add(entity);
        await _context.SaveChangesAsync();

        foreach (var actorId in request.ActorIds)
        {
            var newActorMovie = new ActorMovie()
            {
                ActorId = actorId,
                MovieId = entity.Id
            };
            await _context.ActorMovies.AddAsync(newActorMovie);
        }
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
