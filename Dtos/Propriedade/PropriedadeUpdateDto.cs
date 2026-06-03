namespace MombasaAPI.Dtos.Propriedade;

public class PropriedadeUpdateDto : PropriedadeCreateDto
{
    public bool Ativa { get; set; } = true;
}
