using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
            return Ok($"Updated Entry {newCashFlow.Id} with {newCashFlow.Name}");
        }

        [HttpPatch("{id}")]
        public ActionResult<CashFlowPatchDto> PatchFlow(int id, JsonPatchDocument<CashFlowDto> newCashFlow)
        {
            if (CFList.Count() == 0)
            {
                return NotFound("No items in Cash Flow");
            }

            var CFIdMatch = CFList.FirstOrDefault(cf => cf.Id == id);
            if (CFIdMatch == null)
            {
                return NotFound($"Could not find existing Cash Flow to update");
            }
            

            //Apply Patch to existing CF

            newCashFlow.ApplyTo(CFIdMatch);

            return Ok($"Patched Entry with {CFIdMatch}");







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
            double liabilities = CFList.FindAll(e => e.Flow.Equals(FlowTypes.expense)).Sum(e => e.Amount);
            double netIncome = incomes - liabilities;
            double netIncomeRatio = Math.Round((liabilities / incomes), 4) * 100;
            string mostCostlyName = CFList
                .Where(e => e.Flow.Equals(FlowTypes.expense))
                .OrderByDescending(e => e.Amount)
                .Select(e => e.Name)
                .FirstOrDefault() ?? "nothing at the moment";

            double mostCostlyAmount = CFList
                .Where(e => e.Flow.Equals(FlowTypes.expense))
                .OrderByDescending(e => e.Amount)
                .Select(e => e.Amount)
                .FirstOrDefault<double>();



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
            string ratioText = ($"\nYou're currently using {netIncomeRatio}% of your earnings. {Math.Round((mostCostlyAmount/incomes),4)*100}% of your earnings is going to {mostCostlyName}");

            statusUpdate = statusUpdate+ ratioText;
            return Ok(statusUpdate);
        }


    }
}
