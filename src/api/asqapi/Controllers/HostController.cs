using DTO.Domain;
using asqapi.Services;
using asqapi.Providers;
using managers.Resource;
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
    public class HostController : ControllerBase
    {
        private const string HOST_BASE_QUERIES_CACHE_KEY = "host_base_queries"; 
        private readonly ILogger _logger;
        private readonly IAuthenticationService _authService;
        private readonly IUserResourceManager _userResourceManager;
        private readonly IClaimProvider _claimProvider;
        private readonly IMemoryCache _cache;

        public HostController(
            ILogger<HostController> logger, 
            IMemoryCache cache,
            IAuthenticationService authService, 
            IUserResourceManager userResourceManager,
            IClaimProvider claimProvider
        )
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
            this._cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this._authService = authService ?? throw new ArgumentNullException(nameof(authService));
            this._userResourceManager = userResourceManager ?? throw new ArgumentNullException(nameof(userResourceManager));
            this._claimProvider = claimProvider ?? throw new ArgumentNullException(nameof(claimProvider));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<UserDto> GetHost([FromRoute]Guid id) 
            => await this._userResourceManager.GetHost(id);

        [AllowAnonymous]
        [HttpGet]
        [Route("summary/queries")]
        public async Task<IActionResult> BuildSummaryBaseQueries()
        {
            _logger.LogInformation($"{nameof(HostController)} build subset query endpoint hit.");
            if(!this._cache.TryGetValue(HOST_BASE_QUERIES_CACHE_KEY, out IEnumerable<HostQueryDto> result))
            {
                this._logger.LogInformation($"nothing found for cache key - {HOST_BASE_QUERIES_CACHE_KEY}");
                result = await this._userResourceManager.BuildHostSummaryBaseQueries();
                var options = new MemoryCacheEntryOptions()
                                    .SetSlidingExpiration(TimeSpan.FromSeconds(30));
                this._cache.Set(HOST_BASE_QUERIES_CACHE_KEY, result, options);
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("summary/filtered")]
        public async Task<IActionResult> GetSummariesByFilter(HostFilterConfigDto queryConfig)
        {
            this._logger.LogInformation($"{nameof(BlogController)} get enriched subset endpoint hit.");

            if(!this._cache.TryGetValue(queryConfig.GenerateCacheKey(), out IEnumerable<HostSummaryDto> result))
            {
                this._logger.LogInformation($"nothing found for cache key - {queryConfig.GenerateCacheKey()}");

                result = await this._userResourceManager.GetHostSummariesByFilter(queryConfig);
                var options = new MemoryCacheEntryOptions()
                                    .SetSlidingExpiration(TimeSpan.FromSeconds(30));
                this._cache.Set(queryConfig.GenerateCacheKey(), result, options);
            }
            return Ok(result);
        }
    }
}