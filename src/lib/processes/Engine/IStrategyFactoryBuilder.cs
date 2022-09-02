using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace processes.Engine
{
  public interface IStrategyFactoryBuilder
  {
    Func<IDictionary<string, IEnginePacket>, Task<IEnginePacket>> Build(
      Type serviceType,
      string methodName,
      string nextParamName
    );
  }
}