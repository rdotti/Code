using Shared.Domain.Models;
using Shared.Rabbit.Options;

namespace ContactConsumer.Consumer.Configurations
{
    public static class RabbitExtensions
    {
        public static void AddConsumer<TModel>(this IServiceCollection services, Action<ConsumerOptions<TModel>> configure)
            where TModel : ContactModel
        {
            services.Configure<ConsumerOptions<TModel>>(configure);
            services.AddHostedService<Consumer<TModel>>();
        }
    }
}
