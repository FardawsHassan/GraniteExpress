using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.DtoModels
{
    public class DocumentTypeDto
    {
        public int DocumentTypeId { get; set; }
        [Required]
        public string DocumentName { get; set; }
        [Required]
        public string DocumentCode { get; set; }
        [Required]
        public int DocumentMaxValue { get; set; }

        public virtual List<JournalDto> Journals { get; set; }
    }
}
