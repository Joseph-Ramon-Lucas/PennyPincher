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

        [HttpPost]
        public async Task<ActionResult<BudgetDto>> CreateBudget(BudgetDto budget)
        {
            BudgetForCreationDto newBudget = new BudgetForCreationDto()
            {
                Id = budget.Id,
                Name = budget.Name,
                Type = budget.Type.ToString()
            };

            var newBudgetId = await _budgetRepository.CreateBudgetAsync(newBudget);
            if (newBudgetId != null)
            {            
                return Ok(newBudgetId);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<BudgetDto>> GetAllBudgets(int id)
        {
            var allBudgets = await _budgetRepository.GetAllBudgetsAsync();
            var allBudgetDtos = new List<BudgetDto>();

            if (allBudgets != null && allBudgets.ToList().Count > 0)
            {
                foreach (var budget in allBudgets)
                {
                    allBudgetDtos.Add(new BudgetDto()
                    {
                        Name = budget.Name,
                        Type = Enum.Parse<BudgetTypes>(budget.Type)
                    });
                }

                return Ok(allBudgetDtos);
            }

            return NotFound();
        }

        [HttpGet("{budgetType}/getBudgetsByType")]
        public async Task<ActionResult<List<BudgetDto>>> GetAllBudgetsByTypeAsync(BudgetTypes type)
        {
            if (type == BudgetTypes.Undefined)
            {
                return NotFound();
            }

            var budgetsByType = await _budgetRepository.GetAllBudgetsByTypeAsync(type);
            var budgetDtosByType = new List<BudgetDto>();

            if (budgetsByType != null && budgetsByType.ToList().Count > 0)
            {
                foreach (var budget in budgetsByType)
                {
                    budgetDtosByType.Add(new BudgetDto()
                    {
                        Name = budget.Name,
                        Type = Enum.Parse<BudgetTypes>(budget.Type)
                    });
                }
                
                return Ok(budgetDtosByType);
            }

            return NotFound();
        }


        [HttpPut("{budgetId}")]
        public async Task<ActionResult<BudgetDto>> PartiallyUpdateBudget(int id, JsonPatchDocument<BudgetForUpdateDto> patchDocument)
        {
            Budget existingBudget = await _budgetRepository.GetBudgetByIdAsync(id);
            if (existingBudget == null)
            {
                return NotFound();
            }

            BudgetForUpdateDto budgetToPatch = new BudgetForUpdateDto()
            {
                Name = existingBudget.Name,
                Type = Enum.Parse<BudgetTypes>(existingBudget.Type)
            };

            patchDocument.ApplyTo(budgetToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(budgetToPatch))
            {
                return BadRequest(ModelState);  
            }

            existingBudget.Name = budgetToPatch.Name;
            existingBudget.Type = budgetToPatch.Type.ToString();

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<BudgetDto>> DeleteBudget(int id)
        {
            Budget existingBudget = await _budgetRepository.GetBudgetByIdAsync(id);
            if (existingBudget == null)
            {
                return NotFound();
            }

            await _budgetRepository.DeleteBudgetAsync(existingBudget.Id);
            return NoContent();
        }
    }
}
