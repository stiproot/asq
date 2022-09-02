using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Domain;
using dbaccess.Repository;

namespace managers.Resource
{
    public class LookupResourceManager : ILookupResourceManager
    {
        private readonly ICardTypeResourceAccess _cardTypeResourceAccess;
        private readonly IFociResourceAccess _fociResourceAccess;
        private readonly ITimezoneResourceAccess _timezoneResourceAccess;

        public LookupResourceManager(
            ICardTypeResourceAccess cardTypeResourceAccess, 
            IFociResourceAccess fociResourceAccess,
            ITimezoneResourceAccess timezoneResourceAccess)
        {
            this._cardTypeResourceAccess = cardTypeResourceAccess;
            this._fociResourceAccess = fociResourceAccess;
            this._timezoneResourceAccess = timezoneResourceAccess;
        }

        public async Task<IEnumerable<CardTypeDto>> GetCardTypes()
        {
            return await this._cardTypeResourceAccess.GetCardTypes();
        }

        public async Task<IEnumerable<FocusDto>> GetFoci()
        {
            return await this._fociResourceAccess.GetFoci();
        }

        public async Task<IEnumerable<TimezoneDto>> GetTimezones()
        {
            return await this._timezoneResourceAccess.GetTimezones();
        }
    }
}