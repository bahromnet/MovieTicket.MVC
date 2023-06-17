using Application.MVC.Common.Exceptions;
using Application.MVC.Common.Interfaces;
using Domain.MVC.Entities;
using MediatR;

namespace Application.MVC.UseCases.Actors.Commands;

public record UpdateActorCommand : IRequest
{
    public int Id { get; init; }
    public string ProfilePictureURL { get; init; }
    public string FullName { get; init; }
    public string Bio { get; init; }
}

public class UpdateActorCommandHandler : IRequestHandler<UpdateActorCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateActorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateActorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Actors.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(Actor), request.Id);

        entity.FullName = request.FullName;
        entity.ProfilePictureURL = request.ProfilePictureURL;
        entity.Bio = request.Bio;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
