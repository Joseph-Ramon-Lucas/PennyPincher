namespace PennyPincher.Models.DtoModels
{
    public class CashflowEntryForCreationDto
    {
        public int Amount { get; set; }
        
        public CashflowType Flow { get; set; }
    }
}
