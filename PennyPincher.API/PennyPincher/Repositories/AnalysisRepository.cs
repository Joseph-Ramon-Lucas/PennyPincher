using Dapper;
using Npgsql;
using PennyPincher.Models;
using PennyPincher.Models.DtoModels;
using System;

namespace PennyPincher.Repositories
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly IDbService _dbService;

        public AnalysisRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        public Task<int?> CreateAnalysisAsync(AnalysisForCreationDto analysis)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAnalysisAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Analysis>> GetAllAnalysesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Analysis>> GetAllAnalysesByTypeAsync(AnalysisTypes analysis)
        {
            throw new NotImplementedException();
        }

        public Task<Analysis> GetAnalysisByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AnalysisStatusDto> GetAnalysisStatusByGroupId(int id)
        {
            try
            {

                // limit 1000 rows to prevent bottle necking
                string sql_grossIncome = "SELECT SUM(amount) FROM budget_junction bj " +
                            "JOIN budget_cashflow_entry bce " +
                            "ON bce.budget_cashflow_entry_id = bj.budget_cashflow_entry_id " +
                            "WHERE budget_group_id=@id " +
                            "AND cashflow_entry_type = 'Income' " +
                            "LIMIT 1000"
                            ;

                string sql_grossLiabilities = "SELECT SUM(amount) FROM budget_junction bj " +
                            "JOIN budget_cashflow_entry bce " +
                            "ON bce.budget_cashflow_entry_id = bj.budget_cashflow_entry_id " +
                            "WHERE budget_group_id=@id " +
                            "AND cashflow_entry_type = 'Expense' " +
                            "LIMIT 1000"
                            ;

                string sql_mostCostly = "SELECT budget_name, amount FROM budget_junction bj " +
                            "JOIN budget_cashflow_entry bce " +
                            "ON bce.budget_cashflow_entry_id = bj.budget_cashflow_entry_id " +
                            "WHERE budget_group_id=@id " +
                            "AND cashflow_entry_type = 'Expense' " +
                            "ORDER BY amount DESC " +
                            "LIMIT 1"
                            ;

                double grossIncome = await _dbService.GetAsync<double>(sql_grossIncome, new { id });
                
                double liabilities = await _dbService.GetAsync<double>(sql_grossLiabilities, new { id });
                
                double netIncome = Math.Round(grossIncome - liabilities, 2, MidpointRounding.ToEven);
                
                double netIncomeRatio = Math.Round(netIncome / liabilities, 2, MidpointRounding.ToEven) * 100;
                
                AnalysisStatusMostCostlyDto mostCostlyItem = await _dbService.GetAsync<AnalysisStatusMostCostlyDto>(sql_mostCostly, new { id });
                
                double percentOfEarningsGoingToMostCostlyAmount = Math.Round((mostCostlyItem.Amount / grossIncome), 4, MidpointRounding.ToEven) * 100;


                return new AnalysisStatusDto()
                {
                    GrossIncome = grossIncome,
                    NetIncome = netIncome,
                    Liabilities = liabilities,
                    NetIncomeRatio = netIncomeRatio,
                    MostCostlyName = mostCostlyItem.Budget_Name,
                    MostCostlyAmount = mostCostlyItem.Amount,
                    PercentOfEarningsGoingToMostCostlyAmount = percentOfEarningsGoingToMostCostlyAmount
                };



            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Gathering specific analysis data from the database: {ex.Message}");
                throw;
            }

        }

        public Task<bool> UpdateAnalysisAsync(Analysis analysis)
        {
            throw new NotImplementedException();
        }
    }
}
