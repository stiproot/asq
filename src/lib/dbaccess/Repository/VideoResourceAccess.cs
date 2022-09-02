using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace dbaccess.Repository
{
    public class VideoResourceAccess: BaseResourceAccess<VideoDto, TbVideo>, IVideoResourceAccess
    {
        private readonly IEnumerable<string> _includes = new List<string>()
        {
            "TbFocusVideoMappings.Focus",
            "Img",
            "Vid",
            "CreationUser.Img",
            "CreationUser.TbFocusUserMappings.Focus",
        }; 

        public VideoResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<VideoDto> GetVideo(object id)
        {
            Expression<Func<TbVideo, bool>> predicate = null;
            TbVideo entity;
            if(long.TryParse(id.ToString(), out long longId))
            {
                predicate = (TbVideo u) => u.Id.Equals(longId);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                predicate = (TbVideo u) => u.UniqueId.Equals(id);
            }
            else throw new NotImplementedException();

            entity = await this._repo.FindSingleOrDefaultAsync(predicate, this._includes);

            if(entity == null)
            {
                return null;
            }
            return this._mapper.Map<TbVideo, VideoDto>(entity);
        }

        public async Task<IEnumerable<VideoSummaryDto>> GetVideoSummariesByFilter(VideoFilterConfigDto filter)
        {
            Expression<Func<TbVideo, bool>> predicate = 
                (video) =>  
                    // Foci...
                    (!filter.Foci.Any() || video.TbFocusVideoMappings.Any(mapping => filter.Foci.Contains(mapping.FocusId))) && 
                    // Creation user guid...
                    (filter.CreationUserUniqueId == null || video.CreationUser.UniqueId == filter.CreationUserUniqueId.ToString()) &&
                    // Elastic...
                    (
                        (filter.Elastic == null || filter.Elastic == string.Empty) ||
                        (
                            // title
                            video.Title.Contains(filter.Elastic) ||
                            // name, surname & username
                            (
                                video.CreationUser.Name.Contains(filter.Elastic) || 
                                video.CreationUser.Surname.Contains(filter.Elastic) || 
                                video.CreationUser.Username.Contains(filter.Elastic)
                            )
                        )
                    ) &&
                    // Creation user name...
                    (
                        (filter.CreationUserName == null || filter.CreationUserName == string.Empty) || 
                        (
                            video.CreationUser.Name.Contains(filter.CreationUserName) || 
                            video.CreationUser.Surname.Contains(filter.CreationUserName) ||
                            video.CreationUser.Username.Contains(filter.CreationUserName)
                        )
                    );
            Expression<Func<TbVideo, object>> orderByDescFunc = (TbVideo b) => b.CreationDateUtc;
            Expression<Func<TbVideo, object>> orderByFunc = null; 
            int take = filter.Take ?? 10;
            
            var summaries = this._mapper.Map<IEnumerable<TbVideo>, IEnumerable<VideoSummaryDto>>( 
                await this._repo.FindToListAsync(predicate, this._includes, orderByFunc, orderByDescFunc, take));
            
            return summaries;
        }

        public async Task<VideoDto> AddVideo(VideoDto dto)
          => await this.Add(dto);

        public async Task UpdateVideo(VideoDto dto)
          => await this.Update(dto);

        public async Task DeleteVideo(object id)
          => await this.Delete(id);
    }
}