using System.ComponentModel.DataAnnotations;

namespace GraniteExpress.DtoModels
{
    public class CashFlowDto
    {
        public int CashFlowId { get; set; }
        public int? OrderId { get; set; }
        [Required]
        [MaxLength(100)]
        public string CashFlowDescription { get; set; }
        [Required]
        public bool IsVisible { get; set; } = false;

        public virtual List<JournalDetailDto> JournalDetails { get; set; }
    }
}
