using Application.MVC.Common.Static;
using Application.MVC.UseCases.Cinemas.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieTicket.MVC.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class CinemasController : Controller
{
    private readonly IMediator _mediator;

    public CinemasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var allCinemas = _mediator.Send(new GetAllCinemaQuery());
        return View(allCinemas);
    }
}
