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
    class BudgetControllerMock:BudgetController
    {
        
        public BudgetControllerMock(IUserService userService = null,
            ILogService loggerService = null, BudgetService budgetservice=null,
          IUnitOfWork unitOfWork=null, IProductService productService=null, IDeliverableService deliverableService=null,
           IDeliverableServiceV2 deliverableServiceV2=null, IPropertyService propertyService=null)
            : base(userService,loggerService, budgetservice,unitOfWork,productService,deliverableService,deliverableServiceV2,propertyService)
        {
        }
    }
}
