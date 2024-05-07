using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public class DocumentType

    {
        [Key]
        public int DocumentTypeId { get; set; }
        [Required]
        public string DocumentName { get; set; }
        [Required]
        public string DocumentCode { get; set; }
        [Required]
        public int DocumentMaxValue { get; set; }


        public List<Journal> Journals { get; set; }
    }
}
