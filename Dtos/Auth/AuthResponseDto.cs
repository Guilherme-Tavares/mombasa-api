namespace MombasaAPI.Dtos.Auth;

public class AuthResponseDto
{
    public required string Token { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
}