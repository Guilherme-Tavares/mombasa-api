namespace MombasaAPI.Dtos.Rebanho;

public class RebanhoUpdateDto : RebanhoCreateDto
{
    public bool Ativo { get; set; } = true;
}
