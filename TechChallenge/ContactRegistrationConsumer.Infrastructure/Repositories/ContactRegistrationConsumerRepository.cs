using ContactConsumer.Domain.Repositories;
using Shared.Domain.Entities;
using Shared.Infraestructure.Repository;
using Shared.Infraestructure.Repositories;

namespace ContactConsumer.Infrastructure.Repositories
{
    public class ContactConsumerRepository(ApplicationDbContext context) : EFRepository<Contact>(context),
        IContactConsumerRepository
    {
    }
}
