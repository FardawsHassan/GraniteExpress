using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public class AccountType
    {
        [Key]
        public int AccountTypeId { get; set; }
        [Required]
        public string AccountTypeCode { get; set; }
        [Required]
        public string AccountTypeName { get; set; }
        [Required]
        public bool IsDebit { get; set; }
        public int? BalanceId { get; set; } //
        public int? CashId { get; set; } //
        public int? IncomeId { get; set; } //
        public int? EquityId { get; set; } //


        public List<Account> Accounts { get; set; }
    }
}
