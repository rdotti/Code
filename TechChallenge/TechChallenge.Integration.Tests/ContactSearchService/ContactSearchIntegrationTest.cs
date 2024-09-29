using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;

namespace TechChallenge.Integration.Tests.ContactSearchService
{
    public class ContactSearchIntegrationTest : IClassFixture<TechChallengeWebApplicationFactory<Program>>, IClassFixture<DockerFixture>
    {
        private readonly HttpClient _client;

        public ContactSearchIntegrationTest(TechChallengeWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Contato_IntegrationValidate_BuscarTodosOsContatos()
        {
            //Arrange

            //Act
            var result = await _client.GetAsync("/api/Contato");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Contato_IntegrationValidate_BuscarContatoPorId()
        {
            //Act
            var result = await _client.GetAsync("/api/Contato/1");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Contato_IntegrationValidate_BuscarContatoPorIdQueNaoExiste()
        {
            //Act
            var result = await _client.GetAsync("/api/Contato/99");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Contato_IntegrationValidate_BuscarContatoPorDdd()
        {
            //Act
            var result = await _client.GetAsync("/api/Contato/GetByDDD/16");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
