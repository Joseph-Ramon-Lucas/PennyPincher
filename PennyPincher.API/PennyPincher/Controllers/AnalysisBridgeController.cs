using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;

namespace PennyPincher.Controllers
{
    [ApiController]
    [Route("/api/analysis")]

    public class AnalysisBridgeController : Controller
    {
        [HttpGet("/api/analysis/expenses",Name="getExpenses")]
        public List<CategoryTypes> getExpenseCategoryTypes()
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

        [HttpPost(Name ="convertLogToCF")]
        public List<CashFlowDto> convertLogToCF(List<ItemDto> loggedItems)
        {
            List<CashFlowDto> CFLogs = new List<CashFlowDto>();

            List<CategoryTypes> Expenses = getExpenseCategoryTypes();

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

        [HttpGet]
        public ActionResult<List<CashFlowDto>> getLogItems()
        {

            List<ItemDto> loggedItems = ItemsDataStore.Current.Items;
            List<CashFlowDto> CFLogs = convertLogToCF(loggedItems);
            


            return Ok(CFLogs);
        }
    }
}
