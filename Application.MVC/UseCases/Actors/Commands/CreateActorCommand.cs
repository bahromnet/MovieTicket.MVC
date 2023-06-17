using Application.MVC.Common.Interfaces;
using Domain.MVC.Entities;
using MediatR;

namespace Application.MVC.UseCases.Actors.Commands;

public record CreateActorCommand : IRequest<int>
{
    public string ProfilePictureURL { get; init; }
    public string FullName { get; init; }
    public string Bio { get; init; }
}

public class CreateActorCommandHandler : IRequestHandler<CreateActorCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateActorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateActorCommand request, CancellationToken cancellationToken)
    {
        var entity = new Actor
        {
            FullName = request.FullName,
            ProfilePictureURL = request.ProfilePictureURL,
            Bio = request.Bio
        };

        _context.Actors.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
