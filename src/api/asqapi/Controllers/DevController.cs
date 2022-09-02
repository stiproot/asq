using asqapi.Services;
using asqapi.Models;
using DTO.Domain;
using DTO.Temp;
using ZoomClient.Providers;
using managers.Resource;
using processes.Factory;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace asqapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DevController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAuthenticationService _authService;
        private readonly IAccountResourceManager _accountManager;
        private readonly IZoomResourceManager _zoomResourceManager;
        private readonly IProcessFactory _processFactory;
        private readonly IImgResourceManager _imgResourceManager;
        private readonly IMeetingResourceManager _meetingResourceManager;
        private readonly IZoomSettingProvider _zoomSettingProvider;

        public DevController(
            ILogger<DevController> logger, 
            IAuthenticationService authService, 
            IAccountResourceManager accountManager, 
            IProcessFactory processFactory,
            IImgResourceManager imgResourceManager,
            IMeetingResourceManager meetingResourceManager,
            IZoomResourceManager zoomResourceManager,
            IZoomSettingProvider zoomSettingProvider
            )
        {
            this._logger = logger;
            this._authService = authService;
            this._accountManager = accountManager;
            this._processFactory = processFactory;
            this._imgResourceManager = imgResourceManager;
            this._meetingResourceManager = meetingResourceManager;
            this._zoomResourceManager = zoomResourceManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("meeting/create/ok")]
        public async Task<IActionResult> OkResponse([FromBody]MeetingDto meeting)
        {
            await Task.FromResult<bool>(true);
            return Ok(true);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("meeting/create/error")]
        public async Task<IActionResult> ErrorResponse([FromBody]MeetingDto meeting)
        {
            try
            {
                await Task.FromResult<bool>(true);
                throw new InvalidOperationException();
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("user/test")]
        public async Task<IActionResult> Test()
        {
            try
            {
                Console.WriteLine(User.Claims.Count());
                Console.WriteLine(JsonSerializer.Serialize(
                    User.Claims.FirstOrDefault(c => c.Type == "host_id").Value));
                Console.WriteLine(User.IsInRole(Role.HOST));
                await Task.FromResult<bool>(true);
                return Ok(true);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("test/logger")]
        public async Task<IActionResult> TestLogger()
        {
            try
            {
                this._logger.LogWarning("Log warning");
                this._logger.LogError("Log error");
                this._logger.LogInformation("Log information");
                await Task.FromResult(true);
                return Ok(true);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Authorize(Roles = Role.HOST)]
        [Route("user/test/token")]
        public async Task<IActionResult> TestGetUserWithToken()
        {
            return Ok(await Task.FromResult<string>("Success"));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("user/create/ok")]
        public async Task<IActionResult> OkResponse([FromBody]UserDto user)
        {
            try
            {
                await Task.FromResult<bool>(true);
                return Ok(true);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("user/create/error")]
        public async Task<IActionResult> ErrorResponse([FromBody]UserDto user)
        {
            try
            {
                await Task.FromResult<bool>(true);
                throw new InvalidOperationException();
            }
            catch(Exception)
            {
                throw;
            }
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("img/test")]
        //public async Task<IActionResult> TestImg([FromBody]ImgDto img)
        //{
            //try
            //{
                //Console.WriteLine("test img endpoint hit");

                //var resp = await this._imgResourceManager.WriteImg(img, Guid.NewGuid());
                
                //return Ok(new {Url = resp.Item1});
            //}
            //catch(Exception)
            //{
                //throw;
            //}
        //}

        [HttpPost]
        [AllowAnonymous]
        [Route("log-datetime")]
        public async Task<IActionResult> LogDateTime([FromBody]DateTimeContainerDto dateTimeContainer)
        {
          await Task.FromResult<bool>(true);

          this._logger.LogInformation("{0}.{1} - {2}", nameof(DevController), nameof(DevController.LogDateTime), dateTimeContainer.dateTime);

          return Ok(new { dateTime = dateTimeContainer.dateTime });
        }
    }
}