using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Core.Entities
{
    public class Contato : EntityBase
    {
        public required string Nome { get; set; }
        
        public required string Telefone { get; set; }

        public required string EMail { get; set; }
        public int DDD { get; set; }
    }
}