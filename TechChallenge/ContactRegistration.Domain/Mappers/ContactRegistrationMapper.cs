using Shared.Domain.Entities;
using Shared.Domain.Models;

namespace ContactRegistration.Domain.Mappers
{
    public static class ContactRegistrationMapper
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
