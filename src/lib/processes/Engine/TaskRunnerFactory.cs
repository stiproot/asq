namespace processes.Engine
{
    public class TaskRunnerFactory: ITaskRunnerFactory
    {
        public ITaskRunner Create() => new TaskRunner();
    }
}