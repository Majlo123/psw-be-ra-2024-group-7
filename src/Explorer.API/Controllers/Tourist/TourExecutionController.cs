using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourExecutions")]
    public class TourExecutionController : BaseApiController
    {
        private readonly ITourExecutionService _tourExecutionService;

        public TourExecutionController(ITourExecutionService tourExecutionService)
        {
            _tourExecutionService = tourExecutionService;
        }
        [HttpGet]
        public ActionResult<PagedResult<TourExecutionDto>> GetPaged([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourExecutionService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourExecutionDto> GetById(int id)
        {
            var result = _tourExecutionService.Get(id);
            return CreateResponse(result);
        }
        [HttpGet("{touristId:int}/{tourId:int}")]
        public ActionResult<TourExecutionDto> GetByUserAndTourIds(int touristId, int tourId)
        {
            var result = _tourExecutionService.GetByUserAndTourIds(touristId, tourId);
            return CreateResponse(result);
        }

        [HttpPost("startNewTour")]
        public ActionResult<TourExecutionDto> StartNewTour([FromBody] TourExecutionDto tourExecution)
        {
            var result = _tourExecutionService.StartNewTour(tourExecution);
            return CreateResponse(result);
        }

        [HttpPost("leaveTour")]
        public ActionResult<TourExecutionDto> LeaveTour([FromBody] TourExecutionDto tourExecution)
        {
            var result = _tourExecutionService.LeaveTour(tourExecution.TouristId, tourExecution.TourId);
            return CreateResponse(result);
        }
    }

}

