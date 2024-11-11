using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.DtoModels
{
    public class JournalDetailDto
    {
        public int JournalDetailId { get; set; }
        [Required]
        public int JournalId { get; set; }
        [Required]
        public int AccountId { get; set; }
        public bool IsDebit { get; set; } = false;
        public int? ClientId { get; set; } //
        [Precision(30, 6)]
        public decimal? CurrencyAmount { get; set; }
        [Precision(30, 6)]
        public decimal? ExchangeRate { get; set; }
        public int? CashFlowId { get; set; } //
        
        public virtual JournalDto Journal { get; set; }
        public virtual AccountDto Account { get; set; }
        public virtual CashFlowDto CashFlow { get; set; }
    }
}
