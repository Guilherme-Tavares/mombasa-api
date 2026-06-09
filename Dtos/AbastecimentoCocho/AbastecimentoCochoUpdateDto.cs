namespace MombasaAPI.Dtos.AbastecimentoCocho;

public class AbastecimentoCochoUpdateDto : AbastecimentoCochoCreateDto
{
    public bool Esgotado { get; set; } = false;
}
