using managers.Resource;
using processes.Engine;
using System;

namespace processes.Adapter
{
    public class TrackingAdapterFactory: ITrackingAdapterFactory
    {
        public ITrackingAdapter Create(AccountOperationEnu op, IAccountResourceManager accountResourceManager)
        {
            return op switch
            {

                AccountOperationEnu.CREATE  => new AccountCreationTrackingAdapter(accountResourceManager),
                AccountOperationEnu.EDIT    => new AccountUpdateTrackingAdapter(accountResourceManager),
                _                           => throw new Exception("Invalid crud operation supplied to tracking adapter factory")
            };
        }

        public ITrackingAdapter Create(MeetingOperationEnu op, IMeetingResourceManager meetingResourceManager)
        {
            return op switch
            {
                MeetingOperationEnu.CREATE                  => new MeetingCreationTrackingAdapter(meetingResourceManager),
                MeetingOperationEnu.EDIT                    => new MeetingUpdateTrackingAdapter(meetingResourceManager),
                MeetingOperationEnu.DOWNLOAD_RECORDING      => new MeetingRecordingDownloadTrackingAdapter(meetingResourceManager),
                _                                           => throw new Exception("Invalid crud operation supplied to tracking adapter factory")
            };
        }

        //public ITrackingAdapter Create(IMailResourceManager resourceManager) => new MailSendTrackingAdapter(resourceManager);
    }
}