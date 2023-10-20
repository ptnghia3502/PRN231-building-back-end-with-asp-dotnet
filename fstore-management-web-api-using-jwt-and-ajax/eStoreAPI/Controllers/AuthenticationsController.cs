using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Services.ViewModels;

namespace eStoreAPI.Controllers
{
    public class AuthenticationsController : BaseController
    {
        private readonly IMemberService _memberService;
        private readonly string _secretKey = "ThisIsMySecretKeyNoOneCantKnowThisAhihi";
        private readonly IConfiguration _configuration;

        public AuthenticationsController(IMemberService memberService, IConfiguration configuration)
        {
            _memberService = memberService;
            _configuration = configuration;
        }

        /// <summary>
        /// Login.
        /// </summary>
        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginView memberDto)
        {
            MemberView? member = null;
            if (memberDto.Email == _configuration.GetSection("AdminCredentials")["Email"] &&
                memberDto.Password == _configuration.GetSection("AdminCredentials")["Password"])
            {
                member = new MemberView
                {
                    Email = memberDto.Email,
                    Password = memberDto.Password,
                };
            }
            else
            {
                member = await _memberService.GetMemberByEmail(memberDto.Email, memberDto.Password);
                if (member == null)
                {
                    return NotFound("Invalid email or password");
                }
            }

            var token = GenerateJwtToken(member);
            var response = new
            {
                Member = member,
                Token = token
            };

            return Ok(response);
        }

        private string GenerateJwtToken(MemberView member)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, member.Email)
            };

            if (member.Email.Equals("admin@estore.com"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Member"));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
