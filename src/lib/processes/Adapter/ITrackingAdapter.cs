using System.Threading.Tasks;
using DTO.Tracking;
using System;

namespace processes.Adapter
{
    public interface ITrackingAdapter
    {
        Task<BaseTracking> Get(Guid id);
        Task Update(BaseTracking tracking);
    }
}