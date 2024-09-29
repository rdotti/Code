using ContactRegistration.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Shared.Domain.Entities;
using Shared.Rabbit.Producer;

namespace ContactRegistration.Infrastructure.Repositories
{
    public class ContactRegistrationRepository(IRabbitProducer _producer, IConfiguration _configuration) : IContactRegistrationRepository
    {
        public void Delete(int id)
        {
            _producer.Publish(id, _configuration.GetSection("RabbitMQ")
                                    .GetSection("Deleted")
                                    .GetSection("QueueName").Value!);
        }

        public void Insert(Contact entity)
        {
            _producer.Publish(entity,
                _configuration.GetSection("RabbitMQ")
                    .GetSection("Deleted")
                    .GetSection("QueueName").Value!);
        }

        public void Update(Contact entity)
        {
            _producer.Publish(entity, 
                _configuration.GetSection("RabbitMQ")
                    .GetSection("Deleted")
                    .GetSection("QueueName").Value!);
        }
    }
}
