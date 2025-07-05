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


        public async Task<AnalysisStatusDto?> GetAnalysisStatusByGroupId(int groupId, int userId)
        {

            try
            {
                // limit 1000 rows to prevent bottle necking

                string sql_grossIncome = @"
                    SELECT SUM(ce.amount)
                    FROM public.management_profile AS mp
                    JOIN public.cashflow_entry AS ce
                        ON mp.cashflow_entry_id = ce.cashflow_entry_id
                    JOIN public.cashflow_group AS cg
                        ON mp.cashflow_group_id = cg.cashflow_group_id
                    JOIN public.user_account AS ua
                        ON mp.user_id = ua.user_id
                    WHERE mp.cashflow_group_id = @groupId
                      AND mp.user_id = @userId
                      AND ce.cashflow_entry_type = 'Income'
                    LIMIT 1000;
                    ";

                //string sql_ = 
                    
                    
                    
                //    $"SELECT SUM(amount) FROM {financeType}_junction bj " +
                //        $"JOIN {financeType}_cashflow_entry bce " +
                //        $"ON bce.{financeType}_cashflow_entry_id = bj.{financeType}_cashflow_entry_id " +
                //        $"WHERE {financeType}_group_id=@groupId " +
                //        "AND cashflow_entry_type = 'Income' " +
                //        "LIMIT 1000"
                //        ;

                //string sql_grossLiabilities = $"SELECT SUM(amount) FROM {financeType}_junction bj " +
                //            $"JOIN {financeType}_cashflow_entry bce " +
                //            $"ON bce.{financeType}_cashflow_entry_id = bj.{financeType}_cashflow_entry_id " +
                //            $"WHERE {financeType}_group_id=@groupId " +
                //            "AND cashflow_entry_type = 'Expense' " +
                //            "LIMIT 1000"
                //            ;

                //string sql_mostCostly = $"SELECT {financeType}_name, amount FROM {financeType}_junction bj " +
                //            $"JOIN {financeType}_cashflow_entry bce " +
                //            $"ON bce.{financeType}_cashflow_entry_id = bj.{financeType}_cashflow_entry_id " +
                //            $"WHERE {financeType}_group_id=@groupId " +
                //            $"AND cashflow_entry_type = 'Expense' " +
                //            $"ORDER BY amount DESC " +
                //            $"LIMIT 1"
                //            ;

                double grossIncome = await _dbService.GetAsync<double>(sql_grossIncome, new { groupId, userId });

                //double liabilities = await _dbService.GetAsync<double>(sql_grossLiabilities, new { groupId });

                //double netIncome = Math.Round(grossIncome - liabilities, 2, MidpointRounding.ToEven);

                //double netIncomeRatio = Math.Round(netIncome / liabilities, 2, MidpointRounding.ToEven) * 100;

                //AnalysisStatusMostCostlyDto mostCostlyItem = await _dbService.GetAsync<AnalysisStatusMostCostlyDto>(sql_mostCostly, new { groupId });

                //double percentOfEarningsGoingToMostCostlyAmount = Math.Round((mostCostlyItem.Amount / grossIncome), 4, MidpointRounding.ToEven) * 100;


                return new AnalysisStatusDto()
                {
                    GrossIncome = grossIncome,
                    //NetIncome = netIncome,
                    //Liabilities = liabilities,
                    //NetIncomeRatio = netIncomeRatio,
                    //MostCostlyName = mostCostlyItem.Budget_Name,
                    //MostCostlyAmount = mostCostlyItem.Amount,
                    //PercentOfEarningsGoingToMostCostlyAmount = percentOfEarningsGoingToMostCostlyAmount
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
