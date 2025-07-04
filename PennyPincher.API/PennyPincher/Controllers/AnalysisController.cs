using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models.DtoModels;
using PennyPincher.Repositories;

namespace PennyPincher.Controllers
{
    [Route("api/analysis")]
    [ApiController]
    public class AnalysisController : Controller
    {
        private readonly IAnalysisRepository _analysisRepository;

        public AnalysisController(IAnalysisRepository analysisRepository)
        {
            _analysisRepository = analysisRepository;
        }


        [HttpGet("{groupId}/status")]
        public async Task<ActionResult<AnalysisStatusDto>> GetStatus(int groupId, bool isBudget=true)
        {
            AnalysisStatusDto? existingAnalysis;

            bool existingGroup = await _analysisRepository.checkFinanceTypeGroupExists(groupId, isBudget);

            if (!existingGroup)
            {
                string errorMessage = $"Group id {groupId} doesn't exist";
                Console.WriteLine(errorMessage);
                return NotFound(errorMessage);
            }

            existingAnalysis = await _analysisRepository.GetAnalysisStatusByGroupId(groupId, isBudget);
            if (existingAnalysis == null)
            {
                return NotFound();
            }

            return Ok(existingAnalysis);

        }
    }
    
}
