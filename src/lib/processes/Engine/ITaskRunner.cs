using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace processes.Engine
{
    public interface ITaskRunner
    {
        Guid Id{ get; }
        ITaskRunner SetStrategyFactory(IStrategyFactory strategyFactory);
        ITaskRunner SetStrategyFactory(Func<IDictionary<string, IEnginePacket>, IEnginePacket> strategyFactory);
        ITaskRunner SetStrategyFactory(Func<IDictionary<string, IEnginePacket>, Task<IEnginePacket>> strategyFactory);
        ITaskRunner SetSharedContext(ISharedContext context);
        ITaskRunner AddParam(params ITaskRunner[] runner);
        ITaskRunner AddParam(params IEnginePacket[] packet);
        ITaskRunner AddParam(params Func<ISharedContext, IEnginePacket>[] contextParam);
        ITaskRunner SetExceptionHandler(Func<Exception, Task> exceptionHandlerAsync);
        ITaskRunner SetExceptionHandler(Action<Exception> exceptionHandler);
        Task<IEnginePacket> Run(CancellationTokenSource cancellationTokenSource);
        void Validate();
    }
}