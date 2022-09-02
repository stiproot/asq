using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Domain;

namespace managers.Resource
{
    public interface ILookupResourceManager
    {
        Task<IEnumerable<CardTypeDto>> GetCardTypes();
        Task<IEnumerable<FocusDto>> GetFoci();
        Task<IEnumerable<TimezoneDto>> GetTimezones();
    }
}