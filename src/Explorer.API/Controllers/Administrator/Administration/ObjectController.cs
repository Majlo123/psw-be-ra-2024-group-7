using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/author/object")]
    public class ObjectController : BaseApiController
    {
        private readonly IObjectService _objectService;

        public ObjectController(IObjectService objectService)
        {
            _objectService = objectService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ObjectDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _objectService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<EquipmentDto> Create([FromBody] ObjectDto objectt)
        {
            var result = _objectService.Create(objectt);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<EquipmentDto> Update([FromBody] ObjectDto objectt)
        {
            var result = _objectService.Update(objectt);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _objectService.Delete(id);
            return CreateResponse(result);
        }
    }
}
