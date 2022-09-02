using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Domain;

namespace dbaccess.Repository.QueryEnrichment
{
    public interface IMeetingQueryEnrichmentResourceAccess
    {
        Task<IEnumerable<MeetingQueryDto>> BuildMeetingSummaryQueries(MeetingSummaryQueryBuilderConfigDto config);
    }
}