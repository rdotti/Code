namespace Shared.Rabbit.Consumer;
public class ConsumerOptions<TModel> where TModel : class
{
    private int _threadsCount = 1;
    public string QueueName {  get; set; }
    public string ExchangeName {  get; set; }
    public string RoutingKey { get; set; }
    public int Retries { get; set; }
    public long AwaitQueueTime { get; set; }
    public int ThreadsCount
    {
        get => _threadsCount;
        set => _threadsCount = value < 1 ? 1 : value;
    }
    public bool CreateQueues { get; set; } = false;
    public string ModelName => typeof(TModel).Name;
}

