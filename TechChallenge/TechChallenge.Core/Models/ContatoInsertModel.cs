using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Core.Models
{
    public class ContatoInsertModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome é obrigatório")]
        public required string Nome { get; set; }

        [Length(8, 9, ErrorMessage = "Telefone inválido")]
        public required string Telefone { get; set; }
        
        [RegularExpression(@"^[a-z0-9.]+@[a-z0-9]+\.[a-z]+(\.[a-z]+)?$", ErrorMessage = "E-mail inválido")]
        public required string EMail { get; set; }

        [Range(10, 99, ErrorMessage = "Região inválida")]
        public int DDD { get; set; }
    }
}
