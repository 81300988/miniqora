using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Quora.BLL.Interface;
using Quora.Model;
using QuoraAPI.JwtAuthentication;
using QuoraAPI.RedisHelper;

namespace QuoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _distributedCache;

        public RedisController(IQuestionService questionService, IConfiguration configuration, IDistributedCache distributedCache)
        {
            _questionService = questionService;
            _configuration = configuration;
            _distributedCache = distributedCache;
    }


        [HttpGet]
        [Route("RedisGetQuestions")]
        public async Task<IActionResult> GetQuestion([FromQuery] int pageIndex = 1)
        {
            string tokenString = Request.Headers["Authorization"];
            int currentUserId = JwtAthentication.GetCurrentUserId(tokenString);
            var userId = _distributedCache.GetString(currentUserId.ToString());
            int NoOfItem = Convert.ToInt32(_configuration["Setting:NumberOfItemNeedToBeCache"]);
            int pageSize = Convert.ToInt32(_configuration["Setting:PageSize"]);
            int totalQuestion = 0;
            string totalQuestionKey = "totalQuestion" + currentUserId.ToString();

            List<Question> questions= new List<Question>();
            if (!string.IsNullOrEmpty(userId))
            {

                var output = await _distributedCache.GetAsync<IEnumerable<Question>>(currentUserId.ToString());
                questions = output.Skip(pageSize * (pageIndex-1)).Take(pageSize).ToList();
                questions.ForEach(x => x.AvatarUrl = Url.Content(string.Format("~/Images/UserAvatars/{0}", x.Avatar)));
                questions.ForEach(x => x.CategoryImageUrl = Url.Content(string.Format("~/Images/Categories/{0}", x.ImageName)));

                string totalQues = await _distributedCache.GetAsync<string>(totalQuestionKey);
                totalQuestion = Convert.ToInt32(totalQues);

            }
            else
            {
                var questionsToCache = _questionService.GetQuestions(currentUserId, NoOfItem, 1, out totalQuestion);

                questions = questionsToCache.Skip(pageSize* (pageIndex-1)).Take(pageSize).ToList();

                questions.ForEach(x => x.AvatarUrl = Url.Content(string.Format("~/Images/UserAvatars/{0}", x.Avatar)));
                questions.ForEach(x => x.CategoryImageUrl = Url.Content(string.Format("~/Images/Categories/{0}", x.ImageName)));

                await _distributedCache.SetAsync<IEnumerable<Question>>(currentUserId.ToString(), questionsToCache, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2) });

                await _distributedCache.SetAsync<string>(totalQuestionKey, totalQuestion.ToString(), new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2) });

               
            }
            GetQuestionsResponse response = new GetQuestionsResponse()
            {
                Questions = questions,
                TotalQuestion = totalQuestion
            };

            return Ok(new { response = response });

        }
    }
}