using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models.DtoModels;
using PennyPincher.Repositories;

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
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<CashflowEntryDto>> GetAllCashflowEntries(int id)
        {
            return NotFound();
        }


        [HttpGet("{cashflowType}/getEntriesByType")]
        public async Task<ActionResult<List<CashflowEntryDto>>> GetAllCashflowsByTypeAsync()
        {
            return NotFound();
        }

        [HttpPut("{cashflowId}")]
        public async Task<ActionResult<CashflowEntryDto>> PartiallyUpdateCashflowEntry()
        {
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<CashflowEntryDto>> DeleteCashflowEntry(int id)
        {
            return NoContent();
        }
    }
}
