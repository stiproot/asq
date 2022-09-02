namespace processes.Process
{
    public enum ProcessTypeEnu
    {
        // account
        CreateAccountProcess,
        UpdateAccountProcess,

        // notification
        QueueNotificationProcess,
        SendMailProcess,

        // meeting
        CreateMeetingProcess,
        UpdateMeetingProcess,
        ParticipateInMeetingProcess,
        DownloadMeetingRecordingProcess
    }
}