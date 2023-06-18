using Application.MVC.Common.Static;
using Application.MVC.UseCases.Producers.Commons;
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
        var allProducers = await _mediator.Send(new GetAllProducerQuery());
        return View(allProducers);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id, int movieId)
    {
        var producerDetails = await _mediator.Send(new GetByIdProducerQuery { Id = id });
        if (producerDetails == null) return View("NotFound");
        ViewData["MovieId"] = movieId;
        return View(producerDetails);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")] CreateProducerCommand createProducer)
    {
        if (!ModelState.IsValid) return View(createProducer);

        await _mediator.Send(createProducer);
        return RedirectToAction(nameof(Index));
    }

    //GET: producers/edit/1
    public async Task<IActionResult> Edit(int id)
    {
        var producerDetails = await _mediator.Send(new GetByIdProducerQuery { Id = id });
        if (producerDetails == null) return View("NotFound");
        return View(producerDetails);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] UpdateProducerCommand updateProducer)
    {
        if (!ModelState.IsValid) return View(updateProducer);

        if (id == updateProducer.Id)
        {
            await _mediator.Send(updateProducer);
            return RedirectToAction(nameof(Index));
        }
        return View(updateProducer);
    }

    //GET: producers/delete/1
    public async Task<IActionResult> Delete(int id)
    {
        var producerDetails = await _mediator.Send(new GetByIdProducerQuery { Id = id });
        if (producerDetails == null) return View("NotFound");
        return View(producerDetails);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(DeleteProducerCommand deleteProducer)
    {
        var producerDetails = await _mediator.Send(new GetByIdProducerQuery { Id = deleteProducer.Id });
        if (producerDetails == null) return View("NotFound");

        await _mediator.Send(deleteProducer);
        return RedirectToAction(nameof(Index));
    }

}
