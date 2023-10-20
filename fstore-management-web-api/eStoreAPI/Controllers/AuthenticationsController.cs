using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.ViewModels;

namespace eStoreAPI.Controllers
{
    public class AuthenticationsController : BaseController
    {
        private readonly IMemberService _memberService;
        public AuthenticationsController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        /// <summary>
        /// Login.
        /// </summary>
        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginView memberDto)
        {
            var member = await _memberService.GetMemberByEmail(memberDto.Email, memberDto.Password);
            if (member == null)
            {
                return NotFound("Invalid email or password");
            }
            return Ok(member);
        }
    }
}
