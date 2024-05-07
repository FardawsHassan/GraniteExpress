using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        public string AccountCode { get; set; }
        [Required]
        public int AccountTypeId { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        [Required]
        public string AccountName { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public AccountType AccountType { get; set; }
        public Currency Currency { get; set; }
        public List<JournalDetail> JournalDetail { get; set; }
        public List<TemplateDetails> TemplateDetails { get; set; }
    }
}
