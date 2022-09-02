using DTO.Domain;
using dbaccess.Repository;
using System.Threading.Tasks;
using System;

namespace managers.Resource
{
    public class ImgResourceManager : IImgResourceManager
    {
        private IImgResourceAccess _imgResourceAccess;

        public ImgResourceManager(IImgResourceAccess imgResourceAccess)
        {
            this._imgResourceAccess = imgResourceAccess;
        }

        public async Task<ImgDto> GetImg(object id)
        {
            return await this._imgResourceAccess.GetImg(id);
        }

        public async Task<ImgDto> AddImg(ImgDto dto)
        {
            return await this._imgResourceAccess.AddImg(dto);
        }

        //public async Task<Tuple<string, string>> WriteImg(ImgDto dto, Guid userId)
        //{
            //return await this._imgResourceAccess.WriteImg(dto, userId);
        //}
    }
}