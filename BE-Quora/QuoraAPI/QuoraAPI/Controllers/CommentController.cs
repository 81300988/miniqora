using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quora.BLL.Interface;
using Quora.Model;
using QuoraAPI.JwtAuthentication;

namespace QuoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        [Route("GetCommentByQuestionId")]
        public IActionResult GetCommentByQuestionId([FromQuery] int id)
        {
            var comments = _commentService.GetCommentByQuestionId(id);
            comments.ForEach(x => x.AvatarUrl = Url.Content(string.Format("~/Images/UserAvatars/{0}", x.Avatar)));

            return Ok(new { comments = comments });
        }

        [HttpGet("{id}")]
        [Route("GetCommentByAnswerId")]
        public IActionResult GetCommentByAnswerId([FromQuery] int id)
        {
            var comments = _commentService.GetCommentByAnswerId(id);
            comments.ForEach(x => x.AvatarUrl = Url.Content(string.Format("~/Images/UserAvatars/{0}", x.Avatar)));

            return Ok(new { comments = comments });
        }

        [HttpPost()]
        [Route("CommentForQuestion")]
        public IActionResult CommentForQuestion(AddQuestionCommentRequest model)
        {
            string tokenString = Request.Headers["Authorization"];
            int currentUserId = JwtAthentication.GetCurrentUserId(tokenString);

            var comments = _commentService.CommentForQuestion(currentUserId, model.QuestionId, model.CommentContent);
            comments.ForEach(x => x.AvatarUrl = Url.Content(string.Format("~/Images/UserAvatars/{0}", x.Avatar)));

            return Ok(new { comments = comments });
        }

        [HttpPost()]
        [Route("CommentForAnswer")]
        public IActionResult CommentForAnswer(AddAnswerCommentRequest model)
        {
            string tokenString = Request.Headers["Authorization"];
            int currentUserId = JwtAthentication.GetCurrentUserId(tokenString);

            var comments = _commentService.CommentForAnswer(currentUserId, model.AnswerId, model.CommentContent);
            comments.ForEach(x => x.AvatarUrl = Url.Content(string.Format("~/Images/UserAvatars/{0}", x.Avatar)));

            return Ok(new { comments = comments });
        }
    }
}