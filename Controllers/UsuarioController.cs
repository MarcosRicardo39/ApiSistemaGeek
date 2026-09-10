using ApiSistemaGeek.Data;
using ApiSistemaGeek.DTOs;
using ApiSistemaGeek.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public UsuarioController(
        AppDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
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

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Nome),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.TipoUsuario)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!
            )
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return Ok(new
        {
            token = tokenString,
            user.Id,
            user.Nome,
            user.Email,
            user.TipoUsuario
        });
    }
}