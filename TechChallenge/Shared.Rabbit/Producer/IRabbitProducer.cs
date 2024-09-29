namespace Shared.Rabbit.Producer
{
    public interface IRabbitProducer
    {
        void Publish<T>(T message, string queueName);
    }
}
