using DTO.Domain;
using System.Threading.Tasks;

namespace dbaccess.Repository
{
  public interface IVidResourceAccess
  {
    Task<VidDto> GetVid(object id);
    Task<VidDto> AddVid(VidDto dto);
    Task UpdateVid(VidDto dto);
    Task DeleteVid(object id);
  }
}