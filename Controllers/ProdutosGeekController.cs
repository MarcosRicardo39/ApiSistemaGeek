using ApiSistemaGeek.Data;
using ApiSistemaGeek.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSistemaGeek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosGeekController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosGeekController(AppDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dados = await _context.ProdutoGeek.ToListAsync();
            return Ok(dados);
        }

       
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoGeek produto)
        {
            if (produto == null)
                return BadRequest();

            await _context.ProdutoGeek.AddAsync(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProdutoGeek produto)
        {
            if (id != produto.Id)
                return BadRequest("ID não corresponde");

            var existe = await _context.ProdutoGeek.FindAsync(id);
            if (existe == null)
                return NotFound();

            existe.Nome = produto.Nome;
            existe.Preco = produto.Preco;
            existe.Descricao = produto.Descricao;

            _context.ProdutoGeek.Update(existe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _context.ProdutoGeek.FindAsync(id);

            if (produto == null)
                return NotFound();

            _context.ProdutoGeek.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}