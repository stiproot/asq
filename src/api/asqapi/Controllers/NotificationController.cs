using asqapi.Providers;
using managers.Resource;
using DTO.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;

namespace asqapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IClaimProvider _claimProvider;
        private readonly INotificationResourceManager _notificationResourceManager;

        public NotificationController(
            ILogger<NotificationController> logger,
            IClaimProvider claimProvider,
            INotificationResourceManager notificationResourceManager
        )
        {
            this._logger = logger;
            this._claimProvider = claimProvider;
            this._notificationResourceManager = notificationResourceManager;
        }

        [HttpPost]
        [Route("more")]
        public async Task<IActionResult> GetMoreNotifications([FromBody]NotificationQueryDto query)
        {
            this._logger.LogInformation($"{nameof(NotificationController)}.{nameof(GetMoreNotifications)} endpoint hit...");

            var userId = this._claimProvider.UserId(User) ?? throw new System.InvalidOperationException();

            //this._logger.LogInformation("User Id {0}", userId);
            //this._logger.LogInformation("Older Than Id {0}", query.OlderThanId);

            query.UserId = userId;

            var notification = await this._notificationResourceManager.GetUnseenNotifications(query);

            this._logger.LogInformation("Notifications found {0}", notification.Count());

            return Ok(notification);
        }

        [HttpGet]
        [Route("{id}/read")]
        public async Task<IActionResult> ReadNotification([FromRoute]System.Guid id)
        {
            this._logger.LogInformation($"{nameof(NotificationController)}.{nameof(ReadNotification)} endpoint hit...");

            await this._notificationResourceManager.ReadNotification(id);

            return Ok(true);
        }
    }
}