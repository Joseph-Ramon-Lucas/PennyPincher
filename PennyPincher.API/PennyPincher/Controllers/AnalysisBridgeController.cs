using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;
using System.Xml.Linq;

namespace PennyPincher.Controllers
{
    [ApiController]
    [Route("/api/analysis")]

    public class AnalysisBridgeController : Controller
    {
        List<CashFlowDto> CurrItemLogsCF = CashFlowDataStore.CurrentItemLogCashFlow.CashFlowsList;
        List<CashFlowDto> ProjectedCF = CashFlowDataStore.ProjectedCashFlow.CashFlowsList;

        public enum CFDataStores
        {
            Current = 0,
            Projected =1
        }
        private List<CashFlowDto> CFDataStorePicker(CFDataStores TargetDataStore)
        {
            List<CashFlowDto> CFData = new List<CashFlowDto>();
            switch (TargetDataStore) // Switch case to easily analyze each datastore the same way & scalable for additional datastores
            {
                case CFDataStores.Current:
                    CFData = CashFlowDataStore.CurrentItemLogCashFlow.CashFlowsList;
                    break;
                case CFDataStores.Projected:
                    CFData = CashFlowDataStore.ProjectedCashFlow.CashFlowsList;
                    break;
                default:
                    CFData = CashFlowDataStore.CurrentItemLogCashFlow.CashFlowsList;
                    break;
            }
            return CFData;
        }
        private List<CashFlowDto> ConvertLogToCF(List<ExpenseDto> loggedItems)
        {
            List<CashFlowDto> CFLogs = new List<CashFlowDto>();
            List<CategoryTypes> Expenses = GetExpenseCategoryTypes();
            CFLogs.Clear();

            if (loggedItems.Count == 0)
            {
                return CFLogs;
            }
            foreach (var item in loggedItems)
            {
                CashFlowDto newCF = new CashFlowDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Amount = item.Price,
                    Description = string.Empty,
                    Flow = FlowTypes.income,
                    Category = item.Category,
                };

                if (Expenses.Contains(item.Category))
                {
                    newCF.Flow = FlowTypes.expense;
                }
                CFLogs.Add(newCF);
            }
            return CFLogs;
        }

        private List<CategoryTypes> GetExpenseCategoryTypes()
        {
            //defined which logitem categories are considered a flowtype expense
            //hard coded list of categories for now.
            //can possibly make separate API later for custom expense categories
            List<CategoryTypes> ExpenseCategories = new List<CategoryTypes>() {
                CategoryTypes.Living,
                CategoryTypes.Utilities,
                CategoryTypes.Entertainment,
                CategoryTypes.Shopping,
                CategoryTypes.Takeout
            };
            return ExpenseCategories;
        }


        [HttpPost]
        public ActionResult<List<CashFlowDto>> UpdateCashFlowItemLogStore()
        {

            List<ExpenseDto> loggedItems = ExpenseDataStore.Current.Expenses;
            List<CashFlowDto> ConvertedItemLogs = ConvertLogToCF(loggedItems);

            CurrItemLogsCF.Clear();
            ConvertedItemLogs.ForEach(c =>
            {
                //store converted logs for viewing without overwriting current CashFlows
                CurrItemLogsCF.Add(c);
            });
            return Created();
        }

        [HttpGet]
        public ActionResult<List<CashFlowDto>> GetCashFlowItemLogStore()
        {
            return Ok(CurrItemLogsCF);
        }

        // Purpose: provide overall summary of financial health per chosen CashFlow
        [HttpGet("getstatus/{DataStore}")]
        public ActionResult<string> Status(CFDataStores DataStore)
        {
            List<CashFlowDto> CFData = CFDataStorePicker(DataStore);

            double incomes = CFData.FindAll(e => e.Flow.Equals(FlowTypes.income)).Sum(e => e.Amount);
            double liabilities = CFData.FindAll(e => e.Flow.Equals(FlowTypes.expense)).Sum(e => e.Amount);
            double netIncome = incomes - liabilities;
            double netIncomeRatio = Math.Round((liabilities / incomes), 4) * 100;
            string mostCostlyName = CFData
                .Where(e => e.Flow.Equals(FlowTypes.expense))
                .OrderByDescending(e => e.Amount)
                .Select(e => e.Name)
                .FirstOrDefault() ?? "nothing at the moment";

            double mostCostlyAmount = CFData
                .Where(e => e.Flow.Equals(FlowTypes.expense))
                .OrderByDescending(e => e.Amount)
                .Select(e => e.Amount)
                .FirstOrDefault<double>();



            string statusUpdate = string.Empty;
            if (incomes > liabilities)
            {

                statusUpdate = $"You have {incomes} total income and {liabilities} total liabilities.\n" +
                                $"You're currently taking home {netIncome}";
            }
            else
            {
                statusUpdate = $"Uh oh, you're running out of money.\n" +
                               $"Currently, you have {incomes} total income \n" +
                               $"and {liabilities} total liabilities";
            }
            string ratioText = ($"\nYou're currently using {netIncomeRatio}% of your earnings." +
                                $"{Math.Round((mostCostlyAmount / incomes), 4) * 100}% of your earnings is going to {mostCostlyName}");

            statusUpdate = statusUpdate + ratioText;
            return Ok(statusUpdate);
        }

        [HttpGet("/getstatus/compare/{DataStore}")]
        //Purpose: to compare if the financial health & statuses of Current Cashflow is meeting that of a Projected Cashflow
        public ActionResult<string> CompareCashFlows(CFDataStores DataStore)
        {
            List<CashFlowDto> CFData = CFDataStorePicker(DataStore);

            if (CFData == CurrItemLogsCF)
            {
                return NotFound("These CashFlows are Identical. They're both the Current Item Logs");
            }
            if (CFData.Count == 0 | CurrItemLogsCF.Count == 0)
            {
                return NotFound("1 or both of these CashFlows are empty");
            }
            var CurrTopCostly = CurrItemLogsCF
                .Where(e => e.Flow == FlowTypes.expense)
                .OrderByDescending(e => e.Amount)
                .Take(5);

            var ProjTopCostly = CFData
                .Where(e => e.Flow == FlowTypes.expense)
                .OrderByDescending(e => e.Amount)
                .Take(5);

            var CurrCategoriesSum = CurrItemLogsCF
                .Where(e => e.Flow == FlowTypes.expense)
                .GroupBy(e => e.Category)
                .Select(g => new CashFlowDto()
                {
                    Amount = g.Sum(e => e.Amount),
                    Category = g.Key,
                })
                .OrderByDescending(e => e.Amount);

            var ProjCategoriesSum = CFData
                .Where(e =>  e.Flow == FlowTypes.expense)
                .GroupBy (e => e.Category)
                .Select(g => new CashFlowDto()
                {
                    Amount = g.Sum(e  => e.Amount),
                    Category = g.Key,
                })
                .OrderByDescending (e => e.Amount);

            double CurrMostCostlyCategoryPrice = CurrCategoriesSum.Max(e => e.Amount);
            CategoryTypes CurrMostCostlyCategory = CurrCategoriesSum.OrderByDescending(e => e.Amount)
                                                                .Select(e => e.Category)
                                                                .FirstOrDefault();

            CategoryTypes ProjMostCostlyCurrCategory = ProjCategoriesSum.Where(e => e.Category == CurrMostCostlyCategory)
                                                                .Select (e => e.Category)
                                                                .FirstOrDefault();

            CategoryTypes ProjMostCostlyCurrCategoryDisplay = (ProjMostCostlyCurrCategory == CategoryTypes.None ? CurrMostCostlyCategory : ProjMostCostlyCurrCategory);
            double ProjMostCostlyCurrCategoryPrice = ProjMostCostlyCurrCategory == CategoryTypes.None ? 0 :
                                                                    ProjCategoriesSum.Where(e => e.Category == CurrMostCostlyCategory)
                                                                    .Select(e => e.Amount)
                                                                    .FirstOrDefault();
            double CostlyCategoryRatio = Math.Round((CurrMostCostlyCategoryPrice / ProjMostCostlyCurrCategoryPrice), 4) * 100;


            string compStatment = ($"Here are the top expenses between current spending and projected spending:\n");
            string columns = "Current Expenses\n----------------\n";

            foreach (var c in CurrTopCostly)
            {
                columns += $"{c.Name,-20}\t{c.Amount,-20}\n";
            }
            columns += "\n\nProjected Expenses\n----------------\n";
            foreach (var p in ProjTopCostly)
            {
                columns += $"{p.Name,-20}\t{p.Amount,-20}\n";
            }

            string CurrOtherCategoryStats = string.Empty;
            foreach (var item in CurrCategoriesSum.Skip(1)) // skipping over the most costly item
            {
                CurrOtherCategoryStats += $"{item.Category,-20}\t{item.Amount,-20}\n";
            }

            string CurrCostlyCatAnalysis = $"\n\nYou are currently spending {CurrMostCostlyCategoryPrice} towards this category: {CurrMostCostlyCategory}\n"+
                                   $"How your current spending in other categories rank:\n\n"+
                                   $"{CurrOtherCategoryStats}";

            string ProjCostlyCatAnalysis = $"\n\nYou projected to spend {ProjMostCostlyCurrCategoryPrice} towards this category: {ProjMostCostlyCurrCategoryDisplay}\n"+
                                               $"So far you have spent {CostlyCategoryRatio}% out of your {ProjMostCostlyCurrCategoryDisplay} budget";


            string analysis = compStatment + columns + CurrCostlyCatAnalysis + ProjCostlyCatAnalysis;
            return Ok(analysis);
        }

    }
}
