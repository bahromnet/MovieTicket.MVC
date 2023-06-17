using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models.DtoModels;
using AutoMapper;
using MediatR;

namespace Application.MVC.UseCases.Orders.Queries;

public record GetAllOrderQuery : IRequest<List<OrderDto>>
{
}

public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, List<OrderDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllOrderQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public Task<List<OrderDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        throw new Exception();
    }
}
