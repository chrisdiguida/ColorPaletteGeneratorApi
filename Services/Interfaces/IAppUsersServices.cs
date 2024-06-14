using ColorPaletteGeneratorApi.Dtos;

namespace ColorPaletteGeneratorApi.Services.Interfaces
{
    public interface IAppUsersServices
    {
        Task<GetCurrentAppUserResponseDto> GetCurrentAppUser(Guid appUserRequesterId);
        Task<SignInResponseDto> SignIn(SignInRequestDto request);
        Task<SignInResponseDto> SignUp(SignUpRequestDto request);
    }
}