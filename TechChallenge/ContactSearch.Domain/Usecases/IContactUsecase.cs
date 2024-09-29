using ContactSearch.Domain.Models;

namespace ContactSearch.Domain.Usecases
{
    public interface IContactUsecase
    {
        IList<GetContactModel> GetAll();
        GetContactModel? Get(int id);
        IList<GetContactModel> GetByDDD(int ddd);
    }
}
