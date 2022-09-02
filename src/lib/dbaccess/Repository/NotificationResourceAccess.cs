using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain;
using AutoMapper;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace dbaccess.Repository
{
    public class NotificationResourceAccess: BaseResourceAccess<NotificationDto, TbNotification>, INotificationResourceAccess
    {
        public NotificationResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<NotificationDto> GetNotification(object id)
        {
            Expression<Func<TbNotification, bool>> predicate = null;
            if(long.TryParse(id.ToString(), out long longId))
            {
                predicate = (TbNotification u) => u.Id.Equals(longId);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                predicate = (TbNotification u) => u.UniqueId.Equals(id);
            }
            else throw new System.InvalidOperationException();

            var entity = await this._repo.FindSingleOrDefaultAsync(predicate, null);

            if(entity == null) return null;
            
            return this._mapper.Map<TbNotification, NotificationDto>(entity);
        }

        public async Task<NotificationDto> AddNotification(NotificationDto dto) 
            => await this.Add(dto);

        public async Task AddNotifications(IEnumerable<NotificationDto> dtos) 
            => await this.Add(dtos);

        public async Task UpdateNotification(NotificationDto dto) 
            => await this.Update(dto);

        public async Task UpdateNotification(IEnumerable<NotificationDto> dtos) 
            => await this.Update(dtos);

        public async Task<IEnumerable<NotificationDto>> GetUnseenNotifications(NotificationQueryDto query)
        {
            Expression<Func<TbNotification, bool>> predicate = (n) => n.UserId.Equals(query.UserId) && !n.Seen && (query.OlderThanId == null || n.Id < query.OlderThanId);
            Expression<Func<TbNotification, object>> orderByDesc = (n) => n.CreationDateUtc;
            
            var dtos = this._mapper.Map<IEnumerable<TbNotification>, IEnumerable<NotificationDto>>(
                await this._repo.FindToListAsync(
                    predicate: predicate, 
                    include: null, 
                    orderByFunc: null, 
                    orderByDescFunc: orderByDesc, 
                    take: 10
                )
            );

            return dtos;
        }

        public async Task ReadNotification(object id)
        {
            var notification = await this.GetNotification(id) ?? throw new System.InvalidOperationException("Notification not found");
            notification.Seen = true;
            await this.UpdateNotification(notification);
        }
    }
}