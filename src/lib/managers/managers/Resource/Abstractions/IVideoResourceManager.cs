using DTO.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace managers.Resource
{
  public interface IVideoResourceManager
  {
    Task<VideoDto> GetVideo(long id);
    Task<VideoDto> GetVideo(object id);
    Task<IEnumerable<VideoQueryDto>> BuildVideoSummaryQueries(VideoSummaryQueryBuilderConfigDto config);
    Task<IEnumerable<VideoSummaryDto>> GetVideoSummariesByFilter(VideoFilterConfigDto config);
    Task AddVideo(VideoDto dto);
    Task UpdateVideo(VideoDto dto);

    Task<VidDto> GetVid(object id);
    Task<VidDto> AddVid(VidDto dto);
    Task UpdateVid(VidDto dto);
    Task DeleteVid(object id);
  }
}