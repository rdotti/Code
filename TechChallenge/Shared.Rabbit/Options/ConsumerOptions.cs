namespace Shared.Rabbit.Options;
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
    public bool CreateQueues { get; set; } = true;
    public string ModelName => typeof(TModel).Name;
}

