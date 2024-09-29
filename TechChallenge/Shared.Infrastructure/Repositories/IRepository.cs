using Shared.Domain.Entities;

namespace Shared.Infraestructure.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        IList<T> GetAll();
        T? Get(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
