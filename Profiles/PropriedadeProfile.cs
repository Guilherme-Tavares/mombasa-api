using AutoMapper;
using MombasaAPI.Dtos.Propriedade;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles;

public class PropriedadeProfile : Profile
{
    public PropriedadeProfile()
    {
        CreateMap<PropriedadeCreateDto, Propriedade>();
        CreateMap<PropriedadeUpdateDto, Propriedade>();
        CreateMap<Propriedade, PropriedadeResponseDto>();
    }
}
