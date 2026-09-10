using ApiSistemaGeek.Data;
using ApiSistemaGeek.DTOs;
using ApiSistemaGeek.Model;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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
        [Authorize]
        [HttpPost("saida")]
        public async Task<IActionResult> Saida([FromBody] EntradaEstoqueDTO saida)
        {
            if (saida.Quantidade <= 0)
                return BadRequest("A quantidade de saída deve ser maior que zero.");

            var produto = await _context.ProdutoGeek
                .FindAsync(saida.ProdutoId);

            if (produto == null)
                return NotFound("Produto não encontrado.");

            var estoque = await _context.Estoques
                .FirstOrDefaultAsync(e => e.ProdutoGeekId == saida.ProdutoId);

            if (estoque == null)
                return NotFound("Produto não possui estoque.");

            if (saida.Quantidade > estoque.Quantidade)
                return BadRequest("Quantidade de saída maior que o estoque disponível.");

            estoque.Quantidade -= saida.Quantidade;

            await _context.SaveChangesAsync();

            return Ok(estoque);
        }
        [Authorize]
        [HttpGet("{produtoId}")]
        public async Task<IActionResult> Consultar(int produtoId)
        {
            var produto = await _context.ProdutoGeek
                .FindAsync(produtoId);

            if (produto == null)
                return NotFound("Produto não encontrado.");

            var estoque = await _context.Estoques
                .FirstOrDefaultAsync(e => e.ProdutoGeekId == produtoId);

            if (estoque == null)
                return NotFound("Produto não possui estoque.");

            return Ok(estoque);
        }
    }
}