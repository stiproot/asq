using System.Collections.Generic;
using System.Threading.Tasks;
using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain;
using AutoMapper;

namespace dbaccess.Repository
{
    public class CardTypeResourceAccess : BaseResourceAccess<CardTypeDto, TbCardType>, ICardTypeResourceAccess
    {
        public CardTypeResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<IEnumerable<CardTypeDto>> GetCardTypes()
        {
            return await this.All();
        }
    }
}