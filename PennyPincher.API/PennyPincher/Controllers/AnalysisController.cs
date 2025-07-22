using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models.DtoModels;
using PennyPincher.Repositories;
using System.Text.RegularExpressions;

namespace PennyPincher.Controllers
{
    [Route("api/analysis")]
    [ApiController]
    public class AnalysisController : Controller
    {
        private readonly IAnalysisRepository _analysisRepository;
        private readonly IValidationRepository _validationRepository;

        public AnalysisController(IAnalysisRepository analysisRepository, IValidationRepository validationRepository)
        {
            _analysisRepository = analysisRepository;
            _validationRepository = validationRepository;
           
        }


        [HttpGet("status/{groupId}")]
        public async Task<ActionResult<AnalysisStatusDto>> GetStatus(int groupId, int userId)
        {
            AnalysisStatusDto? existingAnalysis;

            ValidationResponseDto userValidationResponse = await _validationRepository.checkUserExists(userId);
            if (!userValidationResponse.IsSuccess)
            {
                Console.WriteLine(userValidationResponse.ResponseMessage);
                return NotFound(userValidationResponse.ResponseMessage);
            }

            ValidationResponseDto cashflowGroupValidationResponse = await _validationRepository.checkCashflowGroupExists(groupId);
            if (!cashflowGroupValidationResponse.IsSuccess)
            {
                Console.WriteLine(cashflowGroupValidationResponse.ResponseMessage);
                return NotFound(cashflowGroupValidationResponse.ResponseMessage);
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

            ValidationResponseDto userValidationResponse = await _validationRepository.checkUserExists(userId);
            if (!userValidationResponse.IsSuccess)
            {
                Console.WriteLine(userValidationResponse.ResponseMessage);
                return NotFound(userValidationResponse.ResponseMessage);
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

            ValidationResponseDto userValidationResponse = await _validationRepository.checkUserExists(userId);
            if (!userValidationResponse.IsSuccess)
            {
                Console.WriteLine(userValidationResponse.ResponseMessage);
                return NotFound(userValidationResponse.ResponseMessage);
            }

            ValidationResponseDto cashflowGroup1ValidationResponse = await _validationRepository.checkCashflowGroupExists(groupId1);
            if (!cashflowGroup1ValidationResponse.IsSuccess)
            {
                Console.WriteLine(cashflowGroup1ValidationResponse.ResponseMessage);
                return NotFound(cashflowGroup1ValidationResponse.ResponseMessage);
            }

            ValidationResponseDto cashflowGroup2ValidationResponse = await _validationRepository.checkCashflowGroupExists(groupId2);
            if (!cashflowGroup2ValidationResponse.IsSuccess)
            {
                Console.WriteLine(cashflowGroup2ValidationResponse.ResponseMessage);
                return NotFound(cashflowGroup2ValidationResponse.ResponseMessage);
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
