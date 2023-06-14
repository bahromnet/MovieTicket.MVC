using Domain.MVC.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.MVC.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ActorMovie> ActorMovies { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
