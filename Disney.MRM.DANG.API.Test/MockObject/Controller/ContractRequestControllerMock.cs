using Disney.MRM.DANG.API.Controllers;
using Disney.MRM.DANG.Core.Contracts;
using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.Service.Contracts;
using Disney.MRM.DANG.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Disney.MRM.DANG.API.Test.MockObject.Controller
{
    class ContractRequestControllerMock:ContractRequestController
    {
        public ContractRequestControllerMock(IUserService userService = null, ILogService loggerService = null,IContractRequestService contractRequestService = null,
            IProjectService contractProjectService = null, IBudgetService budgetservice = null, IDeliverableServiceV2 deliverableServiceV2 = null)
            : base(userService, loggerService,contractRequestService, contractProjectService, budgetservice, deliverableServiceV2)
        {
        }
    }
}
