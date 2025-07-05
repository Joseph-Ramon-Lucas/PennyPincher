using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public interface IAnalysisRepository
    {
        Task<bool> checkGroupExists(int groupId);
        Task<bool> checkUserExists(int userId);
        Task<AnalysisStatusDto?> GetAnalysisStatusByGroupId(int groupId, int userId);
    }
}
