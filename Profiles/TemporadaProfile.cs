using AutoMapper;
using MombasaAPI.Dtos.Temporada;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles;

public class TemporadaProfile : Profile
{
    public TemporadaProfile()
    {
        CreateMap<TemporadaCreateDto, Temporada>();
        CreateMap<TemporadaUpdateDto, Temporada>();
        CreateMap<Temporada, TemporadaResponseDto>();
    }
}
