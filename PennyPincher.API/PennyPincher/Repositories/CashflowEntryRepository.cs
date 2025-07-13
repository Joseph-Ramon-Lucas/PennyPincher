using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public class CashflowEntryRepository : ICashflowEntryRepository
    {
        private readonly IDbService _dbService;

        public CashflowEntryRepository(IDbService dbService)
        {
            _dbService = dbService; 
        }

        public async Task<int?> CreateCashflowEntryAsync(CashflowEntryForCreationDto entry)
        {
            try
            {
                string sql = "";
                object insertedId = await _dbService.ModifyData<int>(sql, entry);
                
                return Convert.ToInt32(insertedId); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting cashflow entry: {ex.Message}");
                throw;
            }
        }

        public async Task<CashflowEntry> GetCashflowEntryByIdAsync(int id)
        {
            try
            {
                string sql = "";
                var foundEntry = await _dbService.GetAsync<CashflowEntry>(sql, new { id });
                
                return foundEntry;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting specific entry from cashflow_entry: {ex.Message}");
                throw;
            }
        }
        
        public async Task<IEnumerable<CashflowEntry>> GetAllCashflowEntriesAsync()
        {
            try
            {
                string sql = "";
                var allCashflowEntries = await _dbService.GetAllAsync<CashflowEntry>(sql, new { });

                return allCashflowEntries;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all cashflow entries from cashflow_entry: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<CashflowEntry>> GetAllCashflowEntriesByFlowTypeAsync(CashflowType type)
        {
            try
            {
                string sql = "";
                var allCashflowEntriesByFlowType = await _dbService.GetAllAsync<CashflowEntry>(sql, new { });

                return allCashflowEntriesByFlowType;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateCashflowEntryAsync(CashflowEntry entry)
        {
            try
            {
                string sql = "";
                var rowsAffected = await _dbService.ModifyData<CashflowEntry>(sql, entry);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating specific entry from cashflow_entry: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteCashflowEntryAsync(int id)
        {
            try
            {
                string sql = "";
                var rowsAffected = await _dbService.ModifyData<int>(sql, new { id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entry from cashflow_ntry: {ex.Message}");
                throw;
            }
        }
    }
}
