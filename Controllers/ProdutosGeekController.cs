using ApiSistemaGeek.Data;
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
            var dados = await _context.ProdutosGeek.ToListAsync();
            return Ok(dados);
        }
    }
}