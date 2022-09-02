namespace processes.Engine
{
    public class SharedContextFactory: ISharedContextFactory
    {
        public ISharedContext Create() => new SharedContext();
    }
}