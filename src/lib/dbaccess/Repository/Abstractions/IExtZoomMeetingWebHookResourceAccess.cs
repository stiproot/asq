using System.Threading.Tasks;
using DTO.Domain.Ext.Zoom;

namespace dbaccess.Repository
{
    public interface IExtZoomMeetingWebHookResourceAccess
    {
        Task<ExtZoomMeetingWebHookDto> GetExtZoomMeetingWebHook(object id);
        Task<ExtZoomMeetingWebHookDto> AddExtZoomMeetingWebHook(ExtZoomMeetingWebHookDto dto);
    }
}