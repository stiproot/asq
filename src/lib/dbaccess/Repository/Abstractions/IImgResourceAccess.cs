using System;
using System.Threading.Tasks;
using DTO.Domain;

namespace dbaccess.Repository
{
    public interface IImgResourceAccess
    {
      Task<ImgDto> GetImg(object id);
      Task<ImgDto> AddImg(ImgDto dto);
      //Task<Tuple<string, string>> WriteImg(ImgDto dto, Guid userId);
      Task<ImgDto> GetTestImgAsync();
      ImgDto GetTestImg();
    }
}