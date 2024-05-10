using GraniteExpress.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public class SelectJournalView
    {
        public string? DocumentNo { get; set; }
        public DateTime? DocumentDate { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? CurrencyAmount { get; set; }
        public int? AccountId { get; set; }
    }
}

