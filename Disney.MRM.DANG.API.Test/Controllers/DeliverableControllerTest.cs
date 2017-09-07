using Disney.MRM.DANG.API.Controllers;
using Disney.MRM.DANG.API.Test.MockObject.Controller;
using Disney.MRM.DANG.API.Test.MockObject.Service;
using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.Model;
using Disney.MRM.DANG.Service.Contracts;
using Disney.MRM.DANG.Service.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disney.MRM.DANG.API.Test.Controllers
{
    [TestClass]
    public class DeliverableControllerTest
    {
        private Moq.MockRepository mockRepository;
        private Moq.Mock<IDeliverableService> mockdeliverableService;
        private Moq.Mock<IImageService> mockImageService;
        private Moq.Mock<IProductService> mockproductService;
        private Moq.Mock<IPropertyService> mockpropertyService;
        private Moq.Mock<IIntergrationService> mockIntergrationService;
        private Moq.Mock<IBudgetService> mockBudgetService;
        private Moq.Mock<ITrackApprovalService> mockTrackApprovalService;
        private Moq.Mock<IApprovalService> mockApprovalService;
        private Moq.Mock<IInternationalService> mockInternationalService;
        private Moq.Mock<IDeliverableRepository> mockDeliverableRepository;


        [TestInitialize]
        public void ClassInit()
        {
            mockRepository = new MockRepository(MockBehavior.Loose) { DefaultValue = DefaultValue.Mock };
            mockBudgetService = mockRepository.Create<IBudgetService>();
            mockdeliverableService = mockRepository.Create<IDeliverableService>();
            mockImageService = mockRepository.Create<IImageService>();
            mockproductService = mockRepository.Create<IProductService>();
            mockpropertyService = mockRepository.Create<IPropertyService>();
            mockIntergrationService = mockRepository.Create<IIntergrationService>();
            mockTrackApprovalService = mockRepository.Create<ITrackApprovalService>();
            mockApprovalService = mockRepository.Create<IApprovalService>();
            mockInternationalService = mockRepository.Create<IInternationalService>();
            mockDeliverableRepository = mockRepository.Create<IDeliverableRepository>();
        }
        //Contract Request - EDMX fix
        //[TestMethod]
        ////MRM-139
        //public void Filter_DepartmentDeliverablesByTypeOfWork_Tests()
        //{
        //    //Set Data
        //    #region Data
        //    int typeOfWorkId = 1;
        //    int mrmUserId = 25;
        //    string networkLogin = "abc";
        //    string filter = "";
        //    var deliverablesByTOWAndDepartment = new List<DeliverableBudgetSummary>();

        //    DeliverableBudgetSummary deliverableOnAir = new DeliverableBudgetSummary
        //    {
        //        BudgetTypeName = "On Air"
        //    };
        //    DeliverableBudgetSummary deliverableOffAir = new DeliverableBudgetSummary
        //    {
        //        BudgetTypeName = "Off Air"
        //    };
        //    DeliverableBudgetSummary deliverablePaidMedia = new DeliverableBudgetSummary
        //    {
        //        BudgetTypeName = "Paid Media"
        //    };
        //    deliverablesByTOWAndDepartment.Add(deliverableOnAir);
        //    deliverablesByTOWAndDepartment.Add(deliverableOffAir);
        //    deliverablesByTOWAndDepartment.Add(deliverablePaidMedia);

        //    #endregion

        //    //Mock
        //    #region  Mock
        //    //DeliverableService Mock
        //    mockdeliverableService.Setup(x => x.QDeliverablesByTOWAndDepartment(It.IsAny<int>())).Returns(deliverablesByTOWAndDepartment.AsQueryable());
        //    //Deliverable Repository Mock
        //    mockDeliverableRepository.Setup(x => x.QGetDeliverablesByDepartmentAndTypeOfWork(It.IsAny<int>())).Returns(deliverablesByTOWAndDepartment.AsQueryable());

        //    var deliverableService = new DeliverableServiceMock(unitOfWork: null, deliverableRepository: mockDeliverableRepository.Object, deliverableStatusRepository: null, departmentRepository: null, channelRepository: null, typeOfWorkRepository: null, typeOfWorkTypeRepository: null, deliverableCategoryRepository: null, deliverableTypeRepository: null, deliverableSubTypeRepository: null, targetRepository: null, activityTypeRepository: null, budgetTypeRepository: null, budgetTypeTOWRepository: null, familyProductIssueTowDeliverableRepository: null, vendorRepository: null, productionMethodTypeRepository: null, userRepository: null, glAccountRepository: null, activityStatusRepository: null, mediaOutletCategoryRepository: null, mediaOutletRepository: null,
        //    printCategoryRepository: null, calendarRepository: null, logService: null, deliverableTypeCompanyVendorRepository: null, printRepository: null, mediaBuyCommittedRepository: null, iInternalRepository: null, iActivityStatusCategoryRepository: null, channelCostCenterRepository: null, contractRequestHeaderRepository: null, contractRequestLineRepository: null, trackActivityElementRepository: null, assetGroupRepository: null, assetGroupChannelHouseAdvertiserRepository: null, userChannelRepository: null, channelTalentRepository: null, talentDeliverableRepository: null, talentRepository: null, userTitleRepository: null, deliverableUserTitleMrmUserRepository: null, deliverableProductionMethodTypeRepository: null, deliverableDateTypeRepository: null, deliverableGroupDeliverableDateTypeRepository: null, deliverableDeliverableDateTypeRepository: null, deliverableGroupRepository: null, scriptRepository: null, musicRepository: null, _musicSubLibraryRepository: null, _musicLibraryRepository: null, recordingTypeRepository: null, musicUsageTypeRepository: null, wbsDeliverablesRepository: null,
        //                          graphicElementRepository: null, graphicImageRepository: null, graphicElementTypeRepository: null, graphicElementTypeGraphicPackageChannelRepository: null,
        //                            graphicHeaderRepository: null,
        //    trackTypeRepository: null, workOrderVendorRepository: null, workOrderTypeRepository: null,
        //    deliverableDeliverableGroupDeliverableDateTypeRepository: null, iDeliverableDateTypeRepository: null, mediaTypeRepository: null, approvalRepository: null, approvalTypeChannelBudgetTypeMRMUserUserTitleRepository: null, imageService: null, graphicFrameRateRepository: null, graphicPackageRepository: null
        //    , iApprovalTypeChannelBudgetTypeMRMUserUserTitleRepository: null
        //    , channelDeliverableTypeDeliverableGroupRepository: null,
        //     deliverableDateSummaryRepository: null,
        //    deliverableSecondaryTargetRepostiory: null,
        //    deliverablePlannedLengthRepository: null,
        //     internationalService: null,
        //     commentRepository: null,
        //     commentTypeRepository: null,
        //     iWBSFiscalYear_ChannelRepository: null,
        //     deliverableHouseNumberRepository: null, propertyService: null,
        //     lineOfBusinessRepository: null,
        //     deliverableLineOfBusinessRepository: null,
        //     deliverableBudgetRepository: null,
        //     ideliverable_VendorRepository: null,
        //     deliverableInternationalDetailRepository: null,
        //     deliverableInternationalPathRepository: null,
        //     hDeliverableBudgetRepository: null);

        //    //Budget Controller Mock
        //    var controller = new DeliverableController(userService: null,
        //        loggerService: null, deliverableService: deliverableService,
        //         productService: null, propertyService: null,
        //         _intergrationService: null,
        //        _iBudgetService: null, iTrackApprovalService: null, imageService: null,
        //         approvalService: null, internationalService: null);
        //    #endregion


        //    //Assertions
        //    #region Assertions
        //    //Assert Deliverable Service Method Call
        //    var deliverablesResult = deliverableService.QDeliverablesByTOWAndDepartment(typeOfWorkId);

        //    mockdeliverableService.Verify();
        //    mockDeliverableRepository.Verify(); //Asserts service calls

        //    Assert.IsNotNull(deliverablesResult);//Result is not Null
        //    //Assertion of Controller method
        //    filter = "" + "|" + "" + "|" + "Paid Media" + "|" + "false"; //Passing test data to filter for Paid Media records
        //    var paidMediaResult = controller.DepartmentDeliverablesByTypeOfWork(typeOfWorkId, filter, mrmUserId, networkLogin, "");
        //    Assert.IsNotNull(paidMediaResult.Data);
        //    Assert.AreEqual(paidMediaResult.Total, 1);

        //    filter = "";
        //    var noFilterResult = controller.DepartmentDeliverablesByTypeOfWork(typeOfWorkId, filter, mrmUserId, networkLogin, "");
        //    Assert.IsNotNull(noFilterResult.Data);
        //    Assert.AreEqual(noFilterResult.Total, 3);
        //    #endregion


        //}

        //MRM-695 - Same TOW to multiple Deliverables and WBS's
        [TestMethod]
        public void QDeliverablesByTOWAndDepartment_Test()
        {
            #region Data Setup
            int typeOfWorkId = 1;
            List<DeliverableBudgetSummary> deliverablesByTOWAndDepartment = new List<DeliverableBudgetSummary>();
            DeliverableBudgetSummary deliverableOnAir = new DeliverableBudgetSummary
            {
                TypeOfWorkId = 1,
                TypeOfWorkName = "Tow MRM",
                DeliverableId= 14030212,
                BudgetTypeName = "On Air"
            };
            DeliverableBudgetSummary deliverableOffAir = new DeliverableBudgetSummary
            {
                TypeOfWorkId = 1,
                TypeOfWorkName = "Tow MRM",
                DeliverableId = 14030212,
                BudgetTypeName = "Off Air"
            };
            DeliverableBudgetSummary deliverablePaidMedia = new DeliverableBudgetSummary
            {
                TypeOfWorkId = 1,
                TypeOfWorkName = "Tow MRM",
                DeliverableId = 14031212,
                BudgetTypeName = "Paid Media"
            };
            deliverablesByTOWAndDepartment.Add(deliverableOnAir);
            deliverablesByTOWAndDepartment.Add(deliverableOffAir);
            deliverablesByTOWAndDepartment.Add(deliverablePaidMedia);
            #endregion

            #region Mocking
            mockdeliverableService.Setup(x => x.QDeliverablesByTOWAndDepartment(It.IsAny<int>())).Returns(deliverablesByTOWAndDepartment.AsQueryable());
            //Deliverable Repository Mock
            mockDeliverableRepository.Setup(x => x.QGetDeliverablesByDepartmentAndTypeOfWork(It.IsAny<int>())).Returns(deliverablesByTOWAndDepartment.AsQueryable());

            var deliverableService = new DeliverableServiceMock(deliverableRepository: mockDeliverableRepository.Object);

            //Budget Controller Mock
            var controller = new DeliverableControllerMock(deliverableService: deliverableService);
            #endregion

            #region Service
            var deliverablesResult = deliverableService.QDeliverablesByTOWAndDepartment(typeOfWorkId);
            #endregion

            #region Assertions
            Assert.IsFalse(deliverablesResult == null);
            Assert.IsTrue(deliverablesResult.ElementAt(0).BudgetTypeName == "On Air");
            Assert.IsTrue(deliverablesResult.ElementAt(1).BudgetTypeName == "Off Air");
            Assert.IsTrue(deliverablesResult.ElementAt(2).BudgetTypeName == "Paid Media");
            Assert.IsTrue(deliverablesResult.ElementAt(1).DeliverableId == 14030212); 
            Assert.IsTrue(deliverablesResult.ElementAt(2).DeliverableId == 14031212);
            Assert.IsTrue(deliverablesResult.ElementAt(0).TypeOfWorkId==1);
            #endregion
        }
        
        //MRM-219 - Get Code Invoice Details based on Deliverable Budget Id
        [TestMethod]
        public void GetCodeInvoiceDetailsByDbId_Test()
        {
            #region Data Setup
            DeliverableBudgetSummary deliverableBudgetSummary = new DeliverableBudgetSummary()
            {
                Id=122113, 
                DeliverableId= 140000,
                DeliverableName="Test for 219",
                SAPVendorId= 795,//Keep Me Posted
                WBSElementId=999,
                ProductionMethodTypeId=1100
            };
            DeliverableBudget deliverableBudgetTest = new DeliverableBudget()
            {
                Id = 122113,
                DeliverableBudgetUniqueID = new Guid(),
                PHApplyPercent = 45
            };
            #endregion

            #region Mocking
            mockDeliverableRepository.Setup(x => x.GetCodeInvoiceDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(deliverableBudgetSummary);
            #endregion

            #region Service
            var deliverableServiceMock = new DeliverableServiceMock(deliverableRepository:mockDeliverableRepository.Object);
            DeliverableBudgetSummary result = deliverableServiceMock.GetCodeInvoiceDetails(deliverableBudgetTest.Id, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());
            #endregion

            #region Assert
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.DeliverableId  == deliverableBudgetSummary.DeliverableId);
            Assert.IsTrue(result.DeliverableName == "Test for 219");
            Assert.IsTrue(result.SAPVendorId ==795);
            Assert.IsTrue(result.WBSElementId ==  deliverableBudgetSummary.WBSElementId);
            Assert.IsTrue(result.ProductionMethodTypeId ==  deliverableBudgetSummary.ProductionMethodTypeId);
            #endregion
        }
    }
}
