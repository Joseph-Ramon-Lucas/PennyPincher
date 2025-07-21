namespace PennyPincher.Models.DtoModels
{
    public class ValidationResponseDto
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; } = string.Empty;
    }
}
