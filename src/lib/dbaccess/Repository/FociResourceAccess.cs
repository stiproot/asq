using System.Collections.Generic;
using System.Threading.Tasks;
using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain;
using AutoMapper;

namespace dbaccess.Repository
{
    public class FociResourceAccess : BaseResourceAccess<FocusDto, TbFocus>, IFociResourceAccess
    {
        public FociResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<IEnumerable<FocusDto>> GetFoci()
        {
            return await this.All();
        }
    }
}