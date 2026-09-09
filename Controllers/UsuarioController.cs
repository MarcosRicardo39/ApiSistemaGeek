using ApiSistemaGeek.Data;
using ApiSistemaGeek.DTOs;
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
    public async Task<IActionResult> Post([FromBody] CadastroUsuarioDTO cadastro)
    {
        if (cadastro == null)
            return BadRequest();

        var usuario = new Usuario
        {
            Nome = cadastro.Nome,
            Email = cadastro.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(cadastro.Senha),
            TipoUsuario = "Usuario"
        };

        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            usuario.Id,
            usuario.Nome,
            usuario.Email,
            usuario.TipoUsuario
        });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginUsuarioDTO login)
    {
        var user = _context.Usuarios
            .FirstOrDefault(x => x.Email == login.Email);

        if (user == null)
            return Unauthorized("Usuário ou senha inválidos");

        bool senhaValida = BCrypt.Net.BCrypt.Verify(
            login.Senha,
            user.SenhaHash
        );

        if (!senhaValida)
            return Unauthorized("Usuário ou senha inválidos");

        return Ok(new
        {
            user.Id,
            user.Nome,
            user.Email,
            user.TipoUsuario
        });
    }
}