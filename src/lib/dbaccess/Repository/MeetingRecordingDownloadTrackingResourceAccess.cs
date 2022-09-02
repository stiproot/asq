using dbaccess.Factory;
using dbaccess.Models;
using DTO.Tracking;
using AutoMapper;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;

namespace dbaccess.Repository
{
    public class MeetingRecordingDownloadTrackingResourceAccess: BaseResourceAccess<MeetingRecordingDownloadTrackingDto, TbMeetingRecordingDownloadTracking>, IMeetingRecordingDownloadTrackingResourceAccess
    {
        public MeetingRecordingDownloadTrackingResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<MeetingRecordingDownloadTrackingDto> GetMeetingRecordingDownloadTracking(object id)
        {
            Expression<Func<TbMeetingRecordingDownloadTracking, bool>> predicate = null; 
            TbMeetingRecordingDownloadTracking entity;
            if(long.TryParse(id.ToString(), out long longId))
            {
                predicate = (TbMeetingRecordingDownloadTracking u) => u.Id.Equals(longId);
                //entity = await this._repo.FindByKey(id);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                predicate = (TbMeetingRecordingDownloadTracking u) => u.UniqueId.Equals(id);
            }
            else throw new System.InvalidOperationException();

            entity = await this._repo.FindSingleOrDefaultAsync(predicate, null);

            if(entity == null) return null;
            
            return this._mapper.Map<TbMeetingRecordingDownloadTracking, MeetingRecordingDownloadTrackingDto>(entity);
        }

        public async Task<MeetingRecordingDownloadTrackingDto> AddMeetingRecordingDownloadTracking(MeetingRecordingDownloadTrackingDto dto)
            => await this.Add(dto);

        public async Task UpdateMeetingRecordingDownloadTracking(MeetingRecordingDownloadTrackingDto dto) 
        {
            await this.Update(dto);
        }
    }
}