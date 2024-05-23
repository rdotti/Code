using Microsoft.EntityFrameworkCore;
using TechChallenge.Core.Entities;
using TechChallenge.Core.Repository;

namespace TechChallenge.Infraestructure.Repository
{
    public class EFRepository<T>(ApplicationDbContext context) : IRepository<T> where T : EntityBase
    {
        protected ApplicationDbContext _context = context;
        protected DbSet<T> _dbSet = context.Set<T>();

        public void Delete(int id)
        {
            _dbSet.Remove(Get(id));
            _context.SaveChanges();
        }

        public T Get(int id) =>
            _dbSet.First(d => d.Id == id);

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
