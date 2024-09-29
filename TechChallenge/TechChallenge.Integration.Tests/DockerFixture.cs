using Docker.DotNet.Models;
using Docker.DotNet;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shared.Infraestructure.Repository;

namespace TechChallenge.Integration.Tests
{
    public class DockerFixture : IDisposable
    {
        private readonly DockerClient _dockerClient;
        private readonly string _containerId;

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
                        { "1433", new List<PortBinding> { new() { HostPort = "11433" } } }
                    },
                    PublishAllPorts = true
                }
            }).GetAwaiter().GetResult();


            _containerId = createContainerResponse.ID;

            _dockerClient.Containers.StartContainerAsync(_containerId, new ContainerStartParameters()).GetAwaiter().GetResult();
            ConnectionTest().GetAwaiter().GetResult();
            CreateDatabase().GetAwaiter().GetResult();
            RunMigrations();

        }

        private static async Task ConnectionTest()
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    using var connection = new SqlConnection("Server=localhost,11433;User Id=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true;");
                    await connection.OpenAsync();
                    return;
                }
                catch (SqlException)
                {
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }

            throw new Exception("Não foi possível estabelecer uma conexão SQL Server!");
        }

        private static async Task CreateDatabase()
        {
            using var connection = new SqlConnection("Server=localhost,11433;User Id=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true;");
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = "CREATE DATABASE TechChallenge_Fase1";
            await command.ExecuteNonQueryAsync();
        }

        private static void RunMigrations()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,11433;Database=TechChallenge_Fase1;User Id=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true;");

            using var context = new ApplicationDbContext(optionsBuilder.Options);
            context.Database.Migrate();
        }

        public void Dispose()
        {
            _dockerClient.Containers.StopContainerAsync(_containerId, new ContainerStopParameters()).GetAwaiter().GetResult();
            _dockerClient.Containers.RemoveContainerAsync(_containerId, new ContainerRemoveParameters()).GetAwaiter().GetResult();
            _dockerClient.Dispose();
        }
    }
}
