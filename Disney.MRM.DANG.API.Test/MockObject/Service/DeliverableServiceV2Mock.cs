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
    public class DeliverableServiceV2Mock : DeliverableServiceV2
    {

        public DeliverableServiceV2Mock(IMasterVendorViewRepository masterVendorViewRepository = null,
            IGraphicHeaderRepository graphicsHeaderRepository = null,
            IPropertyService propertyService = null,
            ICommentRepository commentRepository = null,
            IDeliverableDateRepository deliverableDateRepository = null,
            IDeliverableRepository deliverableRepository = null,
            IDeliverableCommentService icommentService = null,
            ICommentTypeRepository commentTypeService = null,
            ITalentRepository talentRepository = null,
            IDeliverableUserTitleMrmUserRepository deliverableUserTitleMrmUserRepository = null,
            IDeliverableSecondaryTargetRepostiory deliverableSecondaryTargetRepository = null,
            IDeliverable_TalentRepository talentDeliverableRepository = null,
            IDeliverableStatusRepository deliverableStatusRepository = null,
            IChannelTalentRepository channelTalentRepository = null,
            IDeliverable_TalentRepository deliverableTalentRepository = null,
            ICalendarRepository calendarRepository = null,
            IUnitOfWork iunitOfWork = null,
            IDeliverable_VendorRepository deliverable_VendorRepository = null,
            ITrackService itrackService = null,
            IActivityTypeActivityStatusRepository activityTypeActivityStatusRepository = null,
            ITypeOfWorkRepository typeOfWorkRepository = null,
            ITypeOfWorkCategoryRepository typeOfWorkCategoryRepository = null,
            IScriptRepository scriptRepository = null,
            IChannelDeliverableGroupUserTitleRepository channelDeliverableGroupUserTitleRepository = null,
            IApprovalTypeChannelBudgetTypeMRMUserUserTitleRepository approvalTypeChannelBudgetTypeMRMUserUserTitleRepository = null,
            IDeliverableDateRepository deliverableDeliverableGroupDeliverableDateTypeRepository = null,
            IOffAirDesignRepository offAirDesignRepository = null,
            IDeliverableDateTypeRepository deliverableDateTypeRepository = null,
            IDeliverableInternationalDetailRepository deliverableInternationalDetailRepository = null,
            IDeliverableInternationalPathRepository deliverableInternationalPathRepository = null,
            ICampaignRepository campaignRepository = null,
            IUserTitleRepository userTitleRepository = null,
            IDeliverableGroupDeliverableDateTypeRepository deliverableGroupDeliverableDateTypeRepository = null,
            IOffAirDesignProductionMethodTypeRepository offAirDesignProductionMethodTypeRepository = null,
            IProductionMethodCategoryRepository productionMethodCategoryRepository = null,
            IAssetGroupChannelHouseAdvertiserRepository assetGroupChannelHouseAdvertiserRepository = null,
            IPaidMediaRepository paidMediaRepository = null,
            IGraphicHeaderRepository graphicHeaderRepository = null,
            IGraphicElementRepository graphicElementRepository = null,
            IGraphicImageRepository graphicImageRepository = null,
            IGraphicElementTypeGraphicPackageChannelRepository graphicElementTypeGraphicPackageChannelRepository = null,
            IDeliverableBudgetService budgetService = null,
            IPaidMediaInvoiceRepository paidMediaInvoiceRepository = null,
            IPaidMediaDetailRepository paidMediaDetailRepository = null,
            IMusicRepository musicRepository = null,
            IMusicSubLibraryRepository musicSubLibraryRepository = null,
            IDeliverableScriptService scriptService = null,
            IDeliverableBudgetRepository deliverableBudgetRepository = null,
            ITrackSearchRepository trackSearchRepository = null,
            IActivityStatusRepository activityStatusRepository = null,
            WBSFiscalYear_ChannelRepository wBSFiscalYear_ChannelRepository = null,
            IDeliverableProductionMethodTypeRepository mopDeliverableRepositry = null,
            IDeliverableLineOfBusinessRepository lobRepositry = null,
            IProductionMethodTypeRepository productionMethodTypeRepository = null,
            ILineOfBusinessRepository lineOfBusinessRepository = null,
            IDeliverableGroupTargetPlatformRepository targetPlatformRepository = null,
            ITrackActivityElementRepository trackActivityElementRepository = null,
            IDeliverableGroupRepository deliverableGroupRepository = null,
            IApprovalTypeRepository approvalTypeRepository = null,
            IApprovalService approvalService = null,
            IDepartmentRepository departmentRepository = null,
            IDeliverableScriptService deliverableScriptService = null,
            IDeliverable_BusinessAreaRepository deliverable_BusinessAreaRepository = null,
            IChannelRepository channelRepository = null,
            IVendorRepository vendorRepository = null,
            IContractRequest_DeliverableRepository ContractRequestDeliverableRepository=null,
             IUserRepository userRepository = null
            )
            : base(masterVendorViewRepository, graphicsHeaderRepository, propertyService, commentRepository, deliverableDateRepository, deliverableRepository, icommentService, commentTypeService, talentRepository, deliverableUserTitleMrmUserRepository, deliverableSecondaryTargetRepository, talentDeliverableRepository, deliverableStatusRepository, channelTalentRepository,
                deliverableTalentRepository, calendarRepository, iunitOfWork, deliverable_VendorRepository, itrackService, activityTypeActivityStatusRepository, typeOfWorkRepository, typeOfWorkCategoryRepository, scriptRepository, channelDeliverableGroupUserTitleRepository,
                approvalTypeChannelBudgetTypeMRMUserUserTitleRepository, deliverableDeliverableGroupDeliverableDateTypeRepository, offAirDesignRepository, deliverableDateTypeRepository, deliverableInternationalDetailRepository, deliverableInternationalPathRepository, campaignRepository, userTitleRepository, deliverableGroupDeliverableDateTypeRepository, offAirDesignProductionMethodTypeRepository, productionMethodCategoryRepository, assetGroupChannelHouseAdvertiserRepository, paidMediaRepository, graphicHeaderRepository,
                graphicElementRepository, graphicImageRepository, graphicElementTypeGraphicPackageChannelRepository, budgetService, paidMediaInvoiceRepository, paidMediaDetailRepository, musicRepository, musicSubLibraryRepository, scriptService, deliverableBudgetRepository, trackSearchRepository, activityStatusRepository, wBSFiscalYear_ChannelRepository, mopDeliverableRepositry, lobRepositry,
                productionMethodTypeRepository, lineOfBusinessRepository, targetPlatformRepository, trackActivityElementRepository,
                deliverableGroupRepository, approvalTypeRepository, approvalService, departmentRepository,
                deliverableScriptService, deliverable_BusinessAreaRepository, channelRepository, vendorRepository, ContractRequestDeliverableRepository, userRepository)
        {
        }
    }
}
