using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using dbaccess.Models;
using DTO.Domain;
using dbaccess.Common;

namespace dbaccess.Repository.QueryEnrichment
{
    public class BlogPostQueryEnrichmentResourceAccess: BaseQueryEnrichmentResourceAccess, IBlogPostQueryEnrichmentResourceAccess
    {
        public BlogPostQueryEnrichmentResourceAccess(
            IAsqDbContextFactory<ASQContext> contextFactory
        ) : base (contextFactory) { }

        public async Task<IEnumerable<BlogPostQueryDto>> BuildBlogPostSummaryQueries(BlogSummaryQueryBuilderConfigDto config)
        {
            var q = await
                    (
                     from mapping in this._context.TbFocusBlogPostMappings.Include("Focus") 
                     join blogPost in this._context.TbBlogPosts.Include("CreationUser") on mapping.BlogPostId equals blogPost.Id
                     where 
                        !blogPost.Inactive && !mapping.Inactive &&
                        (config.CreationUserUniqueId == null || blogPost.CreationUser.UniqueId == config.CreationUserUniqueId.ToString())
                     select mapping.Focus
                    )
                    .Distinct()
                    .Select(focus => new BlogPostQueryDto
                    {
                        Display = focus.Description,
                        Config = new BlogPostFilterConfigDto
                        {
                            Foci = new List<short>(){ focus.Id },
                            CreationUserUniqueId = config.CreationUserUniqueId
                        }
                    }).ToListAsync();

            return q;
        }
    }
}