using AutoMapper;
using ColorPaletteGeneratorApi.Data.Repositories.Interfaces;
using ColorPaletteGeneratorApi.Dtos;
using ColorPaletteGeneratorApi.Models;
using ColorPaletteGeneratorApi.Services.Interfaces;

namespace ColorPaletteGeneratorApi.Services.Implementations
{
    public class PalettesServices(IPalettesRepository palettesRepository, IMapper mapper, IUnitOfWork unitOfWork, IPaletteGeneratorServices paletteGeneratorServices) : IPalettesServices
    {
        private readonly IPalettesRepository _palettesRepository = palettesRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPaletteGeneratorServices _paletteGeneratorServices = paletteGeneratorServices;

        /// <summary>
        /// Generates a color palette based on the provided hex color for a specific user.
        /// </summary>
        /// <param name="appUserRequesterId"></param>
        /// <param name="hexColor"></param>
        /// <returns></returns>
        public async Task<PaletteDto> GeneratePalette(Guid appUserRequesterId, string hexColor)
        {
            List<PaletteColorDto> colors = _paletteGeneratorServices.GeneratePalette(hexColor);
            bool alreadyCreated = await _palettesRepository.Exists(appUserRequesterId, hexColor);
            return new()
            {
                BaseColor = hexColor,
                Colors = colors,
                AlreadyCreated = alreadyCreated
            };
        }

        /// <summary>
        /// Retrieves all palettes for a specific user with optional filtering and pagination.
        /// </summary>
        /// <param name="appUserRequesterId"></param>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<List<PaletteDto>> GetAll(Guid appUserRequesterId, string filter, int page)
        {
            List<Palette> palettes = await _palettesRepository.GetAll(appUserRequesterId, filter, page);
            List<PaletteDto> paletteDtos = _mapper.Map<List<PaletteDto>>(palettes);
            paletteDtos.ForEach(x =>
            {
                x.Colors = _paletteGeneratorServices.GeneratePalette(x.BaseColor);
            });
            return paletteDtos;
        }

        /// <summary>
        /// Creates a new palette for a specific user.
        /// </summary>
        /// <param name="appUserRequesterId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task Create(Guid appUserRequesterId, CreatePaletteRequestDto request)
        {
            if (await _palettesRepository.Exists(appUserRequesterId, request.BaseColor)) throw new ApiException($"You already created a palette with the base color '{request.BaseColor}'");
            Palette palette = _mapper.Map<Palette>(request);
            palette.AppUserId = appUserRequesterId;
            _palettesRepository.Create(palette);
            await _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Updates the name of an existing palette.
        /// </summary>
        /// <param name="appUserRequesterId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task UpdateName(Guid appUserRequesterId, UpdatePaletteRequestDto request)
        {
            Palette palette = await _palettesRepository.Get(appUserRequesterId, request.Id) ?? throw new ApiException($"The palette with id '{request.Id}' does not exist.", StatusCodes.Status404NotFound);
            palette.Name = request.Name;
            _palettesRepository.Update(palette);
            await _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Deletes a palette for a specific user.
        /// </summary>
        /// <param name="appUserRequesterId"></param>
        /// <param name="paletteId"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task Delete(Guid appUserRequesterId, Guid paletteId)
        {
            Palette palette = await _palettesRepository.Get(appUserRequesterId, paletteId) ?? throw new ApiException($"The palette with id '{paletteId}' does not exist.", StatusCodes.Status404NotFound);
            _palettesRepository.Delete(palette);
            await _unitOfWork.SaveChanges();
        }
    }
}
