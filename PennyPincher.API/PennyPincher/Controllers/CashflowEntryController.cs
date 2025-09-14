using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;
using PennyPincher.Models.DtoModels;
using PennyPincher.Repositories;
using static PennyPincher.Models.TypeCollections;

namespace PennyPincher.Controllers
{
    [Route("api/cashflow_entry")]
    [ApiController]
    public class CashflowEntryController : Controller
    {
        private readonly ICashflowEntryRepository _cashflowRepository;

        public CashflowEntryController(ICashflowEntryRepository cashflowRepository)
        {
            _cashflowRepository = cashflowRepository;
        }
        
        [HttpGet("get_entry/{id}")]
        public async Task<ActionResult<CashflowEntryDto>> GetCashflowEntryById(int id)
        {
            var foundCashflowEntry = await _cashflowRepository.GetCashflowEntryByIdAsync(id);
            if (foundCashflowEntry != null)
            {
                return Ok(foundCashflowEntry);
            }
            
            return NotFound(); 
        }
        
        [HttpGet("all_entries")]
        public async Task<ActionResult<CashflowEntryDto>> GetAllCashflowEntries()
        {
            var allCashflowEntries = await _cashflowRepository.GetAllCashflowEntriesAsync();
            var allCashflowEntriesDtos = new List<CashflowEntryDto>();

            if (allCashflowEntries.ToList().Count > 0)
            {
                foreach (var cashflowEntry in allCashflowEntries)
                {
                    allCashflowEntriesDtos.Add(new CashflowEntryDto()
                    {
                        Amount = cashflowEntry.amount,
                        Flow = cashflowEntry.flow
                    });
                }
            }
            
            return Ok(allCashflowEntriesDtos); 
        }
        
        [HttpGet("entries_by_flow_type/{flow}")]
        public async Task<ActionResult<CashflowEntryDto>> GetAllCashflowEntriesByFlowType(CashflowType flow)
        {
            var allCashflowEntriesByType = await _cashflowRepository.GetAllCashflowEntriesByFlowTypeAsync(flow);
            var allCashflowEntriesDtos = new List<CashflowEntryDto>();

            if (allCashflowEntriesByType.ToList().Count > 0)
            {
                foreach (var cashflowEntry in allCashflowEntriesByType)
                {
                    allCashflowEntriesDtos.Add(new CashflowEntryDto()
                    {
                        Id = cashflowEntry.cashflow_entry_id,
                        Name = cashflowEntry.cashflow_entry_name,
                        Description = cashflowEntry.description,
                        Amount = cashflowEntry.amount,
                        EntryDate = cashflowEntry.entry_date,
                        Flow = cashflowEntry.flow,
                        CategoryTypeAsString = cashflowEntry.category_type.ToString()
                    });
                }
                
                return Ok(allCashflowEntriesDtos);
            }
            
            return NotFound();
        }
        
        [HttpPost("create_entry")]
        public async Task<ActionResult<CashflowEntryDto>> CreateCashflowEntry(CashflowEntryDto cashflowEntry)
        {
            CashflowEntryForCreationDto newCashflowEntry = new CashflowEntryForCreationDto()
            {
                Name = cashflowEntry.Name,
                Description = cashflowEntry.Description,
                Amount = cashflowEntry.Amount,
                EntryDate = cashflowEntry.EntryDate,
                Flow = cashflowEntry.Flow,
                CategoryType = cashflowEntry.CategoryType,
                UserId = cashflowEntry.UserId
            };

            var newCashflowId = await _cashflowRepository.CreateCashflowEntryAsync(newCashflowEntry);
            if (newCashflowId != null)
            {
                return Ok(newCashflowEntry);
            }
            
            return BadRequest();
        }
        
        [HttpPatch("partially_update_entry/{id}")]
        public async Task<ActionResult<CashflowEntryDto>> PartiallyUpdateCashflowEntry(int id, [FromBody] JsonPatchDocument<CashflowEntryForUpdateDto> patchDocument)
        {
            CashflowEntry existingCashflowEntry = await _cashflowRepository.GetCashflowEntryByIdAsync(id);
            if (existingCashflowEntry == null)
            {
                return NotFound();
            }

            CashflowEntryForUpdateDto cashflowEntryToPatch = new CashflowEntryForUpdateDto()
            {
                Name = existingCashflowEntry.cashflow_entry_name,
                Amount = existingCashflowEntry.amount,
            };

            patchDocument.ApplyTo(cashflowEntryToPatch, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(cashflowEntryToPatch))
            {
                return BadRequest(ModelState);
            }

            existingCashflowEntry.amount = cashflowEntryToPatch.Amount;
            existingCashflowEntry.cashflow_entry_name = cashflowEntryToPatch.Name;
            
            bool rowsAffected = await _cashflowRepository.UpdateCashflowEntryAsync(existingCashflowEntry);
            if (rowsAffected)
            {
                return NoContent();
            }
            
            return BadRequest();
        }

        [HttpPut("completely_update_entry/{id}")]
        public async Task<ActionResult<CashflowEntryDto>> CompletelyUpdateCashflowEntry(int id, [FromBody] CashflowEntryForUpdateDto cashflowEntryWithUpdate)
        {
            CashflowEntry existingCashflowEntry = await _cashflowRepository.GetCashflowEntryByIdAsync(id);
            if (existingCashflowEntry == null)
            {
                return NotFound();
            }

            existingCashflowEntry.cashflow_entry_name = cashflowEntryWithUpdate.Name;
            existingCashflowEntry.description = cashflowEntryWithUpdate.Description;
            existingCashflowEntry.amount = cashflowEntryWithUpdate.Amount;
            existingCashflowEntry.entry_date = cashflowEntryWithUpdate.EntryDate;
            existingCashflowEntry.flow = cashflowEntryWithUpdate.Flow;
            existingCashflowEntry.category_type = cashflowEntryWithUpdate.CategoryType;

            bool rowsAffected = await _cashflowRepository.UpdateCashflowEntryAsync(existingCashflowEntry);
            if (rowsAffected)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("delete_entry/{id}")]
        public async Task<ActionResult<CashflowEntryDto>> DeleteCashflowEntry(int id)
        {
            CashflowEntry existingCashflowEntry = await _cashflowRepository.GetCashflowEntryByIdAsync(id);
            if (existingCashflowEntry == null)
            {
                return NotFound();
            }
            
            await _cashflowRepository.DeleteCashflowEntryAsync(existingCashflowEntry.cashflow_entry_id);
            return NoContent();
        }
    }
}
