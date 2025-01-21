using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;

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

        [HttpGet("/api/analysis/expenses",Name="getExpenses")]
        public List<CategoryTypes> GetExpenseCategoryTypes()
        {
            List<CategoryTypes> ExpenseCategories = new List<CategoryTypes>() {
                CategoryTypes.Living,
                CategoryTypes.Utilities,
                CategoryTypes.Entertainment,
                CategoryTypes.Shopping,
                CategoryTypes.Takeout
            };
            return ExpenseCategories;
        }
        //todo:
        //edit list of expenses
        //detele from list of expenses

        [HttpPost("/api/analysis/convert",Name ="convertLogToCF")]
        public List<CashFlowDto> ConvertLogToCF(List<ItemDto> loggedItems)
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
                    Flow = FlowTypes.income
                };

                if (Expenses.Contains(item.Category))
                {
                    newCF.Flow = FlowTypes.expense;
                }
                CFLogs.Add(newCF);
            }
            return CFLogs;
        }

        [HttpPost]
        public ActionResult<List<CashFlowDto>> UpdateCashFlowItemLogStore()
        {

            List<ItemDto> loggedItems = ItemsDataStore.Current.Items;
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
        [HttpGet("status/{dataStore}")]
        public ActionResult<string> status(CFDataStores dataStore)
        {
            List<CashFlowDto> CFData = new List<CashFlowDto>();
            switch (dataStore) // Switch case to easily analyze each datastore the same way & scalable for additional datastores
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
            string ratioText = ($"\nYou're currently using {netIncomeRatio}% of your earnings. {Math.Round((mostCostlyAmount / incomes), 4) * 100}% of your earnings is going to {mostCostlyName}");

            statusUpdate = statusUpdate + ratioText;
            return Ok(statusUpdate);
        }

        //todo:
        //compare to CurrentCF



    }
}
