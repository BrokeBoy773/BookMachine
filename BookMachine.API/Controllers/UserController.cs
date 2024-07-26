using BookMachine.API.Contracts.Requests.UserRequests;
using BookMachine.Core.Interfaces.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookMachine.API.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost ("Registration", Name = "Registration")]
        public async Task<IActionResult> UserRegistrationAsync([FromBody] UserRegistrationRequest request)
        {
            await _userService.UserRegistrationAsync(request.UserName, request.Email, request.Password);

            return Ok();
        }

        [HttpPost("Login", Name = "Login")]
        public async Task<IActionResult> UserLoginAsync([FromBody]UserLoginRequest request)
        {
            var token = await _userService.UserLoginAsync(request.Email, request.Password);

            HttpContext.Response.Cookies.Append("cookies", token);

            return Ok();
        }

        [HttpPost("Logout", Name = "Logout")]
        public IActionResult UserLogout()
        {
            HttpContext.Response.Cookies.Delete("cookies");

            return Ok();
        }
    }
}
