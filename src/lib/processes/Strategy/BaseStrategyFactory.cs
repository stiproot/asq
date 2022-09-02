using System.Collections.Generic;
using System.Threading.Tasks;
using processes.Engine;
using System;

namespace processes.Strategy
{
    public abstract class BaseStrategyFactory: IStrategyFactory
    {
        protected string _nextParamName;
        protected readonly IEnginePacketFactory _packetFactory;

        public BaseStrategyFactory(IEnginePacketFactory packetFactory) => (this._packetFactory) = (packetFactory);

        public IStrategyFactory SetNextParamName(string nextParamName)
        {
            this._nextParamName = nextParamName;
            return this;
        }

        public abstract Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param);
    }
}