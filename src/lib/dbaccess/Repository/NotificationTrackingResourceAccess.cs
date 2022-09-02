using dbaccess.Factory;
using dbaccess.Models;
using DTO.Tracking;
using AutoMapper;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace dbaccess.Repository
{
    public class NotificationTrackingResourceAccess: BaseResourceAccess<NotificationTrackingDto, TbNotificationTracking>, INotificationTrackingResourceAccess
    {
        public NotificationTrackingResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<NotificationTrackingDto> GetNotificationTracking(object id)
        {
            TbNotificationTracking entity;
            if(long.TryParse(id.ToString(), out long longId))
            {
                entity = await this._repo.FindByKey(id);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                Expression<Func<TbNotificationTracking, bool>> predicate = (TbNotificationTracking u) => u.UniqueId.Equals(id);
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
            
            return this._mapper.Map<TbNotificationTracking, NotificationTrackingDto>(entity);
        }

        public async Task<NotificationTrackingDto> AddNotificationTracking(NotificationTrackingDto dto) 
            => await this.Add(dto);

        public async Task UpdateNotificationTracking(NotificationTrackingDto dto) 
            => await this.Update(dto);
    }
}