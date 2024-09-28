using TechChallenge.Core.Extensions;
using TechChallenge.Core.Models;
using TechChallenge.Core.Repository;
using TechChallenge.Queue.Producer;

namespace TechChallenge.API.Services
{
    public class ContatoService(IContatoRepository _repository, IContatoProducer _producer) : IContatoService
    {
        public void Delete(int id) => _producer.SendDelete(id);

        public ContatoGetModel? Get(int id) => _repository.Get(id)?.FromEntity();

        public IList<ContatoGetModel> GetAll() => 
            _repository.GetAll().Select(c => c.FromEntity()).ToList();

        public IList<ContatoGetModel> GetByDDD(int ddd) => GetAll().Where(c => c.DDD == ddd).ToList();

        public void Insert(ContatoInsertModel model) => _producer.SendInsert(model.ToEntity());

        public void Update(ContatoUpdateModel model) => _producer.SendUpdate(model.ToEntity());
    }
}
