using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public interface IAnalysisRepository
    {
        Task<bool> checkGroupExists(int groupId);
        Task<bool> checkUserExists(int userId);
        Task<AnalysisStatusDto?> GetUserAnalysisStatusByGroupId(int groupId, int userId);
        Task<AnalysisStatusDto?> GetAllAnalysisStatusesByUserId(int userId);
    }
}
