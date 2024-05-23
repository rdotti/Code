using TechChallenge.Core.Entities;
using TechChallenge.Core.Repository;

namespace TechChallenge.Infraestructure.Repository
{
    public class ContatoRepository(ApplicationDbContext context) : EFRepository<Contato>(context), IContatoRepository
    {
        public IEnumerable<Contato> GetByDDD(int ddd) => _dbSet.Where(c => c.DDD == ddd);
    }
}
