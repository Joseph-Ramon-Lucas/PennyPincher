using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public interface ICashflowEntryRepository
    {
        Task<int?> CreateCashflowEntrysync(AnalysisForCreationDto analysis);

        Task<Analysis> GetCashflowEntryByIdAsync(int id);

        Task<IEnumerable<Analysis>> GetAllCashflowEntriesAsync();

        Task<IEnumerable<Analysis>> GetAllCashflowsByTypeAsync(AnalysisTypes type);

        Task<bool> UpdateCashflowEntryAsync(Analysis analysis);

        Task<bool> DeleteCashflowEntryAsync(int id);
    }
}
