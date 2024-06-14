using ColorPaletteGeneratorApi.Dtos;
using ColorPaletteGeneratorApi.Extentions;
using ColorPaletteGeneratorApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ColorPaletteGeneratorApi.Controllers
{
    public class AppUsersController(IAppUsersServices appUsersServices) : BaseController
    {
        private readonly IAppUsersServices _appUsersServices = appUsersServices;

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<SignInResponseDto>> SignUp(SignUpRequestDto request)
        {
            return Ok(await _appUsersServices.SignUp(request));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<SignInResponseDto>> SignIn(SignInRequestDto request)
        {
            return Ok(await _appUsersServices.SignIn(request));
        }

        [HttpGet]
        public async Task<GetCurrentAppUserResponseDto> GetCurrentAppUser() => await _appUsersServices.GetCurrentAppUser(User.GetAppUserId());

    }
}
