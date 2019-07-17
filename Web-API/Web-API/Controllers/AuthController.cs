using Microsoft.AspNetCore.Mvc;
using Web_API.Services;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService) 
        {
            _userService = userService;
        }
        
        [Route("token")]
        [HttpPost]
        public IActionResult GetToken(string userName, string password)
        {
            var user = _userService.GetUser(userName, password);

            if (user != null)
            {
                var tokenString = _userService.GenerateJSONWebToken(user);
               
                return Ok(new { token = tokenString });
            }

            return NotFound();
        }
    }
}