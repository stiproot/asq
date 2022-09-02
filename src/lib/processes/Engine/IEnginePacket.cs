namespace processes.Engine
{
    public interface IEnginePacket
    {
        object Data{ get; set; }
        string ParamName{ get; set; }
        T Cast<T>();
    }
}