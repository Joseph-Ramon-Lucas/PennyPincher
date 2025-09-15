using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserByIdAsync(int id);

        Task<User> GetUserByEmailAsync(string email);

        Task<bool> UpdateUserAsync(User user);

        Task<int?> CreatUserAsync(UserForCreationDto user);

        Task<bool> DeleteUserByIdAsync(int id);

        public UserDto UserToDto(User user);
    }
}
