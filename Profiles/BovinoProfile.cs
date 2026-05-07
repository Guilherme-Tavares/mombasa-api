using AutoMapper;
using MombasaAPI.Dtos.Bovino;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles
{
    public class BovinoProfile : Profile
    {
        public BovinoProfile()
        {
            // DTO usa 'BovinoOrigem', Model usa 'Origem' — mapeamento explícito necessário
            CreateMap<BovinoCreateDto, Bovino>()
                .ForMember(dest => dest.Origem, opt => opt.MapFrom(src => src.BovinoOrigem));

            CreateMap<BovinoUpdateDto, Bovino>()
                .ForMember(dest => dest.Origem, opt => opt.MapFrom(src => src.BovinoOrigem));

            CreateMap<Bovino, BovinoResponseDto>();
        }
    }
}
