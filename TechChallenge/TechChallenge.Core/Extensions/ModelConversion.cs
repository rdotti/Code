using TechChallenge.Core.Entities;
using TechChallenge.Core.Models;

namespace TechChallenge.Core.Extensions
{
    public static class ModelConversion
    {
        public static Contato ToEntity(this ContatoInsertModel model) => new()
        {
            DDD = model.DDD,
            EMail = model.EMail,
            Nome = model.Nome,
            Telefone = model.Telefone
        };

        public static Contato ToEntity(this ContatoUpdateModel model) => new()
        {
            Id = model.Id,
            DDD = model.DDD,
            EMail = model.EMail,
            Nome = model.Nome,
            Telefone = model.Telefone
        };

        public static ContatoGetModel FromEntity(this Contato entity) => new()
        {
            DataCriacao = entity.DataCriacao,
            DDD = entity.DDD,
            EMail = entity.EMail,
            Id = entity.Id,
            Nome = entity.Nome,
            Telefone = entity.Telefone
        };
    }
}
