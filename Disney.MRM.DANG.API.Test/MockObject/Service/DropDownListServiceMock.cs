using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disney.MRM.DANG.Service.Implementations;
using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.Service.Contracts;
using Disney.MRM.DANG.Repository;

namespace Disney.MRM.DANG.API.Test.MockObject.Service
{
    public class DropDownListServiceMock : DropDownListService
    {

        public DropDownListServiceMock(IDropDownListRepository idropDownListRepository = null,
            IUserRepository iuserRepository = null,
            IChannelTalentRepository ichannelTalentRepository = null,
            ITitleSeasonSeriesRepository ititleSeasonSeriesRepository = null,
            IChannelDeliverableTypeDeliverableGroupRepository ichannelDeliverableTypeDeliverableGroupRepository = null,
            IAssetGroupChannelHouseAdvertiserRepository iassetGroupChannelHouseAdvertiserRepository = null,
            IDeliverable_TalentRepository italentDeliverableRepository = null,
            IDeliverableType_DeliverableSubTypeRepository iDeliverableType_DeliverableSubTypeRepository = null,
            IDeliverableTypeRepository iDeliverableTypeRepository = null,
            IChannel_DeliverableGroup_VendorRepository ichannel_DeliverableGroup_VendorRepository = null,
            IActivityTypeActivityStatusRepository iactivityTypeActivityStatusRepository = null,
            ITargetRepository targetRepository = null,
            ITypeOfWorkRepository typeOfWorkRepository = null,
            IDeliverableDateTypeRepository deliverableDateTypeRepository = null,
            ITalentRepository talentRepository = null,
            IMusicSubLibraryRepository musicSubLibraryRepository = null,
            IMusicLibraryRepository musicLibraryRepository = null,
            IMediaOutletRepository mediaOutletRepository = null,
            IMediaOutletCategoryRepository mediaOutletCategoryRepository = null,
            IGLAccountMediaOutletCategoryRepository gLAccountMediaOutletCategoryRepository = null,
            IWBSFiscalYear_ChannelRepository wBSFiscalYear_ChannelRepository = null,
            IInvoiceStatusRepository invoiceStatusRepository = null,
            IChannelCostCenterRepository channelCostCenterRepository = null,
            IChannelProfitCenterRepository channelProfitCenterRepository = null,
            IVendorRepository vendorRepository = null,
            IWorkOrderType_Channel_WorkOrderVendorRepository workOrderType_Channel_WorkOrderVendorRepository = null,
            IWorkOrderVendorRepository workOrderVendorRepository = null,
            IDeliverableGroup_DeliverableStatusRepository deliverableGroup_DeliverableStatusRepository = null,
            IMasterVendorViewRepository masterVendorViewRepository = null,
            IDeliverableGroup_DeliverableDateTypeRepository deliverableGroup_DeliverableDateTypeRepository = null,
            IBudgetStatusRepository budgetStatusRepository = null,
            IDeliverableCategoryRepository deliverableCategoryRepository = null,
            IBusinessAreaRepository businessAreaRepository = null,
            IChannelDeliverableGroupRepository ChannelDeliverableGroupRepository = null,
            IChannel_DeliverableGroup_VendorRepository channelDeliverableGroupVendorRepository = null,
            IProductionMethodTypeRepository ProductionMethodTypeRepository = null,
            IWBSElementRepository wbsElementRepository = null,
            IChargeCodeTypeRepository chargeCodeTypeRepository = null
            )
            : base(idropDownListRepository, iuserRepository,
             ichannelTalentRepository, ititleSeasonSeriesRepository,
             ichannelDeliverableTypeDeliverableGroupRepository,
             iassetGroupChannelHouseAdvertiserRepository,
             italentDeliverableRepository,
             iDeliverableType_DeliverableSubTypeRepository,
             iDeliverableTypeRepository,
             ichannel_DeliverableGroup_VendorRepository,
             iactivityTypeActivityStatusRepository,
             targetRepository,
             typeOfWorkRepository, deliverableDateTypeRepository,
             talentRepository,
             musicSubLibraryRepository,
             musicLibraryRepository,
             mediaOutletRepository,
             mediaOutletCategoryRepository,
             gLAccountMediaOutletCategoryRepository,
             wBSFiscalYear_ChannelRepository,
             invoiceStatusRepository,
             channelCostCenterRepository,
             channelProfitCenterRepository,
             vendorRepository,
             workOrderType_Channel_WorkOrderVendorRepository,
             workOrderVendorRepository,
             deliverableGroup_DeliverableStatusRepository,
             masterVendorViewRepository,
             deliverableGroup_DeliverableDateTypeRepository,
             budgetStatusRepository,
             deliverableCategoryRepository,
             businessAreaRepository,
             ChannelDeliverableGroupRepository,
             channelDeliverableGroupVendorRepository,
             ProductionMethodTypeRepository,
             wbsElementRepository,
             chargeCodeTypeRepository)
        {
        }
    }
}

