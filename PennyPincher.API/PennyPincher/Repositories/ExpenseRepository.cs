using System.Data;
using Dapper;
using PennyPincher.Data;

namespace PennyPincher.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IDbConnection _dbConnection;
        
        public ExpenseRepository(IDbConnection connection)
        {
            _dbConnection = connection;
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            string sql = "SELECT id, user_id, name, category_type, price FROM expenses";
            return await _dbConnection.QueryAsync<Expense>(sql);
        }

        public async Task<Expense?> GetStudentAsync(int id)
        {
            string sql = "SELECT id, user_id, name, category_type, price FROM expenses WHERE id = @id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Expense>(sql, new { id });
        }

        public async Task<int> AddExpenseAsync(Expense expense)
        {
            string sql = @"
                INSERT INTO expenses (name, category_type, price) 
                VALUES (@name, @category_type, @price)
                SELECT LAST_INSERT_ID();";
            
            return await _dbConnection.ExecuteAsync(sql, expense);
        }

        public async Task<bool> UpdateExpenseAsync(Expense expense)
        {
            string sql = @"
                UPDATE expenses 
                SET name = @name, category_type = @category_type, price = @price";
            
            int rowsAffected = await _dbConnection.ExecuteAsync(sql, expense);
            return rowsAffected > 0;    
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {
            string sql = "DELETE FROM expenses WHERE Id = @Id";
            int rowsAffected = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
