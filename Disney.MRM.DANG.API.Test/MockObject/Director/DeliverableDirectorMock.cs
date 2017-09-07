using Disney.MRM.DANG.API.Contracts;
using Disney.MRM.DANG.API.Managers.Deliverable;
using Disney.MRM.DANG.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disney.MRM.DANG.API.Test.MockObject.Director
{
    public class DeliverableDirectorMock :  DeliverableDirector
    {
        public DeliverableDirectorMock(IDeliverableManager deliverableManager=null, IDeliverableBudgetManager budgetManager=null, IDeliverableCommentManager deliverableCommentManager=null,
            IPaidMediaActivityManager paidMediaActivityManager=null, IDeliverablePropertyManager deliverablePropertyManager=null, IIntergrationManager intergrationManager=null,
            IDeliverableServiceV2 deliverableService=null, IDeliverableGeneralInfoManager deliverableGeneralInfoManager=null, IDeliverableBudgetManager deliverableBudgetManager=null, IOffAirActivityManager offAirActivityManager=null, IDeliverableDateService deliverableDateService=null)
            :base( deliverableManager,  budgetManager,  deliverableCommentManager,
                 paidMediaActivityManager,  deliverablePropertyManager,  intergrationManager,
             deliverableService,  deliverableGeneralInfoManager,  deliverableBudgetManager,  offAirActivityManager,  deliverableDateService)
        {

        }
    }
}
