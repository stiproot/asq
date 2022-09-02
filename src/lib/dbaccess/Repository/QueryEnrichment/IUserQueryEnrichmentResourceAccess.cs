using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Domain;

namespace dbaccess.Repository.QueryEnrichment
{
    public interface IUserQueryEnrichmentResourceAccess
    {
        Task<IEnumerable<HostQueryDto>> BuildHostSummaryBaseQueries();
    }
}