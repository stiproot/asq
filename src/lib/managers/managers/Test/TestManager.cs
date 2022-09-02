using System;
using System.Threading.Tasks;
using DTO.Domain;
using DTO.Tracking;
using dbaccess;
using dbaccess.Models;
using dbaccess.Common;
using dbaccess.Factory.Test;

namespace managers.Test
{
    public class TestManager : ITestManager
    {
        private IUserFactory _userFactory;
        public TestManager(IUserFactory userFactory)
        {
            _userFactory = userFactory;
        }

        public UserDto GetUser(AccountTypeEnu accountType = AccountTypeEnu.HOST)
        {
            return _userFactory.GenerateUserDto(accountType);
        }
    }
}