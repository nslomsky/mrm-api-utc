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
    class DropDownControllerMock : DropDownController
    {
        public DropDownControllerMock(IUserService userService = null, ILogService loggerService = null, IProjectService projectService = null, IContractRequestService contractRequestService = null,
            BudgetService budgetService = null, IApprovalService approvalService = null, IInternationalService internationalService = null,
            IDeliverableService iDeliverableService = null, IDeliverableServiceV2 deliverableServiceV2 = null, IFinanceService financeService = null)
            : base(userService, loggerService, projectService, contractRequestService, budgetService, approvalService, internationalService, iDeliverableService, deliverableServiceV2, financeService)
        {
        }
    }
}
