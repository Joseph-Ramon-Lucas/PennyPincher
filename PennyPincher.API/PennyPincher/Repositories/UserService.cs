using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public class UserService : IUserService
    {
        private readonly IDbService _dbService;
       
        public UserService(IDbService dbService) {
            _dbService = dbService;
        }
        public Task<int?> CreatUserAsync(UserForCreationDto user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                string sql_getAllUsers = @"
                    SELECT *
                    FROM
                    public.user_account
                    ";
                List<User> users = await _dbService.GetAllAsync<User>(sql_getAllUsers, new { });
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all users from user_account: {ex.Message}");
                throw;
            }


        }

        public Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
