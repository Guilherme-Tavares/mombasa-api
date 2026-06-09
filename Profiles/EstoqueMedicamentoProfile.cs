using AutoMapper;
using MombasaAPI.Dtos.EstoqueMedicamento;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles;

public class EstoqueMedicamentoProfile : Profile
{
    public EstoqueMedicamentoProfile()
    {
        CreateMap<EstoqueMedicamentoCreateDto, EstoqueMedicamento>();
        CreateMap<EstoqueMedicamentoUpdateDto, EstoqueMedicamento>();
        CreateMap<EstoqueMedicamento, EstoqueMedicamentoResponseDto>();
    }
}
