using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public class Currency
    {
        [Key]
        public int CurrencyId { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
        [Precision(18, 4)]
        [DefaultValue(1)]
        public decimal? DefaultValue { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
