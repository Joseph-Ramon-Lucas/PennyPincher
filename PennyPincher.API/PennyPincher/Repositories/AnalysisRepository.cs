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

        public async Task<bool> checkGroupExists(int groupId)
        {
            try
            {
                string sql = @"SELECT * FROM public.cashflow_group 
                                WHERE cashflow_group_id = @groupId 
                                LIMIT 1000";
                var foundBudget = await _dbService.GetAsync<int>(sql, new { groupId });

                return foundBudget != 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting specific budget from budget_group: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> checkUserExists(int userId)
        {
            try
            {
                string sql = @"SELECT * FROM public.user_account 
                                WHERE user_id = @userId
                                LIMIT 1000;";

                var foundUser = await _dbService.GetAsync<int>(sql, new { userId });

                return foundUser != 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting specific budget from budget_group: {ex.Message}");
                throw;
            }
        }


        public async Task<AnalysisStatusDto?> GetUserAnalysisStatusByGroupId(int groupId, int userId)
        {

            try
            {
                // limit 1000 rows to prevent bottle necking
                // coalesce to avoid nulls when using sum
                string sql_aggregate_cashflows = @"
                    SELECT 
                        COALESCE(SUM(CASE WHEN ce.cashflow_entry_type = 'Income' THEN ce.amount END), 0) AS income,
                        COALESCE(SUM(CASE WHEN ce.cashflow_entry_type = 'Expense' THEN ce.amount END), 0) AS expense
                    FROM public.management_profile AS mp
                    JOIN public.cashflow_entry AS ce
                        ON mp.cashflow_entry_id = ce.cashflow_entry_id
                    JOIN public.cashflow_group AS cg
                        ON mp.cashflow_group_id = cg.cashflow_group_id
                    JOIN public.user_account AS ua
                        ON mp.user_id = ua.user_id
                    WHERE mp.cashflow_group_id = @groupId
                        AND mp.user_id = @userId
                    LIMIT 1000;
                    ";

                string sql_mostCostly = @"
                    SELECT cashflow_entry_name as Name, COALESCE(amount, 0)
                    FROM public.management_profile AS mp
                    JOIN public.cashflow_entry AS ce
                        ON mp.cashflow_entry_id = ce.cashflow_entry_id
                    JOIN public.cashflow_group AS cg
                        ON mp.cashflow_group_id = cg.cashflow_group_id
                    JOIN public.user_account AS ua
                        ON mp.user_id = ua.user_id
                    WHERE mp.cashflow_group_id = @groupId
                      AND mp.user_id = @userId
                      AND ce.cashflow_entry_type = 'Expense'
                    ORDER BY amount DESC
                    LIMIT 1;
                    ";

                AnalysisAggregateCashflowsDto aggregateCashflows = await _dbService.GetAsync<AnalysisAggregateCashflowsDto>(sql_aggregate_cashflows, new { groupId, userId });

                double grossIncome = aggregateCashflows.Income;

                double liabilities = aggregateCashflows.Expense;

                double netIncome = Math.Round(grossIncome - liabilities, 2, MidpointRounding.ToEven);

                double netIncomeRatio = Math.Round(netIncome / liabilities, 2, MidpointRounding.ToEven) * 100;

                AnalysisStatusMostCostlyDto mostCostlyItem = await _dbService.GetAsync<AnalysisStatusMostCostlyDto>(sql_mostCostly, new { groupId, userId });
                if (mostCostlyItem == null) { mostCostlyItem = new AnalysisStatusMostCostlyDto(); }

                double percentOfEarningsGoingToMostCostlyAmount = Math.Round((mostCostlyItem.Amount / grossIncome), 4, MidpointRounding.ToEven) * 100;


                return new AnalysisStatusDto()
                {
                    GrossIncome = grossIncome,
                    NetIncome = netIncome,
                    Liabilities = liabilities,
                    NetIncomeRatio = netIncomeRatio,
                    MostCostlyName = mostCostlyItem.Name,
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

        public async Task<AnalysisStatusDto?> GetAllAnalysisStatusesByUserId(int groupId, int userId)
        {
            try
            {

                return new AnalysisStatusDto();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Gathering specific analysis data from the database: {ex.Message}");
                throw;
            }
        }
    }
}
