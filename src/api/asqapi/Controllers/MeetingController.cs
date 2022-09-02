using asqapi.Services;
using asqapi.Models;
using asqapi.Providers;
using Caching.Abstractions;
using DTO.Domain;
using DTO.Events;
using DTO.Notification;
using managers.Resource;
using processes.Process;
using processes.Factory;
using ZoomClient.Providers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace asqapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MeetingController : ControllerBase
    {
        private readonly ILogger<MeetingController> _logger;
        private readonly IOptions<ApiCaching> _cachingOptions; 
        private readonly IAuthenticationService _authService;
        private readonly IAccountResourceManager _accountManager;
        private readonly IUserResourceManager _userResourceManager;
        private readonly IZoomResourceManager _zoomResourceManager;
        private readonly IProcessFactory _processFactory;
        private readonly IImgResourceManager _imgResourceManager;
        private readonly IMeetingResourceManager _meetingResourceManager;
        private readonly IZoomSettingProvider _zoomSettingProvider;
        private readonly IClaimProvider _claimProvider;
        private readonly ICache _cache;

        public MeetingController(
            ILogger<MeetingController> logger, 
            IOptions<ApiCaching> cachingOptions,
            ICache cache,
            IAuthenticationService authService, 
            IAccountResourceManager accountManager, 
            IUserResourceManager userResourceManager,
            IProcessFactory processFactory,
            IImgResourceManager imgResourceManager,
            IMeetingResourceManager meetingResourceManager,
            IZoomResourceManager zoomResourceManager,
            IZoomSettingProvider zoomSettingProvider,
            IClaimProvider claimProvider
        )
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
            this._cachingOptions = cachingOptions ?? throw new ArgumentNullException(nameof(cachingOptions));
            this._cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this._authService = authService ?? throw new ArgumentNullException(nameof(authService));
            this._accountManager = accountManager ?? throw new ArgumentNullException(nameof(accountManager));
            this._userResourceManager = userResourceManager ?? throw new ArgumentNullException(nameof(userResourceManager));
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
            => await Task.FromResult<string>("Meeting controller - \"I'm alive\"");

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMeeting([FromRoute]Guid id)
        {
            this._logger.LogInformation("Get meeting endpoint hit with id ({0})", id);

            var utcOffset = this._claimProvider.UtcOffset(User);

            var enrichmentRules = new List<(string, Func<MeetingDto, object>)>
            {
                (nameof(MeetingDto.StartDateUtc), (m) => m.StartDateUtc.AddHours(m.Timezone.UtcOffset))
            };
            
            var meeting = await this._meetingResourceManager.GetMeeting(id, enrichmentRules);

            return Ok(meeting);
        }

        [HttpPost]
        [Authorize(Roles = Role.HOST)]
        [Route("create")]
        public async Task<IActionResult> CreateMeeting([FromBody]MeetingDto meeting)
        {
            try
            {
                this._logger.LogInformation("Attmpting meeting creation...");

                meeting.Validate(true);

                this._logger.LogInformation("Meeting validation passed");

                // create account creation process event
                var @event = new MeetingCreationEvent
                {
                    id = Guid.NewGuid(),
                    event_date_utc = DateTime.UtcNow,
                    meeting = meeting,
                };

                // create process
                var process = _processFactory.Create(ProcessTypeEnu.CreateMeetingProcess)
                                        .SetEvent(@event)
                                        .SetLogger(_logger)
                                        .Init();

                await process.Execute();

                this._logger.LogInformation("Meeting successfully created");
                this._logger.LogInformation("Attempting meeting retrieval");

                var enrichedMeeting = await this._meetingResourceManager.GetMeeting(meeting.UniqueId);

                this._logger.LogInformation("Meeting retrieved");
                this._logger.LogInformation("Attempting mail config creation");
                //this._logger.LogInformation(JsonSerializer.Serialize(enrichedMeeting));

                var @notificationEvent = new SendNotificationEvent
                {
                    id = Guid.NewGuid(),
                    event_date_utc = DateTime.UtcNow,
                    notification_config = new NotificationConfig
                    {
                        ToEmailAddress = this._claimProvider.Email(User),
                        ToName = this._claimProvider.Name(User),
                        ToSurname = this._claimProvider.Surname(User),
                        ToUsername = this._claimProvider.Username(User),
                        NotificationType = NotificationTypeEnu.MEETING_CREATION,
                        ExtMeetingStartUrl = enrichedMeeting.ExtMeeting.PayloadSerializer.start_url,
                        MeetingStartDatetime = enrichedMeeting.StartDateUtc.AddHours(enrichedMeeting.Timezone.UtcOffset).ToString("yyyy-MM-dd HH:mm"),
                        MeetingTimezone = enrichedMeeting.Timezone.Display,
                        MeetingId = enrichedMeeting.Id
                    },
                };

                this._logger.LogInformation("Mail config created");
                this._logger.LogInformation("Attempting mail process");

                var notificationProcess = this._processFactory.Create(ProcessTypeEnu.QueueNotificationProcess)
                                            .SetEvent(@notificationEvent)
                                            .Init();

                await notificationProcess.Execute();

                this._logger.LogInformation("Mail process ran successfully");

                meeting = await this._meetingResourceManager.GetMeeting(meeting.UniqueId);

                return Ok(meeting);
            }
            catch(Exception ex)
            {
                this._logger.LogError("Meeting creation failed / Stack-trace {0}", ex.StackTrace);
                throw;
            }
        }

        [HttpPut]
        [Authorize(Roles = Role.HOST)]
        [Route("update")]
        public async Task<IActionResult> UpdateMeeting([FromBody]MeetingDto meeting)
        {
            try
            {
                this._logger.LogInformation("Attmpting meeting update...");

                // validation
                if(meeting.HostId != this._claimProvider.HostId(User))
                {
                    throw new UnauthorizedAccessException("Host is unable to modify this meeting");
                }

                meeting.Validate(true);

                // create account creation process event
                var @event = new MeetingUpdateEvent
                {
                    id = Guid.NewGuid(),
                    event_date_utc = DateTime.UtcNow,
                    meeting = meeting,
                };

                // create process
                var process = _processFactory.Create(ProcessTypeEnu.UpdateMeetingProcess)
                                        .SetEvent(@event)
                                        //.SetLogger(_logger)
                                        .Init();

                await process.Execute();

                return Ok(true);
            }
            catch(Exception ex)
            {
                this._logger.LogError("Meeting update failed / Stack-trace {0}", ex.StackTrace);
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("summary/queries")]
        public async Task<IActionResult> BuildSummaryBaseQueries([FromBody]MeetingSummaryQueryBuilderConfigDto config)
        {
            _logger.LogInformation($"{nameof(MeetingController)} build subset query endpoint hit.");

            var result = await this._cache.GetOrCreateAsync<IEnumerable<MeetingQueryDto>>(
                config.GenerateCacheKey(), 
                async () => await this._meetingResourceManager.BuildMeetingSummaryQueries(config),
                DateTime.UtcNow.AddMinutes(this._cachingOptions.Value.MeetingQueryExpirationMinutes)
            );

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("summary/filtered")]
        public async Task<IActionResult> GetMeetingSummariesByFilter(MeetingFilterConfigDto queryConfig)
        {
            this._logger.LogInformation($"{nameof(MeetingController)} get enriched subset endpoint hit.");

            // If the requesting user is logged in, then we would like to display the time in their default timezone.
            if(User != null)
            {
                var utcOffset = this._claimProvider.UtcOffset(User);
                queryConfig.RequestingUserUtcOffset = utcOffset;
            }

            var result = await this._cache.GetOrCreateAsync(
                queryConfig.GenerateCacheKey(),
                async () => await this._meetingResourceManager.GetMeetingSummariesByFilter(queryConfig),
                DateTime.UtcNow.AddMinutes(this._cachingOptions.Value.MeetingSummaryExpirationMinute)
            );

            return Ok(result);
        }

        [HttpGet]
        [Route("{meetingId}/register/{register}")]
        public async Task<IActionResult> RegisterForMeeting([FromRoute]Guid meetingId, [FromRoute]bool register)
        {
            // in this case, we would like to inform the host that a there is an attendee...
            // therefore we need an send email process...

            var meeting = await this._meetingResourceManager.GetMeeting(meetingId);
            var hostUser = await this._userResourceManager.GetHost(meeting.HostId);

            var @meetingParticipationEvent = new MeetingParticipationEvent
            {
                user_id = this._claimProvider.UserId(User) ?? throw new InvalidOperationException("UserId not found in claim"),
                meeting_id = meetingId,
                meeting = meeting,
                register = register 
            };

            await this._processFactory.Create(processes.Process.ProcessTypeEnu.ParticipateInMeetingProcess)
                    .SetEvent(@meetingParticipationEvent)
                    .SetLogger(this._logger)
                    .Init()
                    .Execute();

            var @notificationEvent = new SendNotificationEvent
            {
                id = Guid.NewGuid(),
                event_date_utc = DateTime.UtcNow,
                notification_config = new NotificationConfig
                {
                    NotificationType = NotificationTypeEnu.MEETING_REGISTRATION,
                    ToEmailAddress = hostUser.Email,
                    ToName = hostUser.Name,
                    ToSurname = hostUser.Surname,
                    ToUsername = hostUser.Username,
                    UserId = hostUser.Id, 
                    MeetingTitle = meeting.Title,
                    MeetingThumbnailUrl = meeting.Img.ThumbnailUrl,
                    ParticipantName = this._claimProvider.Name(User),
                    ParticipantSurname = this._claimProvider.Surname(User),
                    ParticipantUsername = this._claimProvider.Username(User)
                }
            };

            await this._processFactory.Create(ProcessTypeEnu.QueueNotificationProcess)
                .SetEvent(@notificationEvent)
                .Init()
                .Execute();

            return Ok(true);
        }
    }
}
