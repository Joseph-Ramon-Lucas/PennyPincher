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
        public ActionResult<CashFlowDto> CreateFlow(CashFlowDto cashFlow) {
            CashFlowDataStore.CurrentCashFlow.CashFlowsList.Add(cashFlow);
            return Ok(cashFlow);
        }

        [HttpGet]
        public ActionResult<List<CashFlowDto>> GetAllFlows() {
            int currentFlowCount = CashFlowDataStore.CurrentCashFlow.CashFlowsList.Count;
            if (currentFlowCount == 0)
            {
                return Ok("No Cash Flow added yet");
            }
            else
            {
                return Ok(CashFlowDataStore.CurrentCashFlow.CashFlowsList);
            }
        }

        [HttpGet("{cashFlowID}", Name = "GetFlow")]
        public ActionResult<CashFlowDto> GetFlow(int cashFlowID)
        {
            int cashFlowCount = CashFlowDataStore.CurrentCashFlow.CashFlowsList.Count();
            if (cashFlowCount == 0)
            {
                return NotFound("no items in CashFlow");
            }
            for (int i = 1; i<=cashFlowCount; i++)
            {
                if (i == cashFlowID)
                {
                    return Ok(CashFlowDataStore.CurrentCashFlow.CashFlowsList[i-1]);
                }
            }
            //item not found
            return NotFound("Item not found :(");
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
