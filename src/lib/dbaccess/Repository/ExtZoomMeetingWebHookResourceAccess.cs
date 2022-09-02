using System;
using System.Threading.Tasks;
using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain.Ext.Zoom;
using AutoMapper;
using System.Linq.Expressions;

namespace dbaccess.Repository
{
    public class ExtZoomMeetingWebHookResourceAccess: BaseResourceAccess<ExtZoomMeetingWebHookDto, TbExtZoomWebHook>, IExtZoomMeetingWebHookResourceAccess
    {
        public ExtZoomMeetingWebHookResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<ExtZoomMeetingWebHookDto> GetExtZoomMeetingWebHook(object id)
        {
            Expression<Func<TbExtZoomWebHook, bool>> predicate = null; 
            if(long.TryParse(id.ToString(), out long longId))
            {
                predicate = (TbExtZoomWebHook u) => u.Id.Equals(longId);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                predicate = (TbExtZoomWebHook u) => u.UniqueId.Equals(id);
            }
            else
            {
                throw new NotImplementedException("Can only search by id and uniqueid");
            }

            var entity = await this._repo.FindSingleOrDefaultAsync(predicate, null);

            if(entity == null)
            {
                return null;
            }
            
            return this._mapper.Map<TbExtZoomWebHook, ExtZoomMeetingWebHookDto>(entity);
        }

        public async Task<ExtZoomMeetingWebHookDto> AddExtZoomMeetingWebHook(ExtZoomMeetingWebHookDto dto) 
        {
            return await this.Add(dto);
        }
    }
}