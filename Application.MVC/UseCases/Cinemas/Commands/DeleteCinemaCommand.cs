using Application.MVC.Common.Exceptions;
using Application.MVC.Common.Interfaces;
using Domain.MVC.Entities;
using MediatR;

namespace Application.MVC.UseCases.Cinemas.Commands;

public record DeleteCinemaCommand(int Id) : IRequest;

public class DeleteCinemaCommandHandler : IRequestHandler<DeleteCinemaCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCinemaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCinemaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Cinemas.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Cinema), request.Id);
        }
        _context.Cinemas.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
