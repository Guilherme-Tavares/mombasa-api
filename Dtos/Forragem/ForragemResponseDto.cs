namespace MombasaAPI.Dtos.Forragem;

public class ForragemResponseDto
{
    public required string Id { get; set; }
    public required string DivisaoId { get; set; }
    public required string Tipo { get; set; }
    public DateOnly? DataPlantio { get; set; }
    public bool Ativa { get; set; }
}
