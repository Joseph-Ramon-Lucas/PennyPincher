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
        public ActionResult<List<CashFlowDto>> GetAllFlows(FlowTypes? targetFlowType) 
        {
            List<CashFlowDto> CFList = CashFlowDataStore.CurrentCashFlow.CashFlowsList;
            int currentFlowCount = CFList.Count;
            List<CashFlowDto> CFFiltered= new List<CashFlowDto>();
            if (currentFlowCount == 0)
            {
                return Ok("No Cash Flow added yet");

            }
            if (targetFlowType != null)
            {
                CFList.ForEach(c =>
                {
                    if (c.Flow.Equals(targetFlowType))
                    {
                        CFFiltered.Add(c);
                    } 
                } );

                if (CFFiltered.Count <= 0)
                {
                    return NotFound("No items were found for this CashFlow type");
                }
                return Ok(CFFiltered);
            }
            else
            {
                return Ok(CashFlowDataStore.CurrentCashFlow.CashFlowsList);
            }
        }

        [HttpGet("{targetCashFlowID}", Name = "GetFlow")]
        public ActionResult<CashFlowDto> GetFlow(int targetCashFlowID)
        {
            List<CashFlowDto> CFList = CashFlowDataStore.CurrentCashFlow.CashFlowsList;
            int cashFlowCount = CFList.Count();
            if (cashFlowCount == 0)
            {
                return NotFound("no items in CashFlow");
            }
            int pos = CFList.FindIndex(f => f.Id == targetCashFlowID);

            if (pos >= 0)
            {
                return Ok(CFList[pos]);
            }
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
