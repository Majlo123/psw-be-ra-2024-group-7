using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Administration
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/administration/tourist_equipment")]
    public class TouristEquipmentController : BaseApiController
    {
        private readonly ITouristEquipmentService _touristEquipmentService;
        public TouristEquipmentController(ITouristEquipmentService touristEquipmentService)
        {
            _touristEquipmentService = touristEquipmentService;
        }
        [HttpGet]
        public ActionResult<BuildingBlocks.Core.UseCases.PagedResult<TouristEquipmentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _touristEquipmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        [HttpPost]
        public ActionResult<TouristEquipmentDto> Create([FromBody] TouristEquipmentDto equipment)
        {
            var result = _touristEquipmentService.Create(equipment);
            return CreateResponse(result);
        }
        [HttpPut("{id:int}")]
        public ActionResult<TouristEquipmentDto> Update([FromBody] TouristEquipmentDto equipment)
        {
            var result = _touristEquipmentService.Update(equipment);
            return CreateResponse(result);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _touristEquipmentService.Delete(id);
            return CreateResponse(result);
        }
    }
}
