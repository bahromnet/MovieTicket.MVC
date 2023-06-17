using Application.MVC.Common.Interfaces;
using Domain.MVC.Entities;
using MediatR;

namespace Application.MVC.UseCases.Cinemas.Commands;

public record CreateCinemaCommand : IRequest<int>
{
    public string Logo { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
}

public class CreateCinemaCommandHandler : IRequestHandler<CreateCinemaCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCinemaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCinemaCommand request, CancellationToken cancellationToken)
    {
        var entity = new Cinema
        {
            Logo = request.Logo,
            Name = request.Name,
            Description = request.Description
        };

        _context.Cinemas.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }
}
