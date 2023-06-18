using Application.MVC.Common.Static;
using Application.MVC.UseCases.Actors.Commands;
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

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var actorDetails = await mediator.Send(new GetByIdActorQuery { Id = id });

        if (actorDetails == null) return View("NotFound");
        return View(actorDetails);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]CreateActorCommand createActor)
    {
        if (!ModelState.IsValid)
        {
            return View(createActor);
        }
        await mediator.Send(createActor);
        return View(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var actorDetails = await mediator.Send(new GetByIdActorQuery { Id = id });
        if (actorDetails == null) return View("NotFound");
        return View(actorDetails);
    }

    public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")]UpdateActorCommand updateActor)
    {
        if (!ModelState.IsValid)
        {
            return View(updateActor);
        }
        await mediator.Send(updateActor);
        return View(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var actorDetails = await mediator.Send(new GetByIdActorQuery { Id = id });
        if (actorDetails == null)
        {
            return View("NotFound");
        }
        return View(actorDetails);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(DeleteActorCommand deleteActor)
    {
        var actorDetails = await mediator.Send(new GetByIdActorQuery { Id = deleteActor.Id });
        if (actorDetails == null)
        {
            return View("NotFound");
        }
        await mediator.Send(deleteActor);
        return RedirectToAction(nameof(Index));
    }

}
