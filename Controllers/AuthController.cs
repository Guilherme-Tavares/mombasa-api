using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MombasaAPI.Dtos.Auth;
using MombasaAPI.Exceptions;
using MombasaAPI.Models;
using MombasaAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MombasaAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/auth")]
public class AuthController : ControllerBase
{
    private readonly ProdutorService _service;
    private readonly IPasswordHasher<Produtor> _hasher;
    private readonly IConfiguration _config;

    public AuthController(ProdutorService service, IPasswordHasher<Produtor> hasher, IConfiguration config)
    {
        _service = service;
        _hasher = hasher;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var produtor = await _service.FindByEmail(dto.Email);

            if (produtor is null || produtor.Senha is null)
                throw new ServiceException(
                    "Credenciais inválidas",
                    c => c.Unauthorized(new { message = "E-mail ou senha inválidos" })
                );

            var result = _hasher.VerifyHashedPassword(produtor, produtor.Senha, dto.Senha);
            if (result == PasswordVerificationResult.Failed)
                throw new ServiceException(
                    "Credenciais inválidas",
                    c => c.Unauthorized(new { message = "E-mail ou senha inválidos" })
                );

            var token = GerarToken(produtor);
            return Ok(new AuthResponseDto
            {
                Token = token,
                Nome = produtor.Nome,
                Email = produtor.Email!
            });
        }
        catch (ServiceException e)
        {
            return e.ToActionResult(this);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    private string GerarToken(Produtor produtor)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? string.Empty));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, produtor.Id),
            new Claim(JwtRegisteredClaimNames.Name, produtor.Nome),
            new Claim(JwtRegisteredClaimNames.Email, produtor.Email!)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}