using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;

namespace PennyPincher.Controllers
{
    [ApiController]
    [Route("/api/analysis")]

    public class AnalysisBridgeController : Controller
    {
        List<CashFlowDto> CFILs = CashFlowDataStore.CashFlowItemLogs.CashFlowsList;

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

            CFILs.Clear();
            ConvertedItemLogs.ForEach(c =>
            {
                //store converted logs for viewing without overwriting current CashFlows
                CFILs.Add(c);
            });
            return Created();
        }

        [HttpGet]
        public ActionResult<List<CashFlowDto>> GetCashFlowItemLogStore()
        {
            return Ok(CFILs);
        }

        //todo:
        //compare to CurrentCF



    }
}
