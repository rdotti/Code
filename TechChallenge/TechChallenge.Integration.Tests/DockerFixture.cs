using Docker.DotNet.Models;
using Docker.DotNet;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TechChallenge.Infraestructure.Repository;

namespace TechChallenge.Integration.Tests
{
    public class DockerFixture : IDisposable
    {
        private DockerClient _dockerClient;
        private string _containerId;

        public DockerFixture()
        {

            _dockerClient = new DockerClientConfiguration().CreateClient();

            var createContainerResponse = _dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Image = "mcr.microsoft.com/mssql/server",
                Env = new List<string>
                {
                    "ACCEPT_EULA=Y",
                    "SA_PASSWORD=1q2w3e4r@#$"
                },
                ExposedPorts = new Dictionary<string, EmptyStruct>
                {
                    { "1433", new EmptyStruct() }
                },
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        { "1433", new List<PortBinding> { new PortBinding { HostPort = "11433" } } }
                    },
                    PublishAllPorts = true
                }
            }).GetAwaiter().GetResult();


            _containerId = createContainerResponse.ID;

            _dockerClient.Containers.StartContainerAsync(_containerId, new ContainerStartParameters()).GetAwaiter().GetResult();
            WaitForSqlServerAvailability().GetAwaiter().GetResult();
            CreateDatabaseAsync().GetAwaiter().GetResult();
            ApplyMigrations();

        }

        private async Task WaitForSqlServerAvailability()
        {
            var maxRetries = 10;
            var retryDelay = TimeSpan.FromSeconds(5);

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    using (var connection = new SqlConnection("Server=localhost,11433;User Id=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true;"))
                    {
                        await connection.OpenAsync();
                        return;
                    }
                }
                catch (SqlException)
                {
                    await Task.Delay(retryDelay);
                }
            }

            throw new Exception("O SQL Server não está disponível após múltiplas tentativas.");
        }

        private async Task CreateDatabaseAsync()
        {
            using (var connection = new SqlConnection("Server=localhost,11433;User Id=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true;"))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "CREATE DATABASE TechChallenge_Fase1";
                await command.ExecuteNonQueryAsync();
            }
        }

        private void ApplyMigrations()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,11433;Database=TechChallenge_Fase1;User Id=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true;");

            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                context.Database.Migrate();
            }
        }

        public void Dispose()
        {
            _dockerClient.Containers.StopContainerAsync(_containerId, new ContainerStopParameters()).GetAwaiter().GetResult();
            _dockerClient.Containers.RemoveContainerAsync(_containerId, new ContainerRemoveParameters()).GetAwaiter().GetResult();
            _dockerClient.Dispose();
        }
    }
}
