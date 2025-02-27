using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.Data.Models;
using TrabajoPracticoP3.Services.Implementations;
using TrabajoPracticoP3.Services.Interfaces;



[ApiController]
[Route("api/[controller]")]
public class AuthenticateController : ControllerBase
{
    private readonly IUserServices _userServices;
    private readonly IConfiguration _config;

    public AuthenticateController(IUserServices userServices, IConfiguration config)
    {
        _userServices = userServices;
        _config = config;
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] CredentialsDto credentialsDto)
    {
        // Paso 1: Validamos las credenciales
        BaseResponse validarUsuarioResult = _userServices.ValidarUsuario(credentialsDto.Email, credentialsDto.Password);

        if (validarUsuarioResult.Message == "wrong email")
        {
            return BadRequest(validarUsuarioResult.Message);
        }
        else if (validarUsuarioResult.Message == "wrong password")
        {
            return Unauthorized();
        }

        if (validarUsuarioResult.Result)
        {
            User user = _userServices.GetByEmail(credentialsDto.Email);
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));
            var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("Name", user.Name),
                new Claim(ClaimTypes.Role, user.UserType.ToString())

            };

            var jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signature
            );

            string tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(tokenToReturn);
        }

        // En caso de que no se cumplan las condiciones anteriores
        return BadRequest("Invalid credentials");
    }
}


