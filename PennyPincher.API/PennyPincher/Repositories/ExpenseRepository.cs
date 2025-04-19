using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Npgsql;
using PennyPincher.Models;

namespace PennyPincher.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly string _connectionString;
        
        public ExpenseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            //_connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int?> CreateExpenseAsync(ExpenseForCreationDto expense)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string insertSql = @"
                    INSERT INTO expenses (user_id, name, category_type, price) 
                    VALUES (@UserId, @Name, @Category, @Price)
                    RETURNING id;
                ";

                int insertedId = await conn.ExecuteScalarAsync<int>(insertSql, expense);
                return insertedId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
             
            try
            {
                string sql = "SELECT id, user_id, name, category_type, price FROM expenses";
                var allExpenses = await conn.QueryAsync<Expense>(sql);

                return allExpenses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesByCategoryAsync(CategoryTypes category)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string sql = @"
                    SELECT id, users_id, name, category_type, price 
                    FROM expenses
                    WHERE category_type = @category";

                var allExpenses = await conn.QueryAsync<Expense>(sql);
                return allExpenses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Expense> GetExpenseByIdAsync(int id)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string sql = "SELECT id, users_id, name, category_type, price FROM expenses WHERE id = @id";
                var foundExpense = await conn.QuerySingleAsync<Expense>(sql);
                return foundExpense;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateExpenseAsync(Expense expense)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string sql = @"
                    UPDATE expenses 
                    SET name = @name, category_type = @category_type, price = @price";

                var rowsAffected = await conn.ExecuteAsync(sql, expense);
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            try
            {
                string sql = "DELETE FROM expenses WHERE Id = @Id";
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
