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
    public class FinanceServiceMock : FinanceService
    {
        public FinanceServiceMock(
            IUnitOfWork unitOfWork = null,
            IDeliverableRepository deliverableRepository = null,
            ITypeOfWorkRepository typeOfWorkRepository = null,
            IPaymentTermRepository paymentTermRepository = null,
            IInvoiceStatusRepository invoiceStatusRepository = null,
            IVendorRepository vendorRepository = null,
            ICalendarRepository calendarRepository = null,
            IWBSFiscalYear_ChannelRepository wbsFiscalYearChannelRepository = null,
            IGLAccountRepository glAccountRepository = null,
            IProductionMethodTypeRepository productionMethodTypeRepository = null,
            IInvoiceHeaderRepository invoiceHeaderRepository = null,
            IInvoiceLineRepository invoiceLineRepository = null,
            ILogService logService = null,
            IDeliverableBudgetRepository Deliverablebudgetrepository = null,
            IUserChannelRepository userChannelRepository = null,
            IBudgetTypeRepository BudgetTypeRepository = null,
            IBudgetRepository budgetRepository = null,
            IApInvoiceBatchRepository apInvoiceBatchRepository = null,
            IInternalRepository internalRepository = null,
            IChannelCostCenterRepository channelCostCenterRepository = null,
            IChannelProfitCenterRepository channelProfitCenterRepository = null,
            IBudgetByChannelRepository _iBudgetByChannelRepository = null,
            IActualsReconciliationDashboardRepository actualsReconciliationDashboardRepository = null,
            IActualsReconciliationRepository actualsReconciliationRepository = null,
            IBudgetByCategoryRollupRepository budgetByCategoryRollupRepository = null,
            IWBS_DeliverablesRepository _iWBS_DeliverablesRepository = null,
            IUserRepository iUserRepository = null,
            IWorkOrderTransactionRepositry iworkOrderTransactionRepositry = null, IWBSElementRepository iwbselementRepository = null) : base(
                 unitOfWork,
             deliverableRepository,
             typeOfWorkRepository,
             paymentTermRepository,
             invoiceStatusRepository,
             vendorRepository,
             calendarRepository,
             wbsFiscalYearChannelRepository,
             glAccountRepository,
             productionMethodTypeRepository,
             invoiceHeaderRepository,
             invoiceLineRepository,
             logService,
             Deliverablebudgetrepository,
             userChannelRepository,
             BudgetTypeRepository,
             budgetRepository,
             apInvoiceBatchRepository,
             internalRepository,
             channelCostCenterRepository,
             channelProfitCenterRepository,
             _iBudgetByChannelRepository,
             actualsReconciliationDashboardRepository,
             actualsReconciliationRepository,
            budgetByCategoryRollupRepository,
             _iWBS_DeliverablesRepository,
             iUserRepository,
             iworkOrderTransactionRepositry, iwbselementRepository
           )
        {

        }
    }
}
