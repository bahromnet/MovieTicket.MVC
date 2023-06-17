using Application.MVC.Common.Exceptions;
using Application.MVC.Common.Interfaces;
using Domain.MVC.Entities;
using Domain.MVC.Enums;
using MediatR;

namespace Application.MVC.UseCases.Movies.Commands;

public record UpdateMovieCommand : IRequest
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string ImageURL { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public MovieCategory MovieCategory { get; set; }
    public Cinema Cinema { get; init; }
    public Producer Producer { get; init; }
    public List<int> ActorIds { get; init; }
}

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateMovieCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        Movie? entity = await _context.Movies.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(Movie), request.Id);

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Price = request.Price;
        entity.ImageURL = request.ImageURL;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.MovieCategory = request.MovieCategory;
        entity.CinemaId = request.Cinema.Id;
        entity.ProducerId = request.Producer.Id;

        await _context.SaveChangesAsync(cancellationToken);

        var existingActorsDb = _context.ActorMovies.Where(n => n.MovieId == request.Id).ToList();
        _context.ActorMovies.RemoveRange(existingActorsDb);
        await _context.SaveChangesAsync();

        foreach (var ActorId in request.ActorIds)
        {
            var newActorMovie = new ActorMovie()
            {
                MovieId = request.Id,
                ActorId = ActorId
            };
            await _context.ActorMovies.AddAsync(newActorMovie);
        }
        await _context.SaveChangesAsync(cancellationToken);
    }
}
