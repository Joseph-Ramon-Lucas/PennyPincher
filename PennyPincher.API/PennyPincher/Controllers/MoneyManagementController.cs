using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;
using System.Text.Json;

namespace PennyPincher.Controllers
{
    [ApiController]
    [Route("/api/management")]


    public class MoneyManagementController : Controller
    {
        List<CashFlowDto> CFList = CashFlowDataStore.CurrentCashFlow.CashFlowsList;

        [HttpPost]
        public ActionResult<CashFlowDto> CreateFlow(CashFlowDto cashFlow) {
            CashFlowDataStore.CurrentCashFlow.CashFlowsList.Add(cashFlow);
            return Ok(cashFlow);
        }

        [HttpGet]
        public ActionResult<List<CashFlowDto>> GetAllFlows(FlowTypes? targetFlowType) 
        {

            int cashFlowCount = CFList.Count;
            List<CashFlowDto> CFFiltered= new List<CashFlowDto>();
            if (cashFlowCount == 0)
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



        [HttpGet("status")]
        public ActionResult<string> status()
        {

            double incomes = CFList.FindAll(e => e.Flow.Equals(FlowTypes.income)).Sum(e => e.Amount);
            double liabilities= CFList.FindAll(e => e.Flow.Equals(FlowTypes.expense)).Sum(e => e.Amount);
            double netIncome = incomes - liabilities;


            string statusUpdate = string.Empty;
            if (incomes > liabilities) {

                statusUpdate = $"You have {incomes} total income and {liabilities} total liabilities.\n" +
                                $"You're currently taking home {netIncome}";
            }
            else
            {
                statusUpdate = $"Uh oh, you're running out of money.\n" +
                               $"Currently, you have {incomes} total income \n" +
                               $"and {liabilities} total liabilities";
            }
            return Ok(statusUpdate);
        }

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
