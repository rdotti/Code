using Shared.Domain.Entities;

namespace ContactRegistration.Domain.Repositories
{
    public interface IContactRegistrationRepository
    {
        void Delete(int id);
        void Insert(Contact entity);
        void Update(Contact entity);
    }
}
