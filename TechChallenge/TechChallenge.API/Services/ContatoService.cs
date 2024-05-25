using TechChallenge.Core.Extensions;
using TechChallenge.Core.Models;
using TechChallenge.Core.Repository;

namespace TechChallenge.API.Services
{
    public class ContatoService(IContatoRepository _repository) : IContatoService
    {
        public void Delete(int id) => _repository.Delete(id);

        public ContatoGetModel? Get(int id) => _repository.Get(id)?.FromEntity();

        public IList<ContatoGetModel> GetAll() => 
            _repository.GetAll().Select(c => c.FromEntity()).ToList();

        public IList<ContatoGetModel> GetByDDD(int ddd) => GetAll().Where(c => c.DDD == ddd).ToList();

        public void Insert(ContatoInsertModel entity) => _repository.Insert(entity.ToEntity());

        public void Update(ContatoUpdateModel entity) => _repository.Update(entity.ToEntity());
    }
}
