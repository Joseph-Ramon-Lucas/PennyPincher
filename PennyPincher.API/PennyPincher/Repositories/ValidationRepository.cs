using PennyPincher.Models;
using PennyPincher.Models.DtoModels;
using System.Text.RegularExpressions;

namespace PennyPincher.Repositories
{
    public class ValidationRepository : IValidationRepository
    {
        private readonly IDbService _dbService;

        public ValidationRepository(IDbService dbService)
        {
            _dbService = dbService;
        }


        public async Task<ValidationResponseDto> checkCashflowGroupExists(int groupId)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM public.cashflow_group 
                                WHERE cashflow_group_id = @groupId 
                                LIMIT 1000";
                var foundBudget = await _dbService.GetAsync<int>(sql, new { groupId });

                if (foundBudget != 0)
                {
                    return new ValidationResponseDto()
                    {
                        IsSuccess = true,
                    };
                }
                return new ValidationResponseDto()
                {
                    IsSuccess = false,
                    ResponseMessage = $"Group id {groupId} doesn't exist"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting specific budget from budget_group: {ex.Message}");
                throw;
            }
        }


        public async Task<ValidationResponseDto> checkUserExists(int userId)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM public.user_account 
                                WHERE user_id = @userId
                                LIMIT 1000;";

                var foundUser = await _dbService.GetAsync<int>(sql, new { userId });

                if (foundUser != 0)
                {
                    return new ValidationResponseDto()
                    {
                        IsSuccess = true,
                    };
                }
                return new ValidationResponseDto()
                {
                    IsSuccess = false,
                    ResponseMessage = $"User id {userId} doesn't exist"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting specific user from user table: {ex.Message}");
                throw;
            }
        }

        public async Task<ValidationResponseDto> checkUserExistsByEmail(string userEmail)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM public.user_account 
                                WHERE email = @userEmail
                                LIMIT 1000;";

                var foundUser = await _dbService.GetAsync<int>(sql, new { userEmail });

                if (foundUser != 0)
                {
                    return new ValidationResponseDto()
                    {
                        IsSuccess = true,
                    };
                }
                return new ValidationResponseDto()
                {
                    IsSuccess = false,
                    ResponseMessage = $"User Email {userEmail} doesn't exist"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting specific user from user table: {ex.Message}");
                throw;
            }
        }
    }
}
