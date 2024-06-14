using ColorPaletteGeneratorApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ColorPaletteGeneratorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableCors(Constants.DEFAULT_CORS_POLICY)]
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}

