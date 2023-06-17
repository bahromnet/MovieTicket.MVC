using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models.DtoModels;
using AutoMapper;
using MediatR;

namespace Application.MVC.UseCases.Actors.Queries;

public record GetByIdActorQuery(int Id) : IRequest<ActorDto>;
//{
//    public int Id { get; set; }
//}

public class GetByIdActorQueryHandler : IRequestHandler<GetByIdActorQuery, ActorDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdActorQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ActorDto> Handle(GetByIdActorQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Actors.FindAsync(new object[] { request.Id }, cancellationToken);
        var result = _mapper.Map<ActorDto>(entity);

        return result;
    }
}
