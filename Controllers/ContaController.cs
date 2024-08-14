using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemCrud.Models;
using SistemCrud.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly UsuarioServices _usuarioServices;

        public ContaController(UsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel loginModel)
        {
            var user = await _usuarioServices.BuscarPorUUIDUserNameESenha(loginModel.Login, loginModel.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Credenciais inválidas." });
            }

            var token = GerarTokenJWT(user);
            return Ok(new { token });
        }
        private string GerarTokenJWT(User user)
        {
            string chaveSecreta = "e3c46810-b96e-40d9-a9eb-9ffa7b373e5b";
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("login", user.UUIDUserName),
                new Claim("nome", user.UUIDUserName)
            };

            var token = new JwtSecurityToken(
                issuer: "system_tasks",
                audience: "seus_usuarios",
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credencial
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
