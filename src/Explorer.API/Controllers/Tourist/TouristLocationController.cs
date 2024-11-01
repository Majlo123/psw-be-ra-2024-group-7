using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/touristLocation")]
    public class TouristLocationController : BaseApiController
    {
        private readonly ITouristLocationService _touristLocationService;
        private readonly ITourService _tourService;

        public TouristLocationController(ITouristLocationService touristLocationService, ITourService tourService)
        {
            _touristLocationService = touristLocationService;
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TouristLocationDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _touristLocationService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TouristLocationDto> Create([FromBody] TouristLocationDto touristLocation)
        {
            var result = _touristLocationService.Create(touristLocation);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TouristLocationDto> Update([FromBody] TouristLocationDto touristLocation)
        {
            var result = _touristLocationService.Update(touristLocation);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _touristLocationService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourDto> Get(long id)
        {
            var result = _tourService.Get(id);
            return CreateResponse(result);
        }
    }
}
