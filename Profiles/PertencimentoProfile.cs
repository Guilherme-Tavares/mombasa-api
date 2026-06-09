using AutoMapper;
using MombasaAPI.Dtos.Pertencimento;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles;

public class PertencimentoProfile : Profile
{
    public PertencimentoProfile()
    {
        CreateMap<PertencimentoCreateDto, Pertencimento>();
        CreateMap<PertencimentoUpdateDto, Pertencimento>();
        CreateMap<Pertencimento, PertencimentoResponseDto>();
    }
}
