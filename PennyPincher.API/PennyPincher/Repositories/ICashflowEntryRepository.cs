using PennyPincher.Models;
using PennyPincher.Models.DtoModels;
using static PennyPincher.Models.TypeCollections;

namespace PennyPincher.Repositories
{
    public interface ICashflowEntryRepository
    {
        Task<int?> CreateCashflowEntryAsync(CashflowEntryForCreationDto analysis);

        Task<CashflowEntry> GetCashflowEntryByIdAsync(int id);
        
        Task<IEnumerable<CashflowEntry>> GetAllCashflowEntriesAsync();

        Task<IEnumerable<CashflowEntry>> GetAllCashflowEntriesByFlowTypeAsync(CashflowType type);

        Task<bool> UpdateCashflowEntryAsync(CashflowEntry cashflowEntry);

        Task<bool> DeleteCashflowEntryAsync(int id);
    }
}
