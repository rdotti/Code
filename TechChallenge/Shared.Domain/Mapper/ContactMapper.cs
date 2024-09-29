using Shared.Domain.Entities;
using Shared.Domain.Models;

namespace Shared.Domain.Mapper
{
    public static class ContactMapper
    {
        public static Contact ToEntity(this InsertContactModel model) => new()
        {
            DDD = model.DDD,
            EMail = model.EMail,
            Nome = model.Nome,
            Telefone = model.Telefone
        };

        public static Contact ToEntity(this UpdateContactModel model) => new()
        {
            Id = model.Id,
            DDD = model.DDD,
            EMail = model.EMail,
            Nome = model.Nome,
            Telefone = model.Telefone
        };
    }
}
