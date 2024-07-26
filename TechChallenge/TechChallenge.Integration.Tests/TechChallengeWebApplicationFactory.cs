using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TechChallenge.Infraestructure.Repository;

namespace TechChallenge.Integration.Tests
{
    public class TechChallengeWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        "Server=localhost,11433;Database=TechChallenge_Fase1;User Id=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true;")
                );
            });
            base.ConfigureWebHost(builder);
        }
    }
}
