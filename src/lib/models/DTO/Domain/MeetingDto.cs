using System;
using System.Linq;
using extensions;
using System.Collections.Generic;
using DTO.Domain.Ext.Zoom;

namespace DTO.Domain
{
  public class MeetingDto: BaseDomainDto
  {
    // Core properties...
    public string Title{ get; set; }
    public string Description{ get; set; }
    public DateTime StartDateUtc{ get; set; }
    public int EstimatedDuration{ get; set; }
    public long HostId { get; set; }
    public short MeetingStatusId { get; set; }
    public long ExMeetingId { get; set; }
    public long ImgId{ get; set; }
    public int TimezoneId{ get; set; }

    // Enrichment properties...
    public UserDto CreationUser {get; set; }
    public HostDto Host {get; set; }
    public MeetingStatusDto MeetingStatus { get; set; }
    public ExtZoomMeetingDto ExtMeeting{ get; set; }
    public ImgDto Img{ get; set; }
    public TimezoneDto Timezone{ get; set; }
    public ICollection<ExtZoomMeetingWebHookDto> ExtMeetingWebHooks{ get; set; }
    public ICollection<FocusMeetingMappingDto> Foci{ get; set; }
    public ICollection<MeetingUserMappingDto> Participants{ get; set; }
    public ICollection<MeetingRecordingDto> Recordings{ get; set; }
    public bool HasPassed{ get; set; }

    new public bool Validate(bool throwException = true)
    {
        base.Validate(throwException);

        bool invalid = this.Title.IsNullOrEmpty()
            || this.Description.IsNullOrEmpty()
            || (this.StartDateUtc == null || this.StartDateUtc < DateTime.UtcNow.AddMinutes(5))
            || this.EstimatedDuration <= 0
            || this.HostId <= 0
            || this.ImgId <= 0
            || this.MeetingStatusId <= 0
            || this.TimezoneId <= 0
            || (this.Foci == null || this.Foci?.Count() == 0);

        if(invalid && throwException)
        {
            throw new Exception("Invalid meeting structure");
        }

        return invalid;
    }
    public bool isDiff(MeetingDto compareTo)
    {
        // we are only comparing what can change via the ui at this point.
        return this.Description != compareTo.Description
            || this.Title != compareTo.Title
            || this.EstimatedDuration != compareTo.EstimatedDuration
            || this.StartDateUtc != compareTo.StartDateUtc;
    }
  }
}