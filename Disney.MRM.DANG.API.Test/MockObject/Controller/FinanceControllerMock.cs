using Disney.MRM.DANG.API.Controllers;
using Disney.MRM.DANG.Core.Contracts;
using Disney.MRM.DANG.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disney.MRM.DANG.API.Test.MockObject.Controller
{
    public class FinanceControllerMock : FinanceController
    {
        public FinanceControllerMock(IUserService userService = null, ILogService loggerServicee = null, IFinanceService financeServicee = null)
             : base(userService, loggerServicee, financeServicee)
        {

        }
    }
}
