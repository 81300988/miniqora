using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Quora.BLL.Interface;
using Quora.Model;
using QuoraAPI.JwtAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace QuoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IConfiguration _configuration;

        public QuestionController(IQuestionService questionService, IConfiguration configuration)
        {
            _questionService = questionService;
            _configuration = configuration;
        }

        [HttpGet()]
        [Route("GetQuestions")]
        public IActionResult GetQuestion([FromQuery] int pageIndex = 1)
        {
            string tokenString = Request.Headers["Authorization"];
            int currentUserId = JwtAthentication.GetCurrentUserId(tokenString);

            int pageSize = Convert.ToInt32(_configuration["Setting:PageSize"]);
            int totalQuestion = 0;
            var questions = _questionService.GetQuestions(currentUserId, pageSize, pageIndex, out totalQuestion).ToList();
            questions.ForEach(x => x.AvatarUrl = Url.Content(string.Format("~/Images/UserAvatars/{0}", x.Avatar)));
            questions.ForEach(x => x.CategoryImageUrl = Url.Content(string.Format("~/Images/Categories/{0}", x.ImageName)));

            GetQuestionsResponse response = new GetQuestionsResponse()
            {
                Questions = questions,
                TotalQuestion = totalQuestion
            };

            return Ok(new { response = response });
        }

        [HttpPost()]
        [Route("AddQuestion")]
        public IActionResult AddQuestion(AddQuestionRequest model)
        {
            string tokenString = Request.Headers["Authorization"];
            int currentUserId = JwtAthentication.GetCurrentUserId(tokenString);

            model.UserId = currentUserId;

            var questionId = _questionService.AddQuestion(model);

            return Ok(new { questionId = questionId });
        }

        [HttpGet("{id}")]
        [Route("GetQuestionDetail")]
        public IActionResult GetQuestionDetail([FromQuery] int id)
        {
            var questions = _questionService.GetQuestionDetail(id);

            questions.Answers.ForEach(x => x.AvatarUrl = Url.Content(string.Format("~/Images/UserAvatars/{0}", x.Avatar)));
            questions.Comments.ForEach(x => x.AvatarUrl = Url.Content(string.Format("~/Images/UserAvatars/{0}", x.Avatar)));
            questions.Answers.ForEach(x => x.AnswerComments.ForEach(y => y.AvatarUrl = Url.Content(string.Format("~/Images/UserAvatars/{0}", y.Avatar))));

            return Ok(new { questions = questions });
        }
    }
}