using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public interface IBudgetRepository
    {
        Task<int?> CreateBudgetAsync(BudgetForCreationDto budget);

        Task<Budget> GetBudgetByIdAsync(int id);

        Task<IEnumerable<Budget>> GetAllBudgetsAsync();

        Task<IEnumerable<Budget>> GetAllBudgetsByTypeAsync(BudgetTypes type);

        Task<bool> UpdateBudgetAsync(Budget budget);

        Task<bool> DeleteBudgetAsync(int id);
    }
}
