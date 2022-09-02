using DTO.Domain.Ext.Zoom;
using DTO.Domain;
using processes.Engine;
using managers.Resource;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System;

namespace processes.Strategy
{
    public class CheckFilesAndCreateMeetingRecordingsStrategyFactory: BaseStrategyFactory
    {
        private const string _meetingParamName = "meeting";
        private const string _extMeetingRecordingParamName = "extMeetingRecording";
        private const string _recordingDirPathParamName = "recordingDirPath";
        private const string _staticVideoServerBaseParamName = "staticVideoServerUrl";
        private readonly IMeetingResourceManager _meetingResourceManager;

        public CheckFilesAndCreateMeetingRecordingsStrategyFactory(
            IEnginePacketFactory packetFactory,
            IMeetingResourceManager meetingResourceManager
        ): base(packetFactory) 
            => (this._meetingResourceManager) = (meetingResourceManager);

        public override Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param)
        {
            return async () => 
            {
                var meeting = param[_meetingParamName].Cast<MeetingDto>();
                var extMeetingRecording = param[_extMeetingRecordingParamName].Cast<ExtZoomMeetingRecordingDto>();
                var recordingDirPath = param[_recordingDirPathParamName].Cast<string>();
                var staticVideoServerUrl = param[_staticVideoServerBaseParamName].Cast<string>();

                var requiredFiles = extMeetingRecording.PayloadSerializer.recording_files
                    .Where(f => f.recording_type.Equals("shared_screen_with_speaker_view") && f.file_extension.Equals("MP4"));

                // check if files exist on disk
                var downloadedFiles = requiredFiles.Select(f => 
                {
                    var fileName = f.id + "." + f.file_extension.ToLower();
                    var filePath = recordingDirPath + fileName;

                    if(File.Exists(filePath))
                    {
                        var fileInfo = new System.IO.FileInfo(filePath);
                        Console.WriteLine($"fileInfo is not null? {fileInfo != null}");

                        var size = fileInfo.Length;
                        Console.WriteLine($"fileInfo.Length {fileInfo.Length}");

                        if(size != f.file_size)
                        {
                            Console.WriteLine($"size != f.file_size | {size} != {f.file_size}");
                            return null;
                        }

                        else return f;
                    }
                    else return f;
                });

                if(requiredFiles.Count() != downloadedFiles.Where(f => f != null).Count()) 
                    throw new FileNotFoundException("Files that should have been downloaded are not found on disk");

                var recordings = requiredFiles
                    .Select(f =>
                    {
                        var fileName = f.id + "." + f.file_extension.ToLower();
                        var url = staticVideoServerUrl + fileName;

                        var meetingRecording = new MeetingRecordingDto
                        {
                            Id = (requiredFiles.ToList().IndexOf(f)) * -1,
                            UniqueId = Guid.Parse(f.id),
                            CreationDateUtc = DateTime.UtcNow,
                            CreationUserId = 0,
                            Inactive = false,
                            FileName = fileName, 
                            Path = url,
                            Part = (short)(requiredFiles.ToList().IndexOf(f) + 1),
                            ExtMeetingRecordingId = extMeetingRecording.Id,
                            MeetingId = meeting.Id
                        };

                        return meetingRecording;
                    });

                meeting.Recordings = recordings.ToList();
                meeting.MeetingStatusId = (short)MeetingStatusEnu.COMPLETE_WITH_RECORDINGS;

                await this._meetingResourceManager.UpdateMeeting(meeting);

                return null;
            };
        }
    }
}