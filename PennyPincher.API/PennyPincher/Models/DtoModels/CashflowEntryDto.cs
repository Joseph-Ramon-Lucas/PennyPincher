using System.ComponentModel.DataAnnotations;

namespace PennyPincher.Models.DtoModels
{
    public enum CashflowType
    {
        Income = 0,
        Expense = 1
    }

    public class CashflowEntryDto
    {
        public int Id { get; set; }
        
        public int Amount { get; set; }

        [Required(ErrorMessage = "A FlowType (Income / Expense) is required for this CashFlow")]
        [EnumDataType(typeof(CashflowType))]
        public CashflowType Flow { get; set; }
    }
}
