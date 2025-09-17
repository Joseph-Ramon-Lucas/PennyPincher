using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserByIdAsync(int user_id);

        Task<User> GetUserByEmailAsync(string email);

        Task<int?> CreatUserAsync(UserForCreationDto user);

        Task<bool> UpdateUserAsync(int user_Id, UserForUpdateDto user);

        Task<bool> DeleteUserByIdAsync(int user_id);

        public UserDto UserToDto(User user);
    }
}
