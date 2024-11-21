using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/encounters")]
    public class AdminEncounterController : BaseApiController
    {

        private readonly IEncounterService _encounterService;

        public AdminEncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }

        [HttpGet("encounter")]
        public ActionResult<List<EncounterDto>> GetAllEncounters()
        {
            var result = _encounterService.GetAll();
            return CreateResponse(result);

        }

        [HttpPost("encounter")]
        public ActionResult<EncounterDto> CreateHiddenLocationEncounter( EncounterDto encounter)
        {
            var result = _encounterService.CreateEncounter(encounter);
            return CreateResponse(result);
        }

        [HttpPut("encounter/{id:int}")]
        public ActionResult<EncounterDto> UpdateHiddenLocationEncounter([FromBody] EncounterDto encounter)
        {
            var result = _encounterService.Update(encounter);
            return CreateResponse(result);
        }

        [HttpGet("encounter/{id:int}")]
        public ActionResult<EncounterDto> GetHiddenLocationEncounterById(long id)
        {
            var result = _encounterService.GetById(id);
            return CreateResponse(result);
        }

        //private readonly IHiddenEncounterService _hiddenEncounterService;
        //private readonly ISocialEncounterService _socialEncounterService;
        //private readonly IMiscEncounterService _miscEncounterService;

        //public AdminEncounterController(IHiddenEncounterService hiddenService,ISocialEncounterService socialService, IMiscEncounterService miscService)
        //{
        //    _hiddenEncounterService = hiddenService;
        //    _socialEncounterService = socialService;
        //    _miscEncounterService = miscService;
        //}


        ////Kontroler sa logikom za dobijanje HiddenLocation Encounter-a

        //[HttpGet("hiddenLocationEncounters")]
        //public ActionResult<PagedResult<HiddenLocationEncounterDto>> GetAllHiddenLocationEncounters([FromQuery] int page, [FromQuery] int pageSize)
        //{
        //    var result = _hiddenEncounterService.GetPaged(page,pageSize);
        //    return CreateResponse(result);

        //}

        //[HttpPost("hiddenLocationEncounters")]
        //public ActionResult<HiddenLocationEncounterDto>CreateHiddenLocationEncounter([FromBody] HiddenLocationEncounterDto encounter)
        //{
        //    var result = _hiddenEncounterService.Create(encounter);
        //    return CreateResponse(result);
        //}

        //[HttpPut("hiddenLocationEncounters/{id:int}")]
        //public ActionResult<HiddenLocationEncounterDto> UpdateHiddenLocationEncounter([FromBody] HiddenLocationEncounterDto encounter)
        //{
        //    var result = _hiddenEncounterService.Update(encounter);
        //    return CreateResponse(result);
        //}

        //[HttpGet("hiddenLocationEncounters/{id:int}")]
        //public ActionResult<HiddenLocationEncounterDto> GetHiddenLocationEncounterById(long id)
        //{
        //    var result = _hiddenEncounterService.GetById(id);
        //    return CreateResponse(result);
        //}




        ////Kontroler sa logikom za dobijanje Social Encounter-a
        //[HttpGet("socialEncounters")]
        //public ActionResult<PagedResult<SocialEncounterDto>> GetAllSocialEncounters([FromQuery] int page, [FromQuery] int pageSize)
        //{
        //    var result = _socialEncounterService.GetPaged(page, pageSize);
        //    return CreateResponse(result);

        //}

        //[HttpPost("socialEncounters")]
        //public ActionResult<SocialEncounterDto> CreateSocialEncounter ([FromBody] SocialEncounterDto encounter)
        //{
        //    var result = _socialEncounterService.Create(encounter);
        //    return CreateResponse(result);
        //}

        //[HttpPut("socialEncounters/{id:int}")]
        //public ActionResult<SocialEncounterDto> UpdateSocialEncounter([FromBody] SocialEncounterDto encounter)
        //{
        //    var result = _socialEncounterService.Update(encounter);
        //    return CreateResponse(result);
        //}

        //[HttpGet("socialEncounters/{id:int}")]
        //public ActionResult<SocialEncounterDto> GetSocialEncounerById(long id)
        //{
        //    var result = _socialEncounterService.GetById(id);
        //    return CreateResponse(result);
        //}




        ////Kontroler sa logikom za dobijanje Misc Encounter-a
        //[HttpGet("miscEncounters")]
        //public ActionResult<PagedResult<MiscEncounterDto>> GetAllMiscEncounters([FromQuery] int page, [FromQuery] int pageSize)
        //{
        //    var result = _miscEncounterService.GetPaged(page, pageSize);
        //    return CreateResponse(result);

        //}

        //[HttpPost("miscEncounters")]
        //public ActionResult<MiscEncounterDto> CreateMiscEncounter([FromBody] MiscEncounterDto encounter)
        //{
        //    var result = _miscEncounterService.Create(encounter);
        //    return CreateResponse(result);
        //}

        //[HttpPut("miscEncounters/{id:int}")]
        //public ActionResult<MiscEncounterDto> UpdateMiscEncounter([FromBody] MiscEncounterDto encounter)
        //{
        //    var result = _miscEncounterService.Update(encounter);
        //    return CreateResponse(result);
        //}

        //[HttpGet("miscEncounters/{id:int}")]
        //public ActionResult<MiscEncounterDto> GetMiscEncounterById(long id)
        //{
        //    var result = _miscEncounterService.GetById(id);
        //    return CreateResponse(result);
        //}


    }
}
