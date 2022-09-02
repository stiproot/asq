using DTO.Domain;
using dbaccess.Repository;
using dbaccess.Repository.QueryEnrichment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace managers.Resource
{
    public class BlogPostResourceManager : IBlogPostResourceManager
    {
        private IBlogPostResourceAccess _blogPostResourceAccess;
        private readonly IBlogPostQueryEnrichmentResourceAccess _blogPostQueryEnrichmentResourceAccess;

        public BlogPostResourceManager(
            IBlogPostResourceAccess blogPostResourceAccess,
            IBlogPostQueryEnrichmentResourceAccess blogPostQueryEnrichmentResourceAccess
        )
        {
            this._blogPostResourceAccess = blogPostResourceAccess;
            this._blogPostQueryEnrichmentResourceAccess = blogPostQueryEnrichmentResourceAccess;
        }

        public async Task<BlogPostDto> GetBlogPost(long id)
        {
            return await _blogPostResourceAccess.GetBlogPost(id);
        }

        public async Task<BlogPostDto> GetBlogPost(object id)
        {
            return await _blogPostResourceAccess.GetBlogPost(id);
        }

        public async Task<IEnumerable<BlogPostQueryDto>> BuildBlogPostSummaryQueries(BlogSummaryQueryBuilderConfigDto config)
        {
            return await this._blogPostQueryEnrichmentResourceAccess.BuildBlogPostSummaryQueries(config);
        }

        public async Task<IEnumerable<BlogPostSummaryDto>> GetBlogPostSummariesByFilter(BlogPostFilterConfigDto config)
        {
            return await this._blogPostResourceAccess.GetBlogPostSummariesByFilter(config);
        }

        public async Task AddBlogPost(BlogPostDto dto)
        {
            await _blogPostResourceAccess.AddBlogPost(dto);
        }

        public async Task UpdateBlogPost(BlogPostDto dto)
        {
            await _blogPostResourceAccess.UpdateBlogPost(dto);
        }
    }
}