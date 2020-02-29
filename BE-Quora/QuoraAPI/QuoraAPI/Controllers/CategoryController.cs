using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Quora.BLL.Interface;
using Quora.Model;
using QuoraAPI.JwtAuthentication;

namespace QuoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _configuration;
        public CategoryController(ICategoryService categoryService, IConfiguration configuration)
        {
            _categoryService = categoryService;
            _configuration = configuration;
        }

        [HttpGet()]
        [Route("GetCategories")]
        public List<Category> GetCategories()
        {
            var categories = _categoryService.GetCategories();

            categories.ForEach(x => x.ImageName = Url.Content(string.Format("~/Images/Categories/{0}", x.ImageName)));

            return categories;
        }
        [HttpGet("{id}/{pageIndex}")]
        [Route("GetCategotyPage")]
        public IActionResult GetCategotyPage([FromQuery] int id, [FromQuery] int pageIndex = 1)
        {
            int pageSize = Convert.ToInt32(_configuration["Setting:PageSize"]);

            int totalQuestion = 0;

            string tokenString = Request.Headers["Authorization"];
            int currentUserId = JwtAthentication.GetCurrentUserId(tokenString);

            var questions = _categoryService.GetCategotyPage(currentUserId, id, pageSize, pageIndex, out totalQuestion);

            questions.ForEach(x => x.AvatarUrl = Url.Content(string.Format("~/Images/UserAvatars/{0}", x.Avatar)));
            questions.ForEach(x => x.CategoryImageUrl = Url.Content(string.Format("~/Images/Categories/{0}", x.ImageName)));

            GetQuestionbyCategoryResponse response = new GetQuestionbyCategoryResponse()
            {
                Questions = questions,
                TotalQuestion = totalQuestion
            };
            return Ok(new { response = response });
        }
    }
}