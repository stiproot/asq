using managers.Resource;

namespace processes.Engine
{
    public interface IStrategyFactoryFactory
    {
        IStrategyFactory Create(                
            ZoomOperationEnu type,
            IEnginePacketFactory packetFactory,
            IZoomResourceManager zoomResourceManager
        );

        IStrategyFactory Create(                
            MailOperationEnu operationEnu,
            IEnginePacketFactory packetFactory,
            INotificationResourceManager notificationResourceManager
        );

        IStrategyFactory Create(                
            AccountOperationEnu op,
            IEnginePacketFactory packetFactory,
            IUserResourceManager userResourceManager
        );

        IStrategyFactory Create(                
            MeetingOperationEnu op,
            IEnginePacketFactory packetFactory,
            IMeetingResourceManager meetingResourceManager
        );

        IStrategyFactory Create(                
            MeetingOperationEnu op,
            IEnginePacketFactory packetFactory,
            IZoomResourceManager zoomResourceManager
        );
    }
}