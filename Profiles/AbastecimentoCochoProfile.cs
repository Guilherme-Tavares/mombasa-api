using AutoMapper;
using MombasaAPI.Dtos.AbastecimentoCocho;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles;

public class AbastecimentoCochoProfile : Profile
{
    public AbastecimentoCochoProfile()
    {
        CreateMap<AbastecimentoCochoCreateDto, AbastecimentoCocho>();
        CreateMap<AbastecimentoCochoUpdateDto, AbastecimentoCocho>();
        CreateMap<AbastecimentoCocho, AbastecimentoCochoResponseDto>();
    }
}
