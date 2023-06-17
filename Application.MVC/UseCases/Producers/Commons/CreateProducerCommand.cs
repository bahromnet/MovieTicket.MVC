using Application.MVC.Common.Interfaces;
using Domain.MVC.Entities;
using MediatR;

namespace Application.MVC.UseCases.Producers.Commons;

public record CreateProducerCommand : IRequest<int>
{
    public string ProfilePictureURL { get; set; }
    public string FullName { get; set; }
    public string Bio { get; set; }
}

public class CreateProducerCommandHandler : IRequestHandler<CreateProducerCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProducerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProducerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Producer
        {
            FullName = request.FullName,
            ProfilePictureURL = request.ProfilePictureURL,
            Bio = request.Bio
        };

        _context.Producers.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
