using dbaccess.Factory;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace dbaccess.Repository
{
    public abstract class BaseResourceAccess<TDto, TEnt> where TEnt: class
    {
        protected readonly IGenericRepository<TEnt> _repo;
        protected readonly IMapper _mapper;

        public BaseResourceAccess(
            IGenericRepositoryFactory genericRepositoryFactory, 
            IMapper mapper
        )
        {
            this._repo = genericRepositoryFactory.Create<TEnt>();
            this._mapper = mapper;
        }

        protected async Task<IEnumerable<TDto>> All()
            => this._mapper.Map<IEnumerable<TEnt>, IEnumerable<TDto>>(await this._repo.All());

        protected async Task<TDto> Add(TDto dto)
        {
            var ent = this.MapToEnt(dto);
            ent = await this._repo.Add(ent);
            return this.MapToDto(ent);
        }

        protected async Task Add(IEnumerable<TDto> dtos)
        {
            var ents = this.MapToEnt(dtos);
            await this._repo.Add(ents);
        }

        protected async Task Update(TDto dto)
        {
            var ent = this.MapToEnt(dto);
            await this._repo.Update(ent);
        }

        protected async Task Update(IEnumerable<TDto> dtos)
        {
            var ents = this.MapToEnt(dtos);
            await this._repo.Update(ents);
        }

        protected async Task Delete(object id)
        {
            var ent = await this._repo.FindByKey(id);
            await this._repo.Delete(ent);
        }

        protected async Task Delete(IEnumerable<TDto> dtos)
        {
            var entities = this.MapToEnt(dtos);
            await this._repo.Delete(entities);
        }

        protected virtual TDto MapToDto(TEnt entity) 
            => _mapper.Map<TEnt, TDto>(entity);

        protected virtual IEnumerable<TDto> MapToDto(IEnumerable<TEnt> ents) 
            =>_mapper.Map<IEnumerable<TEnt>, IEnumerable<TDto>>(ents);

        protected virtual TEnt MapToEnt(TDto dto) 
            => this._mapper.Map<TDto, TEnt>(dto);

        protected virtual IEnumerable<TEnt> MapToEnt(IEnumerable<TDto> dtos) 
            => this._mapper.Map<IEnumerable<TDto>, IEnumerable<TEnt>>(dtos);
    }
}
