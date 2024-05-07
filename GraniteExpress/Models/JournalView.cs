using GraniteExpress.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public class JournalView
    {
        public string? DocumentName { get; set; }
        public string? DocumentNo { get; set; }
        public DateTime? DocumentDate { get; set; }
        public decimal? CurrencyAmount { get; set; }
        public decimal? ExchangeRate { get; set; }
        public bool? IsDebit { get; set; } = false;
        public string? ClientFirstName { get; set; }
        public string? ClientLastName { get; set; }
        public string? AccountCode { get; set; }
        public bool? IsActive { get; set; }
        public string? JournalDescription { get; set; }
        public int? DocumentTypeId { get; set; }
        public int? JournalId { get; set; }
    }
}

