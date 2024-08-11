using Domain.Repository;
using Infra.Mongo.Repositories;
using Infra.Mongo.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infra.Mongo.Extensions
{
    public static class MongoExtension
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSettings>(configuration.GetSection(nameof(MongoSettings)));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoSettings>>().Value);
            services.AddSingleton<IMongoService, MongoService>();
            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ISalesRepository, SalesRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
