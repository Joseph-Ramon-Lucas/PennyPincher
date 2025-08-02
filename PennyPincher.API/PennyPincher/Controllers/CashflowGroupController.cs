using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;
using PennyPincher.Models.DtoModels;
using PennyPincher.Repositories;

namespace PennyPincher.Controllers
{
    [Route("/api/cashflow_group")]
    [ApiController]
    public class CashflowGroupController : Controller
    {
        private readonly ICashflowGroupRepository _cashflowGroupRepository;

        public CashflowGroupController(ICashflowGroupRepository cashflowGroupRepository)
        {
            _cashflowGroupRepository = cashflowGroupRepository;
        }

        [HttpGet("get_group/{id}")]
        public async Task<ActionResult<CashflowGroupDto>> GetCashflowGroupById(int id)
        {
            var foundGroup = await _cashflowGroupRepository.GetCashflowGroupByIdAsync(id);
            if (foundGroup != null)
            {
                return Ok(foundGroup);  
            }
            
            return NotFound();
        }

        [HttpGet("all_cashflow_groups")]
        public async Task<ActionResult<CashflowGroupDto>> GetAllCashflowGroups()
        {
            var allCashflowGroups = await _cashflowGroupRepository.GetAllCashflowGroupsAsync();
            var allCashflowGroupsDtos = new List<CashflowGroupDto>();

            if (allCashflowGroupsDtos.ToList().Count > 0)
            {
                foreach (var cashflowGroup in allCashflowGroups)
                {
                    allCashflowGroupsDtos.Add(new CashflowGroupDto()
                    {
                        Id = cashflowGroup.cashflow_group_id,
                        Name = cashflowGroup.group_name,
                        Description = cashflowGroup.description
                    });
                }
                
                return Ok(allCashflowGroupsDtos);
            }

            return NotFound();
        }

        [HttpPost("create_cashflow_group")]
        public async Task<ActionResult<CashflowGroupDto>> CreateCashflowGroup(CashflowGroupDto cashflowGroup)
        {
            CashflowGroupForCreationDto newCashflowGroup = new CashflowGroupForCreationDto()
            {
                Name = cashflowGroup.Name,
                Description = cashflowGroup.Description
            };
            
            var newCashflowGroupId = await _cashflowGroupRepository.CreateCashflowGroupAsync(cashflowGroup);
            if (newCashflowGroupId != null)
            {
                return Ok(newCashflowGroup);
            }
            
            return BadRequest();
        }

        [HttpPut("update_cashflow_group/{id}")]
        public async Task<ActionResult<CashflowGroupDto>> PartiallyUpdateCashflowGroup(int id, [FromBody] JsonPatchDocument<CashflowGroupForUpdateDto> patchDocument)
        {
            CashflowGroup? existingCashflowGroup = await _cashflowGroupRepository.GetCashflowGroupByIdAsync(id);
            if (existingCashflowGroup == null)
            {
                return NotFound();
            }

            CashflowGroupForUpdateDto cashflowGroupToPatch = new CashflowGroupForUpdateDto()
            {
                Name = existingCashflowGroup.group_name,
                Description = existingCashflowGroup.description
            };
            
            patchDocument.ApplyTo(cashflowGroupToPatch, ModelState);

            if (!ModelState.IsValid || (TryValidateModel(cashflowGroupToPatch)))
            {
                return BadRequest(ModelState);
            }
            
            existingCashflowGroup.group_name = cashflowGroupToPatch.Name;
            existingCashflowGroup.description = cashflowGroupToPatch.Description;
            
            bool rowsAffected = await _cashflowGroupRepository.UpdateCashflowGroupAsync(existingCashflowGroup);
            if (rowsAffected)
            {
                return NoContent();
            }
            
            return BadRequest();
        }

        [HttpDelete("delete_cashflow_group/{id}")]
        public async Task<ActionResult<CashflowGroupDto>> DeleteCashflowGroup(int id)
        {
            CashflowGroup? existingCashflowGroup = await _cashflowGroupRepository.GetCashflowGroupByIdAsync(id);
            if (existingCashflowGroup == null)
            {
                return NotFound();
            }
            
            await _cashflowGroupRepository.DeleteCashflowGroupAsync(existingCashflowGroup.cashflow_group_id);
            return NoContent();
        }
    }
}