using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.DtoModels
{
    public class ClientDto
    {
        public int ClientId { get; set; }
        [Required]
        public int ClientTypeId { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientCode { get; set; }

        public virtual ClientypeDto clienType { get; set; }
        public virtual List<JournalDetailDto> JournalDetail { get; set; }
        public virtual List<JournalDto> Journals { get; set; }
        public virtual List<TemplateDetailsDto> TemplateDetails { get; set; }
    }
}
