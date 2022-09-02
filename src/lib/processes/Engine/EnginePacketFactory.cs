namespace processes.Engine
{
  public class EnginePacketFactory: IEnginePacketFactory
  {
    public IEnginePacket Create(object obj, string paramName) 
      => new EnginePacket(new TypeCaster(), obj, paramName);
  }
}