using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DTO.Events;

namespace processes
{
    public interface IProcess
    {
        IEvent Event{ get; set; }
        IProcess SetEvent(IEvent @event);
        IProcess SetLogger(ILogger logger);
        IProcess Init();
        Task Execute();
    }
}