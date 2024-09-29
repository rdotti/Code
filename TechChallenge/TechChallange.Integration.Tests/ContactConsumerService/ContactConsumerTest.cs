using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using System.Net;
using Shared.Domain.Models;

namespace TechChallange.Integration.Tests.ContactConsumerService
{
    public class ContactConsumerTest : IClassFixture<TechChallengeWebApplicationFactory<Program>>, IClassFixture<DockerFixture>
    {
        private readonly HttpClient _client;

        public ContactConsumerTest(TechChallengeWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Contato_IntegrationValidate_InserirContato()
        {
            //Arrange
            var contatoInsert = new InsertContactModel()
            {
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
        public async Task Contato_IntegrationValidate_AtualizarContato()
        {
            //Arrange
            var contatoInsert = new UpdateContactModel()
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
