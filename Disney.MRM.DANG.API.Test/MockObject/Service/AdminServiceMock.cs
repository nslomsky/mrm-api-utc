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
    public class AdminServiceMock : AdminService
    {
        public AdminServiceMock(
            IUnitOfWork unitOfWork = null, IVendorRepository vendor = null, IUserTitleRepository userTitle = null
            , ITypeOfWorkCategoryRepository TOWCategoryRepository = null,
          IMediaOutletCategoryRepository MediaOutletCategoryRepository = null, IGLAccountMediaOutletCategoryRepository GLAccountMediaOutletCategoryRepository = null,
          IWBSElementRepository wbsElementRepository = null, IMediaOutletRepository mediaOutletRepository = null, IChannelDeliverableGroupUserTitleRepository channelDeliverableGroupUserTitleRepository = null
            , IMasterVendorRepository masterVendorRepository = null, IDeliverableGroupRepository deliverableGroupRepository = null,
          ITypeOfWorkRepository towRepository = null
            ) : base(unitOfWork, vendor, userTitle, TOWCategoryRepository,
                     MediaOutletCategoryRepository, GLAccountMediaOutletCategoryRepository, wbsElementRepository, mediaOutletRepository, channelDeliverableGroupUserTitleRepository, masterVendorRepository, deliverableGroupRepository,towRepository
            )
        {

        }
    }
}
