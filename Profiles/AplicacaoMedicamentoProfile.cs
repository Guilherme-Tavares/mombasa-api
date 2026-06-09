using AutoMapper;
using MombasaAPI.Dtos.AplicacaoMedicamento;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles;

public class AplicacaoMedicamentoProfile : Profile
{
    public AplicacaoMedicamentoProfile()
    {
        CreateMap<AplicacaoMedicamentoCreateDto, AplicacaoMedicamento>();
        CreateMap<AplicacaoMedicamentoUpdateDto, AplicacaoMedicamento>();
        CreateMap<AplicacaoMedicamento, AplicacaoMedicamentoResponseDto>();
    }
}
