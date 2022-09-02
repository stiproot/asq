using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using dbaccess.Models;
using DTO.Domain;
using dbaccess.Common;

namespace dbaccess.Repository.QueryEnrichment
{
    public class UserQueryEnrichmentResourceAccess: BaseQueryEnrichmentResourceAccess, IUserQueryEnrichmentResourceAccess
    {
        public UserQueryEnrichmentResourceAccess(
            IAsqDbContextFactory<ASQContext> contextFactory
        ) : base (contextFactory) { }

        public async Task<IEnumerable<HostQueryDto>> BuildHostSummaryBaseQueries()
        {
            var q = await
                    (
                     from mapping in this._context.TbFocusHostMappings.Include("Focus") 
                     join host in this._context.TbHosts on mapping.HostId equals host.Id
                     //where 
                        //!host.Inactive && !mapping.Inactive
                     select mapping.Focus
                    )
                    .Distinct()
                    .Select(focus => new HostQueryDto
                    {
                        Display = focus.Description,
                        Config = new HostFilterConfigDto
                        {
                            Foci = new List<short>(){ focus.Id }
                        }
                    }).ToListAsync();

            return q;
        }
    }
}