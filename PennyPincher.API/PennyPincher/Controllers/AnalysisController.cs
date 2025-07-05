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
        public async Task<ActionResult<AnalysisStatusDto>> GetStatus(int groupId, int userId)
        {
            AnalysisStatusDto? existingAnalysis;

            bool groupExists = await _analysisRepository.checkGroupExists(groupId);

            if (!groupExists)
            {
                string errorMessage = $"Group id {groupId} doesn't exist";
                Console.WriteLine(errorMessage);
                return NotFound(errorMessage);
            }
            bool userExists = await _analysisRepository.checkUserExists(userId);
            if (!userExists)
            {
                string errorMessage = $"User id {userId} does not exist.";
                Console.WriteLine(errorMessage);
                return NotFound(errorMessage);
            }
            // checkUserExisits()

            existingAnalysis = await _analysisRepository.GetAnalysisStatusByGroupId(groupId, userId);
            if (existingAnalysis != null)
            {
                return NotFound();
            }

            return Ok(existingAnalysis);

        }
    }
    
}
