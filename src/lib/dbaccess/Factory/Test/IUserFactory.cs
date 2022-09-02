using System;
using System.Collections.Generic;
using AutoMapper;
using dbaccess.Models;
using DTO.Domain;

namespace dbaccess.Factory.Test
{
    public interface IUserFactory
    {
       TbUser GenerateUser(AccountTypeEnu accountType);
       UserDto GenerateUserDto(AccountTypeEnu accountType = AccountTypeEnu.HOST);
    }
}