namespace MombasaAPI.Dtos.Bovino
{
    public class BovinoUpdateDto : BovinoCreateDto
    {
        public bool Ativo { get; set; } = true;
    }
}
