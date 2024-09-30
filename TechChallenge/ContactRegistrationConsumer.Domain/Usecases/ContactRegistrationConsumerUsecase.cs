using ContactConsumer.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Mapper;
using Shared.Domain.Models;

namespace ContactConsumer.Domain.Usecases
{
    public class ContactConsumerUsecase<T>(IServiceProvider _serviceProvider)
        : IContactConsumerUsecase<T> where T : ContactModel
    {
        public void Delete(int entityId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _repository = scope.ServiceProvider.GetRequiredService<IContactConsumerRepository>();
                _repository.Delete(entityId);
            }            
        }

        public void Insert(T entity)
        {
            if(entity is InsertContactModel insertModel)
            {
                using(var scope = _serviceProvider.CreateScope())
                {
                    var _repository = scope.ServiceProvider.GetRequiredService<IContactConsumerRepository>();
                    _repository.Insert(insertModel.ToEntity());
                }
            }
        }

        public void Update(T entity)
        {
            if(entity is UpdateContactModel updateModel)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _repository = scope.ServiceProvider.GetRequiredService<IContactConsumerRepository>();
                    _repository.Update(updateModel.ToEntity());
                }                
            }
        }
    }
}
