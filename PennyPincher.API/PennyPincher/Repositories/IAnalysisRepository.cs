using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public interface IAnalysisRepository
    {
        Task<int?> CreateAnalysisAsync(AnalysisForCreationDto analysis);

        Task<Analysis> GetAnalysisByIdAsync(int id);

        Task<IEnumerable<Analysis>> GetAllAnalysesAsync();

        Task<IEnumerable<Analysis>> GetAllAnalysesByTypeAsync(AnalysisTypes analysis);

        Task<bool> UpdateAnalysisAsync(Analysis analysis);

        Task<bool> DeleteAnalysisAsync(int id); 

        Task<AnalysisStatusDto> GetAnalysisStatusByGroupId(int id);
    }
}
