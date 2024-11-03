using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;

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
        public IActionResult StartNewTour([FromBody] TourExecutionDto tourExecution)
        {
            var result = _tourExecutionService.StartNewTour(tourExecution);
 
            if (result.IsSuccess)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    

        [HttpPost("leaveTour")]
        public ActionResult<TourExecutionDto> LeaveTour([FromBody] TourExecutionDto tourExecution)
        {
            var result = _tourExecutionService.LeaveTour(tourExecution.TouristId, tourExecution.TourId);
            return CreateResponse(result);
        }
        [HttpPut("checkLocation/{id:int}")]
        public ActionResult CheckLocation([FromQuery] float latitude, [FromQuery] float longitude, int id)
        {
            var result = _tourExecutionService.CheckLocation(id, latitude, longitude);

            return CreateResponse(result);

        }
        [HttpGet("completed/{id:int}")]
        public ActionResult<TourExecutionDto> GetCompletedKeyPoints(int id)
        {
            var result = _tourExecutionService.GetCompletedKeyPoints(id);
            return CreateResponse(result);
        }
    }

}

