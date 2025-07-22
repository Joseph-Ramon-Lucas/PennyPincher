using Dapper;
using Npgsql;
using PennyPincher.Models;
using PennyPincher.Models.DtoModels;
using System;
using System.Text.RegularExpressions;
using static PennyPincher.Models.TypeCollections;

namespace PennyPincher.Repositories
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly IDbService _dbService;

        public AnalysisRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        // Need to find a better spot to house these utilities... maybe a utility class?
        public CategoryTypes ConvertStringToCategoryType(string categoryTypeAsString)
        {
            switch (categoryTypeAsString)
            {
                case "None": return CategoryTypes.None;
                case "Living": return CategoryTypes.Living;
                case "Utilities": return CategoryTypes.Utilities;
                case "Entertainment": return CategoryTypes.Entertainment;
                case "Shopping": return CategoryTypes.Shopping;
                case "Takeout": return CategoryTypes.Takeout;
                case "Housing": return CategoryTypes.Housing;
                case "Transportation": return CategoryTypes.Transportation;
                case "Food": return CategoryTypes.Food;
                case "Health": return CategoryTypes.Health;
                case "Income": return CategoryTypes.Income;

                default: return CategoryTypes.None;
            };
        }

        public string ConvertCategoryTypesToString(CategoryTypes categoryType)
        {
            switch (categoryType)
            {
                case CategoryTypes.None: return "None";
                case CategoryTypes.Living: return "Living";
                case CategoryTypes.Utilities: return "Utilities";
                case CategoryTypes.Entertainment: return "Entertainment";
                case CategoryTypes.Shopping: return "Shopping";
                case CategoryTypes.Takeout: return "Takeout";
                case CategoryTypes.Housing: return "Housing";
                case CategoryTypes.Transportation: return "Transportation";
                case CategoryTypes.Food: return "Food";
                case CategoryTypes.Health: return "Health";
                case CategoryTypes.Income: return "Income";
                
                default: return "None";
            }
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
                string sql_mostCostly1 = @"
                    SELECT 
                        ce.cashflow_entry_id as Id,
                        ce.cashflow_entry_name as Name,
                        ce.Description,
                        ce.entry_date as EntryDate,
                        COALESCE(ce.amount, 0) as Amount,
                        ce.cashflow_entry_type as Flow,
                        ce.category_type as CategoryTypeAsString
                    FROM public.management_profile AS mp
                    JOIN public.cashflow_entry AS ce
                        ON mp.cashflow_entry_id = ce.cashflow_entry_id
                    JOIN public.cashflow_group AS cg
                        ON mp.cashflow_group_id = cg.cashflow_group_id
                    JOIN public.user_account AS ua
                        ON mp.user_id = ua.user_id
                    WHERE 
                        mp.cashflow_group_id = @groupId1
                    AND mp.user_id = @userId
                    AND ce.cashflow_entry_type = 'Expense'
                    ORDER BY amount DESC
                    LIMIT 5;";

                string sql_mostCostly2 = @"
                    SELECT 
                        ce.cashflow_entry_id as Id,
                        ce.cashflow_entry_name as Name,
                        ce.Description,
                        ce.entry_date as EntryDate,
                        COALESCE(ce.amount, 0) as Amount,
                        ce.cashflow_entry_type as Flow,
                        ce.category_type as CategoryTypeAsString
                    FROM public.management_profile AS mp
                    JOIN public.cashflow_entry AS ce
                        ON mp.cashflow_entry_id = ce.cashflow_entry_id
                    JOIN public.cashflow_group AS cg
                        ON mp.cashflow_group_id = cg.cashflow_group_id
                    JOIN public.user_account AS ua
                        ON mp.user_id = ua.user_id
                    WHERE 
                        mp.cashflow_group_id = @groupId2
                    AND mp.user_id = @userId
                    AND ce.cashflow_entry_type = 'Expense'
                    ORDER BY amount DESC
                    LIMIT 5;";

                string sql_groupSum1 = @"
                    SELECT 
                        COALESCE(SUM(CASE WHEN ce.cashflow_entry_type = 'Expense' THEN ce.amount END), 0) AS Amount

                    FROM public.management_profile AS mp
                    JOIN public.cashflow_entry AS ce
                        ON mp.cashflow_entry_id = ce.cashflow_entry_id
                    JOIN public.cashflow_group AS cg
                        ON mp.cashflow_group_id = cg.cashflow_group_id
                    JOIN public.user_account AS ua
                        ON mp.user_id = ua.user_id
                    GROUP BY 
                        mp.cashflow_group_id = @groupId1
                    AND mp.user_id = @userId
                    AND ce.cashflow_entry_type = 'Expense'
                    ORDER BY amount DESC
                    LIMIT 5;
                    ";

                string sql_groupSum2 = @"
                    SELECT 
                        COALESCE(SUM(CASE WHEN ce.cashflow_entry_type = 'Expense' THEN ce.amount END), 0) AS Amount

                    FROM public.management_profile AS mp
                    JOIN public.cashflow_entry AS ce
                        ON mp.cashflow_entry_id = ce.cashflow_entry_id
                    JOIN public.cashflow_group AS cg
                        ON mp.cashflow_group_id = cg.cashflow_group_id
                    JOIN public.user_account AS ua
                        ON mp.user_id = ua.user_id
                    GROUP BY 
                        mp.cashflow_group_id = @groupId2
                    AND mp.user_id = @userId
                    AND ce.cashflow_entry_type = 'Expense'
                    ORDER BY amount DESC
                    LIMIT 5;
                    ";

                    

                List<CashflowEntryDto> mostCostlyGroupList1 =
                    await _dbService.GetAllAsync<CashflowEntryDto>(sql_mostCostly1, new { userId, groupId1 });
                List<CashflowEntryDto> mostCostlyGroupList2 =
                    await _dbService.GetAllAsync<CashflowEntryDto>(sql_mostCostly2, new { userId, groupId2 });


                double mostCostlyAmount1 = mostCostlyGroupList1
                                            .DefaultIfEmpty(new CashflowEntryDto() { Amount = 0 })
                                            .First().Amount;
                double mostCostlyAmount2 = mostCostlyGroupList2
                                            .DefaultIfEmpty(new CashflowEntryDto() { Amount = 0 })
                                            .First().Amount;

                string mostCostlyCategoryAsString1 = mostCostlyGroupList1
                                            .DefaultIfEmpty(new CashflowEntryDto() { CategoryTypeAsString = "None" })
                                            .First().CategoryTypeAsString;
                string mostCostlyCategoryAsString2 = mostCostlyGroupList2
                                            .DefaultIfEmpty(new CashflowEntryDto() { CategoryTypeAsString = "None" })
                                            .First().CategoryTypeAsString;

                CategoryTypes mostCostlyCategoryForDisplay = 
                   (mostCostlyAmount1 > mostCostlyAmount2) ? 
                        ConvertStringToCategoryType(mostCostlyCategoryAsString1) : 
                        ConvertStringToCategoryType(mostCostlyCategoryAsString2);

                double mostCostlyAmountBetweenGroupsRatio = Math.Round((mostCostlyAmount1 / mostCostlyAmount2), 4) * 100;
                
                double groupSum1 = await _dbService.GetAsync<double>(sql_groupSum1, new { userId, groupId1 });
                double groupSum2 = await _dbService.GetAsync<double>(sql_groupSum2, new { userId, groupId2 });



                return new AnalysisComparisonDto()
                {
                    CurrentTopExpenses = mostCostlyGroupList1,
                    ProjectedTopExpenses = mostCostlyGroupList2,

                    CurrentCategorySum = groupSum1,
                    ProjectedCategorySum = groupSum2,

                    CurrentMostCostlyCategoryAmount = mostCostlyAmount1,
                    ProjectedMostCostlyCategoryAmount = mostCostlyAmount2,

                    CurrentMostCostlyCategory = ConvertStringToCategoryType(mostCostlyCategoryAsString1),
                    ProjectedMostCostlyCategory = ConvertStringToCategoryType(mostCostlyCategoryAsString2),

                    ProjectedMostCostlyCurrentCategoryDisplay = mostCostlyCategoryForDisplay,

                    CostlyCategoryRatio = mostCostlyAmountBetweenGroupsRatio

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
