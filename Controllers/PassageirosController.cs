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
    public class PassageirosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PassageirosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPassageiro(CriarPassageiroDTO dto)
        {
            var novoPassageiro = new Passageiro
            {
                Nome = dto.Nome,
                CPF = dto.CPF
            };
            _context.Passageiros.Add(novoPassageiro);
            await _context.SaveChangesAsync();
            return Ok( new { Mensagem = "Passageiro criado com sucesso." });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassageiroInfoDTO>>> GetPassageiros([FromQuery] string? nome)
        {
            var passageiros = await _context.Passageiros.AsNoTracking().Where(p => string.IsNullOrEmpty(nome) || p.Nome.Contains(nome)).OrderBy(p => p.Nome).Select(p => new PassageiroInfoDTO
            {
                Id = p.Id,
                Nome = p.Nome
            }).ToListAsync();
            return Ok(passageiros);
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<PassageiroInfoDTO>> GetPassageiroPorCPF(string cpf)
        {
            var passageiro = await _context.Passageiros.AsNoTracking().Where(p => p.CPF == cpf).Select(p => new PassageiroInfoDTO
            {
                Id = p.Id,
                Nome = p.Nome
            }).FirstOrDefaultAsync();
            if (passageiro is null)
            {
                return NotFound("Passageiro não encontrado.");
            }
            return Ok(passageiro);
        }
    }
}
