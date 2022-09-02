using asqapi.Services;
using asqapi.Providers;
using asqapi.Models;
using DTO.Domain;
using managers.Resource;
using processes.Factory;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace asqapi.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class VideoController : ControllerBase
  {
    private readonly ILogger _logger;
    private readonly IOptions<StaticFileServerSettings> _staticFileServerSettings;
    private readonly IAuthenticationService _authService;
    private readonly IAccountResourceManager _accountManager;
    private readonly IZoomResourceManager _zoomResourceManager;
    private readonly IProcessFactory _processFactory;
    private readonly IImgResourceManager _imgResourceManager;
    private readonly IVideoResourceManager _videoResourceManager;
    private readonly IClaimProvider _claimProvider;
    private readonly IMemoryCache _cache;

    public VideoController(
      ILogger<VideoController> logger, 
      IOptions<StaticFileServerSettings> staticFileServerSettings,
      IMemoryCache cache,
      IAuthenticationService authService, 
      IAccountResourceManager accountManager, 
      IProcessFactory processFactory,
      IImgResourceManager imgResourceManager,
      IVideoResourceManager blogPostResourceManager,
      IZoomResourceManager zoomResourceManager,
      IClaimProvider claimProvider
    )
    {
      this._logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
      this._staticFileServerSettings = staticFileServerSettings ?? throw new ArgumentNullException(nameof(staticFileServerSettings));
      this._cache = cache ?? throw new ArgumentNullException(nameof(cache));
      this._authService = authService ?? throw new ArgumentNullException(nameof(authService));
      this._accountManager = accountManager ?? throw new ArgumentNullException(nameof(accountManager));
      this._processFactory = processFactory ?? throw new ArgumentNullException(nameof(processFactory));
      this._imgResourceManager = imgResourceManager ?? throw new ArgumentNullException(nameof(imgResourceManager));
      this._videoResourceManager = blogPostResourceManager ?? throw new ArgumentNullException(nameof(blogPostResourceManager));
      this._zoomResourceManager = zoomResourceManager ?? throw new ArgumentNullException(nameof(zoomResourceManager));
      this._claimProvider = claimProvider ?? throw new ArgumentNullException(nameof(claimProvider));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetVideo([FromRoute]Guid id)
    {
      this._logger.LogInformation("Attempting video retrieval. Id: {0}", id);

      var dto = await this._videoResourceManager.GetVideo(id);

      return Ok(dto);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateVideo([FromBody]VideoDto dto)
    {
      try
      {
        this._logger.LogInformation("Attmpting video creation...");

        dto.Validate(true);

        await this._videoResourceManager.AddVideo(dto);

        dto = await this._videoResourceManager.GetVideo(dto.UniqueId);

        return Ok(dto);
      }
      catch(Exception ex)
      {
        this._logger.LogError(ex, "Video creation failed.");
        throw;
      }
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateVideo([FromBody]VideoDto dto)
    {
      try
      {
        this._logger.LogInformation("Attempting video update...");

        // Validation...
        if(dto.CreationUserId != this._claimProvider.UserId(User))
        {
          throw new UnauthorizedAccessException("User is unable to modify this video.");
        }

        dto.Validate(true);

        await this._videoResourceManager.UpdateVideo(dto);

        return Ok(true);
      }
      catch(Exception ex)
      {
        this._logger.LogError(ex, "Video updated failed.");
        throw;
      }
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("summary/queries")]
    public async Task<IActionResult> BuildSummaryQueries([FromBody]VideoSummaryQueryBuilderConfigDto config)
    {
      this._logger.LogInformation($"{nameof(VideoController)} build subset query endpoint hit.");

      if(!this._cache.TryGetValue(config.GenerateCacheKey(), out IEnumerable<VideoQueryDto> result))
      {
        result = await this._videoResourceManager.BuildVideoSummaryQueries(config);
        var options = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(30));
        this._cache.Set(config.GenerateCacheKey(), result, options);
      }

      return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("summary/filtered")]
    public async Task<IActionResult> GetSummariesByFilter(VideoFilterConfigDto queryConfig)
    {
      this._logger.LogInformation($"{nameof(VideoController)} get enriched subset endpoint hit.");

      if(!this._cache.TryGetValue(queryConfig.GenerateCacheKey(), out IEnumerable<VideoSummaryDto> result))
      {
        this._logger.LogInformation($"nothing found for cache key - {queryConfig.GenerateCacheKey()}");

        result = await this._videoResourceManager.GetVideoSummariesByFilter(queryConfig);

        var options = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(30));

        this._cache.Set(queryConfig.GenerateCacheKey(), result, options);
      }
      return Ok(result);
    }
  }
}