using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraniteExpress.Models
{
    public class Clientype
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ClientType { get; set; }

        public List<Client> Clients { get; set; }
    }
}
