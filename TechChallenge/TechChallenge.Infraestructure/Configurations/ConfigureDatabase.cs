using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Core.Repository;
using TechChallenge.Infraestructure.Repository;
using TechChallenge.Queue.Producer;

namespace TechChallenge.Infraestructure.Configurations
{
    public static class ServicesExtension
    {
        public static void ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IContatoRepository, ContatoRepository>();
        }

        public static void ConfigureQueueProducer(this IServiceCollection services)
        {
            services.AddScoped<IContatoProducer, ContatoProducer>();
        }
    }
}
