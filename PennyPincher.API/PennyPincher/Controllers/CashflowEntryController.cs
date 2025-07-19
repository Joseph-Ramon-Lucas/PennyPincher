using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;
using PennyPincher.Models.DtoModels;
using PennyPincher.Repositories;
using static PennyPincher.Models.TypeCollections;

namespace PennyPincher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashflowEntryController : ControllerBase
    {
        private readonly ICashflowEntryRepository _cashflowRepository;

        [HttpPost]
        public async Task<ActionResult<CashflowEntryDto>> CreateCashflowEntry(CashflowEntryDto cashflowEntry)
        {
            CashflowEntryForCreationDto newCashflowEntry = new CashflowEntryForCreationDto()
            {
                Amount = cashflowEntry.Amount,
                Flow = cashflowEntry.Flow,
            };

            var newCashflowId = await _cashflowRepository.CreateCashflowEntryAsync(newCashflowEntry);
            if (newCashflowId != null)
            {
                return Ok(newCashflowEntry);
            }
            
            return BadRequest();
        }

        [HttpGet]
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
 
        [HttpGet("/{flow}")]
        public async Task<ActionResult<CashflowEntryDto>> GetAllCashflowEntriesByFlowType(CashflowType flow)
        {
            var allCashflowEntriesByType = await _cashflowRepository.GetAllCashflowEntriesByFlowTypeAsync(flow);
            var allCashflowEntriesDtos = new List<CashflowEntryDto>();

            if (allCashflowEntriesByType != null && allCashflowEntriesDtos.ToList().Count > 0)
            {
                foreach (var cashflowEntry in allCashflowEntriesByType)
                {
                    allCashflowEntriesDtos.Add(new CashflowEntryDto()
                    {
                        Id = cashflowEntry.cashflow_entry_id,
                        Amount = cashflowEntry.amount,
                        Flow = cashflowEntry.flow
                    });
                }
                
                return Ok(allCashflowEntriesDtos);
            }
            
            return NotFound();
        }

        [HttpGet("getCashflowEntryById")]
        public async Task<ActionResult<CashflowEntryDto>> GetCashflowEntryById(int id)
        {
            var foundCashflowEntry = await _cashflowRepository.GetCashflowEntryByIdAsync(id);
            if (foundCashflowEntry != null)
            {
                return Ok(foundCashflowEntry);
            }
            
            return NotFound(); 
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CashflowEntryDto>> PartiallyUpdateCashflowEntry(int id, [FromBody] JsonPatchDocument<CashflowEntryForUpdateDto> patchDocument)
        {
            CashflowEntry existingCashflowEntry = await _cashflowRepository.GetCashflowEntryByIdAsync(id);
            if (existingCashflowEntry == null)
            {
                return NotFound();
            }

            CashflowEntryForUpdateDto cashflowEntryToPatch = new CashflowEntryForUpdateDto()
            {
                Amount = existingCashflowEntry.amount,
                Flow = existingCashflowEntry.flow
            };

            patchDocument.ApplyTo(cashflowEntryToPatch, ModelState);

            if (!ModelState.IsValid || (TryValidateModel(cashflowEntryToPatch)))
            {
                return BadRequest(ModelState);
            }
            
            existingCashflowEntry.amount = cashflowEntryToPatch.Amount;
            existingCashflowEntry.flow = cashflowEntryToPatch.Flow;
            
            bool rowsAffected = await _cashflowRepository.UpdateCashflowEntryAsync(existingCashflowEntry);
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
