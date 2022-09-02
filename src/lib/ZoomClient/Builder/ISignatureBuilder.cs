using System;

namespace ZoomClient.Builder
{
    public interface ISignatureBuilder
    {
        ISignatureBuilder SetMeetingNo(string meetingNo);
        ISignatureBuilder SetRole(string role);
        string Build();
    }
}