using Shared.Domain.Entities;
using Shared.Infraestructure.Repositories;

namespace ContactSearch.Domain.Repositories
{
    public interface IContactRepository : IRepository<Contact>;
}
