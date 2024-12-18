using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;
using System.Text.Json;

namespace PennyPincher.Controllers
{
    [ApiController]
    [Route("/api/management")]

    // This controller is meant to model the projected cash flows you would like to acheive given incomes and expenses
    // Model hypotheticals or goals and how they align with your current rate of spending in LogHistoryController
    public class MoneyManagementController : Controller
    {
        List<CashFlowDto> CFList = CashFlowDataStore.CurrentCashFlow.CashFlowsList;

        [HttpPost]
        public ActionResult<CashFlowDto> CreateFlow(CashFlowDto cashFlow) {
            CFList.Add(cashFlow);
            return Ok(cashFlow);
        }

        [HttpGet]
        public ActionResult<List<CashFlowDto>> GetAllFlows(FlowTypes? targetFlowType) 
        {

            List<CashFlowDto> CFFiltered= new List<CashFlowDto>();

            if (targetFlowType != null)
            {
                CFFiltered = CFList.Where(c => c.Flow.Equals(targetFlowType)).ToList();

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
            if (CFList.Count() == 0)
            {
                return NotFound("No items in Cash Flow");
            }
            int pos = CFList.FindIndex(f => f.Id == targetCashFlowID);

            if (pos >= 0)
            {
                return Ok(CFList[pos]);
            }
            return NotFound("Item not found :(");
        }

        [HttpPut]
        public ActionResult<CashFlowDto> UpdateFlow(CashFlowDto newCashFlow)
        {
            //when they both equal the same id, replace
            if (CFList.Count() == 0)
            {
                return NotFound("No items in Cash Flow");
            }
            CashFlowDto? CFIdMatch = CFList.FirstOrDefault(cf => cf.Id == newCashFlow.Id);
            if (CFIdMatch == null)
            {
                return BadRequest($"Could not find Cash Flow {newCashFlow.Name} to update");
            }
            CFList[CFIdMatch.Id-1] = newCashFlow;
            return Ok($"Updated Entry with {newCashFlow.Name}");
        }

        [HttpDelete]
        public ActionResult<CashFlowDto> DeleteFlow(int targetCashFlowID)
        {

            if (CFList.Count() == 0)
            {
                return NotFound("No items in Cash Flow");
            }
            CashFlowDto? CFIdMatch = CFList.FirstOrDefault(cf => cf.Id == targetCashFlowID);
            if (CFIdMatch == null)
            {
                return BadRequest($"Could not find Cash Flow with id {targetCashFlowID} to delete");
            }

            CFList.Remove(CFIdMatch);
            return Ok(CFList);
            

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


    }
}
