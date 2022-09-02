using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Domain;

namespace dbaccess.Repository.QueryEnrichment
{
    public interface IVideoQueryEnrichmentResourceAccess
    {
        Task<IEnumerable<VideoQueryDto>> BuildVideoSummaryQueries(VideoSummaryQueryBuilderConfigDto config);
    }
}