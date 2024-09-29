using ContactConsumer.Domain.Models;
using ContactConsumer.Domain.Repositories;
using Shared.Domain.Mapper;
using Shared.Domain.Models;

namespace ContactConsumer.Domain.Usecases
{
    public class ContactConsumerUsecase<T>(IContactConsumerRepository _repository)
        : IContactConsumerUsecase<T> where T : ContactModel
    {
        public void Delete(T entity)
        {
            if(entity is DeleteContactModel deleteModel)
            {
                _repository.Delete(deleteModel.Id);
            }
        }

        public void Insert(T entity)
        {
            if(entity is InsertContactModel insertModel)
            {
                _repository.Insert(insertModel.ToEntity());
            }
        }

        public void Update(T entity)
        {
            if(entity is UpdateContactModel updateModel)
            {
                _repository.Update(updateModel.ToEntity());
            }
        }
    }
}
