using BaurezGames.Shared.AdditionGame;
using BaurezGames.Shared.MoreOrLessGame;

using Microsoft.AspNetCore.Mvc;

namespace BaurezGames.Server.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AdditionGameController : ControllerBase
    {
        private readonly ILogger<MoreOrLessGameController> _logger;
        private readonly AdditionGameService _additionGameService;

        public AdditionGameController(ILogger<MoreOrLessGameController> logger, AdditionGameService additionGameService)
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