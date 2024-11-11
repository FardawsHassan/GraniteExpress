using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        [Required]
        public int ClientTypeId { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientCode { get; set; }


        public virtual Clientype clienType { get; set; }
        public virtual List<JournalDetail> JournalDetail { get; set; }
        public virtual List<Journal> Journals { get; set; }
        public virtual List<TemplateDetails> TemplateDetails { get; set; }
        //begenningBalance
    }
}
