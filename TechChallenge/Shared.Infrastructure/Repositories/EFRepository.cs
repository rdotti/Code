using Microsoft.EntityFrameworkCore;
using Shared.Domain.Entities;
using Shared.Infraestructure.Repository;

namespace Shared.Infraestructure.Repositories
{
    public class EFRepository<T>(ApplicationDbContext context) : IRepository<T> where T : EntityBase
    {
        protected ApplicationDbContext _context = context;
        protected DbSet<T> _dbSet = context.Set<T>();

        public void Delete(int id)
        {
            var entity = Get(id);
            
            if (entity == null)
                return;

            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public T? Get(int id) =>
            _dbSet.FirstOrDefault(d => d.Id == id);

        public IList<T> GetAll() =>
            _dbSet.ToList();

        public void Insert(T entity)
        {
            entity.DataCriacao = DateTime.Now;
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
