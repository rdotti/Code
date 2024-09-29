using ContactRegistration.Domain.Usecases;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ContactRegistration.Domain.Configurations
{
    public static class StartupExtensions
    {
        public static void ConfigurationDomain(this IServiceCollection services)
        {
            services.TryAddScoped<IContactRegistrationUsecase, ContactRegistrationUsecase>();
        }
    }
}
