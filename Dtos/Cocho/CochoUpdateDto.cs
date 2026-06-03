namespace MombasaAPI.Dtos.Cocho;

public class CochoUpdateDto : CochoCreateDto
{
    public bool Ativo { get; set; } = true;
}
