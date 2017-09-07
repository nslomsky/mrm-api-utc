using Disney.MRM.DANG.Core.Contracts;
using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disney.MRM.DANG.API.Test.MockObject.Service
{
    public class BudgetServiceMock : BudgetService
    {
        public BudgetServiceMock(
            IUnitOfWork unitOfWork = null, IBudgetRepository budget = null, IChannelRepository channel = null
            , IUserChannelRepository userChannel = null,
          ILogService loggerService = null, ITypeOfWorkRepository iTypeOfWork = null,
          IFamilyProductIssueTowDeliverableRepository iFamilyProductIssueTowDeliverableRepository = null,
IBudgetByChannelRepository budgetByChannel = null, IWBSElementRepository iBudgetTypeTOWRepository = null,
IBudgetByCategoryRepository budgetByCategory = null
            , IBudgetByCategoryRollupRepository iBudgetByCategoryRollup = null,
ITypeOfWork_DeliverableCategory _ITypeOfWork_DeliverableCategory = null,
            IWBSElementCalendarRepository _iForecastBudgetTypeCalendarTOWRepository = null,
            ICalendarRepository _iCalendarRepository = null,
            ITypeOfWorkCategoryRepository iTypeOfWorkCategoryRepository = null,
            IWBSFiscalYear_ChannelRepository iWBSFiscalYear_ChannelRepository = null,
            IBudgetTypeRepository iBudgetTypeRepository = null,
            IDeliverableBudgetRepository _iDeliverableBudgetRepository =null,
            IDeliverableRepository _deliverableRepository= null
            ) : base(unitOfWork, budget, channel, userChannel,
                     loggerService, iTypeOfWork, iFamilyProductIssueTowDeliverableRepository,
                    budgetByChannel, iBudgetTypeTOWRepository, budgetByCategory
            , iBudgetByCategoryRollup,  _ITypeOfWork_DeliverableCategory, _iForecastBudgetTypeCalendarTOWRepository,
                    _iCalendarRepository,
             iTypeOfWorkCategoryRepository, iWBSFiscalYear_ChannelRepository, iBudgetTypeRepository, _iDeliverableBudgetRepository, _deliverableRepository)
        {

        }
    }
}
