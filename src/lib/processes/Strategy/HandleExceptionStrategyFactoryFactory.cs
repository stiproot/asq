using DTO.Tracking;
using processes.Engine;
using processes.Adapter;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace processes.Strategy
{
    public class HandleExeptionStrategyFactoryFactory: IHandleExceptionStrategyFactoryFactory
    {
        public Func<Exception, Task> CreateFactory(Guid id, string componentId, ITrackingAdapter trackingAdapter)
        {
            return async (Exception exception) => 
            {
                try
                {
                    var tracking = await trackingAdapter.Get(id);
                    if(tracking == null)
                    {
                        throw new Exception("Unable to get tracking object in failure callback");
                    }

                    var components = tracking.TrackingComponents;
                    var component = components.FirstOrDefault(tc => tc.identifier.Equals(componentId));
                    if(component == null)
                    {
                        throw new Exception("Unable to get tracking object's component in failure callback");
                    }
                    
                    component.response = null; 
                    component.failed = true;
                    component.exception_info = ExceptionModelFactory.CreateExceptionDto(exception);
                    tracking.TrackingComponents = components;
                    tracking.Failed = true;

                    await trackingAdapter.Update(tracking);
                }
                catch(Exception)
                {
                    throw;
                }
            };
        }
    }
}