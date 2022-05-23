using Microsoft.AspNetCore.Mvc;

namespace BaurezGames.Server.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AdditionGameController : ControllerBase
    {
        private readonly ILogger<AdditionGameController> _logger;
        private readonly IAdditionGameService _additionGameService;

        public AdditionGameController(ILogger<AdditionGameController> logger, IAdditionGameService additionGameService)
        {
            _logger = logger;
            _additionGameService = additionGameService;
        }

        [HttpGet]
        public string Get(int NbElements)
        {
            return _additionGameService.NewGame(1, 9, NbElements);
        }
    }
}