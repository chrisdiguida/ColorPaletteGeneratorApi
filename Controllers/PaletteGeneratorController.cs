using ColorPaletteGeneratorApi.Attributes;
using ColorPaletteGeneratorApi.Dtos;
using ColorPaletteGeneratorApi.Extentions;
using ColorPaletteGeneratorApi.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ColorPaletteGeneratorApi.Controllers
{
    public class PaletteGeneratorController(IPalettesServices palettesServices) : BaseController
    {
        private readonly IPalettesServices _palettesServices = palettesServices;

        [HttpGet]
        public async Task<ActionResult<PaletteDto>> GeneratePalette([Required][HexColor] string hexColor)
        {
            return Ok(await _palettesServices.GeneratePalette(User.GetAppUserId(), hexColor));
        }

        [HttpGet]
        public async Task<ActionResult<List<PaletteDto>>> GetAll(string filter, int page)
        {
            return Ok(await _palettesServices.GetAll(User.GetAppUserId(), filter, page));
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePaletteRequestDto request)
        {
            await _palettesServices.Create(User.GetAppUserId(), request);
            return Ok();
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateName(UpdatePaletteRequestDto request)
        {
            await _palettesServices.UpdateName(User.GetAppUserId(), request);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid paletteId)
        {
            await _palettesServices.Delete(User.GetAppUserId(), paletteId);
            return Ok();
        }
    }
}
