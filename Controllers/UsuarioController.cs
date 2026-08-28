using ApiSistemaGeek.Data;
using ApiSistemaGeek.DTOs.ApiSistemaGeek.DTOs;
using ApiSistemaGeek.Model;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Usuario usuario)
    {
        if (usuario == null)
            return BadRequest();

        usuario.TipoUsuario = "Usuario";

        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();

        return Ok(usuario);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginUsuarioDTO login)
    {
        var user = _context.Usuarios
            .FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);

        if (user == null)
            return Unauthorized("Usuário ou senha inválidos");

        return Ok(user);
    }
}