using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration;

[Route("api/tour/image")]
public class ImageController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        //var path = Path.Combine("C:\\Users\\PC\\Desktop", file.FileName);
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), file.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { filePath = path });
    }

    [HttpGet]
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
