namespace PennyPincher.Models.DtoModels
{
    public class CashflowEntryForUpdateDto
    {
        public int Amount { get; set; }
        
        public CashflowType Flow { get; set; }  
    }
}
