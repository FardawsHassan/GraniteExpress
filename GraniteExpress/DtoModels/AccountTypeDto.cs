using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.DtoModels
{
    public class AccountTypeDto
    {
        public int AccountTypeId { get; set; }
        [Required]
        public string AccountTypeCode { get; set; }
        [Required]
        public string AccountTypeName { get; set; }
        [Required]
        public bool IsDebit { get; set; }
        public int? BalanceId { get; set; } // future
        public int? CashId { get; set; } //
        public int? IncomeId { get; set; } //
        public int? EquityId { get; set; } //

        public virtual List<AccountDto> Accounts { get; set; }
    }
}
