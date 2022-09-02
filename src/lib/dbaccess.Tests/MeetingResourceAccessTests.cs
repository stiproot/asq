//using System.Text.Json;
//using Xunit;
//using dbaccess.Common;
//using dbaccess.Models;
//using dbaccess.Factory;
//using dbaccess.Mapper;
//using dbaccess.Repository;
//using dbaccess.Factory.Test;
//using DTO.Domain;

//namespace dbaccess.Tests
//{
    //public class MeetingResourceAccessTests
    //{
        //private readonly IMeetingResourceAccess _access;
        //private readonly IImgResourceAccess _imgAccess;
        //private readonly IAsqDbContextFactory<ASQContext> _contextFactory;
        //private readonly IUserFactory _userFactory;

        //public MeetingResourceAccessTests(
            //IMeetingResourceAccess access, 
            //IImgResourceAccess imgAccess, 
            //IAsqDbContextFactory<ASQContext> contextFactory, 
            //IUserFactory userFactory)
        //{
            //this._access = access;
            //this._imgAccess = imgAccess;
            //this._contextFactory = contextFactory;
            //this._userFactory = userFactory;
        //}

        ////[Fact]
        ////public void TestMeetingQuery()
        ////{
            ////var result = this._access.GetMeetingsByQuery(MeetingQueryEnu.MOST_RECENT).Result;
            ////Assert.NotNull(result);
        ////}

        //[Fact]
        //public void TestGetSingleMeetingWithId()
        //{
            //long meetingId = 8;
            //var result = this._access.GetMeeting(meetingId).Result;
            //Assert.NotNull(result);
        //}

        //[Fact]
        //public async void GetMeetingByExtMeetingId()
        //{
            //long extMeetingId = 95363192622;
            //var result = await this._access.GetMeetingByExtMeetingId(extMeetingId);
            ////throw new Exception(JsonSerializer.Serialize(result));
            //Assert.NotNull(result);
        //} 

        //[Fact]
        //public async void GetMeetingWithMeetingId()
        //{
            //long meetingId = 20006;
            //var result = await this._access.GetMeeting(meetingId);

            //Assert.NotNull(result);
            //Assert.NotNull(result.CreationUser);
            //Assert.NotEmpty(result.CreationUser.Name);
            //Assert.NotEmpty(result.CreationUser.Username);
            //Assert.NotEmpty(result.CreationUser.Surname);
        //}

        ////[Fact]
        ////public async void GetEnrichedMeetingsWithQueryType()
        ////{
            ////var queries = await this._access.BuildMeetingQueriesForUser();
            ////Assert.True(queries.Count() > 0);

            //////throw new Exception(JsonSerializer.Serialize(queries.FirstOrDefault()));

            ////var subset = await this._enrichAccess.GetMeetingSummariesByFilter(queries.FirstOrDefault().Config);
            ////Assert.True(subset.Count() > 0);
            ////var result = await this._access.GetEnrichedQueryMeetings(common.MeetingQueryEnu.MOST_RECENT, 2);
            ////Assert.NotNull(result);
            //////throw new Exception(JsonSerializer.Serialize(result));
        ////}
    //}
//}
