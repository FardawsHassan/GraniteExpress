using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.DtoModels
{
    public class AccountDto
    {
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

        public virtual AccountTypeDto AccountType { get; set; }
        public virtual CurrencyDto Currency { get; set; }
        public virtual List<JournalDetailDto> JournalDetail { get; set; }
        public virtual List<TemplateDetailsDto> TemplateDetails { get; set; }
    }
}
