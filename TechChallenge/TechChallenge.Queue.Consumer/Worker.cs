using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;
using TechChallenge.Core.Entities;
using Microsoft.Extensions.Configuration;
using TechChallenge.Core.Common;
using TechChallenge.Core.Repository;
using System.Threading.Channels;

namespace TechChallenge.Queue.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly QueueConfiguration _queueConfiguration;
        private readonly IServiceProvider _serviceProvider;

        public Worker(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _queueConfiguration = configuration.GetSection("QueueConfiguration").Get<QueueConfiguration>() ?? throw new Exception("Queue não configurada.");
        }

        private ConnectionFactory GetFactory() =>
            new()
            {
                HostName = _queueConfiguration.HostName,
                UserName = _queueConfiguration.UserName,
                Password = _queueConfiguration.Password
            };

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var connection = GetFactory().CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        ConsumeInsertQueue(channel);
                        ConsumeUpdateQueue(channel);
                        ConsumeDeleteQueue(channel);
                    }
                }

                await Task.Delay(5000, stoppingToken);

            }
        }

        private void ConsumeInsertQueue(IModel channel)
        {
            const string queueName = "contato-insert";
            channel.QueueDeclare(queueName, false, false, false, null);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, args) =>
            {
                var message = Encoding.UTF8.GetString(args.Body.ToArray());
                var contato = JsonSerializer.Deserialize<Contato>(message);

                if (contato != null)
                {
                    using(IServiceScope scope = _serviceProvider.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<IContatoRepository>();
                        _repository.Insert(contato);
                    }
                }

                Console.WriteLine(message);
            };

            channel.BasicConsume(queueName, true, consumer);
        }

        private void ConsumeUpdateQueue(IModel channel)
        {
            const string queueName = "contato-update";
            channel.QueueDeclare(queueName, false, false, false, null);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, args) =>
            {
                var message = Encoding.UTF8.GetString(args.Body.ToArray());
                var contato = JsonSerializer.Deserialize<Contato>(message);

                if (contato != null)
                {
                    using (IServiceScope scope = _serviceProvider.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<IContatoRepository>();
                        _repository.Update(contato);
                    }
                }
                    
                Console.WriteLine(message);
            };

            channel.BasicConsume(queueName, true, consumer);
        }

        private void ConsumeDeleteQueue(IModel channel)
        {
            const string queueName = "contato-delete";
            channel.QueueDeclare(queueName, false, false, false, null);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, args) =>
            {
                var message = Encoding.UTF8.GetString(args.Body.ToArray());

                if (int.TryParse(message, out var contatoId))
                {
                    using (IServiceScope scope = _serviceProvider.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<IContatoRepository>();
                        _repository.Delete(contatoId);
                    }
                }

                Console.WriteLine(message);
            };

            channel.BasicConsume(queueName, true, consumer);
        }

    }
}