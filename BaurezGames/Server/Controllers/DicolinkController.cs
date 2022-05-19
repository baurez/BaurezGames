using System.Reflection.Metadata.Ecma335;

using BaurezGames.Shared.Dicolink;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaurezGames.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DicolinkController : ControllerBase
    {
        private readonly IDicolinkService _dicolinkService;

        public DicolinkController(IDicolinkService dicolinkService)
        {
            _dicolinkService = dicolinkService;
        }

        [HttpGet]
        public async Task<IEnumerable<DefinitionResponse>?> GetDefinitions(string mot, int limite)
            => await _dicolinkService.GetDefinitionAsync(mot, limite);

    }
}

