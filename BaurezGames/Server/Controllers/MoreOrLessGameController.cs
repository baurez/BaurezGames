using Microsoft.AspNetCore.Mvc;

namespace BaurezGames.Server.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class MoreOrLessGameController : ControllerBase
    {
        private readonly ILogger<MoreOrLessGameController> _logger;
        private readonly IMoreOrLessGameService _moreOrLessGameService;

        public MoreOrLessGameController(ILogger<MoreOrLessGameController> logger, IMoreOrLessGameService moreOrLessGameService)
        {
            _logger = logger;
            _moreOrLessGameService = moreOrLessGameService;
        }

        [HttpGet]
        public string Get()
        {
            return _moreOrLessGameService.NewGame(-10, 10);
        }

        [HttpPost]
        public MoreOrLessGameResult? Post(MoreOrLessGameResponse? response)
        {
            return _moreOrLessGameService.SubmitResponse(response);
        }

    }
}