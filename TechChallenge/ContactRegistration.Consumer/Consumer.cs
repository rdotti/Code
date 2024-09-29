using ContactConsumer.Domain.Models;
using ContactConsumer.Domain.Usecases;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Domain.Models;
using Shared.Rabbit.Consumer;
using System.Text;

namespace ContactConsumer.Consumer
{
    public class Consumer<T>(ILogger<Consumer<T>> _logger, IConnection _connection,
        IOptions<ConsumerOptions<T>> _options, IContactConsumerUsecase<T> _usecase) : BackgroundService
        where T : ContactModel
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var channel = CreateChannel();
            channel.BasicQos(0, (ushort)_options.Value.ThreadsCount, true);

            for (int x = 0; x < _options.Value.ThreadsCount; x++)
                CreateConsumer(channel, x, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        public void CreateConsumer(IModel channel, int consumerId, CancellationToken cancellationToken)
        {
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += (context, e) =>
                Task.Factory.StartNew(() => OnReceived(context, e, cancellationToken), cancellationToken);
            channel.BasicConsume(_options.Value.QueueName, false, consumer);
        }

        private async Task OnReceived(object context, BasicDeliverEventArgs e, CancellationToken cancellationToken)
        {
            var channel = ((AsyncEventingBasicConsumer)context).Model;
            try
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                var messageBody = JsonConvert.DeserializeObject<T>(message);

                if(typeof(T) == typeof(UpdateContactModel))
                {
                    _usecase.Update(messageBody);
                }
                else if (typeof(T) == typeof(DeleteContactModel))
                { 
                    _usecase.Delete(messageBody);
                }
                else if (typeof(T) == typeof(InsertContactModel))
                {
                    _usecase.Insert(messageBody);
                }
            }
            catch (Exception)
            {
                var countRetries = GetRetryCount(e.BasicProperties);
                if (countRetries <= _options.Value.Retries)
                {
                    channel.BasicNack(e.DeliveryTag, false, false);
                    return;
                }

                var basicProperties = e.BasicProperties;
                basicProperties.Persistent = true;
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                channel.BasicPublish(_options.Value.ExchangeName, GetDeadRoutingKey(_options.Value.RoutingKey), basicProperties,
                    Encoding.UTF8.GetBytes(message));

                channel.BasicAck(e.DeliveryTag, false);
            }
        } 

        private IModel CreateChannel()
        {
            var channel = _connection.CreateModel();
            var awaitQueueName = GetAwaitQueueName(_options.Value.QueueName);
            var deadQueueName = GetDeadQueueName(_options.Value.QueueName);

            if (_options.Value.CreateQueues)
            {
                channel.QueueDeclare(_options.Value.QueueName, true, false, false, new Dictionary<string, object>
                {
                    {"x-dead-letter-exchange", _options.Value.ExchangeName },
                    {"x-dead-letter-routing-key", GetAwaitRoutingKey(_options.Value.RoutingKey) }
                });

                channel.QueueDeclare(awaitQueueName, true, false, false, new Dictionary<string, object>
                {
                    {"x-dead-letter-exchange", _options.Value.ExchangeName },
                    {"x-dead-letter-routing-key", _options.Value.RoutingKey },
                    {"x-message-ttl", _options.Value.AwaitQueueTime * 1000 }
                });

                channel.QueueDeclare(deadQueueName, true, false, false, new Dictionary<string, object>());

                channel.QueueBind(_options.Value.QueueName, _options.Value.ExchangeName, _options.Value.RoutingKey);
                channel.QueueBind(awaitQueueName, _options.Value.ExchangeName, GetAwaitRoutingKey(_options.Value.RoutingKey));
                channel.QueueBind(deadQueueName, _options.Value.ExchangeName, GetDeadRoutingKey(_options.Value.RoutingKey));
            }

            return channel;
        }

        private static string GetAwaitRoutingKey(string routingKey) =>
            $"{routingKey}{(routingKey.EndsWith("key") ? "" : "_key")}_wait";

        private static string GetDeadRoutingKey(string routingKey) =>
            $"{routingKey}{(routingKey.EndsWith("key") ? "" : "_key")}_dead";

        private static string GetAwaitQueueName(string queueName) =>
            $"{queueName}{(queueName.EndsWith("queue") ? "" : "_queue")}_wait";

        private static string GetDeadQueueName(string queueName) =>
            $"{queueName}{(queueName.EndsWith("queue") ? "" : "_queue")}_dead";

        private static long GetRetryCount(IBasicProperties properties)
        {
            if(properties.Headers?.ContainsKey("x-death") != true)
            {
                return 0;
            }

            var deathProperties = (List<object>)properties.Headers["x-death"];
            var lastRetry = (Dictionary<string, object>)deathProperties[0];
            var count = lastRetry["count"];
            return (long)count;
        }
    }
}
