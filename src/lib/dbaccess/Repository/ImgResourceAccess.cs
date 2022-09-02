using dbaccess.Factory;
using dbaccess.Models;
using dbaccess.Common;
using DTO.Domain;
using AutoMapper;
using System.IO;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;

namespace dbaccess.Repository
{
    public class ImgResourceAccess: BaseResourceAccess<ImgDto, TbImg>, IImgResourceAccess
    {
        private readonly IStaticFileSettingProvider _settings;

        public ImgResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper, IStaticFileSettingProvider settings): base(repositoryFactory, mapper)
            => this._settings = settings;

        public async Task<ImgDto> GetImg(object id)
        {
            TbImg entity;
            if(long.TryParse(id.ToString(), out long longId))
            {
                entity = await this._repo.FindByKey(id);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                Expression<Func<TbImg, bool>> predicate = (TbImg u) => u.UniqueId.Equals(id);
                entity = await this._repo.FindSingleOrDefaultAsync(predicate, null);
            }
            else return null;

            return this._mapper.Map<TbImg, ImgDto>(entity);
        }

        public async Task<ImgDto> AddImg(ImgDto dto) 
          => await this.Add(dto);

        //public async Task<Tuple<string, string>> WriteImg(ImgDto dto, Guid userId)
        //{
            //string dir = Path.Combine(this._settings.GetImgBasePath(), userId.ToString());
            //if(!Directory.Exists(dir))
            //{
                //Directory.CreateDirectory(dir);
            //}

            //string newFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(dto.FileName)}";
            //string file = Path.Combine(dir, newFileName);

            ////if(dto.Data == null && dto.Base64 != null)
            ////{
                ////dto.Data = Convert.FromBase64String(dto.Base64);
            ////}

            ////if(dto.Data == null && dto.Id <= 0 && !string.IsNullOrEmpty(dto.Path))
            ////{
                ////// todo: some more efficient check
                ////dto.Data = Convert.FromBase64String(dto.Path.Split(',')[1]);
            ////}

            ////await File.WriteAllBytesAsync(file, dto.Data);

            //string imgUrl = $"{this._settings.GetStaticImgServerBaseUrl()}{userId}/{newFileName}";

            //return new Tuple<string, string>(imgUrl, imgUrl);
        //}

        public ImgDto GetTestImg()
        {
            const string testUserId = "9b1deb4d-3b7d-4bad-9bdd-2b0d7b3dcb6d";
            string path = Path.Combine(this._settings.GetImgBasePath(), testUserId, "profile.png");
            return new ImgDto
            {
                Id = 0,
                CreationDateUtc = DateTime.UtcNow,
                CreationUserId = 0,
                Inactive = false,
                UniqueId = Guid.NewGuid(),
                Url = null,
                ThumbnailUrl = null
            };
        }

        public async Task<ImgDto> GetTestImgAsync() => await Task.FromResult<ImgDto>(this.GetTestImg());
    }
}