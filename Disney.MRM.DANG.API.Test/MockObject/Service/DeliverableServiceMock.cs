using Disney.MRM.DANG.Core.Contracts;
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
    public class DeliverableServiceMock : DeliverableService
    {
        public DeliverableServiceMock(IUnitOfWork unitOfWork = null, IDeliverableRepository deliverableRepository = null,
            IDeliverableStatusRepository deliverableStatusRepository = null, IDepartmentRepository departmentRepository = null,
            IChannelRepository channelRepository = null, ITypeOfWorkRepository typeOfWorkRepository = null,
            ITypeOfWorkCategoryRepository typeOfWorkTypeRepository = null, IDeliverableCategoryRepository deliverableCategoryRepository = null,
            IDeliverableTypeRepository deliverableTypeRepository = null, IDeliverableSubTypeRepository deliverableSubTypeRepository = null,
            ITargetRepository targetRepository = null, IActivityTypeRepository activityTypeRepository = null,
            IBudgetTypeRepository budgetTypeRepository = null, IWBSElementRepository budgetTypeTOWRepository = null,
            IFamilyProductIssueTowDeliverableRepository familyProductIssueTowDeliverableRepository = null, IVendorRepository vendorRepository = null,
            IProductionMethodTypeRepository productionMethodTypeRepository = null, IUserRepository userRepository = null,
            IGLAccountRepository glAccountRepository = null, IActivityStatusRepository activityStatusRepository = null, IMediaOutletCategoryRepository mediaOutletCategoryRepository = null,
            IMediaOutletRepository mediaOutletRepository = null,
            IPrintCategoryRepository printCategoryRepository = null, ICalendarRepository calendarRepository = null,
            ILogService logService = null, IDeliverableTypeCompanyVendorRepository deliverableTypeCompanyVendorRepository = null, IOffAirDesignRepository printRepository = null,
            IMediaBuyCommittedRepository mediaBuyCommittedRepository = null,
            IInternalRepository iInternalRepository = null,
            IActivityStatusCategoryRepository iActivityStatusCategoryRepository = null, IChannelCostCenterRepository channelCostCenterRepository = null,
            //Contract Request - EDMX fix
            //IContractRequestHeaderRepository contractRequestHeaderRepository = null, IContractRequestLineRepository contractRequestLineRepository = null,
            ITrackActivityElementRepository trackActivityElementRepository = null, IAssetGroupRepository assetGroupRepository = null,
            IAssetGroupChannelHouseAdvertiserRepository assetGroupChannelHouseAdvertiserRepository = null,
            IUserChannelRepository userChannelRepository = null,
            IChannelTalentRepository channelTalentRepository = null,
            IDeliverable_TalentRepository talentDeliverableRepository = null, ITalentRepository talentRepository = null,
            IUserTitleRepository userTitleRepository = null, IDeliverableUserTitleMrmUserRepository deliverableUserTitleMrmUserRepository = null,
            IDeliverableProductionMethodTypeRepository deliverableProductionMethodTypeRepository = null, IDeliverableDateTypeRepository deliverableDateTypeRepository = null,
            IDeliverableGroupDeliverableDateTypeRepository deliverableGroupDeliverableDateTypeRepository = null,
            IDeliverableDateRepository deliverableDeliverableDateTypeRepository = null, IDeliverableGroupRepository deliverableGroupRepository = null,
            IScriptRepository scriptRepository = null, IMusicRepository musicRepository = null, IMusicSubLibraryRepository _musicSubLibraryRepository = null,
            IMusicLibraryRepository _musicLibraryRepository = null, IRecordingTypeRepository recordingTypeRepository = null, IMusicUsageTypeRepository musicUsageTypeRepository = null,
            IWBS_DeliverablesRepository wbsDeliverablesRepository = null,
            IGraphicElementRepository graphicElementRepository = null, IGraphicImageRepository graphicImageRepository = null,
            IGraphicElementTypeRepository graphicElementTypeRepository = null, IGraphicElementTypeGraphicPackageChannelRepository graphicElementTypeGraphicPackageChannelRepository = null,
            IGraphicHeaderRepository graphicHeaderRepository = null,
            ITrackTypeRepository trackTypeRepository = null, IWorkOrderVendorRepository workOrderVendorRepository = null,
            IWorkOrderTypeRepository workOrderTypeRepository = null,

            IDeliverableDateRepository deliverableDeliverableGroupDeliverableDateTypeRepository = null,
            IDeliverableDateTypeRepository iDeliverableDateTypeRepository = null,
            IMediaTypeRepository mediaTypeRepository = null, IApprovalRepository approvalRepository = null,
            IApprovalTypeChannelBudgetTypeMRMUserUserTitleRepository approvalTypeChannelBudgetTypeMRMUserUserTitleRepository = null, IImageService imageService = null, IGraphicFrameRateRepository graphicFrameRateRepository = null, IGraphicPackageRepository graphicPackageRepository = null
            , IApprovalTypeChannelBudgetTypeMRMUserUserTitleRepository iApprovalTypeChannelBudgetTypeMRMUserUserTitleRepository = null
            , IChannelDeliverableTypeDeliverableGroupRepository channelDeliverableTypeDeliverableGroupRepository = null,
            IDeliverableDateSummaryRepository deliverableDateSummaryRepository = null,
            IDeliverableSecondaryTargetRepostiory deliverableSecondaryTargetRepostiory = null,
            IDeliverablePlannedLengthRepository deliverablePlannedLengthRepository = null,
            IInternationalService internationalService = null,
            ICommentRepository commentRepository = null,
            ICommentTypeRepository commentTypeRepository = null,
            IWBSFiscalYear_ChannelRepository iWBSFiscalYear_ChannelRepository = null,
            IDeliverableHouseNumberRepository deliverableHouseNumberRepository = null,
            IPropertyService propertyService = null,
            ILineOfBusinessRepository lineOfBusinessRepository = null,
            IDeliverableLineOfBusinessRepository deliverableLineOfBusinessRepository = null,
            IDeliverableBudgetRepository deliverableBudgetRepository = null,
            IDeliverable_VendorRepository ideliverable_VendorRepository = null,
            IDeliverableInternationalDetailRepository deliverableInternationalDetailRepository = null,
            IDeliverableInternationalPathRepository deliverableInternationalPathRepository = null,
            //Contract Request - EDMX fix
            IHDeliverableBudgetRepository hDeliverableBudgetRepository = null, IDeliverableBudgetRepository deliverable_BudgetRepository = null) : base(
                unitOfWork, deliverableRepository, deliverableStatusRepository, departmentRepository, channelRepository, typeOfWorkRepository, typeOfWorkTypeRepository, deliverableCategoryRepository, deliverableTypeRepository, deliverableSubTypeRepository, targetRepository, activityTypeRepository, budgetTypeRepository, budgetTypeTOWRepository, familyProductIssueTowDeliverableRepository, vendorRepository, productionMethodTypeRepository, userRepository, glAccountRepository, activityStatusRepository, mediaOutletCategoryRepository, mediaOutletRepository,
            printCategoryRepository, calendarRepository, logService, deliverableTypeCompanyVendorRepository, printRepository, mediaBuyCommittedRepository, iInternalRepository, iActivityStatusCategoryRepository, channelCostCenterRepository,/*contractRequestHeaderRepository, contractRequestLineRepository,*/ trackActivityElementRepository, assetGroupRepository, assetGroupChannelHouseAdvertiserRepository, userChannelRepository, channelTalentRepository, talentDeliverableRepository, talentRepository, userTitleRepository, deliverableUserTitleMrmUserRepository, deliverableProductionMethodTypeRepository, deliverableDateTypeRepository, deliverableGroupDeliverableDateTypeRepository, deliverableDeliverableDateTypeRepository, deliverableGroupRepository, scriptRepository, musicRepository, _musicSubLibraryRepository, _musicLibraryRepository, recordingTypeRepository, musicUsageTypeRepository, wbsDeliverablesRepository,
                                  graphicElementRepository, graphicImageRepository, graphicElementTypeRepository, graphicElementTypeGraphicPackageChannelRepository,
                                    graphicHeaderRepository,
            trackTypeRepository, workOrderVendorRepository, workOrderTypeRepository,
            deliverableDeliverableGroupDeliverableDateTypeRepository, iDeliverableDateTypeRepository, mediaTypeRepository, approvalRepository, approvalTypeChannelBudgetTypeMRMUserUserTitleRepository, imageService, graphicFrameRateRepository, graphicPackageRepository
            , iApprovalTypeChannelBudgetTypeMRMUserUserTitleRepository
            , channelDeliverableTypeDeliverableGroupRepository,
             deliverableDateSummaryRepository,
            deliverableSecondaryTargetRepostiory,
            deliverablePlannedLengthRepository,
             internationalService,
             commentRepository,
             commentTypeRepository,
             iWBSFiscalYear_ChannelRepository,
             deliverableHouseNumberRepository, propertyService,
             lineOfBusinessRepository,
             deliverableLineOfBusinessRepository,
             deliverableBudgetRepository,
             ideliverable_VendorRepository,
             deliverableInternationalDetailRepository,
             deliverableInternationalPathRepository,
             hDeliverableBudgetRepository, deliverable_BudgetRepository)
        {

        }
    }
}
