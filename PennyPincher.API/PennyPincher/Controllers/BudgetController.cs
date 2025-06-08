using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;
using PennyPincher.Models.DtoModels;
using PennyPincher.Repositories;

namespace PennyPincher.Controllers
{
    [Route("/api/budget")]
    [ApiController]
    public class BudgetController : Controller
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetController(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        [HttpPost]
        public async Task<ActionResult<BudgetDto>> CreateBudget(BudgetDto budget)
        {
            BudgetForCreationDto newBudget = new BudgetForCreationDto()
            {
                GroupName = budget.GroupName
            };

            var newBudgetId = await _budgetRepository.CreateBudgetAsync(newBudget);
            if (newBudgetId != null)
            {            
                return Ok(newBudgetId);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<BudgetDto>> GetAllBudgets()
        {
            var allBudgets = await _budgetRepository.GetAllBudgetsAsync();
            var allBudgetDtos = new List<BudgetDto>();

            if (allBudgets != null && allBudgets.ToList().Count > 0)
            {
                foreach (var budget in allBudgets)
                {
                    allBudgetDtos.Add(new BudgetDto()
                    {
                        Id = budget.budget_group_id,
                        GroupName = budget.group_name,
                    });
                }

                return Ok(allBudgetDtos);
            }

            return NotFound();
        }

        [HttpGet("getBudgetById")]
        public async Task<ActionResult<BudgetDto>> GetBudgetById(int id)
        {
            var foundBudget = await _budgetRepository.GetBudgetByIdAsync(id);
            if (foundBudget != null)
            {
                return Ok(foundBudget);
            }

            return NotFound();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<BudgetDto>> PartiallyUpdateBudget(int id, [FromBody] JsonPatchDocument<BudgetForUpdateDto> patchDocument)
        {
            Budget existingBudget = await _budgetRepository.GetBudgetByIdAsync(id);
            if (existingBudget == null)
            {
                return NotFound();
            }

            BudgetForUpdateDto budgetToPatch = new BudgetForUpdateDto()
            {
                Name = existingBudget.group_name,
            };

            patchDocument.ApplyTo(budgetToPatch, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(budgetToPatch))
            {
                return BadRequest(ModelState);
            }

            existingBudget.group_name = budgetToPatch.Name;
            
            bool rowsAffected = await _budgetRepository.UpdateBudgetAsync(existingBudget);
            if (rowsAffected)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<ActionResult<BudgetDto>> DeleteBudget(int id)
        {
            Budget existingBudget = await _budgetRepository.GetBudgetByIdAsync(id);
            if (existingBudget == null)
            {
                return NotFound();
            }

            await _budgetRepository.DeleteBudgetAsync(existingBudget.budget_group_id);
            return NoContent();
        }
    }
}
