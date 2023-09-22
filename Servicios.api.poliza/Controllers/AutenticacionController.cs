using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Servicios.api.poliza.Models;
using System.Text;
using Servicios.api.poliza.Repositories;

namespace Servicios.api.poliza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacionController : Controller
    {
        private readonly string _secretKey;
        private IUsuarioRepository db = new UsuarioRepository();

        public AutenticacionController(IConfiguration configuration)
        {
            _secretKey = configuration.GetSection("settings").GetSection("secretkey").ToString();
        }

        [HttpPost]
        [Route("validar")]

        public IActionResult validar([FromBody] Usuario request)
        {
            Usuario user = db.ValidateUser(request);
            if (user != null)
            {
                var keyBytes = Encoding.ASCII.GetBytes(_secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.correo));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)

                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new {token = tokenCreado});
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
            }
        }
    }
}
