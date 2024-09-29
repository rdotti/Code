using ContactSearch.Domain.Usecases;
using Microsoft.Extensions.DependencyInjection;

namespace ContactSearch.Domain.Configurations
{
    public static class StartupExtensions
    {
        public static void ConfigurationDomain(this IServiceCollection services)
        {
            services.AddScoped<IContactUsecase, ContactUsecase>();
        }
    }
}
