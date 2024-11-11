using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.DtoModels
{
    public class JournalDto
    {
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
        
        public virtual List<JournalDetailDto> JournalDetail { get; set; }
        //public virtual DocumentTypeDto DocumentType { get; set; }
        //public virtual TemplateDto Template { get; set; }
        //public virtual ClientDto Client { get; set; }
        //public virtual UserDto User { get; set; }
    }
}
