using PennyPincher.Models.DtoModels;

namespace PennyPincher.Models
{
    public class CashflowEntry
    {
        public int cashflow_entry_id { get; set; }
        
        public int amount { get; set; }
        
        public CashflowType flow { get; set; }
    }
}
