using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public interface ICashflowGroupRepository
    {
        Task<int?> CreateCashflowGroupAsync(CashflowGroupDto cashflowGroup);

        Task<CashflowGroup?> GetCashflowGroupByIdAsync(int id);
        
        Task<IEnumerable<CashflowGroup>> GetAllCashflowGroupsAsync();

        Task<bool> UpdateCashflowGroupAsync(CashflowGroup group);

        Task<bool> DeleteCashflowGroupAsync(int id);
    }    
}
