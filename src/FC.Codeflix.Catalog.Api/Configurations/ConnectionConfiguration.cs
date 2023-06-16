using FC.Codeflix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace FC.Codeflix.Catalog.Api.Configurations
{
    public static class ConnectionConfiguration
    {
        public static IServiceCollection AddAppConections(this IServiceCollection services)
        {
            services.AddDbConnection();
            return services;
        }

        private static IServiceCollection AddDbConnection(this IServiceCollection services)
        {
            services.AddDbContext<CodeflixCatalogDbContext>(
                    options => options.UseInMemoryDatabase("InMemory-DSV-Database")
                );
            return services;
        }
    }
}
