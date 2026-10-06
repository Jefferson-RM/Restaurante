using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        string? adminUsuario = _config["AdminUsuario"];
        string? adminContrasena = _config["AdminContrasena"];

        if (request.Usuario != adminUsuario || request.Contrasena != adminContrasena)
        {
            return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });
        }

        string token = GenerarToken();

        return Ok(new { token });
    }

    private string GenerarToken()
    {
        string clave = _config["Jwt:Clave"]!;
        string emisor = _config["Jwt:Emisor"]!;

        SymmetricSecurityKey llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave));
        SigningCredentials credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "admin")
        };

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: emisor,
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: credenciales
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}