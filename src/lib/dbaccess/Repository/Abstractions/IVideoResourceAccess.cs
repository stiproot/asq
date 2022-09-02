using DTO.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace dbaccess.Repository
{
    public interface IVideoResourceAccess
    {
        Task<VideoDto> GetVideo(object id);
        Task<IEnumerable<VideoSummaryDto>> GetVideoSummariesByFilter(VideoFilterConfigDto filter);
        Task<VideoDto> AddVideo(VideoDto dto);
        Task UpdateVideo(VideoDto dto);
        Task DeleteVideo(object id);
    }
}