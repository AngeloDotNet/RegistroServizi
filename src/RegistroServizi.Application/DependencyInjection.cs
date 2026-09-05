using Microsoft.Extensions.DependencyInjection;
using RegistroServizi.Application.Interfaces;
using RegistroServizi.Application.Services;

namespace RegistroServizi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddRegistroServiziApplication(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddSingleton<ClientTimeProvider>();
        services.AddSingleton<ITimeZoneService, TimeZoneService>();

        //services.AddValidatorsFromAssemblyContaining<TValidator>();

        return services;
    }
}