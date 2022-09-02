using ZoomClient;
using managers.Resource;
using processes.Factory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using System;

namespace RecordingDownloadWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IZoomHttpClient _zoomHttpClient;
        private readonly IMeetingResourceManager _meetingResourceManager;
        private readonly IProcessFactory _processFactory;

        private readonly string _resp = "{\"uuid\":\"lErw43IcTGuHnxUC80nMww==\",\"id\":93811978504,\"account_id\":\"B8FSyXeTRbaJHclERBWBdw\",\"host_id\":\"1aMrf-ffTHGi4HWH_JIbug\",\"topic\":\"ASQ Catch-up\",\"type\":2,\"start_time\":\"2021-03-22T14:56:24Z\",\"timezone\":\"Africa/Harare\",\"host_email\":\"mark@asq.properties\",\"duration\":111,\"total_size\":622584770,\"recording_count\":2,\"share_url\":\"https://zoom.us/rec/share/sWUy7q5q1C710x2cwCbbpXJRi1lSYPeohD5RW0EueowTZPyu81PGZ4vuQ8rSRxjO.c7QXxnv9bMKmoCJS\",\"recording_files\":[{\"id\":\"2a4a7053-5ee8-4c03-b61e-177d00d68d11\",\"meeting_id\":\"lErw43IcTGuHnxUC80nMww==\",\"recording_start\":\"2021-03-22T14:56:26Z\",\"recording_end\":\"2021-03-22T16:48:25Z\",\"file_type\":\"MP4\",\"file_extension\":\"MP4\",\"file_size\":515666922,\"play_url\":\"https://zoom.us/rec/play/w7fyu2KkXlakctwaaXIgGa-hYeVwvrVmp3gkW40e-J68smTvZ1MLz2ok5nSh8hON-dvc93YJfP63kSqi.1N-4wSc759cnE-b0\",\"download_url\":\"https://zoom.us/rec/download/w7fyu2KkXlakctwaaXIgGa-hYeVwvrVmp3gkW40e-J68smTvZ1MLz2ok5nSh8hON-dvc93YJfP63kSqi.1N-4wSc759cnE-b0\",\"status\":\"completed\",\"recording_type\":\"shared_screen_with_speaker_view\"},{\"id\":\"0ece1493-3015-48bd-97fb-cd188b0fa805\",\"meeting_id\":\"lErw43IcTGuHnxUC80nMww==\",\"recording_start\":\"2021-03-22T14:56:26Z\",\"recording_end\":\"2021-03-22T16:48:25Z\",\"file_type\":\"M4A\",\"file_extension\":\"M4A\",\"file_size\":106917848,\"play_url\":\"https://zoom.us/rec/play/Cjs7Gd7aOEv0KU3O87gglPurtSdjvza3SuEXY_5659cpjxgRHbzfqzxjkkJ2L5_hcmwXeHfuMu-BXwNc.u3ZPxotJrmvexwPh\",\"download_url\":\"https://zoom.us/rec/download/Cjs7Gd7aOEv0KU3O87gglPurtSdjvza3SuEXY_5659cpjxgRHbzfqzxjkkJ2L5_hcmwXeHfuMu-BXwNc.u3ZPxotJrmvexwPh\",\"status\":\"completed\",\"recording_type\":\"audio_only\"}]}";

        public Worker(
            ILogger<Worker> logger,
            IZoomHttpClient zoomHttpClient,
            IMeetingResourceManager meetingResourceManager,
            IProcessFactory processFactory
        )
        {
            this._logger = logger;
            this._zoomHttpClient = zoomHttpClient;
            this._meetingResourceManager = meetingResourceManager;
            this._processFactory = processFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                this._logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    // fetch the meetings that are ready for download
                    var meetings = await this._meetingResourceManager.GetCompleteMeetingsAndMarkAsRecordingDownloading();

                    this._logger.LogInformation("Number of meetings ready for download found {0}", meetings.Count());

                    if(meetings.Any())
                    {
                        // create processes for each meeting that is ready 
                        var tasks = meetings.Select
                        (
                            m =>    this._processFactory
                                            .Create(processes.Process.ProcessTypeEnu.DownloadMeetingRecordingProcess)
                                            .SetEvent(new DTO.Events.MeetingRecordingDownloadEvent
                                            {
                                                meeting = m,
                                                event_date_utc = DateTime.UtcNow,
                                                id = Guid.NewGuid()
                                            })
                                            .SetLogger(this._logger)
                                            .Init()
                                            .Execute()
                        );

                        var continuation = Task.WhenAll(tasks);

                        await continuation;
                    }
                }
                catch(Exception ex)
                {
                    this._logger.LogError("Error message / {0}", ex.Message);
                    this._logger.LogError("Inner exception message / {0}", ex.InnerException.Message);
                    this._logger.LogError("Stack track / {0}", ex.StackTrace);
                    throw;
                }

                await Task.Delay(1000 * 60, stoppingToken);
            }
        }
    }
}
