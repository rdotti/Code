using ContactSearch.Domain.Models;
using Shared.Domain.Entities;

namespace ContactSearch.Domain.Mapper
{
    public static class ContactMapper
    {
        public static GetContactModel FromEntity(this Contact entity) => new()
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
