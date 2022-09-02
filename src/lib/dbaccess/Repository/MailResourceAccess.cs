using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain;
using DTO.Tracking;
using AutoMapper;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace dbaccess.Repository
{
    public class MailResourceAccess: BaseResourceAccess<MailDto, TbMail>, IMailResourceAccess
    {
        public MailResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<MailDto> GetMail(object id)
        {
            TbMail entity;
            if(long.TryParse(id.ToString(), out long longId))
            {
                entity = await this._repo.FindByKey(id);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                Expression<Func<TbMail, bool>> predicate = (TbMail u) => u.UniqueId.Equals(id);
                entity = await this._repo.FindSingleOrDefaultAsync(predicate, null);
            }
            else
            {
                entity = null;
            }

            if(entity == null)
            {
                return null;
            }
            
            return this._mapper.Map<TbMail, MailDto>(entity);
        }

        public async Task<MailDto> AddMail(MailDto dto) 
            => await this.Add(dto);

        public async Task AddMail(IEnumerable<MailDto> dtos) 
            => await this.Add(dtos);

        public async Task UpdateMail(MailDto dto) 
        {
            Func<TbMail, bool> predicate = (TbMail u) => u.UniqueId.Equals(dto.UniqueId);
            await this.Update(dto);
        }

        public async Task UpdateMail(IEnumerable<MailDto> dtos) 
        {
            await this.Update(dtos);
        }

        public async Task<IEnumerable<MailDto>> GetPendingMailAndMarkForProcessing()
        {
            Expression<Func<TbMail, bool>> predicate = (tracking) => tracking.StatusId == (short)MailTrackingStatusEnu.AWAITING;
            Expression<Func<TbMail, object>> orderBy = (tracking) => tracking.CreationDateUtc;
            
            var dtos = this._mapper.Map<IEnumerable<TbMail>, IEnumerable<MailDto>>(
                await this._repo.FindToListAsync(
                    predicate: predicate, 
                    include: null, 
                    orderByFunc: orderBy, 
                    orderByDescFunc: null, 
                    take: 10
                )
            );

            dtos.ToList().ForEach(dto => dto.StatusId = (short)MailTrackingStatusEnu.PROCESSING);

            await this.UpdateMail(dtos);

            return dtos;
        }
    }
}