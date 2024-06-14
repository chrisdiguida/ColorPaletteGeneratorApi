using AutoMapper;
using ColorPaletteGeneratorApi.Data.Repositories.Interfaces;
using ColorPaletteGeneratorApi.Dtos;
using ColorPaletteGeneratorApi.Models;
using ColorPaletteGeneratorApi.Services.Interfaces;

namespace ColorPaletteGeneratorApi.Services.Implementations
{
    public class AppUsersServices(IAppUsersRepository appUsersRepository, IMapper mapper, IUnitOfWork unitOfWork, IHashingService hashingService,
        IAuthenticationTokenService authenticationTokenService) : IAppUsersServices
    {
        private readonly IAppUsersRepository _appUsersRepository = appUsersRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHashingService _hashingService = hashingService;
        private readonly IAuthenticationTokenService _authenticationTokenService = authenticationTokenService;

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="request"></param>
        /// <exception cref="ApiException"></exception>
        public async Task<SignInResponseDto> SignUp(SignUpRequestDto request)
        {
            if (await _appUsersRepository.AppUserExists(request.Email)) throw new ApiException($"User already exists.");

            AppUser appUser = _mapper.Map<AppUser>(request);
            CreateAppUserPassword(appUser, request.Password);
            appUser.LastSignIn = DateTimeOffset.UtcNow;
            appUser.LastAppAccess = DateTimeOffset.UtcNow;
            _appUsersRepository.CreateAppUser(appUser);
            await _unitOfWork.SaveChanges();

            return new()
            {
                Name = appUser.Name,
                Email = appUser.Email,
                Token = _authenticationTokenService.GenerateAuthenticationToken(appUser),
            };
        }

        /// <summary>
        /// Signs in an existing user.
        /// </summary>
        /// <param name="request"></param>
        /// <exception cref="ApiException"></exception>
        public async Task<SignInResponseDto> SignIn(SignInRequestDto request)
        {
            AppUser appUser = await _appUsersRepository.GetAppUser(request.Email) ?? throw new ApiException($"User with email '{request.Email}' does not exist.", StatusCodes.Status404NotFound);
            if (!_hashingService.VerifyHash(request.Password, appUser.PasswordSalt, appUser.PasswordHash)) throw new ApiException("Password was incorrect.", StatusCodes.Status401Unauthorized);
            appUser.LastSignIn = DateTimeOffset.UtcNow;
            appUser.LastAppAccess = DateTimeOffset.UtcNow;
            _appUsersRepository.UpdateAppUser(appUser);
            await _unitOfWork.SaveChanges();

            return new()
            {
                Name = appUser.Name,
                Email = appUser.Email,
                Token = _authenticationTokenService.GenerateAuthenticationToken(appUser),
            };
        }

        /// <summary>
        /// Gets the current authenticated user.
        /// </summary>
        /// <param name="appUserRequesterId"></param>
        /// <exception cref="ApiException"></exception>
        public async Task<GetCurrentAppUserResponseDto> GetCurrentAppUser(Guid appUserRequesterId)
        {
            AppUser appUser = await _appUsersRepository.GetAppUser(appUserRequesterId) ?? throw new ApiException("User does not exist.", StatusCodes.Status401Unauthorized);
            appUser.LastAppAccess = DateTimeOffset.UtcNow;
            _appUsersRepository.UpdateAppUser(appUser);
            await _unitOfWork.SaveChanges();
            return _mapper.Map<GetCurrentAppUserResponseDto>(appUser);
        }

        /// <summary>
        /// Creates a password hash and salt for the specified user.
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="password"></param>
        private void CreateAppUserPassword(AppUser appUser, string password)
        {
            byte[] appUserPasswordSalt = _hashingService.GenerateKey();

            appUser.PasswordHash = _hashingService.CreateHash(password, appUserPasswordSalt);
            appUser.PasswordSalt = appUserPasswordSalt;
        }
    }
}
