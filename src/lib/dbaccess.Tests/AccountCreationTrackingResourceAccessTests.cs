using dbaccess.Common;
using dbaccess.Models;
using dbaccess.Factory;
using dbaccess.Mapper;
using dbaccess.Repository;
using dbaccess.Factory.Test;
using DTO.Tracking;
using Xunit;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace dbaccess.Tests
{
    public class AccountCreationTrackingResourceAccessTests
    {
        private readonly IAccountCreationTrackingResourceAccess _access;
        private readonly IAsqDbContextFactory<ASQContext> _contextFactory;

        public AccountCreationTrackingResourceAccessTests(
            IAccountCreationTrackingResourceAccess access, 
            IAsqDbContextFactory<ASQContext> contextFactory
        )
        {
            this._access = access ?? throw new ArgumentNullException(nameof(access));
            this._contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        //[Fact]
        //public async Task GetAccountCreationTracking()
        //{
            //var id = Guid.Parse("dca96500-638d-4b75-9f3e-59c906849609");

            //var tracking = await this._access.GetAccountCreationTracking(id);

            //Assert.NotNull(tracking);
        //}


        //[Fact]
        //public async void UpdateTrackingWithFindBy()
        //{
            ////Guid id = Guid.Parse("f69f1575-79c3-4a32-b553-c1785ab09f43");
            //Guid id = Guid.Parse("4ac69535-3b5e-4b4a-a284-04445a14ada2");
            //const string name = "PikachuABC";
            //Exception exception = new ArgumentNullException("paramX", name);

            //AccountCreationTrackingDto tracking = await _access.GetAccountCreationTracking(id);

            //Assert.NotNull(tracking);

            //var components = tracking.TrackingComponents;
            //var component = components.FirstOrDefault(tc => tc.identifier.Equals("persist-user"));

            //Assert.NotNull(components);
            //Assert.NotNull(component);
            
            //component.response = null; 
            //component.failed = true;

            //var exceptionDto = ExceptionModelFactory.CreateExceptionDto(exception);
            //string serializedExDto = JsonSerializer.Serialize(exceptionDto);

            //component.exception_info = exceptionDto;
            //tracking.TrackingComponents = components;

            //await this._access.UpdateAccountCreationTracking(tracking);
        //}

        //[Fact]
        //public async void UpdateTrackingWithPrimaryKey()
        //{
            //long id = 68; 
            //const string name = "PikachuXXX";
            //Exception exception = new ArgumentNullException("paramX", name);

            //AccountCreationTrackingDto tracking = await _access.GetAccountCreationTracking(id);

            //Assert.NotNull(tracking);

            //var components = tracking.TrackingComponents;
            //var component = components.FirstOrDefault(tc => tc.identifier.Equals("persist-user"));

            //Assert.NotNull(components);
            //Assert.NotNull(component);
            
            //component.response = null; 
            //component.failed = true;

            //var exceptionDto = ExceptionModelFactory.CreateExceptionDto(exception);
            //string serializedExDto = JsonSerializer.Serialize(exceptionDto);

            //component.exception_info = exceptionDto;
            //tracking.TrackingComponents = components;

            ////tracking.Request = "";
            ////throw new Exception(JsonSerializer.Serialize(_tracking));

            //await this._access.UpdateAccountCreationTracking(tracking);
        //}

    }
}
