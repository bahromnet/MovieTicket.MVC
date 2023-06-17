using Application.MVC.Common.Static;
using Application.MVC.UseCases.Producers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieTicket.MVC.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class ProducersController : Controller
{
    private readonly IMediator _mediator;

    public ProducersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var allProducers = _mediator.Send(new GetAllProducerQuery());
        return View(allProducers);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var producerDetails = _mediator.Send(new GetByIdProducerQuery { Id = id });
        if (producerDetails == null) return View("NotFound");
        return View(producerDetails);
    }

}
