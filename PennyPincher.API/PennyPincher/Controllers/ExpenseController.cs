using PennyPincher.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace PennyPincher.Controllers
{
    [Route("/api/history")]
    [ApiController]
    public class ExpenseController : Controller
    {
        [HttpPost]
        public ActionResult<ExpenseDto> CreateExpense(ExpenseDto expense)
        {
            ExpenseDataStore.Current.Expenses.Add(expense);

            if (!ExpenseDataStore.Current.Expenses.Contains(expense))
            {
                return NotFound();
            }
            
            return Ok(expense);
        }
        
        [HttpGet]
        public ActionResult<List<ExpenseDto>> GetAllExpenses()
        {
            if (ExpenseDataStore.Current.Expenses.Count == 0)
            {
                return NotFound();
            }

            return Ok(ExpenseDataStore.Current.Expenses);
        }
        
        [HttpGet("{categoryType}/getExpensesByCategory")]
        public ActionResult<List<ExpenseDto>> GetExpenseByCategory(CategoryTypes category)
        {
            if (category == CategoryTypes.Undefined)
            {
                return NotFound();
            }
            
            List<ExpenseDto> expenses = ExpenseDataStore.Current.Expenses
                .Where(c => c.Category == category)
                .ToList();

            if (expenses.Count == 0)
            {
                return NotFound();  
            }
            
            return Ok(expenses);
        }
        
        [HttpGet("{expenseId}", Name = "GetExpense")]
        public ActionResult<IEnumerable<ExpenseDto>> GetExpense(int expenseId)
        {
            var foundExpense = ExpenseDataStore.Current.Expenses
                .FirstOrDefault(i => i.Id == expenseId);
            
            if (foundExpense == null)
            {
                return NotFound();
            }
            
            return Ok(foundExpense);
        }

        [HttpPut("{expenseId}")]
        public ActionResult UpdateExpense(int expenseId, ExpenseForUpdateDto expenseWithUpdateDto)
        {
            ExpenseDto? foundExpense = ExpenseDataStore.Current.Expenses
                .FirstOrDefault(i => i.Id == expenseId);
            
            if (foundExpense == null)
            {
                return NotFound();
            }
            
            foundExpense.Name = expenseWithUpdateDto.Name; 
            foundExpense.Price = expenseWithUpdateDto.Price;
            foundExpense.Category = expenseWithUpdateDto.Category;
            
            return NoContent();
        }

        [HttpPatch("{itemId}")]
        public ActionResult PartiallyUpdateExpense(int itemId, JsonPatchDocument<ExpenseForUpdateDto> patchDocument)
        {
            ExpenseDto? expenseFromStore = ExpenseDataStore.Current.Expenses
                .FirstOrDefault(i => i.Id == itemId);
            
            if (expenseFromStore == null)
            {
                return NotFound();
            }

            ExpenseForUpdateDto expenseToPatch = new ExpenseForUpdateDto()
            {
                Name = expenseFromStore.Name,
                Price = expenseFromStore.Price
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
            
            expenseFromStore.Name = expenseToPatch.Name;
            expenseFromStore.Price = expenseToPatch.Price;
            
            return NoContent();   
        }
        
        [HttpDelete]
        public ActionResult DeleteExpense(ExpenseDto expenseToDelete)
        {
            ExpenseDto? expense = ExpenseDataStore.Current.Expenses
                .FirstOrDefault(i => i.Id == expenseToDelete.Id);
            
            if (expense == null)
            {
                return NotFound();
            }

            ExpenseDataStore.Current.Expenses.Remove(expense);
            return NoContent();
        }
    }    
}

