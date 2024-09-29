using Shared.Domain.Models;

namespace ContactSearch.Domain.Models
{
    public class GetContactModel : ContactModel
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
