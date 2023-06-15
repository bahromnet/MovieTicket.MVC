using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models.DtoModels;
using AutoMapper;
using MediatR;

namespace Application.MVC.UseCases.Actors.Queries;

public record GetAllActorQuery : IRequest<List<ActorDto>>
{
}

public class GetAllActorQueryHandler : IRequestHandler<GetAllActorQuery, List<ActorDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllActorQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<ActorDto>> Handle(GetAllActorQuery request, CancellationToken cancellationToken)
    {
        var entities = _context.Actors;
        var result = _mapper.Map<List<ActorDto>>(entities);
        return Task.FromResult(result);
    }
}
