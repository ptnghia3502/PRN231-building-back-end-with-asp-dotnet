using eBookStore.Services.InterfaceSerivce;
using eBookStore.Services.Utilities;
using eBookStore.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace eBookStore.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly JwtOption JwtOpt;

        public AuthController(IUserService userService, IOptions<JwtOption> options)
        {
            _userService = userService;
            JwtOpt = options.Value;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _userService.LoginAsync(model.Email, model.Password);
            if (result is not null)
            {
                var response = new LoginResponseModel
                {
                    User = result,
                    Token = result.GenerateToken(JwtOpt)
                };
                return Ok(response);
            }
            else
            {
                throw new Exception("Wrong email or password");
            }
        }
    }
}
