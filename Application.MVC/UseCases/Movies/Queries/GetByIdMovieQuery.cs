using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models.DtoModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MVC.UseCases.Movies.Queries;

public record GetByIdMovieQuery : IRequest<MovieDto>
{
    public int Id { get; init; }
}

public class GetByIdMovieQueryHandler : IRequestHandler<GetByIdMovieQuery, MovieDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetByIdMovieQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MovieDto> Handle(GetByIdMovieQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Movies
            .Include(c => c.Cinema)
            .Include(p => p.Producer)
            .Include(am => am.ActorsMovies).ThenInclude(a => a.Actor)
            .FirstOrDefaultAsync(n => n.Id == request.Id);
        var result = _mapper.Map<MovieDto>(entity);

        return result;
    }
}
