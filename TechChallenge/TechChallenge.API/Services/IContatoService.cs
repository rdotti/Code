using TechChallenge.Core.Entities;
using TechChallenge.Core.Models;

namespace TechChallenge.API.Services
{
    public interface IContatoService
    {
        IList<ContatoGetModel> GetAll();
        ContatoGetModel Get(int id);
        void Insert(ContatoInsertModel entity);
        void Update(ContatoUpdateModel entity);
        void Delete(int id);
        IList<ContatoGetModel> GetByDDD(int ddd);
    }
}
