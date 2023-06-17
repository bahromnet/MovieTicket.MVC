using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models;
using Application.MVC.Common.Models.DtoModels;
using Application.MVC.Common.Static;
using Application.MVC.UseCases.Movies.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieTicket.MVC.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IApplicationDbContext context;
        private readonly IMediator mediator;

        public MoviesController(IApplicationDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int pg = 1)
        {
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            int recsCount = context.Movies.Count();
            var paginatedList = new PaginatedList(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            List<MovieDto> allMovies = await mediator.Send(new GetAllMovieQuery());
            List<MovieDto> employees = allMovies.Skip(recSkip).Take(paginatedList.PageSize).ToList();
            ViewBag.PaginatedList = paginatedList;
            return View(employees);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchText)
        {
            var allMovies = mediator.Send(new GetAllMovieQuery());
            if (!string.IsNullOrEmpty(searchText))
            {
                var filtredResult = allMovies.Result.Where(n =>
                       string.Equals(n.Name, searchText, StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(n.Description, searchText, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filtredResult);
            }
            return View("Index", allMovies);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await mediator.Send(new GetByIdMovieQuery { Id = id });
            return View(movieDetail);
        }


    }
}
