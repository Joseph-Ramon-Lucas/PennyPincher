using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public interface IBudgetRepository
    {
        Task<int?> CreateBudgetAsync(BudgetForCreationDto budget);

        Task<IEnumerable<Budget>> GetAllBudgetsAsync();

        Task<Budget> GetBudgetByIdAsync(int id);

        Task<bool> UpdateBudgetAsync(Budget budget);

        Task<bool> DeleteBudgetAsync(int id);
    }
}
