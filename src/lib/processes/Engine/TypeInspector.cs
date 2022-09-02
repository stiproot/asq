using System.Threading.Tasks;
using System;

namespace processes.Engine
{
  internal static class TypeInspector
  {
    public static bool HasReturnTypeOfTask(Type type, string methodName)
    {
      var methodInfo = type.GetMethod(methodName);
      var returnParameterType = methodInfo.ReturnParameter.ParameterType;
      return returnParameterType == typeof(Task) || returnParameterType.BaseType == typeof(Task);
    }
  }
}