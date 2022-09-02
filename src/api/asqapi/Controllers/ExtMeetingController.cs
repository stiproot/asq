using asqapi.Services;
using asqapi.Providers;
using DTO.Domain.Ext.Zoom;
using managers.Resource;
using processes.Factory;
using ZoomClient.Providers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using System;

namespace asqapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ExtMeetingController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ITokenProvider _tokenProvider;
        private readonly IAuthenticationService _authService;
        private readonly IAccountResourceManager _accountManager;
        private readonly IZoomResourceManager _zoomResourceManager;
        private readonly IProcessFactory _processFactory;
        private readonly IImgResourceManager _imgResourceManager;
        private readonly IMeetingResourceManager _meetingResourceManager;
        private readonly IZoomSettingProvider _zoomSettingProvider;
        private readonly IClaimProvider _claimProvider;

        public ExtMeetingController(
            ILogger<MeetingController> logger, 
            ITokenProvider tokenProvider,
            IAuthenticationService authService, 
            IAccountResourceManager accountManager, 
            IProcessFactory processFactory,
            IImgResourceManager imgResourceManager,
            IMeetingResourceManager meetingResourceManager,
            IZoomResourceManager zoomResourceManager,
            IZoomSettingProvider zoomSettingProvider,
            IClaimProvider claimProvider
            )
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
            this._tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
            this._authService = authService ?? throw new ArgumentNullException(nameof(authService));
            this._accountManager = accountManager ?? throw new ArgumentNullException(nameof(accountManager));
            this._processFactory = processFactory ?? throw new ArgumentNullException(nameof(processFactory));
            this._imgResourceManager = imgResourceManager ?? throw new ArgumentNullException(nameof(imgResourceManager));
            this._meetingResourceManager = meetingResourceManager ?? throw new ArgumentNullException(nameof(meetingResourceManager));
            this._zoomResourceManager = zoomResourceManager ?? throw new ArgumentNullException(nameof(zoomResourceManager));
            this._zoomSettingProvider = zoomSettingProvider ?? throw new ArgumentNullException(nameof(zoomSettingProvider));
            this._claimProvider = claimProvider ?? throw new ArgumentNullException(nameof(claimProvider));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("alive")]
        public async Task<string> Alive()
            => await Task.FromResult<string>("ExtMeeting controller - \"I'm alive\"");

        [HttpGet]
        [Route("authorize/{meetingId}")]
        public async Task<IActionResult> AuthorizeZoomMeeting([FromRoute]Guid meetingId)
        {
            var meeting = await this._meetingResourceManager.GetMeeting(meetingId);
            var extMeeting =  meeting.ExtMeeting.PayloadSerializer;
            var role = this._claimProvider.isHost(User) ? "1" : "0";

            this._logger.LogInformation($"role {role}");

            if(!meeting.Participants.Any(p => p.UserId.Equals(this._claimProvider.UserId(User))) && meeting.HostId != this._claimProvider.HostId(User))
            {
                throw new InvalidOperationException("User is neither a participant nor the host of this meeting");
            }

            var signature = this._zoomResourceManager.GetSignature(role, extMeeting.id.ToString());
            
            return Ok(new 
            {
                meetingConfig = new 
                {
                    signature = signature,
                    meetingNumber = meeting.ExtMeeting.PayloadSerializer.id,
                    userName = this._claimProvider.Username(User),
                    apiKey = this._zoomSettingProvider.GetApiKey(),
                    userEmail = this._claimProvider.Email(User)
                },
                meeting = meeting
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("webhook/meeting")]
        public async Task CaptureMeetingWebHook(DTO.Zoom.WebHook.Meeting.BaseWebHookMeetingEvent meetingEvent)
        {
            try
            {
                this._logger.LogInformation("{0}.{1} - endpoint hit. MeetingEvent: {2}", nameof(ExtMeetingController), nameof(CaptureMeetingWebHook), JsonSerializer.Serialize(meetingEvent));

                var authorizationHeader = this._tokenProvider.GetAuthorizationHeader();
                var verificationToken = this._zoomSettingProvider.GetWebHookVerificationToken();

                if(authorizationHeader != verificationToken)
                {
                    string exceptionMessage = $"{nameof(ExtMeetingController)}.{nameof(CaptureMeetingWebHook)} - Authorization header does not match the verification token";
                    this._logger.LogError(exceptionMessage);
                    throw new OperationCanceledException(exceptionMessage);
                }

                if(!long.TryParse(meetingEvent.payload.@object.id, out long extMeetingId))
                {
                    throw new OperationCanceledException("No meeting provided in web hook event");
                }

                // get meeting id with ext meeting id
                var meeting = await this._meetingResourceManager.GetMeetingByExtMeetingId(extMeetingId);

                if(meeting == null) throw new OperationCanceledException($"Meeting not found with id provided. Ext Meeting Id: {extMeetingId}");

                var dto = new ExtZoomMeetingWebHookDto()
                {
                    MeetingId = meeting.Id,
                    UniqueId = Guid.NewGuid(),
                    PayloadSerializer = meetingEvent,
                    EventType = meetingEvent.@event
                };

                await this._meetingResourceManager.AddExtZoomMeetingWebHook(dto);
                await this._meetingResourceManager.UpdateMeetingStatus(meeting.Id);

            }
            catch(Exception ex)
            {
                this._logger.LogError(ex, "{0}.{1}", nameof(ExtMeetingController), nameof(CaptureMeetingWebHook));
                throw;
            }
        }
    }
}