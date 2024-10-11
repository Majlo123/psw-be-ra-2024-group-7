using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Administration
{
    [Route("api/administration/tour-problem-report")]
    public class TourProblemReportController : BaseApiController
    {
        private readonly ITourProblemReportService _tourProblemReportService;

        public TourProblemReportController(ITourProblemReportService tourProblemReportService)
        {
            _tourProblemReportService = tourProblemReportService;
        }

        [HttpGet]
        [Authorize(Policy = "touristPolicy")]
        public ActionResult<PagedResult<TourProblemReportDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourProblemReportService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet]
        [Route("api/administration/tour-problem-report/administratorView")]
        [Authorize(Policy = "administratorPolicy")]
        public ActionResult<PagedResult<TourProblemReportDto>> GetAllForAdministrator([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourProblemReportService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        [Authorize(Policy = "touristPolicy")]
        public ActionResult<TourProblemReportDto> Create([FromBody] TourProblemReportDto tourProblemReport)
        {
            var result = _tourProblemReportService.Create(tourProblemReport);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "touristPolicy")]
        public ActionResult<TourProblemReportDto> Update([FromBody] TourProblemReportDto tourProblemReport)
        {
            var result = _tourProblemReportService.Update(tourProblemReport);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "touristPolicy")]
        public ActionResult Delete(int id)
        {
            var result = _tourProblemReportService.Delete(id);
            return CreateResponse(result);
        }
    }
}
