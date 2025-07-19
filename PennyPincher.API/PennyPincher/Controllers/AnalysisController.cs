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


        [HttpGet("status/{groupId}")]
        public async Task<ActionResult<AnalysisStatusDto>> GetStatus(int groupId, int userId)
        {
            AnalysisStatusDto? existingAnalysis;

            bool userExists = await _analysisRepository.checkUserExists(userId);
            if (!userExists)
            {
                string errorMessage = $"User id {userId} does not exist.";
                Console.WriteLine(errorMessage);
                return NotFound(errorMessage);
            }

            bool groupExists = await _analysisRepository.checkGroupExists(groupId);
            if (!groupExists)
            {
                string errorMessage = $"Group id {groupId} doesn't exist";
                Console.WriteLine(errorMessage);
                return NotFound(errorMessage);
            }
           

            existingAnalysis = await _analysisRepository.GetUserAnalysisStatusByGroupId(groupId, userId);
            if (existingAnalysis == null)
            {
                return NotFound();
            }

            return Ok(existingAnalysis);

        }

        [HttpGet("status")]
        public async Task<ActionResult<AnalysisStatusDto>> GetAllStatuses(int userId)
        {
            AnalysisStatusDto? existingAnalysis;

            bool userExists = await _analysisRepository.checkUserExists(userId);
            if (!userExists)
            {
                string errorMessage = $"User id {userId} does not exist.";
                Console.WriteLine(errorMessage);
                return NotFound(errorMessage);
            }

            existingAnalysis = await _analysisRepository.GetAllUserAnalysisStatuses(userId);
            if (existingAnalysis == null)
            {
                return NotFound();
            }

            return Ok(existingAnalysis);
        }

        [HttpGet("comparison")]
        public async Task<ActionResult<AnalysisComparisonDto>> GetUserAnalysisComparisonByUserId(int userId, int groupId1, int groupId2)
        {
            AnalysisComparisonDto? existingAnalysis;

            bool userExists = await _analysisRepository.checkUserExists(userId);
            if (!userExists)
            {
                string errorMessage = $"User id {userId} does not exist.";
                Console.WriteLine(errorMessage);
                return NotFound(errorMessage);
            }

            existingAnalysis = await _analysisRepository.GetUserAnalysisComparison(userId, groupId1, groupId2);
            if (existingAnalysis == null) 
            {     
                return NotFound(); 
            }
            return Ok(existingAnalysis);

        }
    }
    
}
