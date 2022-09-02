using asqapi.Services;
using asqapi.Models;
using DTO.Domain;
using DTO.Events;
using DTO.Notification;
using managers.Resource;
using processes.Process;
using processes.Factory;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;

namespace asqapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAuthenticationService _authService;
        private readonly IAccountResourceManager _accountManager;
        private readonly IUserResourceManager _userResourceManager;
        private readonly IProcessFactory _processFactory;
        private readonly IImgResourceManager _imgResourceManager;

        public UserController(
            ILogger<UserController> logger,
            IAuthenticationService authService, 
            IAccountResourceManager accountManager, 
            IUserResourceManager userResourceManager,
            IProcessFactory processFactory,
            IImgResourceManager imgResourceManager
        )
        {
            this._logger = logger;
            this._authService = authService;
            this._accountManager = accountManager;
            this._userResourceManager = userResourceManager;
            this._processFactory = processFactory;
            this._imgResourceManager = imgResourceManager;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<UserDto> GetUser([FromRoute]Guid id) 
        {
            this._logger.LogInformation("{0}.{1} - endpoint hit. User Id: {2}", nameof(UserController), nameof(GetUser), id);
            return await this._userResourceManager.GetUser(id);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("activate/{token}")]
        public async Task<IActionResult> ActivateAccount([FromRoute]Guid token)
        {
            var user = await this._userResourceManager.ActivateAndReturnUser(token);

            // send out an email event stating that account was successfully activated
            var @notificationEvent = new SendNotificationEvent
            {
                id = Guid.NewGuid(),
                event_date_utc = DateTime.UtcNow,
                notification_config = new NotificationConfig
                {
                    ToEmailAddress = user.Email,
                    ToName = user.Name,
                    ToSurname = user.Surname,
                    ToUsername = user.Username,
                    NotificationType = NotificationTypeEnu.WELCOME
                },
            };

            var mailProcess = this._processFactory.Create(ProcessTypeEnu.QueueNotificationProcess)
                                        .SetEvent(@notificationEvent)
                                        .Init();

            await mailProcess.Execute();

            return Ok(true);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticationModel authModel)
        {
            this._logger.LogInformation($"{nameof(UserController)}.Authenticate hit");

            authModel.Validate();

            var user = await _authService.Authenticate(authModel.Username, authModel.Password);

            if(user == null)
            {
                return BadRequest(new { message = "Username or passeord is incorrect. Please note that if you have created an account, you will need to confirm you email address before logging in." });
            }

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody]UserDto user)
        {
            user.Validate(true);

            var @event = new AccountCreationEvent
            {
                id = Guid.NewGuid(),
                event_date_utc = DateTime.UtcNow,
                user = user,
            };

            var process = _processFactory.Create(ProcessTypeEnu.CreateAccountProcess)
                                    .SetEvent(@event)
                                    .SetLogger(_logger)
                                    .Init();

            await process.Execute();

            var @notificationEvent = new SendNotificationEvent
            {
                id = Guid.NewGuid(),
                event_date_utc = DateTime.UtcNow,
                notification_config = new NotificationConfig 
                {
                    ToEmailAddress = user.Email,
                    ToName = user.Name,
                    ToSurname = user.Surname,
                    ToUniqueId = user.UniqueId,
                    ToUsername = user.Username,
                    NotificationType = NotificationTypeEnu.EMAIL_CONFIRMATION
                }
            };

            var mailProcess = this._processFactory.Create(ProcessTypeEnu.QueueNotificationProcess)
                                                  .SetEvent(@notificationEvent)
                                                  .SetLogger(this._logger)
                                                  .Init();

            await mailProcess.Execute();

            return Ok(true);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody]UserDto user)
        {
            this._logger.LogInformation("{0}.{1} endpoint hit", nameof(UserController), nameof(UpdateUser));

            user.Validate(true);

            // create account creation process event
            var @event = new AccountUpdateEvent
            {
                id = Guid.NewGuid(),
                event_date_utc = DateTime.UtcNow,
                user = user,
            };

            // create process
            var process = _processFactory.Create(ProcessTypeEnu.UpdateAccountProcess)
                                    .SetEvent(@event)
                                    .SetLogger(_logger)
                                    .Init();

            await process.Execute();

            return Ok(true);
        }
    }
}