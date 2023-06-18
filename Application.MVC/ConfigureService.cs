using Application.MVC.Common.Interfaces;
using Application.MVC.Common.Services.ServiceMovie;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.MVC;

public static class ConfigureService
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(option =>
        {
            option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}
