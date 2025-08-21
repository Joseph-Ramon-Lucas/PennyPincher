using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public interface IUserService
    {
        Task<int?> CreatUserAsync(UserForCreationDto user);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserByIdAsync(int id);

        Task<bool> UpdateUserAsync(User user);

        Task<bool> DeleteUserByIdAsync(int id);

        public UserDto UserToDto(User user);
    }
}
