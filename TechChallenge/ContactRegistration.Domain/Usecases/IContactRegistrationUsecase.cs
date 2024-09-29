using Shared.Domain.Models;

namespace ContactRegistration.Domain.Usecases
{
    public interface IContactRegistrationUsecase
    {
        void Insert(InsertContactModel entity);
        void Update(UpdateContactModel entity);
        void Delete(int id);
    }
}
