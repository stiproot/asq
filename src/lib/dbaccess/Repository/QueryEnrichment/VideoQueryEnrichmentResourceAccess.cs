using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using dbaccess.Models;
using DTO.Domain;
using dbaccess.Common;

namespace dbaccess.Repository.QueryEnrichment
{
    public class VideoQueryEnrichmentResourceAccess: BaseQueryEnrichmentResourceAccess, IVideoQueryEnrichmentResourceAccess
    {
        public VideoQueryEnrichmentResourceAccess(
            IAsqDbContextFactory<ASQContext> contextFactory
        ) : base (contextFactory) { }

        public async Task<IEnumerable<VideoQueryDto>> BuildVideoSummaryQueries(VideoSummaryQueryBuilderConfigDto config)
        {
            var q = await
                    (
                     from mapping in this._context.TbFocusVideoMappings.Include("Focus") 
                     join video in this._context.TbVideos.Include("CreationUser") on mapping.VideoId equals video.Id
                     where 
                      !video.Inactive && 
                      !mapping.Inactive &&
                      (
                        config.CreationUserUniqueId == null || 
                        video.CreationUser.UniqueId == config.CreationUserUniqueId.ToString()
                      )
                     select mapping.Focus
                    )
                    .Distinct()
                    .Select(focus => new VideoQueryDto
                    {
                        Display = focus.Description,
                        Config = new VideoFilterConfigDto
                        {
                            Foci = new List<short>(){ focus.Id },
                            CreationUserUniqueId = config.CreationUserUniqueId
                        }
                    }).ToListAsync();

            return q;
        }
    }
}