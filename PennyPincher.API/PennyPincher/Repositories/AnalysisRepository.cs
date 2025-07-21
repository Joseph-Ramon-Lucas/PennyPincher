using Dapper;
using Npgsql;
using PennyPincher.Models;
using PennyPincher.Models.DtoModels;
using System;
using System.Text.RegularExpressions;

namespace PennyPincher.Repositories
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly IDbService _dbService;

        public AnalysisRepository(IDbService dbService)
        {
            _dbService = dbService;
        }


        public async Task<AnalysisStatusDto?> GetUserAnalysisStatusByGroupId(int groupId, int userId)
        {
            //groupid should eventually be an array of ids to select multiple groups at once

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
                    SELECT cashflow_entry_name as Name, COALESCE(ce.amount, 0) as Amount
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

                double netIncomeRatio = Math.Round((liabilities / netIncome) * 100, 2, MidpointRounding.ToEven);

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

        public async Task<AnalysisStatusDto?> GetAllUserAnalysisStatuses(int userId)
        {
            try
            {
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
                    WHERE mp.user_id = @userId
                    LIMIT 1000;
                    ";

                string sql_mostCostly = @"
                    SELECT cashflow_entry_name as Name, COALESCE(ce.amount, 0) as Amount
                    FROM public.management_profile AS mp
                    JOIN public.cashflow_entry AS ce
                        ON mp.cashflow_entry_id = ce.cashflow_entry_id
                    JOIN public.cashflow_group AS cg
                        ON mp.cashflow_group_id = cg.cashflow_group_id
                    JOIN public.user_account AS ua
                        ON mp.user_id = ua.user_id
                    WHERE mp.user_id = @userId
                      AND ce.cashflow_entry_type = 'Expense'
                    ORDER BY amount DESC
                    LIMIT 1;
                    ";

                AnalysisAggregateCashflowsDto aggregateCashflows = await _dbService.GetAsync<AnalysisAggregateCashflowsDto>(sql_aggregate_cashflows, new { userId });

                double grossIncome = aggregateCashflows.Income;

                double liabilities = aggregateCashflows.Expense;

                double netIncome = Math.Round(grossIncome - liabilities, 2, MidpointRounding.ToEven);

                double netIncomeRatio = Math.Round((liabilities / netIncome) * 100, 2, MidpointRounding.ToEven);

                AnalysisStatusMostCostlyDto mostCostlyItem = await _dbService.GetAsync<AnalysisStatusMostCostlyDto>(sql_mostCostly, new { userId });
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

        public async Task<AnalysisComparisonDto?> GetUserAnalysisComparison(int userId, int groupId1, int groupId2)
        {
            try
            {
                string sql_mostCostly = @"
                    SELECT cashflow_entry_name as Name, COALESCE(ce.amount, 0) as Amount
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
                    // can we do a where groupid = @groupid OR groupID2? and get both?


                return new AnalysisComparisonDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Gathering specific analysis data from the database: {ex.Message}");
                throw;
            }
        }
    }
}
