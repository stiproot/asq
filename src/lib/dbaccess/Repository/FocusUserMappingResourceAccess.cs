using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dbaccess.Repository
{
    public class FocusUserMappingResourceAccess : BaseResourceAccess<FocusUserMappingDto, TbFocusUserMapping>, IFocusUserMappingResourceAccess
    {
        public FocusUserMappingResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task DeleteFocusUserMapping(IEnumerable<FocusUserMappingDto> dtos)
        {
            await this.Delete(dtos);
        }
    }
}