using System;
using System.Threading.Tasks;
using dbaccess.Factory;
using dbaccess.Models;
using DTO.Tracking;
using AutoMapper;
using System.Linq.Expressions;

namespace dbaccess.Repository
{
    public class MeetingUpdateTrackingResourceAccess: BaseResourceAccess<MeetingUpdateTrackingDto, TbMeetingUpdateTracking>, IMeetingUpdateTrackingResourceAccess
    {
        public MeetingUpdateTrackingResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<MeetingUpdateTrackingDto> GetMeetingUpdateTracking(object id)
        {
            TbMeetingUpdateTracking entity;
            if(long.TryParse(id.ToString(), out long longId))
            {
                entity = await this._repo.FindByKey(id);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                Expression<Func<TbMeetingUpdateTracking, bool>> predicate = (TbMeetingUpdateTracking u) => u.UniqueId.Equals(id);
                entity = await this._repo.FindSingleOrDefaultAsync(predicate, null);
            }
            else
            {
                entity = null;
            }

            if(entity == null)
            {
                return null;
            }
            
            return this._mapper.Map<TbMeetingUpdateTracking, MeetingUpdateTrackingDto>(entity);
        }

        public async Task<MeetingUpdateTrackingDto> AddMeetingUpdateTracking(MeetingUpdateTrackingDto dto) 
            => await this.Add(dto);

        public async Task UpdateMeetingUpdateTracking(MeetingUpdateTrackingDto dto) 
            => await this.Update(dto);
    }
}