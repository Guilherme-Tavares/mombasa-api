namespace MombasaAPI.Dtos.Divisao;

public class DivisaoUpdateDto : DivisaoCreateDto
{
    public bool Ativa { get; set; } = true;
}
