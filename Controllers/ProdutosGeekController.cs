using ApiSistemaGeek.Data;
using ApiSistemaGeek.Model;
using Microsoft.AspNetCore.Authorization;
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


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoGeek produto)
        {
            _context.ProdutoGeek.Add(produto);
            await _context.SaveChangesAsync();

            var estoque = new Estoque
            {
                ProdutoGeekId = produto.Id,
                Quantidade = 0
            };

            _context.Estoques.Add(estoque);
            await _context.SaveChangesAsync();

            return Ok(produto);
        }
        [Authorize]
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

        [Authorize]
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