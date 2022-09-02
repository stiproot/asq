using System;
using System.Threading.Tasks;
using DTO.Domain;
using DTO.Tracking;

namespace managers.Test
{
    public interface ITestManager
    {
        UserDto GetUser(AccountTypeEnu accountType = AccountTypeEnu.HOST);
    }
}