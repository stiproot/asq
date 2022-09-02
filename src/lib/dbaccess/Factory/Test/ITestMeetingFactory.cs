using System;
using System.Collections.Generic;
using AutoMapper;
using dbaccess.Models;
using DTO.Domain;

namespace dbaccess.Factory.Test
{
    public interface ITestMeetingFactory
    {
       TbMeeting GenerateMeeting();
       MeetingDto GenerateMeetingDto();
    }
}