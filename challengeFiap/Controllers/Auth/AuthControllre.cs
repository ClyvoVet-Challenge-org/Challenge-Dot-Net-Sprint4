using challengeFiap.Application.Service.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace challengeFiap.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthControllre : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthControllre(TokenService token)
        {
            _tokenService = token;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLoginAux user)
        {
            var token = _tokenService.GenerateToken(user.Username);
            return Ok(new { token });
        }


        [HttpGet("dados-protegidos")]
        [Authorize]
        public IActionResult GetDadosProtegidos()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok($"Usuario: {username}, está autenticado");
        }
        public class UserLoginAux()
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
