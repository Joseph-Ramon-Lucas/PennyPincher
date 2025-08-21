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
                string email = newUser.Email;
                string password = newUser.Password;


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

        public async Task<bool> DeleteUserByIdAsync(int id)
        {
            try
            {
                //first should get all the ids of the user's cashflows and groups
                string sql_selectUserObjects = @"
                        SELECT mp.cashflow_entry_id, mp.cashflow_group_id FROM 
                            public.management_profile mp 
                        JOIN public.cashflow_group cg
                            ON mp.cashflow_group_id = cg.cashflow_group_id
                        JOIN public.cashflow_entry ce 
                            ON mp.cashflow_entry_id = ce.cashflow_entry_id

                        WHERE mp.user_id = @id;
                        ";

                List<UserMadeObjectIds> userMadeObjectIds = await _dbService.GetAllAsync<UserMadeObjectIds>(sql_selectUserObjects, new {id});
                Console.WriteLine($"{userMadeObjectIds}");

                //remove user's cashflow items
                string sql_removeUserCashflowItems = @"
                        
                        ";
                return true;


                //remove from chasflow groups table


                //remove user from user from user table

                //remove user from cashflows and groups
                string sql_deleteUser = @"
                        DELETE FROM management_profile
                        WHERE user_id=@id;
                        ";

                int deletedRows = await _dbService.ModifyData<int>(sql_deleteUser, id);
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

        public async Task<User> GetUserByIdAsync(int id)
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
                    WHERE user_id = @id;
                    ";
                User foundUser = await _dbService.GetAsync<User>(sql_getUser, new { id });
                return foundUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting user from user_account: {ex.Message}");
                throw;
            }
        }

        public Task<bool> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
