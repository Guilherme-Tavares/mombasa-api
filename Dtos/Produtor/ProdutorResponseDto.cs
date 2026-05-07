namespace MombasaAPI.Dtos.Produtor
{
    // Senha excluída intencionalmente para não expor credenciais na resposta
    public class ProdutorResponseDto
    {
        public required string Id { get; set; }
        public required string Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
