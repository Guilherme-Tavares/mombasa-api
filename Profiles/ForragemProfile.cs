using AutoMapper;
using MombasaAPI.Dtos.Forragem;
using MombasaAPI.Models;

namespace MombasaAPI.Profiles;

public class ForragemProfile : Profile
{
    public ForragemProfile()
    {
        CreateMap<ForragemCreateDto, Forragem>();
        CreateMap<ForragemUpdateDto, Forragem>();
        CreateMap<Forragem, ForragemResponseDto>();
    }
}
