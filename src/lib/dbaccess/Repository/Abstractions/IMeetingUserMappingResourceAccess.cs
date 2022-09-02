using System.Threading.Tasks;

namespace dbaccess.Repository
{
    public interface IMeetingUserMappingResourceAccess
    {
        Task AddMeetingUserMapping(long meetingId, long userId);
        Task AddMeetingUserMapping(DTO.Domain.MeetingUserMappingDto dto);
        Task RemoveMeetingUserMapping(long meetingId, long userId);
    }
}