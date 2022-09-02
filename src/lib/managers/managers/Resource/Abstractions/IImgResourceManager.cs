using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Domain;

namespace managers.Resource
{
    public interface IImgResourceManager
    {
        Task<ImgDto> GetImg(object id);
        Task<ImgDto> AddImg(ImgDto dto);
        //Task<Tuple<string, string>> WriteImg(ImgDto dto, Guid userId);
    }
}