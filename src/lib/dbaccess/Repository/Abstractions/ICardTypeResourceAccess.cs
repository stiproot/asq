using System.Threading.Tasks;
using System.Collections.Generic;
using DTO.Domain;

namespace dbaccess.Repository
{
    public interface ICardTypeResourceAccess
    {
        Task<IEnumerable<CardTypeDto>> GetCardTypes();
    }
}