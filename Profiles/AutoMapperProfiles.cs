using AutoMapper;
using ColorPaletteGeneratorApi.Dtos;
using ColorPaletteGeneratorApi.Models;

namespace ColorPaletteGeneratorApi.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApiExceptionDto, ApiException>().ReverseMap();
            CreateMap<SignUpRequestDto, AppUser>();
            CreateMap<AppUser, GetCurrentAppUserResponseDto>();
            CreateMap<CreatePaletteRequestDto, Palette>();
            CreateMap<Palette, PaletteDto>();
        }
    }
}
