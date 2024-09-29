using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Shared.Rabbit.Producer;

namespace Shared.Rabbit.Configurations
{
    public static class StartupSharedRabbit
    {
        public static void ConfigurationRabbitProducer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRabbitProducer, RabbitProducer>();
            services.AddSingleton<IConnectionFactory>(x => new ConnectionFactory
            {
                HostName = configuration.GetSection("RabbitHostName").Value,
                UserName = configuration.GetSection("RabbitUsername").Value,
                Password = configuration.GetSection("RabbitPassword").Value
            });
        }
    }
}
