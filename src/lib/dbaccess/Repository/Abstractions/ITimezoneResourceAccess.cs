using System.Threading.Tasks;
using System.Collections.Generic;
using DTO.Domain;

namespace dbaccess.Repository
{
    public interface ITimezoneResourceAccess
    {
        Task<IEnumerable<TimezoneDto>> GetTimezones();
    }
}