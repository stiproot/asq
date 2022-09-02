//using System;
//using System.IO;
//using System.Linq;
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
    //public class UserResourceAccessTests
    //{
        //private readonly IUserResourceAccess _access;
        //private readonly IImgResourceAccess _imgAccess;
        //private readonly IAsqDbContextFactory<ASQContext> _contextFactory;
        //private readonly IUserFactory _userFactory;

        //public UserResourceAccessTests(
            //IUserResourceAccess access, 
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
        ////public void ContextFactoryIsNotNull()
        ////{
            ////Assert.NotNull(this._contextFactory);
        ////}

        ////[Fact]
        ////public void ContextIsNotNull()
        ////{
            ////Assert.NotNull(this._contextFactory.CreateContext());
        ////}

        ////[Fact]
        ////public void UserResourceAccessIsNotNull()
        ////{
            ////Assert.NotNull(this._access);
        ////}

        ////[Fact]
        ////public void UserFactoryNotNull()
        ////{
            ////var user = this._userFactory.GenerateUserDto(AccountTypeEnu.HOST);

            ////Assert.NotNull(this._userFactory);
            ////Assert.IsType(typeof(UserDto), user);
            ////Assert.NotNull(user.Img);
            ////Assert.NotNull(user.Img.Data);
        ////}

        ////[Fact]
        ////public async void AddUser()
        ////{
            ////await this._access.AddUser(this._userFactory.GenerateUserDto(accountType: AccountTypeEnu.HOST));
        ////}

        ////[Fact]
        ////public async void GetUser()
        ////{
            //////const string path = "/home/stiproot/Code/projects/asq/temp/";
            ////const string username = "stiphost";
            ////const string password = "stip";

            ////var user = await this._access.GetUser(username, password);
            //////var host = user.Host;

            //////user.Host.ExtUser = null;
            //////user.Host = null;

            ////Assert.NotNull(user);
            ////Assert.NotNull(user.Host);
            ////Assert.NotNull(user.PaymentInfo);
            ////Assert.True(user.Host.Specialities.Count() > 0);
            ////Assert.True(user.Interests.Count() > 0);

            //////throw new Exception(JsonSerializer.Serialize(user));
            //////throw new Exception(JsonSerializer.Serialize(user));
        ////}

        ////[Fact]
        ////public async void UpdateAccountTypeOfUser()
        ////{
            ////Guid id = Guid.Parse("64dedb7f-c0b4-437c-85b8-8b14a948d56e");

            ////var user = await this._access.GetUser(id);
            ////Assert.NotNull(user);
            ////Assert.True((int)user.AccountType == (int)AccountTypeEnu.HOST);

            ////user.HostId = null;
            ////user.Host = null;
            ////user.AccountType = AccountTypeEnu.STUDENT;

            ////await this._access.UpdateUser(user);
        ////}

        ////[Fact]
        ////public async void UpdateUserImg()
        ////{
            ////Guid id = Guid.Parse("64dedb7f-c0b4-437c-85b8-8b14a948d56e");

            ////var user = await this._access.GetUser(id);
            ////Assert.NotNull(user);

            ////var newImgDto = this._imgAccess.GetTestImg();
            ////user.Img = newImgDto;
            ////user.ImgId = 0;

            ////await this._access.UpdateUser(user);
        ////}
    //}
//}
