using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Services.ServiceMovie;
using Infrastructure.MVC.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.MVC;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DbConnect"));
        });

        return services;
    }
}
