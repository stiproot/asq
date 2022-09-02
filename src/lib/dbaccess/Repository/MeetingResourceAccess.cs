using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace dbaccess.Repository
{
    public class MeetingResourceAccess: BaseResourceAccess<MeetingDto, TbMeeting>, IMeetingResourceAccess
    {
        private readonly IEnumerable<string> _includes = new List<string>()
        {
            "Host.TbFocusHostMappings.Focus",
            "MeetingStatus",
            "Img",
            "Timezone",
            "ExtMeeting",
            "TbFocusMeetingMappings.Focus",
            "TbExtZoomWebHooks",
            "TbMeetingUserMappings.TbMeetingReviews",
            "CreationUser.Img",
            "TbMeetingRecordings"
        }; 

        public MeetingResourceAccess(
            IGenericRepositoryFactory repositoryFactory,
            IMapper mapper
        ): base(repositoryFactory, mapper){ }

        public async Task<MeetingDto> GetMeeting(object id)
        {
            Expression<Func<TbMeeting, bool>> predicate = null;
            TbMeeting entity;
            if(long.TryParse(id.ToString(), out long longId))
            {
                predicate = (TbMeeting u) => u.Id.Equals(longId);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                predicate = (TbMeeting u) => u.UniqueId.Equals(id);
            }
            else throw new NotImplementedException();

            entity = await this._repo.FindSingleOrDefaultAsync(predicate, this._includes);

            if(entity == null) return null;

            return this._mapper.Map<TbMeeting, MeetingDto>(entity);
        }

        public async Task<IEnumerable<MeetingSummaryDto>> GetMeetingSummariesByFilter(MeetingFilterConfigDto filter)
        {
            Expression<Func<TbMeeting, bool>> predicate = 
                (meeting) => 
                    // Foci...
                    (!filter.Foci.Any() || meeting.TbFocusMeetingMappings.Any(mapping => filter.Foci.Contains(mapping.FocusId))) && 
                    // Meeting status...
                    (filter.MeetingStatusId == null || (short)filter.MeetingStatusId == meeting.MeetingStatusId) &&
                    // Creation user guid...
                    (filter.CreationUserUniqueId == null || filter.CreationUserUniqueId.ToString() == meeting.CreationUser.UniqueId) && 
                    // Creation user name...
                    (
                        (filter.CreationUserName == null || filter.CreationUserName == string.Empty) || 
                        (
                            meeting.CreationUser.Name.Contains(filter.CreationUserName) || 
                            meeting.CreationUser.Surname.Contains(filter.CreationUserName) ||
                            meeting.CreationUser.Username.Contains(filter.CreationUserName)
                        )
                    ) &&
                    // Elastic...
                    (
                        (filter.Elastic == null || filter.Elastic == string.Empty) ||
                        (
                            meeting.Title.Contains(filter.Elastic) ||
                            meeting.Description.Contains(filter.Elastic) ||
                            (
                                meeting.CreationUser.Name.Contains(filter.Elastic) || 
                                meeting.CreationUser.Surname.Contains(filter.Elastic) ||
                                meeting.CreationUser.Username.Contains(filter.Elastic)
                            )
                        )
                    );
            Expression<Func<TbMeeting, object>> orderByFunc = null; 
            Expression<Func<TbMeeting, object>> orderByDescFunc = (TbMeeting m) => m.CreationDateUtc;
            var take = filter.Take ?? 10;
            
            var summaries = this._mapper.Map<IEnumerable<TbMeeting>, IEnumerable<MeetingSummaryDto>>( 
                await this._repo.FindToListAsync(predicate, this._includes, orderByFunc, orderByDescFunc, take));
            
            if(filter.RequestingUserUtcOffset != null) summaries.ToList().ForEach(s => s.StartDateUtc = s.StartDateUtc.AddHours((int)filter.RequestingUserUtcOffset));

            return summaries;
        }

        public async Task<MeetingDto> AddMeeting(MeetingDto dto) 
            => await this.Add(dto);

        public async Task UpdateMeeting(MeetingDto dto) 
            => await this.Update(dto);

        public async Task UpdateMeetings(IEnumerable<MeetingDto> dtos) 
            => await this.Update(dtos);

        public async Task DeleteMeeting(object id) 
            => await this.Delete(id);

        public async Task<MeetingDto> GetMeetingByExtMeetingId(long extMeetingId)
        {
            const string sql = "call pr_GetMeetingByExtMeetingId({0});";
            var meeting = await this._repo.FromSqlRawFirstOrDefaultAsync(sql, new object[]{ extMeetingId });
            return this._mapper.Map<TbMeeting, MeetingDto>(meeting);
        }

        public async Task<long> GetMeetingIdByExtMeetingId(long extMeetingId)
            => (await this.GetMeetingByExtMeetingId(extMeetingId)).Id;

        public async Task UpdateMeetingStatus(object id)
        {
            var meeting = await this.GetMeeting(id);
            if(meeting == null) throw new InvalidOperationException($"Meeting not found with id ({id}) provided");

            // this will cover the basic meeting validation logic... later on we can report on whether users left early etc.

            // let's look for a meeting started webhook
            var startedWebHook = meeting.ExtMeetingWebHooks.FirstOrDefault(w => w.EventType.Equals("meeting.started"));
            // let's look for a meeting started webhook
            var endedWebHook = meeting.ExtMeetingWebHooks.FirstOrDefault(w => w.EventType.Equals("meeting.ended"));

            // let's check the number of active participants against the number of participation webhooks
            //var activeParticipants = meeting.Participants.Where(p => !p.Inactive);
            //var participationWebHooks = meeting.ExtMeetingWebHooks.Where(w => w.EventType == "meeting.participant_joined");

            // at this point we are just looking for meeting.started & meeting.ended webhooks...
            if(startedWebHook != null && endedWebHook != null)
            {
                // we can update this meeting status
                meeting.MeetingStatusId = (int)MeetingStatusEnu.COMPLETE;
                await this.UpdateMeeting(meeting);
            }
        }

        public async Task<IEnumerable<MeetingDto>> GetCompleteMeetingsAndMarkAsRecordingDownloading()
        {
            Expression<Func<TbMeeting, bool>> predicate = (meeting) => meeting.MeetingStatusId == (short)DTO.Domain.MeetingStatusEnu.COMPLETE;
            Expression<Func<TbMeeting, object>> orderBy = (tracking) => tracking.CreationDateUtc;
            
            var meetings = await this._repo.FindToListAsync(
                predicate: predicate, 
                include: this._includes, 
                orderByFunc: orderBy, 
                orderByDescFunc: null, 
                take: 10
            );

            var dtos = this._mapper.Map<IEnumerable<TbMeeting>, IEnumerable<MeetingDto>>(meetings);

            dtos.ToList().ForEach(dto => dto.MeetingStatusId = (short)DTO.Domain.MeetingStatusEnu.COMPLETE_RECORDING_DOWNLOAD_IN_PROGRESS);

            await this.UpdateMeetings(dtos);

            return dtos;
        }
    }
}
