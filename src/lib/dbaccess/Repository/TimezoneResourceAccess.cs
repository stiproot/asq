using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain;
using AutoMapper;
using System.Linq.Expressions;

namespace dbaccess.Repository
{
    public class TimezoneResourceAccess : BaseResourceAccess<TimezoneDto, TbTimezone>, ITimezoneResourceAccess
    {
        public TimezoneResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<IEnumerable<TimezoneDto>> GetTimezones()
        {
            Expression<Func<TbTimezone, object>> orderByFunc = (TbTimezone t) => t.Display;
            return this._mapper.Map<IEnumerable<TbTimezone>, IEnumerable<TimezoneDto>>(await this._repo.All(null, orderByFunc, null));
        }
    }
}