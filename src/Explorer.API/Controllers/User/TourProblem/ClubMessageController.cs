using Explorer.API.Controllers.User.ProfileAdministration;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.User.TourProblem
{
    [Authorize(Policy = "userPolicy")]
    [Microsoft.AspNetCore.Mvc.Route("api/club-message")]
    public class ClubMessageController : BaseApiController
    {
        private readonly IClubMessageService _clubMessageService;

        public ClubMessageController(IClubMessageService clubMessageService)
        {
            _clubMessageService = clubMessageService;
        }

        [HttpGet("{touristClubId:long}")]
        public ActionResult<PersonDto> GetMessagesForTouristClub(long touristClubId)
        {
            var result = _clubMessageService.GetAllForClub(touristClubId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ClubMessageDto> Create([FromBody] ClubMessageDto clubMessageDto)
        {
            var result = _clubMessageService.Create(clubMessageDto);
            return CreateResponse(result);
        }

        [HttpPut("{clubMessageId:long}")]
        public ActionResult<ClubMessageDto> Update([FromBody] ClubMessageDto clubMessageDto, long clubMessageId)
        {
            var result = _clubMessageService.UpdateMessage(clubMessageId, clubMessageDto);
            return CreateResponse(result);
        }


        [HttpDelete("{clubMessageId:long}")]
        public ActionResult<PersonDto> Delete(long clubMessageId)
        {
            var result = _clubMessageService.DeleteMessage(clubMessageId);
            return CreateResponse(result);
        }

    }
}
