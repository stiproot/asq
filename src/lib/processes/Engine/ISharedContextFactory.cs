namespace processes.Engine
{
    public interface ISharedContextFactory
    {
        ISharedContext Create();
    }
}