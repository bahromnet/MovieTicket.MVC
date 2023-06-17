using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models.DtoModels;
using AutoMapper;
using MediatR;

namespace Application.MVC.UseCases.Producers.Queries;

public record GetAllProducerQuery : IRequest<List<ProducerDto>>
{
}

public class GetAllProducerQueryHandler : IRequestHandler<GetAllProducerQuery, List<ProducerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllProducerQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<ProducerDto>> Handle(GetAllProducerQuery request, CancellationToken cancellationToken)
    {
        var entities = _context.Producers;
        var result = _mapper.Map<List<ProducerDto>>(entities);
        return Task.FromResult(result);
    }
}
