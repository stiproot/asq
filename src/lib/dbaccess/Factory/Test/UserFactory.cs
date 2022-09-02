using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using dbaccess.Models;
using dbaccess.Repository;
using DTO.Domain;

namespace dbaccess.Factory.Test
{
    public class UserFactory: IUserFactory
    {
        private readonly IMapper _mapper;
        private readonly Random _rnd;
        private readonly IImgResourceAccess _imgAccess;
        private int Next(int min, int max) => this._rnd.Next(min, max);

        public UserFactory(IMapper mapper, IImgResourceAccess imgAccess)
        {
            this._mapper = mapper;
            this._rnd = new Random();
            this._imgAccess = imgAccess;
        }

        public TbUser GenerateUser(AccountTypeEnu accountType = AccountTypeEnu.HOST)
        {
            var img = this._imgAccess.GetTestImg();

            string username = "kalan2";
            //string email = "simon@asq.properties";
            string email = "stipcich.kalan2@gmail.com";

            var user = new TbUser
            {
                Id = 0,
                UniqueId = Guid.NewGuid().ToString(),
                CreationDateUtc = DateTime.UtcNow,
                CreationUserId = 0,
                Inactive = false,
                AccountTypeId = (int)AccountTypeEnu.HOST,
                Username = username ?? Guid.NewGuid().ToString().Substring(0, 25),
                Email = email ?? $"{username ?? Guid.NewGuid().ToString()}@asq.properties",
                Name = Guid.NewGuid().ToString().Substring(0, 25),
                Surname = Guid.NewGuid().ToString().Substring(0, 25),
                Password = Guid.NewGuid().ToString().Substring(0, 25),
                PaymentInfoId = 0,
                HostId = 0,
                ImgId = 0,
                TimezoneId = 4,
                TbFocusUserMappings = new List<TbFocusUserMapping>() 
                { 
                    new TbFocusUserMapping
                    { 
                        Id = 0,
                        UniqueId = Guid.NewGuid().ToString(),
                        CreationDateUtc = DateTime.UtcNow,
                        CreationUserId = 0,
                        Inactive = false,
                        FocusId = 1,
                        UserId = 0,
                    } 
                },
                PaymentInfo = new TbPaymentInfo
                {
                    Id = 0,
                    UniqueId = Guid.NewGuid().ToString(),
                    CreationDateUtc = DateTime.UtcNow,
                    CreationUserId = 0,
                    Inactive = false,
                    CardNumber = this.Next(100000, 999999).ToString() + this.Next(10000000, 99999999).ToString(),
                    Cvc = this.Next(100, 9999).ToString(),
                    ExpirationDate = DateTime.Now.AddYears(2),
                    CardTypeId = 1,
                },
                Host = new TbHost
                {
                    Id = 0,
                    UniqueId = Guid.NewGuid().ToString(),
                    CreationDateUtc = DateTime.UtcNow,
                    CreationUserId = 0,
                    Inactive = false,
                    Company = Guid.NewGuid().ToString(),
                    CareerSummary = Guid.NewGuid().ToString(),
                    ExtUserId = 1,
                    TbFocusHostMappings = new List<TbFocusHostMapping>() 
                    { 
                        new TbFocusHostMapping
                        { 
                            Id = 0,
                            UniqueId = Guid.NewGuid().ToString(),
                            CreationDateUtc = DateTime.UtcNow,
                            CreationUserId = 0,
                            Inactive = false,
                            FocusId = 1,
                            HostId = 0
                        } 
                    },
                },
                Img = new TbImg
                {
                    Id = 0,
                    UniqueId = Guid.NewGuid().ToString(),
                    CreationDateUtc = DateTime.UtcNow,
                    CreationUserId = 0,
                    Inactive = false,
                    Url = img.Url,
                    ThumbnailUrl = img.ThumbnailUrl
                }
            };

            if(accountType == AccountTypeEnu.STUDENT)
            {
                user.Host = null;
                user.AccountTypeId = (int)AccountTypeEnu.STUDENT;
            }

            return user;
        } 

        public UserDto GenerateUserDto(AccountTypeEnu accountType = AccountTypeEnu.HOST)
        {
            return _mapper.Map<TbUser, UserDto>(this.GenerateUser(accountType));
        }

        public async Task<UserDto> GenerateUserDtoAsync(AccountTypeEnu accountType = AccountTypeEnu.HOST)
        {
            return await Task.FromResult<UserDto>(_mapper.Map<TbUser, UserDto>(this.GenerateUser(accountType)));
        }
    }
}