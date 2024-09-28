using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TechChallenge.Core.Common;
using TechChallenge.Core.Entities;

namespace TechChallenge.Queue.Producer
{
    public class ContatoProducer : IContatoProducer
    {
        private readonly QueueConfiguration _queueConfiguration;

        public ContatoProducer(IConfiguration configuration)
        {
            _queueConfiguration = configuration.GetSection("QueueConfiguration").Get<QueueConfiguration>();// ?? throw new Exception("Queue não configurada.");
        }

        private ConnectionFactory GetFactory() =>
            new()
            {
                HostName = _queueConfiguration?.HostName ?? "localhost",
                UserName = _queueConfiguration?.UserName ?? "guest",
                Password = _queueConfiguration?.Password ?? "guest",
            };

        public void SendInsert(Contato contato)
        {
            const string queueName = "contato-insert";

            using (var connection = GetFactory().CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, false, false, false, null);
                    var message = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(contato));
                    channel.BasicPublish("", queueName, null, message);
                 }
            }
        }

        public void SendUpdate(Contato contato)
        {
            const string queueName = "contato-update";

            using (var connection = GetFactory().CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, false, false, false, null);
                    var message = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(contato));
                    channel.BasicPublish("", queueName, null, message);
                }
            }
        }

        public void SendDelete(int id)
        {
            const string queueName = "contato-delete";

            using (var connection = GetFactory().CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, false, false, false, null);
                    var message = Encoding.UTF8.GetBytes(id.ToString());
                    channel.BasicPublish("", queueName, null, message);
                }
            }
        }
    }
}
