using System.Threading.Tasks;
using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain;
using AutoMapper;
using System.Linq.Expressions;
using System;

namespace dbaccess.Repository
{
    public class MeetingUserMappingResourceAccess : BaseResourceAccess<MeetingUserMappingDto, TbMeetingUserMapping>, IMeetingUserMappingResourceAccess
    {
        public MeetingUserMappingResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task AddMeetingUserMapping(MeetingUserMappingDto dto)
        {
            var entity = this._mapper.Map<MeetingUserMappingDto, TbMeetingUserMapping>(dto);
            await this._repo.Add(entity);
        }

        public async Task AddMeetingUserMapping(long meetingId, long userId)
        {
            var dto = new MeetingUserMappingDto(meetingId, userId);
            var entity = this._mapper.Map<MeetingUserMappingDto, TbMeetingUserMapping>(dto);
            await this._repo.Add(entity);
        }

        public async Task RemoveMeetingUserMapping(long meetingId, long userId)
        {
            Expression<Func<TbMeetingUserMapping, bool>> predicate = (entity) => entity.MeetingId.Equals(meetingId) && entity.UserId.Equals(userId);

            var entity = await this._repo.FindFirstOrDefaultAsync(predicate);

            await this._repo.Delete(entity);
        }
    }
}