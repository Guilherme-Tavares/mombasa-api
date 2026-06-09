using AutoMapper;
using MombasaAPI.Dtos.Lotacao;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles;

public class LotacaoProfile : Profile
{
    public LotacaoProfile()
    {
        CreateMap<LotacaoCreateDto, Lotacao>();
        CreateMap<LotacaoUpdateDto, Lotacao>();
        CreateMap<Lotacao, LotacaoResponseDto>();
    }
}
