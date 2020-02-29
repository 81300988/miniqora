using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quora.BLL.Interface;
using Quora.Model;
using QuoraAPI.JwtAuthentication;

namespace QuoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost()]
        [Route("AddAnswer")]
        public IActionResult AddAnswer(AddAnswerRequest model)
        {
            string tokenString = Request.Headers["Authorization"];
            int currentUserId = JwtAthentication.GetCurrentUserId(tokenString);

            model.UserId = currentUserId;

            var answers = _answerService.AddAnswer(model);
            answers.ForEach(x => x.AvatarUrl = Url.Content(string.Format("~/Images/UserAvatars/{0}", x.Avatar)));

            return Ok(new { answers = answers });
        }
        [HttpPost()]
        [Route("UpdateVoteForAnswer")]
        public IActionResult AddVoteForAnswer(UpdateVoteForAnswer model)
        {
            string tokenString = Request.Headers["Authorization"];
            int currentUserId = JwtAthentication.GetCurrentUserId(tokenString);
            
            model.UserId = currentUserId;

            var totalVoteForAnswer = _answerService.UpdateVoteForAnswer(model);

            return Ok(new { totalVoteForAnswer = totalVoteForAnswer });
        }
    }
}