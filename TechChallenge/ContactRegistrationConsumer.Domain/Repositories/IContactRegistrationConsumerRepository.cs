using Shared.Domain.Entities;
using Shared.Infraestructure.Repositories;

namespace ContactConsumer.Domain.Repositories
{
    public interface IContactConsumerRepository : IRepository<Contact>
    {
    }
}
