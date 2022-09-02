using DTO.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dbaccess.Repository
{
    public interface IFocusUserMappingResourceAccess
    {
        Task DeleteFocusUserMapping(IEnumerable<FocusUserMappingDto> dtos);
    }
}