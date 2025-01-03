﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public class JournalDetail
    {
        [Key]
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
        
        public virtual Journal? Journal { get; set; }
        public virtual Account? Account { get; set; }
        public virtual CashFlow? CashFlow { get; set; }
    }
}
