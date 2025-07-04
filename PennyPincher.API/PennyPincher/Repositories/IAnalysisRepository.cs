using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public interface IAnalysisRepository
    {
        Task<bool> checkFinanceTypeGroupExists(int groupId, bool isBudget);
        Task<AnalysisStatusDto?> GetAnalysisStatusByGroupId(int id, bool isBudget = true);
    }
}
