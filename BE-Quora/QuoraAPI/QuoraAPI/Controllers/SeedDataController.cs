using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quora.BLL.Interface;

namespace QuoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SeedDataController : ControllerBase
    {
        private readonly ISeedDataService _seedDataService;

        public SeedDataController(ISeedDataService seedDataService)
        {
            _seedDataService = seedDataService;
        }

        [HttpPut()]
        [Route("CreateRandomQuestion")]
        public IActionResult CreateRandomQuestion()
        {
            _seedDataService.CreateRandomQuestions();

            return Ok();
        }

        [HttpPut()]
        [Route("CreateRandomAnswer")]
        public IActionResult CreateRandomAnswer()
        {
            _seedDataService.CreateRandomAnswer();

            return Ok();
        }

        [HttpPut()]
        [Route("CreateRandomVote")]
        public IActionResult CreateRandomVote()
        {
            _seedDataService.CreateRandomVote();

            return Ok();
        }

        [HttpPut()]
        [Route("CreateRandomCommentForAnswer")]
        public IActionResult CreateRandomCommentForAnswer()
        {
            _seedDataService.CreateRandomCommentForAnswer();

            return Ok();
        }

        [HttpPut()]
        [Route("CreateRandomCommentForQuestion")]
        public IActionResult CreateRandomCommentForQuestion()
        {
            _seedDataService.CreateRandomCommentForQuestion();

            return Ok();
        }
    }
}