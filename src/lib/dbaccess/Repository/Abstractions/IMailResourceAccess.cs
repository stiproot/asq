using DTO.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace dbaccess.Repository
{
    public interface IMailResourceAccess
    {
        Task<MailDto> GetMail(object id);
        Task<MailDto> AddMail(MailDto dto);
        Task AddMail(IEnumerable<MailDto> dtos);
        Task UpdateMail(MailDto dto);
        Task UpdateMail(IEnumerable<MailDto> dtos);
        Task<IEnumerable<MailDto>> GetPendingMailAndMarkForProcessing();
    }
}