using Application.MVC.Common.Static;
using Application.MVC.UseCases.Actors.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieTicket.MVC.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class ActorsController : Controller
{
    private readonly IMediator mediator;
    public ActorsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var data = await mediator.Send(new GetAllActorQuery());
        return View(data);
    }
}
