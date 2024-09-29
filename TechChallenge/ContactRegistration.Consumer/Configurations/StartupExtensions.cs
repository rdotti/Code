using ContactConsumer.Consumer.Infra;
using ContactConsumer.Domain.Models;
using Shared.Domain.Models;

namespace ContactConsumer.Consumer.Configurations
{
    public static class StartupExtensions
    {
        public static void AddDeletedQueue(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConsumer<DeleteContactModel>(options =>
            {
                options.ExchangeName = configuration.GetValue<string>(ApplicationVariables.Queues.DeletedQueue.ExchangeName);
                options.QueueName = configuration.GetValue<string>(ApplicationVariables.Queues.DeletedQueue.QueueName);
                options.RoutingKey = configuration.GetValue<string>(ApplicationVariables.Queues.DeletedQueue.RoutingKey);
                options.ThreadsCount = configuration.GetValue<int>(ApplicationVariables.Queues.DeletedQueue.ThreadCount);
                options.Retries = configuration.GetValue<int>(ApplicationVariables.Queues.DeletedQueue.Retries);
                options.AwaitQueueTime = configuration.GetValue<int>(ApplicationVariables.Queues.DeletedQueue.AwaitTime);
            });
        }

        public static void AddUpdatedQueue(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConsumer<UpdateContactModel>(options =>
            {
                options.ExchangeName = configuration.GetValue<string>(ApplicationVariables.Queues.UpdateQueue.ExchangeNmae);
                options.QueueName = configuration.GetValue<string>(ApplicationVariables.Queues.UpdateQueue.QueueName);
                options.RoutingKey = configuration.GetValue<string>(ApplicationVariables.Queues.UpdateQueue.RoutingKey);
                options.ThreadsCount = configuration.GetValue<int>(ApplicationVariables.Queues.UpdateQueue.ThreadCount);
                options.Retries = configuration.GetValue<int>(ApplicationVariables.Queues.UpdateQueue.Retries);
                options.AwaitQueueTime = configuration.GetValue<int>(ApplicationVariables.Queues.UpdateQueue.AwaitTime);
            });
        }

        public static void AddInsertQueue(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConsumer<InsertContactModel>(options =>
            {
                options.ExchangeName = configuration.GetValue<string>(ApplicationVariables.Queues.InsertQueue.ExchangeNmae);
                options.QueueName = configuration.GetValue<string>(ApplicationVariables.Queues.InsertQueue.QueueName);
                options.RoutingKey = configuration.GetValue<string>(ApplicationVariables.Queues.InsertQueue.RoutingKey);
                options.ThreadsCount = configuration.GetValue<int>(ApplicationVariables.Queues.InsertQueue.ThreadCount);
                options.Retries = configuration.GetValue<int>(ApplicationVariables.Queues.InsertQueue.Retries);
                options.AwaitQueueTime = configuration.GetValue<int>(ApplicationVariables.Queues.InsertQueue.AwaitTime);
            });
        }
    }
}
