using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disney.MRM.DANG.Core.Contracts;
using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.Service.Implementations;

namespace Disney.MRM.DANG.API.Test.MockObject.Service
{
    public class IntegrationServiceMock : IntergrationService
    {
        public IntegrationServiceMock
         (IWorkOrderRepository workOrderRepository = null, ILogService loggerService = null,
         IUnitOfWork unitOfWork = null, IUserRepository iUserRepository = null,
         IActivityTypeActivityStatusRepository activityTypeActivityStatusRepository = null,
         IWorkOrderTransactionRepositry iBillingWorkOrderTransactionRepositry = null,
         IVendorRepository iVendorRepository = null, IGraphicHeaderRepository iGraphicHeaderRepository = null,
         ICostCenterRepository iCostCenterRepository = null,
         IBudgetByCategoryRollupRepository iBudgetByCategoryRollupRepository = null,
         IDeliverableRepository iDeliverableRepository = null,
         IWorkOrderVendorRepository iWorkOrderVendorRepository = null,
         //  IActivityRepository iActivityRepository,
         ITrackActivityElementRepository iTrackActivityElementRepository = null,
          ITitleRepository iTitleRepository = null,
         ISeasonRepository iSeasonRepository = null,
         ISeriesRepository iSeriesRepository = null,
         ITitleCategoryRepository iTitleCategoryRepository = null,
        IPremiereCategoryRepository iPremiereCategoryRepository = null,
         IScheduleRepository iScheduleRepository = null,
         //  ITrackActivityHeaderRepository iTrackActivityHeaderRepository,
         IDeliverableStatusRepository iDeliverableStatusRepository = null,
         IScriptRepository iScriptRepository = null,
         IInternalRepository iInternalRepository = null,
         //Contract Request - EDMX fix
         // IContractRequestHeaderRepository iContractRequestHeaderRepository = null,
         // IContractRequestLineRepository iContractRequestLineRepository = null,
         IContentPlanRepository iContentPlanRepository = null,
         IContentPlanEventRepository iContentPlanEventRepository = null,
         IContentPlanEventDatesRepository iContentPlanEventDatesRepository = null,
         IContentPlanEventScheduleRepository iContentPlanEventScheduleRepository = null,

         IApprovalRepository iApprovalRepository = null,
         IApprovalStatusRepository iApprovalStatusRepository = null,
         IApprovalTypeRepository iApprovalTypeRepository = null,
         ICalendarRepository iCalendarRepository = null,
         IDeliverableBudgetRepository iDeliverableBudgetRepository = null,
         IWBSFiscalYear_ChannelRepository iWBSFiscalYear_ChannelRepository = null,
         ITypeOfWorkRepository iTypeOfWorkRepository = null,
         IInvoiceLineRepository iInvoiceLineRepository = null,
         IInvoiceHeaderRepository iInvoiceHeaderRepository = null,
        //Contract Request - EDMX fix
         IWorkOrderType_Channel_WorkOrderVendorRepository iWorkOrderType_Channel_WorkOrderVendorRepository = null,
         IWBSElementRepository _IWBSElementRepository = null) : base(
          workOrderRepository, loggerService, unitOfWork, iUserRepository, activityTypeActivityStatusRepository,
            iBillingWorkOrderTransactionRepositry, iVendorRepository, iGraphicHeaderRepository, iCostCenterRepository,
            iBudgetByCategoryRollupRepository, iDeliverableRepository, iWorkOrderVendorRepository,
            iTrackActivityElementRepository, iTitleRepository, iSeasonRepository, iSeriesRepository,
            iTitleCategoryRepository, iPremiereCategoryRepository, iScheduleRepository,
            iDeliverableStatusRepository, iScriptRepository, iInternalRepository, /*iContractRequestHeaderRepository,*///Contract Request - EDMX fix
            /*iContractRequestLineRepository,*/ iContentPlanRepository, iContentPlanEventRepository,
            iContentPlanEventDatesRepository, iContentPlanEventScheduleRepository, iApprovalRepository,
            iApprovalStatusRepository, iApprovalTypeRepository, iCalendarRepository, iDeliverableBudgetRepository,
            iWBSFiscalYear_ChannelRepository, iTypeOfWorkRepository, iInvoiceLineRepository, iInvoiceHeaderRepository,
            iWorkOrderType_Channel_WorkOrderVendorRepository, _IWBSElementRepository)
        {

        }
    }
}

