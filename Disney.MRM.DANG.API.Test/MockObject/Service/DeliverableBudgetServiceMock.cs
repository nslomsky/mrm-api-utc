using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.Service.Contracts;
using Disney.MRM.DANG.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disney.MRM.DANG.API.Test.MockObject.Service
{
  public   class DeliverableBudgetServiceMock: DeliverableBudgetService
    {
        public DeliverableBudgetServiceMock(IDeliverableBudgetRepository deliverableBudgetRepository=null,
            IDeliverableSummeryofBudgetRepository DeliverableSummeryofBudgetRepository=null,
            IDeliverableProductionMethodTypeRepository mopDeliverableRepositry=null,
            IDeliverableLineOfBusinessRepository lobRepositry=null,
            IChannelDeliverableGroupRepository channelDeliverableGroupRepository=null,
            IProductionMethodTypeRepository mopRepository=null,
            IDeliverableRepository deliverableRepository=null,
            IUnitOfWork unitOfWork=null,
            IDeliverable_WBSElementRepository Deliverable_WBSElementRepository=null,
            IWBSElementRepository BudgetTypeRowRepository = null, IUserRepository UserRepository=null,
            IContractRequest_DeliverableRepository ContractRequestDeliverableRepository=null
            )
          : base(deliverableBudgetRepository, DeliverableSummeryofBudgetRepository, mopDeliverableRepositry, lobRepositry,
            channelDeliverableGroupRepository, mopRepository, deliverableRepository, unitOfWork, Deliverable_WBSElementRepository, BudgetTypeRowRepository, UserRepository,
            ContractRequestDeliverableRepository)
        {
        }
    }
}
