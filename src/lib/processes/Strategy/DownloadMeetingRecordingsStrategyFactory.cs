using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using processes.Engine;
using managers.Resource;
using System.IO;
using System;

namespace processes.Strategy
{
    public class DownloadMeetingRecordingsStrategyFactory: BaseStrategyFactory
    {
        private const string _extMeetingRecordingParamName = "extMeetingRecording";
        private const string _recordingDirPathParamName = "recordingDirPath";
        private readonly IZoomResourceManager _zoomResourceManager;

        public DownloadMeetingRecordingsStrategyFactory(
            IEnginePacketFactory packetFactory,
            IZoomResourceManager zoomResourceManager
        ): base(packetFactory) => this._zoomResourceManager = zoomResourceManager;

        public override Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param)
        {
            return async () => 
            {
                var zoomMeetingRecordingResponse = param[_extMeetingRecordingParamName].Cast<DTO.Domain.Ext.Zoom.ExtZoomMeetingRecordingDto>().PayloadSerializer;
                var recordingDirPath = param[_recordingDirPathParamName].Cast<string>();

                var potentialFilesToDownload = zoomMeetingRecordingResponse.recording_files
                    .Where(f => f.recording_type.Equals("shared_screen_with_speaker_view") && f.file_extension.Equals("MP4"));

                // check if files exist on disk
                var filesToDownload = potentialFilesToDownload.Select(f => 
                {
                    var fileName = f.id + "." + f.file_extension.ToLower();
                    var filePath = recordingDirPath + fileName;

                    if(File.Exists(filePath))
                    {
                        var fileInfo = new System.IO.FileInfo(filePath);
                        var size = fileInfo.Length;
                        if(size != f.file_size) return f;
                        else return null;
                    }
                    else return f;
                });

                var tasks = filesToDownload 
                    .Where(f => f != null)
                    .Select(f => Task.Factory.StartNew(() =>
                    {
                        using(var client = new WebClient())
                        {
                            // append access token to url
                            f.download_url += $"?access_token={this._zoomResourceManager.GetJwt()}";

                            if(!Directory.Exists(recordingDirPath))
                            {
                                Console.WriteLine($"Creating directory at {recordingDirPath}");
                                Directory.CreateDirectory(recordingDirPath);
                            }

                            client.DownloadFile(new Uri(f.download_url), recordingDirPath + f.id + "." + f.file_extension.ToLower());
                        }
                    }));

                var continuation = Task.WhenAll(tasks);
                await continuation;

                return null;
            };
        }
    }
}