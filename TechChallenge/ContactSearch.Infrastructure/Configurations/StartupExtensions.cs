using ContactSearch.Domain.Repositories;
using ContactSearch.Infrastructure.Rrepositories;
using Microsoft.Extensions.DependencyInjection;

namespace ContactSearch.Infrastructure.Configurations
{
    public static class StartupExtensions
    {
        public static void ConfigurationRepository(this IServiceCollection services)
        {
            services.AddScoped<IContactRepository, ContactRepository>();
        }
    }
}
