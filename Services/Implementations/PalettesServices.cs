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

        public async Task Create(Guid appUserRequesterId, CreatePaletteRequestDto request)
        {
            if (await _palettesRepository.Exists(appUserRequesterId, request.BaseColor)) throw new ApiException($"You already created a palette with the base color '{request.BaseColor}'");
            Palette palette = _mapper.Map<Palette>(request);
            palette.AppUserId = appUserRequesterId;
            _palettesRepository.Create(palette);
            await _unitOfWork.SaveChanges();
        }

        public async Task UpdateName(Guid appUserRequesterId, UpdatePaletteRequestDto request)
        {
            Palette palette = await _palettesRepository.Get(appUserRequesterId, request.Id) ?? throw new ApiException($"The palette with id '{request.Id}' does not exist.", StatusCodes.Status404NotFound);
            palette.Name = request.Name;
            _palettesRepository.Update(palette);
            await _unitOfWork.SaveChanges();
        }

        public async Task Delete(Guid appUserRequesterId, Guid paletteId)
        {
            Palette palette = await _palettesRepository.Get(appUserRequesterId, paletteId) ?? throw new ApiException($"The palette with id '{paletteId}' does not exist.", StatusCodes.Status404NotFound);
            _palettesRepository.Delete(palette);
            await _unitOfWork.SaveChanges();
        }
    }
}
