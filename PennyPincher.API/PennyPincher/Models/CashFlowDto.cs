using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PennyPincher.Models
{

    public enum FlowTypes
    {
        income = 0,
        expense= 1
    }
    public class CashFlowDto : CashFlowUpdateDto
    {
        [Required(ErrorMessage ="An integer ID is required for this CashFlow")]
        [Range(0,int.MaxValue)]
        public int Id { get; set; }

    }
}
