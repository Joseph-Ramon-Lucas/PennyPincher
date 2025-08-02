using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public class CashflowGroupRepository : ICashflowGroupRepository
    {
        private readonly IDbService _dbService;
        
        public CashflowGroupRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<int?> CreateCashflowGroupAsync(CashflowGroupDto group)
        {
            try
            {
                string sql = @"";
                
                object insertedId = await _dbService.ModifyData<int>(sql, group);
                
                return Convert.ToInt32(insertedId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        
        public async Task<CashflowGroup?> GetCashflowGroupByIdAsync(int id)
        {
            try
            {
                string sql = @"";
                var foundGroup = await _dbService.GetAsync<CashflowGroup>(sql, id);
                
                return foundGroup;  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<IEnumerable<CashflowGroup>> GetAllCashflowGroupsAsync()
        {
            try
            {
                string sql = @"";
                var allGroups = await _dbService.GetAllAsync<CashflowGroup>(sql, new { });
                
                return allGroups;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<bool> UpdateCashflowGroupAsync(CashflowGroup group)
        {
            try
            {
                string sql = @"";
                
                var rowsAffected = await _dbService.ModifyData<bool>(sql, group);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.Write("");
                throw;
            }
        }

        public async Task<bool> DeleteCashflowGroupAsync(int id)
        {
            try
            {
                string sql = "";
                var rowsAffected = await _dbService.ModifyData<bool>(sql, id);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                throw;
            }
        }
    }
}