using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Shared.Rabbit.Producer
{
    public class RabbitProducer(IConnection _connection) : IRabbitProducer
    {
        public void Publish<T>(T message, string queueName)
        {
            using var channel = _connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish("", queueName, null, body);
        }
    }
}
