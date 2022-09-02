using managers.Resource;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace asqapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FocusController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ILookupResourceManager _resourceManager;

        public FocusController(
            ILogger<FocusController> logger,
            ILookupResourceManager resourceManager
        ) 
            => (this._logger, this._resourceManager) = (logger, resourceManager);

        [AllowAnonymous]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            this._logger.LogInformation($"{nameof(FocusController)} get all hit");
            return Ok(await _resourceManager.GetFoci());
        }
    }
}