using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraniteExpress.DtoModels
{
    public class ClientypeDto
    {
        public int Id { get; set; }
        [Required]
        public string ClientType { get; set; }

        public virtual List<ClientDto> Clients { get; set; }
    }
}
