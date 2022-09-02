using dbaccess.Models;
using dbaccess.Common;
using DTO.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace dbaccess.Repository.QueryEnrichment
{
    public class MeetingQueryEnrichmentResourceAccess: BaseQueryEnrichmentResourceAccess, IMeetingQueryEnrichmentResourceAccess
    {
        public MeetingQueryEnrichmentResourceAccess(
            IAsqDbContextFactory<ASQContext> contextFactory
        ): base(contextFactory){ }

        public async Task<IEnumerable<MeetingQueryDto>> BuildMeetingSummaryQueries(MeetingSummaryQueryBuilderConfigDto config)
        {
            var q = await
                    (
                        from mapping in this._context.TbFocusMeetingMappings.Include("Focus") 
                        join meeting in this._context.TbMeetings.Include("CreationUser") on mapping.MeetingId equals meeting.Id
                        where 
                                !meeting.Inactive && 
                                !mapping.Inactive && 
                                meeting.MeetingStatusId == config.MeetingStatusId &&
                                (config.CreationUserUniqueId == null || meeting.CreationUser.UniqueId == config.CreationUserUniqueId.ToString())

                        select mapping.Focus
                    )
                    .Distinct()
                    .Select(focus => new MeetingQueryDto
                    {
                        Display = focus.Description,
                        Config = new MeetingFilterConfigDto
                        {
                            MeetingStatusId = config.MeetingStatusId,
                            StartDateUtc = null, 
                            EstimatedDuration = null, 
                            TimezoneId = null, 
                            Foci = new List<short>(){ focus.Id },
                            CreationUserUniqueId = config.CreationUserUniqueId
                        }
                    }).ToListAsync();

            return q;
        }
    }
}