using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoAutenticaçãoBasica.API.Data;
using ProjetoAutenticaçãoBasica.API.Models;
using ProjetoAutenticaçãoBasica.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ProjetoAutenticaçãoBasica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VoosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarVoo(CriarVooDTO dto)
        {
            var novoVoo = new Voo
            {
                Codigo = dto.Codigo,
                Origem = dto.Origem,
                Destino = dto.Destino
            };

            _context.Voos.Add(novoVoo);
            await _context.SaveChangesAsync();

            return Ok("Voo criado com sucesso.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResumoVooDTO>>> GetVoos()
        {
            var voos = await _context.Voos.AsNoTracking().OrderBy(v => v.Codigo).Select(v => new ResumoVooDTO
                {
                    Id = v.Id,
                    Codigo = v.Codigo,
                    Trajeto = v.Origem + " -> " + v.Destino
            }).ToListAsync();
            return Ok(voos);
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<VooManisfestoDTO>> GetVooPorCodigo(int codigo)
        {
            var manisfesto = await _context.Voos.AsNoTracking().Where(v => v.Codigo == codigo).Select(v => new VooManisfestoDTO
            {
                Id = v.Id,
                Codigo = v.Codigo,
                Trajeto = v.Origem + " -> " + v.Destino,
                Passageiro = v.Passagens.Select(p => new PassageiroNoVooDTO
                {
                    Nome = p.Passageiro.Nome,
                    Assento = p.Assento
                }).ToList()
            }).FirstOrDefaultAsync();
            if (manisfesto is null)
            {
                return NotFound("Voo não encontrado.");
            }
            return Ok(manisfesto);
        }
    }
}
