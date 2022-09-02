using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Domain;

namespace dbaccess.Repository.QueryEnrichment
{
    public interface IBlogPostQueryEnrichmentResourceAccess
    {
        Task<IEnumerable<BlogPostQueryDto>> BuildBlogPostSummaryQueries(BlogSummaryQueryBuilderConfigDto config);
    }
}