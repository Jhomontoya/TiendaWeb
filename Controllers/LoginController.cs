using Microsoft.AspNetCore.Mvc;
using WebAppTienda.Models;
using WebAppTienda.Datos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using WebAppTienda.Datos.WebAppTienda.Data;

namespace WebAppTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public LoginController(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(LoginUser userLogin)
        {
            var user = _context.Usuarios.FirstOrDefault(u =>
                u.Nombre.ToLower() == userLogin.Nombre.ToLower() &&
                u.Clave == userLogin.Clave &&
                u.Estado.ToLower() == "activo");

            if (user != null)
            {
                var token = GenerateToken(user);

                // Registrar login
                _context.LoginRegistros.Add(new LoginRegistro
                {
                    UsuarioNombre = user.Nombre,
                    FechaHora = DateTime.Now
                });
                _context.SaveChanges();

                return Ok(new { token });
            }

            return NotFound("Usuario no encontrado o inactivo.");
        }

        private string GenerateToken(Usuario user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["WebAppTienda:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Roles)
            };

            var token = new JwtSecurityToken(
                _config["WebAppTienda:Issuer"],
                _config["WebAppTienda:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
