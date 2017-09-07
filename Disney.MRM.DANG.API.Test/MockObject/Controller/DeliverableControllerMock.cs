using Disney.MRM.DANG.API.Controllers;
using Disney.MRM.DANG.Core.Contracts;
using Disney.MRM.DANG.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disney.MRM.DANG.API.Test.MockObject.Controller
{
    public class DeliverableControllerMock : DeliverableController
    {
        public DeliverableControllerMock(IUserService userService = null, ILogService loggerService = null, IDeliverableService deliverableService = null,
                IProductService productService = null, IPropertyService propertyService = null, IIntergrationService _intergrationService = null,
                IBudgetService _iBudgetService = null, ITrackApprovalService iTrackApprovalService = null, IImageService imageService = null,
                IApprovalService approvalService = null, IInternationalService internationalService = null)
            : base(userService, loggerService, deliverableService, productService, propertyService, _intergrationService, _iBudgetService, iTrackApprovalService,
             imageService, approvalService, internationalService)
        {
        }
    }
}
