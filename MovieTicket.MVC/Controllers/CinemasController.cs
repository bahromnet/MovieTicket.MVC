using Application.MVC.Common.Static;
using Application.MVC.UseCases.Cinemas.Commands;
using Application.MVC.UseCases.Cinemas.Queries;
using Domain.MVC.Entities;
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
        var allCinemas = await _mediator.Send(new GetAllCinemaQuery());
        return View(allCinemas);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var cinemaDetails = await _mediator.Send(new GetByIdCinemaQuery { Id = id });
        if (cinemaDetails == null) return View("Not found");
        return View(cinemaDetails);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Logo,Name,Description")]CreateCinemaCommand createCinema)
    {
        if (!ModelState.IsValid) return View(createCinema);
        await _mediator.Send(createCinema);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var cinemaDetails = await _mediator.Send(new GetByIdCinemaQuery { Id = id });
        if (cinemaDetails == null) return View("NotFound");
        return View(cinemaDetails);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")]UpdateCinemaCommand updateCinema)
    {
        if (!ModelState.IsValid) return View(updateCinema);
        await _mediator.Send(updateCinema);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var cinemaDetails = await _mediator.Send(new GetByIdCinemaQuery { Id = id });
        if (cinemaDetails == null) return View("NotFound");
        return View(cinemaDetails);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirm(DeleteCinemaCommand deleteCinema)
    {
        var cinemaDetails = await _mediator.Send(new GetByIdCinemaQuery { Id = deleteCinema.Id});
        if (cinemaDetails == null) return View("NotFound");

        await _mediator.Send(deleteCinema);
        return RedirectToAction(nameof(Index));
    }
}
