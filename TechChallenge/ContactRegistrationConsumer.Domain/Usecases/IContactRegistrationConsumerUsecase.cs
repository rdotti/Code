using Shared.Domain.Models;

namespace ContactConsumer.Domain.Usecases
{
    public interface IContactConsumerUsecase<T> where T : ContactModel
    {
        void Delete(int entityId);
        void Insert(T entity);
        void Update(T entity);
    }
}
