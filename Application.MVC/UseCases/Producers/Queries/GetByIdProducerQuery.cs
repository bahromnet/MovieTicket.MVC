using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models.DtoModels;
using AutoMapper;
using MediatR;

namespace Application.MVC.UseCases.Producers.Queries;

public record GetByIdProducerQuery : IRequest<ProducerDto>
{
    public int Id { get; set; } 
}

public class GetByIdProducerQueryHandler : IRequestHandler<GetByIdProducerQuery, ProducerDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetByIdProducerQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProducerDto> Handle(GetByIdProducerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Producers.FindAsync(new object[] { request.Id }, cancellationToken);
        var result = _mapper.Map<ProducerDto>(entity);

        return result;
    }
}
