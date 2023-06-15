using Application.MVC.Common.Exceptions;
using Application.MVC.Common.Interfaces;
using Domain.MVC.Entities;
using MediatR;

namespace Application.MVC.UseCases.Cinemas.Commands;

public record UpdateCinemaCommand : IRequest
{
    public int Id { get; init; }
    public string Logo { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
}

public class UpdateCinemaCommandHandler : IRequestHandler<UpdateCinemaCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCinemaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCinemaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Cinemas.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(Actor), request.Id);

        entity.Logo = request.Logo;
        entity.Name = request.Name;
        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
