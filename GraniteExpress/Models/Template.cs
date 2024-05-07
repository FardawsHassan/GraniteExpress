using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public class Template
    {
        [Key]
        public int TemplateId { get; set; }
        [Required]
        public string TemplateName { get; set; }
        [Required]
        public bool isActive { get; set; } = false;

        public List<TemplateDetails> TemplateDetail { get; set; }
        public List<Journal> Journals { get; set; }
    }
}
