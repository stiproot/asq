using ZoomClient.Builder;
using ZoomClient.Factory;
using ZoomClient;
using DTO.Zoom.Meeting;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Net;
using System;

namespace ZoomClient.Tests
{
    public class DownloadTests
    {
        private readonly string _resp = "{\"uuid\":\"lErw43IcTGuHnxUC80nMww==\",\"id\":93811978504,\"account_id\":\"B8FSyXeTRbaJHclERBWBdw\",\"host_id\":\"1aMrf-ffTHGi4HWH_JIbug\",\"topic\":\"ASQ Catch-up\",\"type\":2,\"start_time\":\"2021-03-22T14:56:24Z\",\"timezone\":\"Africa/Harare\",\"host_email\":\"mark@asq.properties\",\"duration\":111,\"total_size\":622584770,\"recording_count\":2,\"share_url\":\"https://zoom.us/rec/share/sWUy7q5q1C710x2cwCbbpXJRi1lSYPeohD5RW0EueowTZPyu81PGZ4vuQ8rSRxjO.c7QXxnv9bMKmoCJS\",\"recording_files\":[{\"id\":\"2a4a7053-5ee8-4c03-b61e-177d00d68d11\",\"meeting_id\":\"lErw43IcTGuHnxUC80nMww==\",\"recording_start\":\"2021-03-22T14:56:26Z\",\"recording_end\":\"2021-03-22T16:48:25Z\",\"file_type\":\"MP4\",\"file_extension\":\"MP4\",\"file_size\":515666922,\"play_url\":\"https://zoom.us/rec/play/w7fyu2KkXlakctwaaXIgGa-hYeVwvrVmp3gkW40e-J68smTvZ1MLz2ok5nSh8hON-dvc93YJfP63kSqi.1N-4wSc759cnE-b0\",\"download_url\":\"https://zoom.us/rec/download/w7fyu2KkXlakctwaaXIgGa-hYeVwvrVmp3gkW40e-J68smTvZ1MLz2ok5nSh8hON-dvc93YJfP63kSqi.1N-4wSc759cnE-b0\",\"status\":\"completed\",\"recording_type\":\"shared_screen_with_speaker_view\"},{\"id\":\"0ece1493-3015-48bd-97fb-cd188b0fa805\",\"meeting_id\":\"lErw43IcTGuHnxUC80nMww==\",\"recording_start\":\"2021-03-22T14:56:26Z\",\"recording_end\":\"2021-03-22T16:48:25Z\",\"file_type\":\"M4A\",\"file_extension\":\"M4A\",\"file_size\":106917848,\"play_url\":\"https://zoom.us/rec/play/Cjs7Gd7aOEv0KU3O87gglPurtSdjvza3SuEXY_5659cpjxgRHbzfqzxjkkJ2L5_hcmwXeHfuMu-BXwNc.u3ZPxotJrmvexwPh\",\"download_url\":\"https://zoom.us/rec/download/Cjs7Gd7aOEv0KU3O87gglPurtSdjvza3SuEXY_5659cpjxgRHbzfqzxjkkJ2L5_hcmwXeHfuMu-BXwNc.u3ZPxotJrmvexwPh\",\"status\":\"completed\",\"recording_type\":\"audio_only\"}]}";
        private readonly IZoomHttpClient _zoomClient;

        public DownloadTests(
            IZoomHttpClient zoomClient
        ) => this._zoomClient = zoomClient;

        [Fact]
        public void DependenciesAreNotNull()
        {
            Assert.NotNull(this._zoomClient);
        }

        [Fact]
        public void CanDeserializeResponse()
        {
            var responseType = JsonSerializer.Deserialize<GetMeetingRecordingsResponse>(this._resp);
            var fileInfo = responseType.recording_files.First();

            using(var client = new WebClient())
            {
                client.DownloadFileAsync(new System.Uri(fileInfo.download_url), "/home/simon/Downloads/asq_download/recording.mp4");
            }
        }

        [Fact]
        public async void GetMeetingRecordingInformation()
        {
            string meetingId = "93811978504";

            //var resp = await this._zoomClient.GetMeetingRecordings(meetingId);
            //throw new Exception(JsonSerializer.Serialize(resp));
        }
    }
}
