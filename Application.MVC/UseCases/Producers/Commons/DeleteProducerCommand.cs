using Application.MVC.Common.Exceptions;
using Application.MVC.Common.Interfaces;
using MediatR;

namespace Application.MVC.UseCases.Producers.Commons;

public record DeleteProducerCommand(int Id) : IRequest;

public class DeleteProducerCommandHandler : IRequestHandler<DeleteProducerCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProducerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProducerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Producers.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Producers), request.Id);
        }
        _context.Producers.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
