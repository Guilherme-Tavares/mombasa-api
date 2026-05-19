using AutoMapper;
using MombasaAPI.Dtos.Medicamento;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles;

public class MedicamentoProfile : Profile
{
    public MedicamentoProfile()
    {
        CreateMap<MedicamentoCreateDto, Medicamento>();
        CreateMap<MedicamentoUpdateDto, Medicamento>();
        CreateMap<Medicamento, MedicamentoResponseDto>();
    }
}
