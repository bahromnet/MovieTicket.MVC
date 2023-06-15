using Application.MVC.Common.Exceptions;
using Application.MVC.Common.Interfaces;
using Domain.MVC.Entities;
using MediatR;

namespace Application.MVC.UseCases.Actors.Commands;

public record DeleteActorCommand(int Id) : IRequest;


public class DeleteActorCommandHandler : IRequestHandler<DeleteActorCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteActorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteActorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Actors.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Actor), request.Id);
        }
        _context.Actors.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
