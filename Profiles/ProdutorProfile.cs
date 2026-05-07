using AutoMapper;
using MombasaAPI.Dtos.Produtor;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles
{
    public class ProdutorProfile : Profile
    {
        public ProdutorProfile()
        {
            CreateMap<ProdutorCreateDto, Produtor>();
            CreateMap<ProdutorUpdateDto, Produtor>();

            // Senha é ignorada no mapeamento inverso — não exposta na resposta
            CreateMap<Produtor, ProdutorResponseDto>();
        }
    }
}
