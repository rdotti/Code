namespace Shared.Domain.Entities
{
    public class Contact : EntityBase
    {
        public required string Nome { get; set; }

        public required string Telefone { get; set; }

        public required string EMail { get; set; }
        public int DDD { get; set; }
    }
}
