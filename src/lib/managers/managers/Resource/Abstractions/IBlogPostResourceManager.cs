using DTO.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace managers.Resource
{
    public interface IBlogPostResourceManager
    {
        Task<BlogPostDto> GetBlogPost(long id);
        Task<BlogPostDto> GetBlogPost(object id);
        Task<IEnumerable<BlogPostQueryDto>> BuildBlogPostSummaryQueries(BlogSummaryQueryBuilderConfigDto config);
        Task<IEnumerable<BlogPostSummaryDto>> GetBlogPostSummariesByFilter(BlogPostFilterConfigDto config);
        Task AddBlogPost(BlogPostDto dto);
        Task UpdateBlogPost(BlogPostDto dto);
    }
}