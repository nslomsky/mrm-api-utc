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
    public class DeliverableV2ControllerMock : DeliverableV2Controller
    {
        public DeliverableV2ControllerMock(IUserService userService=null, ILogService loggerService=null,
            IDeliverableManager deliverableManager=null,
            IDeliverablePropertyManager deliverablePropertyManager=null,
            IDeliverableCommentManager deliverableCommentManager=null,
            IDeliverableBudgetManager deliverableBudgetManager=null,
            IDeliverableGeneralInfoManager deliverableGeneralInfoManager=null,
            IOffAirActivityManager offAirActivityManager=null,
            IInvoiceManager invoiceManager=null,
            IJournalEntryManager journalEntryManager=null,
            ITrackManager trackManager=null,
            IPaidMediaActivityManager paidMediaActivityManager=null,
            IGraphicActivityManager graphicActivityManager=null,
            IScriptManager scriptManager=null,
            IDeliverableServiceV2 deliverableService=null,
            IDeliverableBulkUpdateManager deliverableBulkUpdateManager=null,
            IWorkOrderManager workOrderManager=null,
            IDeliverableDirector deliverableDirector=null,
            ITitleCategoryRepository titleCategoryRepository=null)
             : base(userService, loggerService,deliverableManager,deliverablePropertyManager, deliverableCommentManager,
               deliverableBudgetManager,deliverableGeneralInfoManager, offAirActivityManager, invoiceManager,
               journalEntryManager, trackManager, paidMediaActivityManager, graphicActivityManager, scriptManager,
               deliverableService, deliverableBulkUpdateManager, workOrderManager, deliverableDirector,titleCategoryRepository)
        {

        }
    }
}
