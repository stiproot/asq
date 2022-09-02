using managers.Resource;
using processes.Engine;

namespace processes.Adapter
{
    public interface ITrackingAdapterFactory
    {
        ITrackingAdapter Create(AccountOperationEnu op, IAccountResourceManager accountResourceManager);
        ITrackingAdapter Create(MeetingOperationEnu op, IMeetingResourceManager meetingResourceManager);
        //ITrackingAdapter Create(IMailResourceManager mailResourceManager);
    }
}