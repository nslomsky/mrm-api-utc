using Disney.MRM.DANG.API.Managers.Deliverable;
using Disney.MRM.DANG.API.Managers.Intergration;
using Disney.MRM.DANG.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disney.MRM.DANG.API.Test.MockObject.Managers
{
    public class IntergrationManagerMock : IntergrationManager
    {
        public IntergrationManagerMock(IIntergrationService intergrationService =null, IDeliverableServiceV2 deliverableV2Service =null)
        : base(intergrationService, deliverableV2Service)
        {

        }
    }
}
