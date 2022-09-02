namespace processes.Engine
{
    public interface ISharedContext 
    {
        object GetResult(System.Guid key);
        void AddResult(System.Guid key, object value);
    }
}