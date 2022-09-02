using DTO.Domain;
using dbaccess.Repository;
using dbaccess.Repository.QueryEnrichment;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace managers.Resource
{
  public class VideoResourceManager : IVideoResourceManager
  {
    private IVideoResourceAccess _videoResourceAccess;
    private IVidResourceAccess _vidResourceAccess;
    private readonly IVideoQueryEnrichmentResourceAccess _videoQueryEnrichmentResourceAccess;

    public VideoResourceManager(
        IVideoResourceAccess videoResourceAccess,
        IVidResourceAccess vidResourceAccess,
        IVideoQueryEnrichmentResourceAccess videoQueryEnrichmentResourceAccess
    )
    {
        this._videoResourceAccess = videoResourceAccess ?? throw new ArgumentNullException(nameof(videoResourceAccess));
        this._vidResourceAccess = vidResourceAccess ?? throw new ArgumentNullException(nameof(vidResourceAccess));
        this._videoQueryEnrichmentResourceAccess = videoQueryEnrichmentResourceAccess ?? throw new ArgumentNullException(nameof(videoQueryEnrichmentResourceAccess));
    }

    public async Task<VideoDto> GetVideo(long id)
      => await _videoResourceAccess.GetVideo(id);
    public async Task<VideoDto> GetVideo(object id)
      => await _videoResourceAccess.GetVideo(id);
    public async Task<IEnumerable<VideoQueryDto>> BuildVideoSummaryQueries(VideoSummaryQueryBuilderConfigDto config)
      => await this._videoQueryEnrichmentResourceAccess.BuildVideoSummaryQueries(config);
    public async Task<IEnumerable<VideoSummaryDto>> GetVideoSummariesByFilter(VideoFilterConfigDto config)
      => await this._videoResourceAccess.GetVideoSummariesByFilter(config);
    public async Task AddVideo(VideoDto dto)
      => await _videoResourceAccess.AddVideo(dto);
    public async Task UpdateVideo(VideoDto dto)
      => await _videoResourceAccess.UpdateVideo(dto);

    public async Task<VidDto> GetVid(object id)
      => await this._vidResourceAccess.GetVid(id);
    public async Task<VidDto> AddVid(VidDto dto)
      => await this._vidResourceAccess.AddVid(dto);
    public async Task UpdateVid(VidDto dto)
      => await this._vidResourceAccess.UpdateVid(dto);
    public async Task DeleteVid(object id)
      => await this._vidResourceAccess.DeleteVid(id);
  }
}