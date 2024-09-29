using ContactConsumer.Domain.Usecases;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Models;

namespace ContactConsumer.Domain.Configurations
{
    public static class StartupExtensions
    {
        public static void AddDomain<T>(this IServiceCollection services)
            where T : ContactModel
        {
            services.AddTransient<IContactConsumerUsecase<T>, ContactConsumerUsecase<T>>();
        }
    }
}
