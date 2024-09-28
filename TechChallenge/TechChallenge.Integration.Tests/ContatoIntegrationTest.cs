using System.Net;
using System.Net.Http.Json;
using TechChallenge.Core.Models;

namespace TechChallenge.Integration.Tests
{
    public class ContatoIntegrationTest: IClassFixture<TechChallengeWebApplicationFactory<Program>>, IClassFixture<DockerFixture>
    {
        private readonly HttpClient _client;

        public ContatoIntegrationTest(TechChallengeWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Contato_IntegrationValidate_InserirContato()
        {
            //Arrange
            var contatoInsert = new ContatoInsertModel() {
                DDD = 16,
                EMail = "teste@teste.com",
                Nome = "Joao teste",
                Telefone = "999999999"
            };

            //Act
            var result = await _client.PostAsJsonAsync("/api/Contato", contatoInsert);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
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

        [Fact]
        public async Task Contato_IntegrationValidate_AtualizarContato()
        {
            //Arrange
            var contatoInsert = new ContatoUpdateModel()
            {
                Id = 1,
                DDD = 16,
                EMail = "teste@teste.com",
                Nome = "Joao teste",
                Telefone = "999999991"
            };

            //Act
            var result = await _client.PutAsJsonAsync("/api/Contato", contatoInsert);

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Contato_IntegrationValidate_DeletarContato()
        {
            //Act
            var result = await _client.DeleteAsync("/api/Contato/1");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
