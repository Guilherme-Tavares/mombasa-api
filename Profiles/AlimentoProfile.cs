using AutoMapper;
using MombasaAPI.Dtos.Alimento;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles;

public class AlimentoProfile : Profile
{
    public AlimentoProfile()
    {
        CreateMap<AlimentoCreateDto, Alimento>();
        CreateMap<AlimentoUpdateDto, Alimento>();
        CreateMap<Alimento, AlimentoResponseDto>();
    }
}
