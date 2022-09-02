using processes.Process;

namespace processes.Factory
{
    public interface IProcessFactory
    {
       IProcess Create(ProcessTypeEnu type);
    }
}