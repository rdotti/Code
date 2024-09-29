using ContactRegistration.Domain.Repositories;
using ContactRegistration.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ContactRegistration.Infrastructure.Configurations
{
    public static class StartupExtensions
    {
        public static void ConfigurationRepository(this IServiceCollection services)
        {
            services.AddScoped<IContactRegistrationRepository, ContactRegistrationRepository>();
        }
    }
}
