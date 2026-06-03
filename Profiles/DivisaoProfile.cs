using AutoMapper;
using MombasaAPI.Dtos.Divisao;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles;

public class DivisaoProfile : Profile
{
    public DivisaoProfile()
    {
        CreateMap<DivisaoCreateDto, Divisao>();
        CreateMap<DivisaoUpdateDto, Divisao>();
        CreateMap<Divisao, DivisaoResponseDto>();
    }
}
