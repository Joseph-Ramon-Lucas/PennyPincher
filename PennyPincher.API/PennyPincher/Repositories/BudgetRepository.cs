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
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string sql = "";
                int insertedId = await conn.ExecuteScalarAsync<int>(sql, budget); 
                return insertedId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Budget>> GetAllBudgetsAsync()
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string sql = "";
                var allBudgets = await conn.QueryAsync<Budget>(sql);

                return allBudgets;  
            }
            catch (Exception)
            {
                throw;
            }

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Budget>> GetAllBudgetsByTypeAsync(BudgetTypes budget)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string sql = @"";
                var allBudgetsByCategory = await conn.QueryAsync<Budget>(sql);

                return allBudgetsByCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Budget> GetBudgetByIdAsync(int id)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync(); 

            try
            {
                string sql = "";
                var foundBudget= await conn.QuerySingleAsync<Budget>(sql);

                return foundBudget;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateBudgetAsync(Budget budget)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string sql = "";
                var rowsAffected = await conn.ExecuteAsync(sql, budget);
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteBudgetAsync(int id)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync(); 

            try
            {
                string sql = "";
                var rowsAffected = await conn.ExecuteAsync(sql, id);
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
