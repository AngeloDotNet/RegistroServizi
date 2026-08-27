using Microsoft.Extensions.DependencyInjection;

namespace RegistroServizi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddRegistroServiziApplication(this IServiceCollection services)
    {
        //services.AddValidatorsFromAssemblyContaining<TValidator>();

        return services;
    }
}