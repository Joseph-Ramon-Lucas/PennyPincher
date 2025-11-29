using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public interface IValidationRepository
    {
        Task<ValidationResponseDto> checkCashflowGroupExists(int groupId);
        Task<ValidationResponseDto> checkUserExists(int userId);
        Task<ValidationResponseDto> checkUserExistsByEmail(string userEmail);

        Task<ValidationResponseDto> validateUserPassword(UserForCreationDto userLoggingIn, string hashedPassword, string providedPassword);
    }
}
