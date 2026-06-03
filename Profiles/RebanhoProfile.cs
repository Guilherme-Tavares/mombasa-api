using AutoMapper;
using MombasaAPI.Dtos.Rebanho;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles;

public class RebanhoProfile : Profile
{
    public RebanhoProfile()
    {
        CreateMap<RebanhoCreateDto, Rebanho>();
        CreateMap<RebanhoUpdateDto, Rebanho>();
        CreateMap<Rebanho, RebanhoResponseDto>();
    }
}
