using PennyPincher.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using PennyPincher.Repositories;
using System.Net.Http.Json;

namespace PennyPincher.Controllers
{
    [Route("/api/history")]
    [ApiController]
    public class ExpenseController : Controller
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseDto>> CreateExpense(ExpenseDto expense)
        {
            ExpenseForCreationDto newExpense = new ExpenseForCreationDto() 
            { 
                UserId = expense.UserId,
                Name = expense.Name,
                Category = expense.Category.ToString(), // 2, "Living" 
                Price = expense.Price
            };

            var newExpenseId = await _expenseRepository.CreateExpenseAsync(newExpense);
            if (newExpenseId != null)
            {
                return Ok(newExpenseId);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<List<ExpenseDto>>> GetAllExpenses()
        {
            var allExpenses = await _expenseRepository.GetAllExpensesAsync();
            var allExpenseDtos = new List<ExpenseDto>();    

            if (allExpenses != null && allExpenses.ToList().Count > 0)
            {
                foreach (var expense in allExpenses)
                {
                    allExpenseDtos.Add(new ExpenseDto()
                    {
                        Name = expense.Name,
                        Category = Enum.Parse<CategoryTypes>(expense.Category),
                        Price = expense.Price,
                    });        
                }
                
                return Ok(allExpenseDtos);
            }

            return NotFound();
        }
        
        [HttpGet("{categoryType}/getExpensesByCategory")]
        public async Task<ActionResult<List<ExpenseDto>>> GetExpenseByCategory(CategoryTypes category)
        {
            if (category == CategoryTypes.Undefined)
            {
                return NotFound();
            }

            var expensesByCategory = await _expenseRepository.GetAllExpensesByCategoryAsync(category);
            var expenseDtosByCategory = new List<ExpenseDto>();
            
            if (expensesByCategory != null && expensesByCategory.ToList().Count > 0)
            {
                foreach (var expense in expensesByCategory)
                {
                    expenseDtosByCategory.Add(new ExpenseDto()
                    {
                        Name = expense.Name,
                        Category = Enum.Parse<CategoryTypes>(expense.Category),
                        Price = expense.Price,
                    });
                }

                return Ok(expenseDtosByCategory);
            }

            return NotFound();
        }
        
        [HttpGet("{expenseId}", Name = "GetExpense")]
        public ActionResult<IEnumerable<ExpenseDto>> GetExpense(int expenseId)
        {
            var foundExpense = _expenseRepository.GetExpenseByIdAsync(expenseId);

            if (foundExpense != null)
            {
                Json dataMessage = new Json();

                return Ok(foundExpense);
                
            }

            return NotFound();
        }

        [HttpPut("{expenseId}")]
        public async Task<ActionResult> UpdateExpense(int Id, ExpenseForUpdateDto expenseWithUpdateDto)
        {
            if (expenseWithUpdateDto == null || expenseWithUpdateDto.Id != Id)
            {
                return BadRequest();
            }

            var existingExpense = await _expenseRepository.GetExpenseByIdAsync(Id);
            if (existingExpense == null)
            {
                return NotFound();
            }

            await _expenseRepository.UpdateExpenseAsync(existingExpense);
            return NoContent();
        }

        [HttpPatch("{itemId}")]
        public async Task<ActionResult> PartiallyUpdateExpense(int Id, JsonPatchDocument<ExpenseForUpdateDto> patchDocument)
        {
            Expense existingExpense = await _expenseRepository.GetExpenseByIdAsync(Id);
            if (existingExpense == null)
            {
                return NotFound();
            }

            ExpenseForUpdateDto expenseToPatch = new ExpenseForUpdateDto() 
            { 
                Name = existingExpense.Name,
                Price = existingExpense.Price
            };

            patchDocument.ApplyTo(expenseToPatch, ModelState);  

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(expenseToPatch))
            {
                return BadRequest(ModelState);  
            }

            existingExpense.Name = expenseToPatch.Name;
            existingExpense.Price = expenseToPatch.Price;

            return NoContent();
        }
        
        [HttpDelete]
        public async Task<ActionResult> DeleteExpense(int Id)
        {
            Expense existingExpense = await _expenseRepository.GetExpenseByIdAsync(Id);
            if (existingExpense == null)
            {
                return NotFound();
            }

            await _expenseRepository.DeleteExpenseAsync(existingExpense.Id);
            return NoContent();
        }
    }    
}

