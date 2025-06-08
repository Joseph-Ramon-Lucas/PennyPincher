using Dapper;
using Npgsql;
using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly string _connectionString;

        public BudgetRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found");
        }

        public async Task<int?> CreateBudgetAsync(BudgetForCreationDto budget)
        {
            try
            {
                await using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                string sql = "INSERT INTO budget_group (group_name) VALUES (@GroupName) RETURNING *";
                object insertedId = await conn.ExecuteScalarAsync<int>(sql, budget);

                return Convert.ToInt32(insertedId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting budget group: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Budget>> GetAllBudgetsAsync()
        {
            try
            {
                await using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();

                string sql = "SELECT * FROM budget_group";
                var allBudgets = await conn.QueryAsync<Budget>(sql);

                return allBudgets;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all budgets from budget_group: {ex.Message}");
                throw;
            }
        }

        public async Task<Budget> GetBudgetByIdAsync(int id)
        {
            try
            {
                await using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync(); 

                string sql = "SELECT * FROM budget_group WHERE budget_group_id = @id";
                var foundBudget= await conn.QuerySingleAsync<Budget>(sql, new { id });

                return foundBudget;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting specific budget from budget_group: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateBudgetAsync(Budget budget)
        {
            try
            {
                await using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
                string sql = "UPDATE budget_group SET group_name = @group_name WHERE budget_group_id = @budget_group_id";
                var rowsAffected = await conn.ExecuteAsync(sql, budget);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating specific budget from budget_group: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteBudgetAsync(int id)
        {
            try
            {
                await using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync(); 
                string sql = "DELETE FROM budget_group WHERE budget_group_id = @id";
                var rowsAffected = await conn.ExecuteAsync(sql, new { id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting specific budget from budget_group: {ex.Message}");
                throw;
            }
        }
    }
}
