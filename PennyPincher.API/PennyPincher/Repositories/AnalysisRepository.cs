using Dapper;
using Npgsql;
using PennyPincher.Models;
using PennyPincher.Models.DtoModels;

namespace PennyPincher.Repositories
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly IDbService _dbService;

        public AnalysisRepository(IDbService dbService)
        {
            _dbService = dbService;
        }


        public Task<IEnumerable<Analysis>> GetAllAnalysesByTypeAsync(AnalysisTypes analysis)
        {
            throw new NotImplementedException();
        }

        public async Task<AnalysisStatusDto> GetAnalysisStatusByGroupId(int id)
        {
            try
            {
                string sql = "SELECT amount FROM budget_junction bj " +
                            "JOIN budget_cashflow_entry bce " +
                            "ON bce.budget_cashflow_entry_id = bj.budget_cashflow_entry_id" +
                            "WHERE budget_group_id = @id";

                var allBudgets = await _dbService.GetAllAsync<Budget>(sql, id);
                return new AnalysisStatusDto();



            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
