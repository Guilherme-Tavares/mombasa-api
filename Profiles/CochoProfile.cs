using AutoMapper;
using MombasaAPI.Dtos.Cocho;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles;

public class CochoProfile : Profile
{
    public CochoProfile()
    {
        CreateMap<CochoCreateDto, Cocho>();
        CreateMap<CochoUpdateDto, Cocho>();
        CreateMap<Cocho, CochoResponseDto>();
    }
}
