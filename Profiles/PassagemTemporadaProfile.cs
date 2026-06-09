using AutoMapper;
using MombasaAPI.Dtos.PassagemTemporada;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles;

public class PassagemTemporadaProfile : Profile
{
    public PassagemTemporadaProfile()
    {
        CreateMap<PassagemTemporadaCreateDto, PassagemTemporada>();
        CreateMap<PassagemTemporadaUpdateDto, PassagemTemporada>();
        CreateMap<PassagemTemporada, PassagemTemporadaResponseDto>();
    }
}
