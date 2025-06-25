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

        [HttpPost]
        public async Task<ActionResult<AnalysisDto>> CreateAnalysis(AnalysisDto analysis)
        {
            var newAnalysis = new AnalysisForCreationDto()
            {
                Id = analysis.Id,
                Name = analysis.Name,
                Type = analysis.Type.ToString()
            };

            var newId = await _analysisRepository.CreateAnalysisAsync(newAnalysis);
            if (newId != null)
            {
                return Ok(newId);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<AnalysisDto>> GetAllAnalyses(int id)
        {
            var allAnalyses = await _analysisRepository.GetAllAnalysesAsync();
            var allAnalysesDtos = new List<AnalysisDto>();

            if (allAnalyses != null && allAnalyses.ToList().Count > 0)
            {
                foreach (var analysis in allAnalyses)
                {
                    allAnalysesDtos.Add(new AnalysisDto()
                    {
                        Name = analysis.Name,
                        Type = Enum.Parse<AnalysisTypes>(analysis.Type)
                    });
                }

                return Ok(allAnalysesDtos);
            }

            return NotFound();
        }

        [HttpGet("{analysisType}/getAnalysisByType")]
        public async Task<ActionResult<List<AnalysisDto>>> GetAllAnalysisByTypeAsync(AnalysisTypes type)
        {
            var analysesByType = await _analysisRepository.GetAllAnalysesByTypeAsync(type);
            var analysisDtosByType = new List<AnalysisDto>();

            if (analysesByType != null && analysesByType.ToList().Count > 0)
            {
                foreach (var analysis in analysesByType)
                {
                    analysisDtosByType.Add(new AnalysisDto()
                    {
                        Name = analysis.Name,
                        Type = Enum.Parse<AnalysisTypes>(analysis.Type)
                    });
                }

                return Ok(analysisDtosByType);
            }

            return NotFound();
        }

        [HttpPut("{analysisId}")]
        public async Task<ActionResult<AnalysisDto>> PartiallyUpdateAnalysis(int id, JsonPatchDocument<AnalysisForUpdateDto> patchDocument)
        {
            var existingAnalysis = await _analysisRepository.GetAnalysisByIdAsync(id);
            if (existingAnalysis == null)
            {
                return NotFound();
            }

            var analysisToPatch = new AnalysisForUpdateDto()
            {
                Name = existingAnalysis.Name,
                Type = Enum.Parse<AnalysisTypes>(existingAnalysis.Type)
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(analysisToPatch))
            {
                return BadRequest(ModelState);
            }

            existingAnalysis.Name = analysisToPatch.Name;
            existingAnalysis.Type = analysisToPatch.Type.ToString();

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<AnalysisDto>> DeleteAnalysis(int id)
        {
            var existingAnalaysis = await _analysisRepository.GetAnalysisByIdAsync(id);
            if (existingAnalaysis == null)
            {
                return NotFound();
            }

            await _analysisRepository.DeleteAnalysisAsync(existingAnalaysis.Id);
            return NoContent();
        }

        [HttpGet("{budgetGroupId}/status")]
        public async Task<ActionResult<AnalysisStatusDto>> GetBugetStatus(int budgetGroupId)
        {
            AnalysisStatusDto existingAnalysis;
                
               existingAnalysis = await _analysisRepository.GetAnalysisStatusByGroupId(budgetGroupId);
            if (existingAnalysis == null)
            {
                return NotFound();
            }

            return Ok(existingAnalysis);

        }
    }
    
}
