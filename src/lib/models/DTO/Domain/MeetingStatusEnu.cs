namespace DTO.Domain
{
    public enum MeetingStatusEnu: int
    {
        AWAITING = 1,
        CANCELLED = 2,
        COMPLETE = 3,
        COMPLETE_RECORDING_DOWNLOAD_IN_PROGRESS = 4,
        COMPLETE_WITH_RECORDINGS = 5
    }
}