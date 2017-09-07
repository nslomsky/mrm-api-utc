using Disney.MRM.DANG.API.Managers.Deliverable;
using Disney.MRM.DANG.API.Managers.Intergration;
using Disney.MRM.DANG.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disney.MRM.DANG.API.Test.MockObject.Manager
{
    public class DeliverableBudgetManagerMock : DeliverableBudgetManager
    {
        public DeliverableBudgetManagerMock(IDeliverableBudgetService deliverableBudgetService = null, IUserService userSerive = null,
            IntergrationManager intergrationManager = null, IDeliverableServiceV2 deliverableServiceV2 = null, 
            IDropDownListService dropDownListService = null, IDeliverableBudgetService DeliverableBudgetService = null)
        : base(deliverableBudgetService, userSerive, intergrationManager, deliverableServiceV2, dropDownListService, DeliverableBudgetService)
        {

        }

    }
}
