using managers.Resource;
using processes.Strategy;
using System;

namespace processes.Engine
{
    public class StrategyFactoryFactory: IStrategyFactoryFactory
    {
        public IStrategyFactory Create(                
            ZoomOperationEnu op,
            IEnginePacketFactory packetFactory,
            IZoomResourceManager zoomResourceManager
        )
        {
            return op switch
            {
                ZoomOperationEnu.CreateUserStrategy => new CreateZoomUserStrategyFactory(packetFactory, zoomResourceManager),
                ZoomOperationEnu.DeleteUserStrategy => new DeleteZoomUserStrategyFactory(zoomResourceManager),
                ZoomOperationEnu.CreateMeetingStrategy => new CreateZoomMeetingStrategyFactory(packetFactory, zoomResourceManager),
                ZoomOperationEnu.GetMeetingRecordingStrategy => new GetZoomMeetingRecordingsStrategyFactory(packetFactory, zoomResourceManager),
                ZoomOperationEnu.UpdateMeetingStrategy => new UpdateZoomMeetingStrategyFactory(zoomResourceManager),
                _ => throw new ArgumentException("Invalid arument provided", nameof(op))
            };
        }

        public IStrategyFactory Create(                
            MailOperationEnu op,
            IEnginePacketFactory packetFactory,
            INotificationResourceManager notificationResourceManager
        )
        {
            return op switch
            {

                MailOperationEnu.TRACK => new AddMailStrategyFactory(notificationResourceManager),
                MailOperationEnu.SEND => new SendMailStrategyFactory(packetFactory, notificationResourceManager),
                _ => throw new NotImplementedException()
            };
        }

        public IStrategyFactory Create(                
            AccountOperationEnu op,
            IEnginePacketFactory packetFactory,
            IUserResourceManager userResourceManager
        )
        {
            return op switch
            {
                AccountOperationEnu.CREATE => new CreateUserStrategyFactory(userResourceManager),
                AccountOperationEnu.EDIT => new UpdateUserStrategyFactory(packetFactory, userResourceManager),
                _ => throw new Exception("Invalid crud operation supplied")
            };
        }

        public IStrategyFactory Create(                
            MeetingOperationEnu op,
            IEnginePacketFactory packetFactory,
            IMeetingResourceManager meetingResourceManager
        )
        {
            return op switch
            {
                MeetingOperationEnu.CREATE                          => new CreateMeetingStrategyFactory(meetingResourceManager),
                MeetingOperationEnu.EDIT                            => new UpdateMeetingStrategyFactory(meetingResourceManager),
                MeetingOperationEnu.PARTICIPATE                     => new MeetingParticipationStrategyFactory(meetingResourceManager),
                MeetingOperationEnu.GET                             => new GetMeetingStrategyFactory(packetFactory, meetingResourceManager),
                MeetingOperationEnu.CREATE_EXT_MEETING_RECORDING    => new CreateExtZoomMeetingRecordingStrategyFactory(packetFactory, meetingResourceManager),
                MeetingOperationEnu.SCAN_MEETING_RECORDING_DIR_AND_UPDATE_MEETING_STATUS => new CheckFilesAndCreateMeetingRecordingsStrategyFactory(packetFactory, meetingResourceManager),
                _                                                   => throw new Exception("Invalid crud operation supplied")
            };
        }

        public IStrategyFactory Create(                
            MeetingOperationEnu op,
            IEnginePacketFactory packetFactory,
            IZoomResourceManager zoomResourceManager
        )
        {
            return op switch
            {
                MeetingOperationEnu.DOWNLOAD_RECORDING    => new DownloadMeetingRecordingsStrategyFactory(packetFactory, zoomResourceManager),
                _                                                   => throw new Exception("Invalid crud operation supplied")
            };
        }
    }
}