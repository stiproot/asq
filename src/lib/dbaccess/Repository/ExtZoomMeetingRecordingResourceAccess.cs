using System;
using System.Threading.Tasks;
using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain.Ext.Zoom;
using AutoMapper;
using System.Linq.Expressions;

namespace dbaccess.Repository
{
    public class ExtZoomMeetingRecordingResourceAccess: BaseResourceAccess<ExtZoomMeetingRecordingDto, TbExtZoomMeetingRecording>, IExtZoomMeetingRecordingResourceAccess
    {
        public ExtZoomMeetingRecordingResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<ExtZoomMeetingRecordingDto> GetExtZoomMeetingRecording(object id)
        {
            Expression<Func<TbExtZoomMeetingRecording, bool>> predicate = null; 
            if(long.TryParse(id.ToString(), out long longId))
            {
                predicate = (TbExtZoomMeetingRecording u) => u.Id.Equals(longId);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                predicate = (TbExtZoomMeetingRecording u) => u.UniqueId.Equals(id);
            }
            else throw new NotImplementedException("Can only search by id and uniqueid");

            var entity = await this._repo.FindSingleOrDefaultAsync(predicate, null);

            if(entity == null) return null;
            
            return this._mapper.Map<TbExtZoomMeetingRecording, ExtZoomMeetingRecordingDto>(entity);
        }

        public async Task<ExtZoomMeetingRecordingDto> AddExtZoomMeetingRecording(ExtZoomMeetingRecordingDto dto)
            => await this.Add(dto);
    }
}