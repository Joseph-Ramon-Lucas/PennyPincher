using System.ComponentModel.DataAnnotations;

namespace PennyPincher.Models.DtoModels
{
    public class UserForCreationDto
    {


        [Required(ErrorMessage = "A Name is required for this User")]
        public string Name { get; set; } = string.Empty;


        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Password is required for this User")]
        public string Password { get; set; } = string.Empty;
    }
}
