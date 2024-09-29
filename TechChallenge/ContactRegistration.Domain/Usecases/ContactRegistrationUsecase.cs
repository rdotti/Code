using ContactRegistration.Domain.Mappers;
using ContactRegistration.Domain.Repositories;
using Shared.Domain.Models;

namespace ContactRegistration.Domain.Usecases
{
    public class ContactRegistrationUsecase(IContactRegistrationRepository _repository) : IContactRegistrationUsecase
    {
        public void Delete(int id) => _repository.Delete(id);
        public void Insert(InsertContactModel entity) => _repository.Insert(entity.ToEntity());
        public void Update(UpdateContactModel entity) => _repository.Update(entity.ToEntity());
    }
}
