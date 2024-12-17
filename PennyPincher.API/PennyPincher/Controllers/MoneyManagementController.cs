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
            CFList.Add(cashFlow);
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

        [HttpPut]
        public ActionResult<CashFlowDto> UpdateFlow(CashFlowDto newCashFlow)
        {
            //when they both equal the same id, replace

            CashFlowDto? CFIdMatch = CFList.FirstOrDefault(cf => cf.Id == newCashFlow.Id);
            if (CFIdMatch == null)
            {
                return BadRequest($"Could not find Cash Flow {newCashFlow.Name} to update");
            }
            CFList[CFIdMatch.Id-1] = newCashFlow;
            return Ok($"Updated {newCashFlow.Name}");
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


    }
}
