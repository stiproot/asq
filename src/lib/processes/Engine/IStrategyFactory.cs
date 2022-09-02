using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace processes.Engine
{
    public interface IStrategyFactory
    {
        IStrategyFactory SetNextParamName(string nextParamName);
        Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param);
    }
}