using ContactConsumer.Domain.Repositories;
using ContactConsumer.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ContactConsumer.Infrastructure.Configurations
{
    public static class StartupExtensions
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IContactConsumerRepository, ContactConsumerRepository>();
        }
    }
}
