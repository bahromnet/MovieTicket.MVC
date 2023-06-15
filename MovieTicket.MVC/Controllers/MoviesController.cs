using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models;
using Application.MVC.Common.Models.DtoModels;
using Application.MVC.UseCases.Movies.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MovieTicket.MVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IApplicationDbContext context;
        private readonly IMediator mediator;

        public MoviesController(IApplicationDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

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

    }
}
