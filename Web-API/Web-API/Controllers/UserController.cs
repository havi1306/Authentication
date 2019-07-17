using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web_API.Entities;
using Web_API.Dtos;
using Web_API.Services;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }   

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }
        
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetUser(int? id)
        {
            var user = _userService.GetUser(id);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }
        
        [Route("create")]
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public ActionResult Create([FromBody]UserDto userDto)
        {
            if (userDto.UserName == "string" || userDto.Password == "string" || userDto.RoleId == 0)
            {
                return BadRequest(new { error = "The information is not enough to add"} );
            }
            
            var user = new User()
                {
                    UserName = userDto.UserName,
                    Password = userDto.Password,
                    RoleID = userDto.RoleId,
                };
            
            _userService.CreateUser(user);
            
            return Ok(new { success = "You created successfully"} );
        }
        
        [Authorize(Policy = "Admin")]
        [Route("update/{id}")]
        [HttpPut]
        public IActionResult Update(int? id, [FromBody]UserDto userDto)
        {
            var user = _userService.GetUserById(id);
            var userName = userDto.UserName;
            var password = userDto.Password;
            var roleId = userDto.RoleId;

            if (userName == "string")
            {
                userName = user.UserName;
            }

            if (password == "string")
            {
                password = user.Password;
            }

            if (roleId == 0)
            {
                roleId = user.RoleID;
            }
            if (user != null)
            {
                user.UserName = userName;
                user.Password = password;
                user.RoleID = roleId;
            
                _userService.UpdateUser(user);
                return Ok(new { success = "You updated successfully"} );
            }

            return BadRequest(new { error = "User is not exist"} );
        }

        [Authorize(Policy = "Admin")]
        [Route("delecte/{id}")]
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var user = _userService.GetUserById(id);

            if (user != null)
            {
                _userService.DelectUser(user);
                return Ok(new { success = "You delected successully"} );
            }

            return BadRequest(new { error = "User is not exist"} );
        }
    }
}