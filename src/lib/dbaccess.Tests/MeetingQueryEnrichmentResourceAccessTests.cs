//using System.Collections.Generic;
//using System.Linq;
//using System.Text.Json;
//using Xunit;
//using dbaccess.Common;
//using dbaccess.Models;
//using dbaccess.Repository;
//using dbaccess.Repository.QueryEnrichment;
//using dbaccess.Factory.Test;
//using DTO.Domain;
//using System.Threading.Tasks;
//using System;

//namespace dbaccess.Tests
//{
    //public class MeetingQueryEnrichmentResourceAccessTests
    //{
        //private readonly IMeetingResourceAccess _access;
        //private readonly IMeetingQueryEnrichmentResourceAccess _enrichAccess;
        //private readonly IImgResourceAccess _imgAccess;
        //private readonly IAsqDbContextFactory<ASQContext> _contextFactory;
        //private readonly IUserFactory _userFactory;

        //public MeetingQueryEnrichmentResourceAccessTests(
            //IMeetingResourceAccess access, 
            //IMeetingQueryEnrichmentResourceAccess enrichAccess,
            //IImgResourceAccess imgAccess, 
            //IAsqDbContextFactory<ASQContext> contextFactory, 
            //IUserFactory userFactory)
        //{
            //this._access = access;
            //this._enrichAccess = enrichAccess;
            //this._imgAccess = imgAccess;
            //this._contextFactory = contextFactory;
            //this._userFactory = userFactory;
        //}


        //[Fact]
        //public async Task GetEnrichedMeetingsWithQueryType()
        //{
            //var queries = await this._enrichAccess.BuildMeetingSummaryQueries(null);
            //Assert.True(queries.Count() > 0);

            ////throw new Exception(JsonSerializer.Serialize(queries.FirstOrDefault()));

            //var subset = await this._access.GetMeetingSummariesByFilter(queries.FirstOrDefault().Config);
            //Assert.True(subset.Count() > 0);

            ////throw new Exception(JsonSerializer.Serialize(result));
        //}

        //[Fact]
        //public async Task LookForMeetingWithRecordings()
        //{
            //var filter = new MeetingFilterConfigDto
            //{
                //Foci = new List<short>(){ 1 }
            //};

            //var meetingSummary = await this._access.GetMeetingSummariesByFilter(filter);

            //Assert.NotNull(meetingSummary);
            ////Assert.NotEmpty(meetingSummary.FirstOrDefault().Recordings);
            ////throw new Exception(JsonSerializer.Serialize(meetingSummary.FirstOrDefault()));
        //}
    //}
//}
