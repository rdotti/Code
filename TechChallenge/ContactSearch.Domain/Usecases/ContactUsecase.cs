using ContactSearch.Domain.Mapper;
using ContactSearch.Domain.Models;
using ContactSearch.Domain.Repositories;

namespace ContactSearch.Domain.Usecases
{
    public class ContactUsecase(IContactRepository _repository) : IContactUsecase
    {
        public GetContactModel? Get(int id) => _repository.Get(id)?.FromEntity();

        public IList<GetContactModel> GetAll() =>
            _repository.GetAll().Select(c => c.FromEntity()).ToList();

        public IList<GetContactModel> GetByDDD(int ddd) => GetAll().Where(c => c.DDD == ddd).ToList();
    }
}
