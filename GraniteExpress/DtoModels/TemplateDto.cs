using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.DtoModels
{
    public class TemplateDto
    {
        public int TemplateId { get; set; }
        [Required]
        public string TemplateName { get; set; }
        [Required]
        public bool isActive { get; set; } = false;

        public virtual List<TemplateDetailsDto> TemplateDetail { get; set; }
        public virtual List<JournalDto> Journals { get; set; }
    }
}
