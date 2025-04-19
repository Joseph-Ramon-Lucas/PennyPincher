using PennyPincher.Models;

namespace PennyPincher.Repositories
{
    public interface IExpenseRepository
    {
        Task<int?> CreateExpenseAsync(ExpenseForCreationDto expense);

        Task<Expense> GetExpenseByIdAsync(int id);

        Task<IEnumerable<Expense>> GetAllExpensesAsync();

        Task<IEnumerable<Expense>> GetAllExpensesByCategoryAsync(CategoryTypes category);
        
        Task<bool> UpdateExpenseAsync(Expense expense);
        
        Task<bool> DeleteExpenseAsync(int id);
    }
}
