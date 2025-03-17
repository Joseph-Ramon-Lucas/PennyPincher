using PennyPincher.Data;

namespace PennyPincher.Repositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        
        Task<Expense> GetStudentAsync(int id);
        
        Task<int> AddExpenseAsync(Expense expense);
        
        Task<bool> UpdateExpenseAsync(Expense expense);
        
        Task<bool> DeleteExpenseAsync(int id);
    }
}
