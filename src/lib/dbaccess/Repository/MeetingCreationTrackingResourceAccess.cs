using System;
using System.Threading.Tasks;
using dbaccess.Factory;
using dbaccess.Models;
using DTO.Tracking;
using AutoMapper;
using System.Linq.Expressions;

namespace dbaccess.Repository
{
    public class MeetingCreationTrackingResourceAccess: BaseResourceAccess<MeetingCreationTrackingDto, TbMeetingCreationTracking>, IMeetingCreationTrackingResourceAccess
    {
        public MeetingCreationTrackingResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<MeetingCreationTrackingDto> GetMeetingCreationTracking(object id)
        {
            TbMeetingCreationTracking entity;
            if(long.TryParse(id.ToString(), out long longId))
            {
                entity = await this._repo.FindByKey(id);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                Expression<Func<TbMeetingCreationTracking, bool>> predicate = (TbMeetingCreationTracking u) => u.UniqueId.Equals(id);
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
            
            return this._mapper.Map<TbMeetingCreationTracking, MeetingCreationTrackingDto>(entity);
        }

        public async Task<MeetingCreationTrackingDto> AddMeetingCreationTracking(MeetingCreationTrackingDto dto) 
        {
            return await this.Add(dto);
        }

        public async Task UpdateMeetingCreationTracking(MeetingCreationTrackingDto dto) 
        {
            await this.Update(dto);
        }
    }
}