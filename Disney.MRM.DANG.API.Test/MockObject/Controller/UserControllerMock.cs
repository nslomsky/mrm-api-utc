using Disney.MRM.DANG.API.Contracts;
using Disney.MRM.DANG.API.Controllers;
using Disney.MRM.DANG.Core.Contracts;
using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disney.MRM.DANG.API.Test.MockObject.Controller
{
    public class UserControllerMock : UserController
    {
        public UserControllerMock(IUserService userService = null, ILogService loggerService = null, IHomeService homeService = null)
            : base(userService, loggerService, homeService)
        {

        }
    }
}
