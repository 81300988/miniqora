using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Quora.BLL.Interface;
using Quora.Model;
using QuoraAPI.JwtAuthentication;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Web;

namespace QuoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    { 
        private readonly IUserService _userService;
        private static IConfiguration _config;
        
        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody]LoginModel login)
        {            
            var user = _userService.Login(login);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" }); 

            var tokenString = JwtAthentication.GenerateJSONWebToken(_config["Jwt:Key"], _config["Jwt:Issuer"], user.Email, user.UserId);
            return Ok(new { token = tokenString });
        }

        [HttpGet]
        [Route("profile")]
        public IActionResult Profile()
        {
            string tokenString = Request.Headers["Authorization"];

            try
            {
                int userId = JwtAthentication.GetCurrentUserId(tokenString);
                var user = _userService.GetUserById(userId);
                user.AvatarURL = Url.Content(string.Format("~/Images/UserAvatars/{0}", user.Avatar));

                return Ok(user);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] Register user)
        {
            var newUser = _userService.Register(user);
            if (newUser == null)
            {
                return BadRequest(new { message = "Email Used" });
            }
            return Ok(newUser);
        }
    }
}