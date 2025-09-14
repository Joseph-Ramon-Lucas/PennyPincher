using PennyPincher.Models;
using PennyPincher.Models.DtoModels;
using static PennyPincher.Models.TypeCollections;

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
                string sql = @"INSERT INTO cashflow_entry 
                                (cashflow_entry_name, description, amount, entry_date, cashflow_entry_type, category_type, user_id) 
                                VALUES (@Name, @Description, @Amount, @EntryDate, @Flow, @CategoryType, @UserId) 
                                RETURNING cashflow_entry_id";

                int newId = await _dbService.GetAsync<int>(sql, entry);
                return newId;
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
                string sql = "SELECT * FROM cashflow_entry WHERE cashflow_entry_id = @id";
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
                string sql = "SELECT * FROM cashflow_entry LIMIT 1000";
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
                string sql = $"SELECT * FROM cashflow_entry WHERE cashflow_entry_type = '{type.ToString()}' LIMIT 1000";
                var allCashflowEntriesByFlowType = await _dbService.GetAllAsync<CashflowEntry>(sql, new { });

                return allCashflowEntriesByFlowType;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all cashflow entries from cashflow_entry: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateCashflowEntryAsync(CashflowEntry entry)
        {
            try
            {
                string sql = @"
                            UPDATE cashflow_entry
                            SET cashflow_entry_name = @cashflow_entry_name,
                                description = @description, 
                                amount = @amount, 
                                entry_date = @entry_date,
                                cashflow_entry_type = @flow, 
                                category_type = @category_type,
                                user_id = @user_id
                            WHERE cashflow_entry_id = @cashflow_entry_id
                            ";
                
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
                string sql = "DELETE FROM cashflow_entry WHERE cashflow_entry_id = @id";
                var rowsAffected = await _dbService.ModifyData<int>(sql, new { id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entry from cashflow_entry: {ex.Message}");
                throw;
            }
        }
    }
}
