namespace DTO.Zoom.Meeting
{
    //used for recurring meetings with fixed time only.
    public enum MeetingRegistrationType : int
    {
        AttendeesRegisterOnceAndCanAttendAnyOfTheOccurences = 1,
        AttendeesNeedToRegisterForEachOccurenceToAttend = 2,
        AttendeesCanRegisterOnceAndCanChooseOneOrMoreOccurencesToAttend = 3
    }
}