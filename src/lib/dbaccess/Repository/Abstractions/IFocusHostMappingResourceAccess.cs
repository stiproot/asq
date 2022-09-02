using DTO.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dbaccess.Repository
{
    public interface IFocusHostMappingResourceAccess
    {
        Task DeleteFocusHostMapping(IEnumerable<FocusHostMappingDto> dtos);
    }
}