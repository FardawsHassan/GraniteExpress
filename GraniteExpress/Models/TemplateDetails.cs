using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public class TemplateDetails
    {
        [Key]
        public int TemplateDetailsId { get; set; }
        public int? TemplateId { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        public bool IsDebit{ get; set; }


        public Template Template { get; set; }
        public Account Account { get; set; }
    }
}
