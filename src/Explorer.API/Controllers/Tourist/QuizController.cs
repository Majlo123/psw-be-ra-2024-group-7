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
        public ActionResult<QuizDto> Create([FromBody] QuizDto quizDto)
        {
            var result = _quizService.CreateQuiz(quizDto);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
        }


    }
}
