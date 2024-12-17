using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [ApiController]
    [Route("api/quizzes")]
    public class QuizController : BaseApiController
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost]
        public IActionResult CreateQuiz([FromBody] QuizDto dto)
        {
            var result = _quizService.CreateQuiz(dto);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Errors);
        }

      
    }
}
