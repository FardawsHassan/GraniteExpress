using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string UserId { get; set; }
        [Required]
        public bool IsDelete { get; set; } = false;


        [NotMapped]
        public List<JournalDetail> JournalDetail { get; set; }
        [NotMapped]
        public DocumentType DocumentType { get; set; }
        [NotMapped]
        public Template Template { get; set; }
        [NotMapped]
        public Client Client { get; set; }
        [NotMapped]
        public User User { get; set; }
    }
}
