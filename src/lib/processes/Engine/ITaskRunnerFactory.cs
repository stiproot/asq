namespace processes.Engine
{
    public interface ITaskRunnerFactory
    {
        ITaskRunner Create();
    }
}