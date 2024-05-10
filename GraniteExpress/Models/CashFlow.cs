using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.Models
{
    public class CashFlow
    {
        [Key]
        public int CashFlowId { get; set; }
        public int? OrderId { get; set; }
        [Required]
        [MaxLength(100)]
        public string CashFlowDescription { get; set; }
        [Required]
        public bool IsVisible { get; set; } = false;

        public List<JournalDetail> JournalDetails { get; set; }
    }
}
