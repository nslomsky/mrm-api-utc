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
    public class TrackServiceMock : TrackService
    {

        public TrackServiceMock(ITrackActivityElementRepository itrackRepository = null,
            ITrackTypeRepository itrackTypeRepositry = null,
            IAssetGroupChannelHouseAdvertiserRepository iassetGroupChannelHouseAdvertiserRepository = null,
            IActivityTypeActivityStatusService iactivityTypeActivityStatusService = null,
            DeliverableCommentService icommentService = null,
            IDeliverableRepository ideliverableRepository = null,
            IDeliverableStatusRepository ideliverableStatusRepository = null,
            IDeliverableDateRepository deliverableDateRepository = null,
            IDeliverableDateTypeRepository deliverableDateTypeRepository = null,
            IDeliverableInternationalDetailRepository deliverableInternationalDetailRepository = null,
            IDeliverableInternationalPathRepository internationalPathRepository = null,
            IJellyrollFolderRepository jellyrollFolderRepository = null,
            IJellyrollAssetFormatRepository jellyrollAssetFormatRepository = null,
            IJellyRollPlatformRepository jellyrolllPlatformRepository = null,
            IApprovalRepository approvalRepository = null,
            IUnitOfWork iunitOfWork =null)
            : base(itrackRepository, itrackTypeRepositry, iassetGroupChannelHouseAdvertiserRepository, iactivityTypeActivityStatusService, icommentService, ideliverableRepository,
                   ideliverableStatusRepository, deliverableDateRepository,deliverableDateTypeRepository,deliverableInternationalDetailRepository,internationalPathRepository,jellyrollFolderRepository,
                   jellyrollAssetFormatRepository,jellyrolllPlatformRepository,approvalRepository,iunitOfWork)
        {
            
        }
    }
}
