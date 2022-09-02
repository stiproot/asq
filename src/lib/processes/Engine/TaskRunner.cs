using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace processes.Engine
{
    public class TaskRunner: ITaskRunner
    {
        private readonly Guid _id;
        private ISharedContext _context = null;
        private Func<IDictionary<string, IEnginePacket>, Task<IEnginePacket>> _strategyFactory;
        private Func<IDictionary<string, IEnginePacket>, IEnginePacket> _synchronousStrategyFactory;
        private IStrategyFactory _userDefinedStrategyFactory;
        private IEnumerable<ITaskRunner> _paramRunners;
        private IEnumerable<IEnginePacket> _params;
        private IEnumerable<Func<ISharedContext, IEnginePacket>> _contextParams;
        private Func<Exception, Task> _exceptionHandlerAsync;
        private Action<Exception> _exceptionHandler;
        public Guid Id => this._id;

        public TaskRunner() 
            => (this._id, this._paramRunners, this._params, this._contextParams) 
                = (Guid.NewGuid(), new List<ITaskRunner>(), new List<IEnginePacket>(), new List<Func<ISharedContext, IEnginePacket>>());

        public ITaskRunner SetStrategyFactory(Func<IDictionary<string, IEnginePacket>, Task<IEnginePacket>> strategyFactory)
        {
            this._strategyFactory = strategyFactory;
            return this;
        }

        public ITaskRunner SetStrategyFactory(Func<IDictionary<string, IEnginePacket>, IEnginePacket> strategyFactory)
        {
            this._synchronousStrategyFactory = strategyFactory;
            return this;
        }

        public ITaskRunner SetStrategyFactory(IStrategyFactory strategyFactory)
        {
            this._userDefinedStrategyFactory = strategyFactory;
            return this;
        }

        public ITaskRunner SetSharedContext(ISharedContext context)
        {
            this._context = context;
            return this;
        }

        public ITaskRunner AddParam(params ITaskRunner[] runner)
        {
            this._paramRunners = this._paramRunners.Concat(runner);
            return this;
        }

        public ITaskRunner AddParam(params IEnginePacket[] packet)
        {
            this._params = this._params.Concat(packet);
            return this;
        }

        public ITaskRunner AddParam(params Func<ISharedContext, IEnginePacket>[] contextParam)
        {
            this._contextParams = this._contextParams.Concat(contextParam);
            return this;
        }

        public ITaskRunner SetExceptionHandler(Func<Exception, Task> exceptionHandlerAsync)
        {
            this._exceptionHandlerAsync = exceptionHandlerAsync;
            return this;
        }

        public ITaskRunner SetExceptionHandler(Action<Exception> exceptionHandler)
        {
            this._exceptionHandler = exceptionHandler;
            return this;
        }

        public async Task<IEnginePacket> Run(CancellationTokenSource cancellationTokenSource)
        {
            this.Validate();

            if(this._paramRunners.Any())
            {
                var paramTasks = this._paramRunners.Select(p => p.Run(cancellationTokenSource));
                var continuation = Task.WhenAll(paramTasks);
                
                var allResults = await continuation;

                // add results where IEnginePacket is not null (as those are Task void) to context.
                if(this._context != null)
                {
                    allResults
                        .Where(r => r != null)
                        .ToList()
                        .ForEach(r => this._context.AddResult(this._paramRunners.ToList().ElementAt(allResults.ToList().IndexOf(r)).Id, r.Data));
                }

                var nonNullResults = allResults.Where(r => r != null && r.Data != null && r.ParamName != null);
                if(nonNullResults.Any())
                {
                    this._params = this._params.Concat(nonNullResults);
                }
            }

            if(this._contextParams.Any())
            {
                if(this._context == null) throw new InvalidOperationException("Shared context has not been set");

                var contextResults = this._contextParams.Select(p => p(this._context));

                this._params = this._params.Concat(contextResults);
            }

            try
            {
                if(cancellationTokenSource?.IsCancellationRequested == true) throw new TaskCanceledException();

                var paramsDic = this._params.ToDictionary(p => p.ParamName);

                if(this._userDefinedStrategyFactory != null)
                {
                    return await this._userDefinedStrategyFactory.CreateFactory(paramsDic)();
                }

                return this._strategyFactory != null ? await this._strategyFactory(paramsDic) : this._synchronousStrategyFactory(paramsDic);
            }
            catch(Exception ex)
            {
                cancellationTokenSource?.Cancel();

                if(this._exceptionHandlerAsync != null)
                {
                    await this._exceptionHandlerAsync.Invoke(ex);
                }
                if(this._exceptionHandler != null)
                {
                    this._exceptionHandler.Invoke(ex);
                }

                throw;
            }
        }

        public void Validate()
        {
            if(this._userDefinedStrategyFactory == null && this._strategyFactory == null)
            {
                throw new InvalidOperationException("No strategy factory has been provided");
            }
        }
    }
}