using System;
using System.Collections.Generic;
using AutoMapper;
using dbaccess.Models;
using dbaccess.Repository;
using DTO.Domain;

namespace dbaccess.Factory.Test
{
    public class TestMeetingFactory: ITestMeetingFactory
    {
       private readonly IMapper _mapper;
       private readonly IImgResourceAccess _imgAccess;

       public TestMeetingFactory(IMapper mapper, IImgResourceAccess imgAccess)
       {
           this._mapper = mapper;
           this._imgAccess = imgAccess;
       }

       public TbMeeting GenerateMeeting()
       {
           var img = this._imgAccess.GetTestImg();

           return new TbMeeting
           {
               Id = 0,
               UniqueId = Guid.NewGuid().ToString(),
               CreationDateUtc = DateTime.UtcNow,
               CreationUserId = 0,
               Inactive = false,
               Title = Guid.NewGuid().ToString(),
               Description = Guid.NewGuid().ToString(),
               StartDateUtc = DateTime.UtcNow.AddDays(3),
               EstimatedDuration = 120,
               HostId = 16,
               MeetingStatusId = 1,
               ImgId = 0,
               ExtMeetingId = 0,
               TimezoneId = 4,

               Img = new TbImg
               {
                   Id = 0,
                   UniqueId = Guid.NewGuid().ToString(),
                   CreationDateUtc = DateTime.UtcNow,
                   CreationUserId = 0,
                   Inactive = false,
                   Url = null,
                   ThumbnailUrl = null
               },
               TbFocusMeetingMappings = new List<TbFocusMeetingMapping>() 
               { 
                   new TbFocusMeetingMapping
                   { 
                       Id = 0,
                       UniqueId = Guid.NewGuid().ToString(),
                       CreationDateUtc = DateTime.UtcNow,
                       CreationUserId = 0,
                       Inactive = false,
                       FocusId = 1,
                       MeetingId = 0
                   },
                   new TbFocusMeetingMapping
                   { 
                       Id = 0,
                       UniqueId = Guid.NewGuid().ToString(),
                       CreationDateUtc = DateTime.UtcNow,
                       CreationUserId = 0,
                       Inactive = false,
                       FocusId = 2,
                       MeetingId = 0
                   } 
               },
           };
       }

       public MeetingDto GenerateMeetingDto()
       {
           return this._mapper.Map<TbMeeting, MeetingDto>(this.GenerateMeeting());
       }
    }
}