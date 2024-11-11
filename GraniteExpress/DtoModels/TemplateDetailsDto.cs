using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.DtoModels
{
    public class TemplateDetailsDto
    {
        public int TemplateDetailsId { get; set; }
        public int? TemplateId { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        public bool IsDebit{ get; set; }


        public virtual TemplateDto Template { get; set; }
        public virtual AccountDto Account { get; set; }
    }
}
