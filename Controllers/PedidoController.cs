using ApiSistemaGeek.Data;
using ApiSistemaGeek.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<ProdutoGeek> carrinho)
        {
            if (carrinho == null || carrinho.Count == 0)
                return BadRequest("Carrinho vazio.");

            decimal total = carrinho.Sum(p => p.Preco);

            var pedido = new Pedido
            {
                Data = DateTime.Now,
                Total = total
            };

            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();

            var itens = carrinho.Select(p => new PedidoItem
            {
                PedidoId = pedido.Id,
                ProdutoGeekId = p.Id,
                Quantidade = 1,
                Preco = p.Preco
            }).ToList();

            await _context.PedidoItens.AddRangeAsync(itens);
            await _context.SaveChangesAsync();

            return Ok(pedido);
        }
    }
}