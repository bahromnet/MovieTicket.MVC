using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models.DtoModels;
using AutoMapper;
using MediatR;

namespace Application.MVC.UseCases.Cinemas.Queries;

public record GetAllCinemaQuery : IRequest<List<CinemaDto>>
{
}

public class GetAllCinemaQueryHandler : IRequestHandler<GetAllCinemaQuery, List<CinemaDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllCinemaQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<CinemaDto>> Handle(GetAllCinemaQuery request, CancellationToken cancellationToken)
    {
        var entities = _context.Cinemas;
        var result = _mapper.Map<List<CinemaDto>>(entities);

        return Task.FromResult(result);
    }
}
