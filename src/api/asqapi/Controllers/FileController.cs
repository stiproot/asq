using asqapi.Services;
using asqapi.Providers;
using asqapi.Models;
using DTO.Domain;
using managers.Resource;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using System;

namespace asqapi.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class FileController : ControllerBase
  {
    private readonly ILogger _logger;
    private readonly IOptions<StaticFileServerSettings> _staticFileServerSettings;
    private readonly IAuthenticationService _authService;
    private readonly IImgResourceManager _imgResourceManager;
    private readonly IVideoResourceManager _videoResourceManager;
    private readonly IClaimProvider _claimProvider;
    private readonly IMemoryCache _cache;

    public FileController(
      ILogger<FileController> logger, 
      IOptions<StaticFileServerSettings> staticFileServerSettings,
      IMemoryCache cache,
      IAuthenticationService authService, 
      IImgResourceManager imgResourceManager,
      IVideoResourceManager blogPostResourceManager,
      IClaimProvider claimProvider
    )
    {
      this._logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
      this._staticFileServerSettings = staticFileServerSettings ?? throw new ArgumentNullException(nameof(staticFileServerSettings));
      this._cache = cache ?? throw new ArgumentNullException(nameof(cache));
      this._authService = authService ?? throw new ArgumentNullException(nameof(authService));
      this._imgResourceManager = imgResourceManager ?? throw new ArgumentNullException(nameof(imgResourceManager));
      this._videoResourceManager = blogPostResourceManager ?? throw new ArgumentNullException(nameof(blogPostResourceManager));
      this._claimProvider = claimProvider ?? throw new ArgumentNullException(nameof(claimProvider));
    }

    [HttpPost, DisableRequestSizeLimit]
    [Route("upload/img")]
    public async Task<IActionResult> UploadImg()
    {
      try
      {
        this._logger.LogInformation("Attempting image upload...");

        if (Request.Form.Files.Count != 1)
        {
          throw new NotSupportedException("Attempting upload of multiple files.");
        }

        var file = Request.Form.Files[0];

        var userGuid = this._claimProvider.UserGuid(User);
        var userId = this._claimProvider.UserId(User);
        var userDirPath = $"{this._staticFileServerSettings.Value.ImgBasePath}{userGuid}/";

        if (!Directory.Exists(userDirPath))
        {
            Directory.CreateDirectory(userDirPath);
        }

        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim();
        var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";

        string filePath = Path.Combine(userDirPath, newFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await file.CopyToAsync(stream);
        }

        var dto = await this._imgResourceManager.AddImg(new ImgDto
        {
          Id = 0,
          UniqueId = Guid.NewGuid(),
          CreationUserId = (long)userId,
          CreationDateUtc = DateTime.UtcNow,
          Inactive = false,
          Url =  $"{this._staticFileServerSettings.Value.StaticImgServerBaseUrl}{userGuid}/{newFileName}",
          ThumbnailUrl =  $"{this._staticFileServerSettings.Value.StaticImgServerBaseUrl}{userGuid}/{newFileName}"
        });

        return Ok(dto);
      }
      catch(Exception ex)
      {
        this._logger.LogError(ex, "Failed to upload image.");
        throw;
      }
    }

    [HttpPost, DisableRequestSizeLimit]
    [RequestFormLimits(MultipartBodyLengthLimit = Int32.MaxValue, ValueLengthLimit = Int32.MaxValue)]
    [Route("upload/vid")]
    public async Task<IActionResult> UploadVid()
    {
      try
      {
        this._logger.LogInformation("Attempting video upload...");

        if (Request.Form.Files.Count != 1)
        {
          throw new NotSupportedException("Attempting upload of multiple files.");
        }

        var file = Request.Form.Files[0];

        var userGuid = this._claimProvider.UserGuid(User);
        var userId = this._claimProvider.UserId(User);
        var userDirPath = $"{this._staticFileServerSettings.Value.VideoBasePath}{userGuid}/";

        if (!Directory.Exists(userDirPath))
        {
            Directory.CreateDirectory(userDirPath);
        }

        var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim();
        var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";

        string filePath = Path.Combine(userDirPath, newFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await file.CopyToAsync(stream);
        }

        var dto = await this._videoResourceManager.AddVid(new VidDto
        {
          Id = 0,
          UniqueId = Guid.NewGuid(),
          CreationUserId = (long)userId,
          CreationDateUtc = DateTime.UtcNow,
          Inactive = false,
          Url =  $"{this._staticFileServerSettings.Value.StaticVideoServerBaseUrl}{userGuid}/{newFileName}",
          FilePath = filePath
        });

        return Ok(dto);
      }
      catch(Exception ex)
      {
        this._logger.LogError(ex, "Failed to upload image.");
        throw;
      }
    }
  }
}