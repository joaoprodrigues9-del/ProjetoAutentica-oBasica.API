using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoAutenticaçãoBasica.API.Data;
using ProjetoAutenticaçãoBasica.API.Models;
using ProjetoAutenticaçãoBasica.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ProjetoAutenticaçãoBasica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            var novaUsuario = new Usuario
            {
                Nome = registerDto.Nome,
                Username = registerDto.Username,
                Password = registerDto.Password,
                CreatedAt = DateTime.Now
            };

            _context.Usuarios.Add(novaUsuario);
            await _context.SaveChangesAsync();
            
            return Ok("Usuário registrado com sucesso.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.Password == loginDto.Password);

            if (usuario == null)
            {
                return NotFound("Credenciais inválidas.");
            }

            return Ok(new { Nome = usuario.Nome });
        }
    }
}
