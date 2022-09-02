using System.Threading.Tasks;
using processes.Adapter;
using System;

namespace processes.Engine
{
    public interface IHandleExceptionStrategyFactoryFactory
    {
        Func<Exception, Task> CreateFactory(Guid id, string componentId, ITrackingAdapter trackingAdapter);
    }
}