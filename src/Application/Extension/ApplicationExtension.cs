using Application.Boundaries.Services.ViaCEP;
using Application.Boundaries.Services.ViaCEP.Client;
using Application.Boundaries.Services.ViaCEP.Client.Settings;
using Application.UseCases.Product.CreateProduct;
using Application.UseCases.Product.ListProducts;
using Application.UseCases.Product.RemoveProduct;
using Application.UseCases.Product.UpdateProduct;
using Application.UseCases.Sale.CancelSale;
using Application.UseCases.Sale.CreateSale;
using Application.UseCases.Sale.ListSales;
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

            services.AddTransient<ICreateProductUseCase, CreateProductUseCase>();
            services.AddTransient<IListProductsUseCase, ListProductsUseCase>();
            services.AddTransient<IRemoveProductUseCase, RemoveProductUseCase>();
            services.AddTransient<IUpdateProductUseCase, UpdateProductUseCase>();

            services.AddTransient<ICreateSaleUseCase, CreateSaleUseCase>();
            services.AddTransient<ICancelSaleUseCase, CancelSaleUseCase>();
            services.AddTransient<IListSalesUseCase, ListSalesUseCase>();

            return services;
        }

        public static IServiceCollection AddViaCEP(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IViaCEPService, ViaCEPService>();
            services.Configure<ViaCEPSettings>(configuration.GetSection("ViaCEP"));
            services.AddRefitClient<IViaCEPServiceClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("ViaCEP:BaseUrl").Value));
            return services;
        }

    }
}
