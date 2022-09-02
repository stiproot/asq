using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dbaccess.Repository
{
    public class FocusHostMappingResourceAccess : BaseResourceAccess<FocusHostMappingDto, TbFocusHostMapping>, IFocusHostMappingResourceAccess
    {
        public FocusHostMappingResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task DeleteFocusHostMapping(IEnumerable<FocusHostMappingDto> dtos)
        {
            await this.Delete(dtos);
        }
    }
}