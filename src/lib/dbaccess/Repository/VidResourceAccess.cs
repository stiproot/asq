using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq.Expressions;
using System;

namespace dbaccess.Repository
{
    public class VidResourceAccess: BaseResourceAccess<VidDto, TbVid>, IVidResourceAccess
    {
        public VidResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<VidDto> GetVid(object id)
        {
            Expression<Func<TbVid, bool>> predicate = null;
            TbVid entity;
            if(long.TryParse(id.ToString(), out long longId))
            {
                predicate = (TbVid u) => u.Id.Equals(longId);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                predicate = (TbVid u) => u.UniqueId.Equals(id);
            }
            else throw new NotImplementedException();

            entity = await this._repo.FindSingleOrDefaultAsync(predicate);

            if(entity == null)
            {
                return null;
            }
            return this._mapper.Map<TbVid, VidDto>(entity);
        }

        public async Task<VidDto> AddVid(VidDto dto)
          => await this.Add(dto);

        public async Task UpdateVid(VidDto dto)
          => await this.Update(dto);

        public async Task DeleteVid(object id)
          => await this.Delete(id);
    }
}