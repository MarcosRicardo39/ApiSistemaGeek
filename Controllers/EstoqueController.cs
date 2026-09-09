using ApiSistemaGeek.Data;
using ApiSistemaGeek.DTOs;
using ApiSistemaGeek.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSistemaGeek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstoqueController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Entrada([FromBody] EntradaEstoqueDTO entrada)
        {
            if (entrada.Quantidade <= 0)
                return BadRequest("A quantidade deve ser maior que zero.");

            var produto = await _context.ProdutoGeek
                .FindAsync(entrada.ProdutoId);

            if (produto == null)
                return NotFound("Produto não encontrado.");

            var estoque = await _context.Estoques
                .FirstOrDefaultAsync(e => e.ProdutoGeekId == entrada.ProdutoId);

            if (estoque == null)
            {
                estoque = new Estoque
                {
                    ProdutoGeekId = entrada.ProdutoId,
                    Quantidade = entrada.Quantidade
                };

                _context.Estoques.Add(estoque);
            }
            else
            {
                estoque.Quantidade += entrada.Quantidade;
            }

            await _context.SaveChangesAsync();

            return Ok(estoque);
        }
    }
}