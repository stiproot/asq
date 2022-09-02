using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System;

namespace processes.Engine
{
  public class StrategyFactoryBuilder : IStrategyFactoryBuilder
  {
    private readonly IServiceProvider _serviceProvider;
    private readonly IEnginePacketFactory _enginePacketFactory;

    public StrategyFactoryBuilder(
      IServiceProvider serviceProvider,
      IEnginePacketFactory enginePacketFactory
    )
    {
      this._serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
      this._enginePacketFactory = enginePacketFactory ?? throw new ArgumentNullException(nameof(enginePacketFactory));
    }

    public Func<IDictionary<string, IEnginePacket>, Task<IEnginePacket>> Build(
      Type serviceType,
      string methodName,
      string nextParamName
    )
    {
      return async (param) =>
      {
        var service = this._serviceProvider.GetService(serviceType) ?? throw new InvalidOperationException($"Service with type provided ({serviceType.Name}) not found...");
        var methodInfo = service.GetType().GetMethod(methodName) ?? throw new InvalidOperationException($"{serviceType.Name} does not have method {methodName}");
        var parameters = methodInfo.GetParameters();

        if(parameters.Count() != param.Count())
        {
          var exceptionMessage = String.Format(
            "Invalid parameters for method {0} provided. Parameters expected: {1}. Parameters provided: {2}",
            String.Join(",", parameters.Select(p => $"{p.Name} (type: {p.GetType().Name})")),
            String.Join(",", param.Select(p => $"{p.Key} (type: {p.Value.Data.GetType().Name})"))
          );
          throw new ArgumentException(exceptionMessage);
        }

        var arguments = parameters.Select(p => param[p.Name].Data).ToArray();
        
        object result = null;

        if(TypeInspector.HasReturnTypeOfTask(service.GetType(), methodName))
        {
          var task = (Task)serviceType.InvokeMember(methodName, BindingFlags.InvokeMethod, null, service, arguments, CultureInfo.InvariantCulture);
          await task;
          result = task.GetType().GetProperty("Result").GetValue(task);
        }
        else 
        {
          result = serviceType.InvokeMember(methodName, BindingFlags.InvokeMethod, null, service, arguments, CultureInfo.InvariantCulture);
        }

        return result == null ? null : this._enginePacketFactory.Create(result, nextParamName);
      };
    }
  }
}