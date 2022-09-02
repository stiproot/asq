using managers.Resource;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace asqapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CardTypeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ILookupResourceManager _resourceManager;

        public CardTypeController(
            ILogger<CardTypeController> logger, 
            ILookupResourceManager resourceManager
        ) 
            => (this._logger, this._resourceManager) = (logger, resourceManager);

        [AllowAnonymous]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            this._logger.LogInformation($"{nameof(CardTypeController)} get all hit");
            return Ok(await _resourceManager.GetCardTypes());
        }
    }
}