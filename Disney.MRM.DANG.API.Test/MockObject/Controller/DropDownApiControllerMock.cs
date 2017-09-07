using Disney.MRM.DANG.API.Controllers;
using Disney.MRM.DANG.Core.Contracts;
using Disney.MRM.DANG.Service.Contracts;
using Disney.MRM.DANG.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disney.MRM.DANG.API.Test.MockObject.Controller
{
   public class DropDownApiControllerMock : DropDownApiController
    {
        public DropDownApiControllerMock(IUserService userService = null, 
            ILogService loggerService = null,
            IDropDownListService dropDownListService = null,
            IHomeService homeService = null)
           : base(userService, loggerService, dropDownListService, homeService)
        {
        }
    }
}
