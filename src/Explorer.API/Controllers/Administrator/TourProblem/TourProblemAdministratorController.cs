using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.TourProblem
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/tour-problem")]
    public class TourProblemAdministratorController : BaseApiController
    {
        private readonly ITourProblemReportService _tourProblemReportService;

        public TourProblemAdministratorController(ITourProblemReportService tourProblemReportService)
        {
            _tourProblemReportService = tourProblemReportService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourProblemReportDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourProblemReportService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        [HttpPut("set-deadline/{id:int}")]
        public ActionResult<TourProblemReportDto> SetSolvingDeadline(int id, [FromBody] TourProblemReportDto tourProblemReport)
        {
            var result = _tourProblemReportService.SetSolvingDeadline(id, tourProblemReport);
            return CreateResponse(result);
        }
    }
}

