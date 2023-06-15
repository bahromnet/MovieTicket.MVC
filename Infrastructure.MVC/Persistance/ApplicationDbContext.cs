using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Models;
using Domain.MVC.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.MVC.Persistance;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Actor> Actors { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ActorMovie> ActorMovies { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActorMovie>().HasKey(am => new
        {
            am.ActorId,
            am.MovieId
        });

        modelBuilder.Entity<ActorMovie>().HasOne(m => m.Movie).WithMany(am => am.ActorsMovies).HasForeignKey(m => m.MovieId);
        modelBuilder.Entity<ActorMovie>().HasOne(m => m.Actor).WithMany(am => am.ActorsMovies).HasForeignKey(m => m.ActorId);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

}
