using asqapi.Services;
using asqapi.Providers;
using DTO.Domain;
using managers.Resource;
using processes.Factory;
using ZoomClient.Providers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
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
    public class BlogController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAuthenticationService _authService;
        private readonly IAccountResourceManager _accountManager;
        private readonly IZoomResourceManager _zoomResourceManager;
        private readonly IProcessFactory _processFactory;
        private readonly IImgResourceManager _imgResourceManager;
        private readonly IBlogPostResourceManager _blogPostResourceManager;
        private readonly IZoomSettingProvider _zoomSettingProvider;
        private readonly IClaimProvider _claimProvider;
        private readonly IMemoryCache _cache;

        public BlogController(
            ILogger<BlogController> logger, 
            IMemoryCache cache,
            IAuthenticationService authService, 
            IAccountResourceManager accountManager, 
            IProcessFactory processFactory,
            IImgResourceManager imgResourceManager,
            IBlogPostResourceManager blogPostResourceManager,
            IZoomResourceManager zoomResourceManager,
            IZoomSettingProvider zoomSettingProvider,
            IClaimProvider claimProvider
        )
        {
            this._logger = logger; 
            this._cache = cache;
            this._authService = authService;
            this._accountManager = accountManager;
            this._processFactory = processFactory;
            this._imgResourceManager = imgResourceManager;
            this._blogPostResourceManager = blogPostResourceManager;
            this._zoomResourceManager = zoomResourceManager;
            this._zoomSettingProvider = zoomSettingProvider;
            this._claimProvider = claimProvider;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("alive")]
        public async Task<string> Alive() => await Task.FromResult<string>("Blog post controller - \"I'm alive\"");

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBlogPost([FromRoute]Guid id)
        {
            _logger.LogInformation($"{nameof(BlogController)}.get endpoint hit.");
            return Ok(await this._blogPostResourceManager.GetBlogPost(id));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateBlogPost([FromBody]BlogPostDto dto)
        {
            try
            {
                this._logger.LogInformation("Attmpting blog post creation...");

                dto.Validate(true);

                await this._blogPostResourceManager.AddBlogPost(dto);

                dto = await this._blogPostResourceManager.GetBlogPost(dto.UniqueId);

                return Ok(dto);
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex, "Blog post creation failed.");
                throw;
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateBlogPost([FromBody]BlogPostDto dto)
        {
            try
            {
                _logger.LogInformation("Attempting blog post update...");

                dto.Validate(true);

                // Validation...
                if(dto.CreationUserId != this._claimProvider.UserId(User))
                {
                    throw new UnauthorizedAccessException("User is unable to modify this blog post");
                }

                await this._blogPostResourceManager.UpdateBlogPost(dto);

                return Ok(true);
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex, "Blog post update failed.");
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("summary/queries")]
        public async Task<IActionResult> BuildSummaryQueries([FromBody]BlogSummaryQueryBuilderConfigDto config)
        {
            _logger.LogInformation($"{nameof(BlogController)} build subset query endpoint hit.");

            if(!this._cache.TryGetValue(config.GenerateCacheKey(), out IEnumerable<BlogPostQueryDto> result))
            {
                result = await this._blogPostResourceManager.BuildBlogPostSummaryQueries(config);
                var options = new MemoryCacheEntryOptions()
                                    .SetSlidingExpiration(TimeSpan.FromSeconds(30));
                this._cache.Set(config.GenerateCacheKey(), result, options);
            }

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("summary/filtered")]
        public async Task<IActionResult> GetSummariesByFilter(BlogPostFilterConfigDto queryConfig)
        {
            this._logger.LogInformation($"{nameof(BlogController)} get enriched subset endpoint hit.");

            if(!this._cache.TryGetValue(queryConfig.GenerateCacheKey(), out IEnumerable<BlogPostSummaryDto> result))
            {
                this._logger.LogInformation($"nothing found for cache key - {queryConfig.GenerateCacheKey()}");

                result = await this._blogPostResourceManager.GetBlogPostSummariesByFilter(queryConfig);
                var options = new MemoryCacheEntryOptions()
                                    .SetSlidingExpiration(TimeSpan.FromSeconds(30));
                this._cache.Set(queryConfig.GenerateCacheKey(), result, options);
            }
            return Ok(result);
        }
    }
}