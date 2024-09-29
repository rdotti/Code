using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared.Rabbit.Consumer;
using Shared.Rabbit.Producer;

namespace Shared.Rabbit.Configurations
{
    public static class RabbitProducerExtensions
    {
        public static void ConfigurationRabbitProducer(this IServiceCollection services, Action<RabbitOptions> configure)
        {
            services.AddScoped<IRabbitProducer, RabbitProducer>();
            services.Configure(configure);
            services.AddTransient<IConnectionFactory>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<RabbitOptions>>().Value;
                var factory = new ConnectionFactory
                {
                    HostName = options.HostName,
                    Port = options.Port,
                    UserName = options.Username,
                    Password = options.Password,
                    VirtualHost = options.VirtualHost,
                    DispatchConsumersAsync = true
                };

                return factory;
            });
            services.AddTransient(provider =>
                provider.GetRequiredService<IConnectionFactory>().CreateConnection());
        }
    }
}
