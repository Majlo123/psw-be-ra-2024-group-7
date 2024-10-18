using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author;

[Authorize(Policy = "authorPolicy")]
[Route("api/tours/keypoint")]
public class KeyPointController : BaseApiController
{
    private readonly IKeyPointService _keyPointService;

    public KeyPointController(IKeyPointService keyPointService)
    {
        _keyPointService = keyPointService;
    }

    [HttpPost]
    public ActionResult<KeyPointDto> Create([FromBody] KeyPointDto keyPointDto)
        {
        var result = _keyPointService.Create(keyPointDto);
        return CreateResponse(result);
    }

    [HttpGet]
    public ActionResult<List<KeyPointDto>> GetAll()
    {
        var result = _keyPointService.GetAll();
        return CreateResponse(result);
    }

    [HttpPost]
    [Route("image")]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var path = Path.Combine("C:\\Users\\PC\\Desktop", file.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { filePath = path });
    }

    [HttpGet]
    [Route("image")]
    public IActionResult GetImage([FromQuery] string filePath)
    {
        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, "image/jpeg");
    }
}
