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

        public async Task<bool> checkFinanceTypeGroupExists(int id, bool isBudget)
        {
            string financeType;
            if (isBudget)
            { financeType = "budget"; }
            else
            { financeType = "actual"; }
            try
            {
                string sql = $"SELECT * FROM {financeType}_group WHERE {financeType}_group_id = @id";
                var foundBudget = await _dbService.GetAsync<Budget>(sql, new { id });

                if (foundBudget == null) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting specific budget from budget_group: {ex.Message}");
                throw;
            }
        }


        public async Task<AnalysisStatusDto?> GetAnalysisStatusByGroupId(int id, bool isBudget)
        {

            string financeType;
            if (isBudget)
            { financeType = "budget"; }
            else 
            { financeType = "actual"; }


            try
            {

                // limit 1000 rows to prevent bottle necking
                string sql_grossIncome = $"SELECT SUM(amount) FROM {financeType}_junction bj " +
                        $"JOIN {financeType}_cashflow_entry bce " +
                        $"ON bce.{financeType}_cashflow_entry_id = bj.{financeType}_cashflow_entry_id " +
                        $"WHERE {financeType}_group_id=@id " +
                        "AND cashflow_entry_type = 'Income' " +
                        "LIMIT 1000"
                        ;

                string sql_grossLiabilities = $"SELECT SUM(amount) FROM {financeType}_junction bj " +
                            $"JOIN {financeType}_cashflow_entry bce " +
                            $"ON bce.{financeType}_cashflow_entry_id = bj.{financeType}_cashflow_entry_id " +
                            $"WHERE {financeType}_group_id=@id " +
                            "AND cashflow_entry_type = 'Expense' " +
                            "LIMIT 1000"
                            ;

                string sql_mostCostly = $"SELECT {financeType}_name, amount FROM {financeType}_junction bj " +
                            $"JOIN {financeType}_cashflow_entry bce " +
                            $"ON bce.{financeType}_cashflow_entry_id = bj.{financeType}_cashflow_entry_id " +
                            $"WHERE {financeType}_group_id=@id " +
                            $"AND cashflow_entry_type = 'Expense' " +
                            $"ORDER BY amount DESC " +
                            $"LIMIT 1"
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
    }
}
