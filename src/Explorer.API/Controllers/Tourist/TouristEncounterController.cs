using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/encounters")]
    public class TouristEncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        public TouristEncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }

        [HttpGet]
        public ActionResult<List<EncounterDto>> GetAllEncounters()
        {
            var result = _encounterService.GetAll();
            return CreateResponse(result);
        }
        [HttpGet("{id:int}")]
        public ActionResult<List<EncounterDto>> GetById(int id)
        {
            var result = _encounterService.GetById(id);
            return CreateResponse(result);
        }
    }
}
