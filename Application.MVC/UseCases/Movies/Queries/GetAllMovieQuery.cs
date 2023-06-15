using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models.DtoModels;
using AutoMapper;
using Domain.MVC.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MVC.UseCases.Movies.Queries;

public record GetAllMovieQuery : IRequest<List<MovieDto>>
{
}

public class GetAllMovieQueryHandler : IRequestHandler<GetAllMovieQuery, List<MovieDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllMovieQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<List<MovieDto>> Handle(GetAllMovieQuery request, CancellationToken cancellationToken)
    {
        List<Movie> movies = await _dbContext.Movies
        .Include(m => m.Cinema)
        .Include(m => m.Producer)
        .ToListAsync(cancellationToken);

        List<MovieDto> movieDTOs = _mapper.Map<List<MovieDto>>(movies);

        return movieDTOs;
    }
}
