using DTO.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace dbaccess.Repository
{
    public interface IBlogPostResourceAccess
    {
        Task<BlogPostDto> GetBlogPost(object id);
        Task<IEnumerable<BlogPostSummaryDto>> GetBlogPostSummariesByFilter(BlogPostFilterConfigDto filter);
        Task<BlogPostDto> AddBlogPost(BlogPostDto dto);
        Task UpdateBlogPost(BlogPostDto dto);
        Task DeleteBlogPost(object id);
    }
}