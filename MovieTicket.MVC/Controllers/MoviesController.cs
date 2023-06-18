using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models;
using Application.MVC.Common.Models.DtoModels;
using Application.MVC.Common.Models.ViewModels;
using Application.MVC.Common.Static;
using Application.MVC.UseCases.Movies.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace MovieTicket.MVC.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class MoviesController : Controller
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMovieService _movieService;

    public MoviesController(IApplicationDbContext context, IMediator mediator, IMovieService movieService)
    {
        _context = context;
        _mediator = mediator;
        _movieService = movieService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index(int pg = 1)
    {
        const int pageSize = 10;
        if (pg < 1)
            pg = 1;
        int recsCount = _context.Movies.Count();
        var paginatedList = new PaginatedList(recsCount, pg, pageSize);
        int recSkip = (pg - 1) * pageSize;
        List<MovieDto> allMovies = await _mediator.Send(new GetAllMovieQuery());
        List<MovieDto> employees = allMovies.Skip(recSkip).Take(paginatedList.PageSize).ToList();
        ViewBag.PaginatedList = paginatedList;
        return View(employees);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Filter(string searchText)
    {
        var allMovies = await _mediator.Send(new GetAllMovieQuery());
        if (!string.IsNullOrEmpty(searchText))
        {
            var filtredResult = allMovies.Where(n =>
                   string.Equals(n.Name, searchText, StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(n.Description, searchText, StringComparison.CurrentCultureIgnoreCase)).ToList();

            return View("Index", filtredResult);
        }
        return View("Index", allMovies);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var movieDetail = await _mediator.Send(new GetByIdMovieQuery { Id = id });
        return View(movieDetail);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> Create()
    {
        var movieDropdownsData = await _movieService.GetNewMovieDropdownsValues();

        ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
        ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(NewMovieVM movie)
    {
        if (!ModelState.IsValid)
        {
            var movieDropdownsData = await _movieService.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(movie);
        }

        await _mediator.Send(movie);
        return RedirectToAction(nameof(Index));
    }


    //GET: Movies/Edit/1
    public async Task<IActionResult> Edit(int id)
    {
        var movieDetails = await _mediator.Send(new GetByIdMovieQuery { Id = id });
        if (movieDetails == null) return View("NotFound");

        var response = new NewMovieVM()
        {
            Id = movieDetails.Id,
            Name = movieDetails.Name,
            Description = movieDetails.Description,
            Price = movieDetails.Price,
            StartDate = movieDetails.StartDate,
            EndDate = movieDetails.EndDate,
            ImageURL = movieDetails.ImageURL,
            MovieCategory = movieDetails.MovieCategory,
            CinemaId = movieDetails.Cinema.Id,
            ProducerId = movieDetails.Producer.Id,
            ActorIds = movieDetails.ActorsMovies.Select(n => n.ActorId).ToList(),
        };

        var movieDropdownsData = await _movieService.GetNewMovieDropdownsValues();
        ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
        ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, NewMovieVM movie)
    {
        if (id != movie.Id) return View("NotFound");

        if (!ModelState.IsValid)
        {
            var movieDropdownsData = await _movieService.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(movie);
        }

        await _mediator.Send(movie);
        return RedirectToAction(nameof(Index));
    }

}
