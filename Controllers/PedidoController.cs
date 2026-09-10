using ApiSistemaGeek.Data;
using ApiSistemaGeek.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ApiSistemaGeek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidoController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<ProdutoGeek> carrinho)
        {
            if (carrinho == null || carrinho.Count == 0)
                return BadRequest("Carrinho vazio.");

            foreach (var produto in carrinho)
            {
                var estoque = await _context.Estoques
                    .FirstOrDefaultAsync(e => e.ProdutoGeekId == produto.Id);

                if (estoque == null)
                    return BadRequest($"Produto {produto.Id} não possui estoque.");

                if (estoque.Quantidade < 1)
                    return BadRequest($"Produto {produto.Id} está sem estoque.");
            }

            
            decimal total = carrinho.Sum(p => p.Preco);

            var pedido = new Pedido
            {
                Data = DateTime.Now,
                Total = total
            };

            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();

           
            var itens = new List<PedidoItem>();

            foreach (var produto in carrinho)
            {
                var estoque = await _context.Estoques
                    .FirstAsync(e => e.ProdutoGeekId == produto.Id);

                estoque.Quantidade -= 1;

                itens.Add(new PedidoItem
                {
                    PedidoId = pedido.Id,
                    ProdutoGeekId = produto.Id,
                    Quantidade = 1,
                    Preco = produto.Preco
                });
            }

            await _context.PedidoItens.AddRangeAsync(itens);
            await _context.SaveChangesAsync();

            return Ok(pedido);
        }
    }
}