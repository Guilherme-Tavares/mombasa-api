namespace MombasaAPI.Dtos.Forragem;

public class ForragemUpdateDto : ForragemCreateDto
{
    public bool Ativa { get; set; } = true;
}
