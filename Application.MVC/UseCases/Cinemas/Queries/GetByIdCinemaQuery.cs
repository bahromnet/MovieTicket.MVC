using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models.DtoModels;
using AutoMapper;
using MediatR;

namespace Application.MVC.UseCases.Cinemas.Queries;

public record GetByIdCinemaQuery(int Id) : IRequest<CinemaDto>;

public class GetByIdCinemaQueryHandler : IRequestHandler<GetByIdCinemaQuery, CinemaDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdCinemaQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CinemaDto> Handle(GetByIdCinemaQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Cinemas.FindAsync(new object[] { request.Id }, cancellationToken);
        var result = _mapper.Map<CinemaDto>(entity);

        return result;
    }
}
