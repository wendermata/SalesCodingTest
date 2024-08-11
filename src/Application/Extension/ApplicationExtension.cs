using Application.Boundaries.Services.ViaCEP;
using Application.Boundaries.Services.ViaCEP.Client;
using Application.Boundaries.Services.ViaCEP.Client.Settings;
using Application.UseCases.Product.CreateProduct;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
namespace Application.Extension
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ApplicationExtension).Assembly));

            services.AddTransient<ICreateProduct, CreateProduct>();

            return services;
        }

        public static IServiceCollection AddViaCEP(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IViaCEPService, ViaCEPService>();
            services.Configure<ViaCEPSettings>(configuration.GetSection("ViaCEP"));
            services.AddRefitClient<IViaCEPServiceClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("ViaCEP:BaseAddress").Value));
            return services;
        }

    }
}
