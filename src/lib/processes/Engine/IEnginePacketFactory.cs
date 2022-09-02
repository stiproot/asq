namespace processes.Engine
{
    public interface IEnginePacketFactory
    {
        IEnginePacket Create(object obj, string paramName);
    }
}