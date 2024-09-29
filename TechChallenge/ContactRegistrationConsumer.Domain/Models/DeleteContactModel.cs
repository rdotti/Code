using Shared.Domain.Models;

namespace ContactConsumer.Domain.Models
{
    public class DeleteContactModel : ContactModel
    {
        public int Id { get; set; }
    }
}
