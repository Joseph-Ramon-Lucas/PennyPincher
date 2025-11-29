using Microsoft.AspNetCore.Identity;
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

        public UserDto UserToDto(User user)
        {
            UserDto newUserDto = new UserDto()
            {
                Id = user.Id,
                TokenId = user.TokenId,
                Email = user.Email,

            };
            return newUserDto;
        }
        public async Task<int?> CreatUserAsync(UserForCreationDto newUser)
        {
            try
            {
                var passwordHasher = new PasswordHasher<UserForCreationDto>();

                string email = newUser.Email;
                string rawPassword = newUser.Password;
                string hashedPassword = passwordHasher.HashPassword(newUser, rawPassword);
                Console.WriteLine(hashedPassword);
                
                newUser.Password = hashedPassword;

                string sql_addUser = @"
                    INSERT INTO public.user_account (email, password)
                    VALUES (@email, @password)
                    RETURNING user_id;
                    ";


                int newUserId = await _dbService.ModifyDataReturning<int>(sql_addUser, newUser);
                return newUserId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding a new user to user_account: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteUserByIdAsync(int user_id)
        {
            try
            {
                
                string sql_deleteUser = @"
                        DELETE FROM user_account
                        WHERE user_id = @user_id;
                        ";

                int deletedRows = await _dbService.ModifyData<int>(sql_deleteUser, new { user_id });
                return deletedRows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting users from user_account: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                string sql_getAllUsers = @"
                    SELECT 
                        user_id as id, 
                        token_id as tokenId,
                        email
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

        public async Task<User> GetUserByIdAsync(int user_id)
        {
            try
            {
                string sql_getUser = @"
                    SELECT 
                        user_id as id, 
                        token_id as tokenId,
                        email
                    FROM
                    public.user_account
                    WHERE user_id = @user_id;
                    ";
                User foundUser = await _dbService.GetAsync<User>(sql_getUser, new { user_id });
                return foundUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user from user_account: {ex.Message}");
                throw;
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                string sql_getUser = @"
                    SELECT 
                        user_id as id, 
                        token_id as tokenId,
                        email,
                        password
                    FROM
                    public.user_account
                    WHERE email = @email;
                    ";
                User foundUser = await _dbService.GetAsync<User>(sql_getUser, new { email });
                return foundUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user by email from user_account: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateUserAsync(int user_id, UserForUpdateDto user)
        {
            try
            {
                var passwordHasher = new PasswordHasher<UserForUpdateDto>();

                string email = user.Email;
                string rawPassword = user.Password;
                string hashedPassword = passwordHasher.HashPassword(user, rawPassword);

                user.Password = hashedPassword;

                string sql_updateUser = @"
                                        UPDATE user_account
                                        SET email = @email,
                                            password = @password
                                        WHERE user_id = @user_id
                                         ";
                // no nested object params for Dapper
                var rowsAffected = await _dbService.ModifyData<User>(sql_updateUser, new { user.Email, user.Password, user_id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user from user_account: {ex.Message}");
                throw;
            }
        }
    }
}
