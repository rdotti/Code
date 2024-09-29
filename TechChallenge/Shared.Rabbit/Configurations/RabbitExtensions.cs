using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared.Rabbit.Consumer;

namespace Shared.Rabbit.Configurations
{
    public static class RabbitExtensions
    {
        public static void AddRabbitMq(this IServiceCollection services, Action<RabbitOptions> configure)
        {
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
