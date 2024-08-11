using Application.Boundaries.Services.ViaCEP;
using Application.Boundaries.Services.ViaCEP.Client;
using Application.Boundaries.Services.ViaCEP.Client.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using System.Runtime;
namespace Application.Extension
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ApplicationExtension).Assembly));

            return services;
        }

        public static IServiceCollection AddViaCEP(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRefitClient<IViaCEPServiceClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("ViaCEP:BaseAddress").Value));
            return services;
        }

    }
}   
