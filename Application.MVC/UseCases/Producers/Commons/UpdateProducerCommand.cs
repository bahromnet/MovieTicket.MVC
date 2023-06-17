using Application.MVC.Common.Exceptions;
using Application.MVC.Common.Interfaces;
using Domain.MVC.Entities;
using MediatR;

namespace Application.MVC.UseCases.Producers.Commons;

public record UpdateProducerCommand : IRequest
{
    public int Id { get; set; }
    public string ProfilePictureURL { get; set; }
    public string FullName { get; set; }
    public string Bio { get; set; }
}

public class UpdateProducerCommandHandler : IRequestHandler<UpdateProducerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProducerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProducerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Producers.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(Producer), request.Id);

        entity.FullName = request.FullName;
        entity.ProfilePictureURL = request.ProfilePictureURL;
        entity.Bio = request.Bio;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
