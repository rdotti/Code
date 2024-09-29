namespace Shared.Rabbit.Infra
{
    public static class ApplicationVariables
    {
        public static class Rabbit
        {
            public const string Host = "RabbitHostName";
            public const string Port = "RabbitPort";
            public const string User = "RabbitUsername";
            public const string Password = "RabbitPassword";
            public const string VirtualHost = "RabbitVirtualHost";
        }

        public static class Queues
        {
            public static class DeletedQueue
            {
                public const string ExchangeName = "DeletedExchange";
                public const string QueueName = "DeletedQueueName";
                public const string RoutingKey = "DeletedRoutingKey";
                public const string ThreadCount = "DeletedThreadCount";
                public const string Retries = "DeletedRetries";
                public const string AwaitTime = "DeletedAwaitTime";
            }
            public static class UpdateQueue
            {
                public const string ExchangeNmae = "UpdatedExchange";
                public const string QueueName = "UpdatedQueueName";
                public const string RoutingKey = "UpdatedRoutingKey";
                public const string ThreadCount = "UpdatedThreadCount";
                public const string Retries = "UpdatedRetries";
                public const string AwaitTime = "UpdatedAwaitTime";
            }
            public static class InsertQueue
            {
                public const string ExchangeNmae = "InsertedExchange";
                public const string QueueName = "InsertedQueueName";
                public const string RoutingKey = "InsertedRoutingKey";
                public const string ThreadCount = "InsertedThreadCount";
                public const string Retries = "InsertedRetries";
                public const string AwaitTime = "InsertedAwaitTime";
            }
        }
    }
}
