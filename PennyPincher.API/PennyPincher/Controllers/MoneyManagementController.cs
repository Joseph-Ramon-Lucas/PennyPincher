using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;
using System.Text.Json;

namespace PennyPincher.Controllers
{
    [ApiController]
    [Route("/api/management")]

    
    public class MoneyManagementController : Controller
    {

        [HttpPost]
        public ActionResult<CashFlowDto> CreateItem(CashFlowDto cashFlow) {
            CashFlowDataStore.CurrentCashFlow.CashFlowsList.Add(cashFlow);
            return Ok(cashFlow);
        }

        [HttpGet]
        public ActionResult<List<CashFlowDto>> GetAllItems() {
            int currentFlowCount = CashFlowDataStore.CurrentCashFlow.CashFlowsList.Count;
            if (currentFlowCount == 0)
            {
                return Ok("No Cash Flow added yet");
            }
            else
            {
                List<CashFlowDto> flows = new List<CashFlowDto>();
                for (int i = 0; i<currentFlowCount; i++)
                {
                    flows.Add(CashFlowDataStore.CurrentCashFlow.CashFlowsList[i]);
                }

                return Ok(flows);
            }
        }
        


        //[HttpGet("status")]
        //public ActionResult<string> status()
        //{
        //    double liabilities = expense_food + expense_living + expense_living;
        //    double net = income - liabilities;
        //    if(income <= liabilities)
        //    {
        //        return Ok("Uh oh, you're running out of money.\n " +
        //            "Currently, you have $" + income + " total income \n" +
        //            "and $" + liabilities + " total liabilities");
        //    }
        //    else
        //    {
        //        return Ok("You're currently taking home $" + net);
        //    }
        //}

    }
}
