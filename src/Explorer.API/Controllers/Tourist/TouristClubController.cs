using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/touristClub")]
    public class TouristClubController : BaseApiController
    {
        private readonly IToursitClubService _toursitClubService;
    
        public TouristClubController(IToursitClubService toursitClubService)
        {
            _toursitClubService = toursitClubService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TouristClubDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _toursitClubService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TouristClubDto> Create([FromBody] TouristClubDto club) 
        { 
            var result = _toursitClubService.Create(club);
            return CreateResponse(result);
        }

        [HttpPut("{Id:int}")]
        public ActionResult<TouristClubDto> Update([FromBody] TouristClubDto club)
        {
            var result = _toursitClubService.Update(club);
            return CreateResponse(result);
        }

    }
}
