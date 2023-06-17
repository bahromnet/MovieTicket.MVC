using Application.MVC.Common.Exceptions;
using Application.MVC.Common.Interfaces;
using Domain.MVC.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MVC.UseCases.Movies.Commands;

public record DeleteMovieCommand(int Id) : IRequest;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteMovieCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Movies
            .Include(c => c.Cinema)
            .Include(p => p.Producer)
            .Include(am => am.ActorsMovies).ThenInclude(a => a.Actor)
            .FirstOrDefaultAsync(n => n.Id == request.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Movie), request.Id);
        }
        _context.Movies.Remove(entity);
        if (entity.Cinema != null)
        {
            _context.Cinemas.Remove(entity.Cinema);
        }

        if (entity.Producer != null)
        {
            _context.Producers.Remove(entity.Producer);
        }
        await _context.SaveChangesAsync();
    }
}
