using TechChallenge.Core.Entities;

namespace TechChallenge.Core.Repository
{
    public interface IRepository<T> where T : EntityBase
    {
        IList<T> GetAll();
        T Get(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
