using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace PennyPincher.Controllers
{
    [ApiController]
    [Route("/api/cashflow")]

    // This controller is meant to model the projected cash flows you would like to acheive given incomes and expenses
    // Model hypotheticals or goals and how they align with your current rate of spending in LogHistoryController
    public class CashFlowController : Controller
    {
        List<CashFlowDto> ProjectedCF = CashFlowDataStore.ProjectedCashFlow.CashFlowsList;

        [HttpPost]
        public ActionResult<CashFlowDto> CreateFlow(CashFlowDto cashFlow) {
            ProjectedCF.Add(cashFlow);
            return Ok(cashFlow);
        }

        [HttpGet]
        public ActionResult<List<CashFlowDto>> GetAllFlows(FlowTypes? targetFlowType) 
        {

            if (targetFlowType != null)
            {
                List<CashFlowDto> CFFiltered = ProjectedCF.Where(c => c.Flow.Equals(targetFlowType)).ToList();

                if (CFFiltered.Count == 0)
                {
                    return NotFound("No items were found for this CashFlow type");
                }
                return Ok(CFFiltered);
            }
            else
            {
                return Ok(ProjectedCF);
            }
        }



        [HttpGet("{targetCashFlowID}", Name = "GetFlow")]
        public ActionResult<CashFlowDto> GetFlow(int targetCashFlowID)
        {
            if (ProjectedCF.Count() == 0)
            {
                return NotFound("Cash Flow Item store is empty");
            }
            var CFMatch = ProjectedCF.Where(f => f.Id == targetCashFlowID);
                          
            if (CFMatch.Count() ==0)
            {
                return NotFound($"Cash Flow at ID {targetCashFlowID} not found :(");
            }

            return Ok(CFMatch);
           
        }

        [HttpPut ("{targetCashFlowID}")]
        public ActionResult<CashFlowDto> UpdateFlow(int targetCashFlowID, CashFlowUpdateDto newCashFlow)
        {
            if (ProjectedCF.Count() == 0)
            {
                return NotFound("Cash Flow Item store is empty");
            }

            var CFMatch = ProjectedCF.Where(f => f.Id == targetCashFlowID).ToList();
            if (CFMatch.Count() == 0)
            {
                return NotFound($"Could not find existing Cash Flow at ID {targetCashFlowID} to update");
            }

            
            CFMatch.ForEach(target =>
            {
                target.Name = newCashFlow.Name;
                target.Description = newCashFlow.Description;
                target.Amount = newCashFlow.Amount;
                target.Flow = newCashFlow.Flow;
            });

            return Ok($"Updated Entry {targetCashFlowID} with {newCashFlow.Name}");
        }

        [HttpPatch ("{targetCashFlowID}")]
        public ActionResult<CashFlowUpdateDto> PartiallyUpdateFlow(int targetCashFlowID, JsonPatchDocument<CashFlowUpdateDto> newCashFlow)
        {
            if (ProjectedCF.Count() == 0)
            {
                return NotFound("Cash Flow Item store is empty");
            }

            var CFIdMatch = ProjectedCF.Where(cf => cf.Id == targetCashFlowID).FirstOrDefault();
            if (CFIdMatch == null)
            {
                return NotFound($"Could not find existing Cash Flow at ID {targetCashFlowID} to update");
            }
            


            newCashFlow.ApplyTo(CFIdMatch);

            return Ok(CFIdMatch);

        }


        [HttpDelete ("{targetCashFlowID}")]
        public ActionResult<CashFlowDto> DeleteFlow(int targetCashFlowID)
        {

            if (ProjectedCF.Count() == 0)
            {
                return NotFound("Cash Flow Item store is empty");
            }
            var CFMatch = ProjectedCF.Where(cf => cf.Id == targetCashFlowID).FirstOrDefault();
            if (CFMatch == null)
            {
                return BadRequest($"Could not find Cash Flow with ID {targetCashFlowID} to delete");
            }

            ProjectedCF.Remove(CFMatch);
            return Ok(ProjectedCF);
            
        }

        [HttpDelete]
        public ActionResult<CashFlowDto> DeleteAllFlows()
        {
            if (ProjectedCF.Count() == 0)
            {
                return NotFound("Cash Flow Item store is empty");
            }
            ProjectedCF.Clear();
            return Ok(ProjectedCF);
        }


    }
}
