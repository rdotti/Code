using ContactSearch.Domain.Repositories;
using Shared.Domain.Entities;
using Shared.Infraestructure.Repository;
using Shared.Infraestructure.Repositories;

namespace ContactSearch.Infrastructure.Rrepositories
{
    public class ContactRepository(ApplicationDbContext context) : EFRepository<Contact>(context), IContactRepository
    {
        public IEnumerable<Contact> GetByDDD(int ddd) => _dbSet.Where(c => c.DDD == ddd);
    }
}
