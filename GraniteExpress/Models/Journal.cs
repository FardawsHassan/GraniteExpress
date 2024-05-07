using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public class Journal
    {
        [Key]
        public int JournalId { get; set; }
        public int? TemplateId { get; set; }
        [Required]
        public string JournalDescription { get; set; }
        [Required]
        public int DocumentTypeId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string DocumentNo { get; set; }
        [Required]
        public DateTime DocumentDate { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public bool IsDelete { get; set; } = false;


        public List<JournalDetail> JournalDetail { get; set; }
        public DocumentType DocumentType { get; set; }
        public Template Template { get; set; }
        public Client Client { get; set; }
    }
}
