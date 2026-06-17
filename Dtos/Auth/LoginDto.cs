using System.ComponentModel.DataAnnotations;

namespace MombasaAPI.Dtos.Auth;

public class LoginDto
{
    [Required, EmailAddress]
    public required string Email { get; set; }

    [Required, MinLength(6)]
    public required string Senha { get; set; }
}