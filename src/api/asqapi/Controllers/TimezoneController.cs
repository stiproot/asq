using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using managers.Resource;

namespace asqapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TimezoneController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ILookupResourceManager _resourceManager;

        public TimezoneController(
            ILogger<TimezoneController> logger,
            ILookupResourceManager resourceManager
        )
        {
            this._logger = logger;
            this._resourceManager = resourceManager;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            this._logger.LogInformation($"{nameof(TimezoneController)}.GetAll hit");

            try
            {
                return Ok(await _resourceManager.GetTimezones());
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}