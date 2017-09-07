using Disney.MRM.DANG.API.Controllers;
using Disney.MRM.DANG.API.Test.MockObject.Controller;
using Disney.MRM.DANG.API.Test.MockObject.Service;
using Disney.MRM.DANG.Core;
using Disney.MRM.DANG.Core.Models.Intergration.ESB;
using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.Model;
using Disney.MRM.DANG.Service.Contracts;
using Disney.MRM.DANG.Service.Implementations;
using Disney.MRM.DANG.ViewModel;
using Kendo.Mvc.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Disney.MRM.DANG.API.Test.Controllers
{
    [TestClass]
    public class BudgetControllerTest
    {
        private Moq.MockRepository mockRepository;
        private Moq.Mock<IBudgetService> mockbudgetService;
        private Moq.Mock<IUnitOfWork> mockunitOfWork;
        private Moq.Mock<IDeliverableService> mockdeliverableService;
        private Moq.Mock<IDeliverableServiceV2> mockdeliverableServiceV2;
        private Moq.Mock<IProductService> mockproductService;
        private Moq.Mock<IPropertyService> mockpropertyService;
        private Moq.Mock<ITypeOfWorkRepository> mocktypeOfWorkRepository;
        private Moq.Mock<IBudgetByCategoryRollupRepository> mockBudgetByCategoryRollupRepository;
        private Moq.Mock<IBudgetByCategoryRepository> mockBudgetByCategoryRepository;
        private Moq.Mock<IUserChannelRepository> mockUserChannelRepository;
        private Moq.Mock<IProductFamilyRepository> mockProductFamilyRepository;
        private Moq.Mock<IDeliverableCategoryRepository> mockdeliverableCategoryRepository;
        private Moq.Mock<IForecastSnapshotBatchRepository> mockForecastSnapshotRepository;
        private Moq.Mock<IWBSElementRepository> mockWBSElementRepository;
        

        [TestInitialize]
        public void ClassInit()
        {
            mockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
            mockbudgetService = mockRepository.Create<IBudgetService>();
            mockunitOfWork = mockRepository.Create<IUnitOfWork>();
            mockdeliverableService = mockRepository.Create<IDeliverableService>();
            mockdeliverableServiceV2 = mockRepository.Create<IDeliverableServiceV2>();
            mockproductService = mockRepository.Create<IProductService>();
            mockpropertyService = mockRepository.Create<IPropertyService>();
            mocktypeOfWorkRepository = mockRepository.Create<ITypeOfWorkRepository>();
            mockBudgetByCategoryRollupRepository = mockRepository.Create<IBudgetByCategoryRollupRepository>();
            mockBudgetByCategoryRepository = mockRepository.Create<IBudgetByCategoryRepository>();
            mockUserChannelRepository = mockRepository.Create<IUserChannelRepository>();
            mockProductFamilyRepository = mockRepository.Create<IProductFamilyRepository>();
            mockdeliverableCategoryRepository = mockRepository.Create<IDeliverableCategoryRepository>();
            mockForecastSnapshotRepository = mockRepository.Create<IForecastSnapshotBatchRepository>();
            mockWBSElementRepository = mockRepository.Create<IWBSElementRepository>();
        }

        [TestMethod]

        public void Filter_BudgetTypeofWork_Tests()
        {
            //Set Data
            #region Data
            int typeOfWorkId = 1;
            int mrmUserId = 25;
            string networkLogin = "abc";
            string FY = "2015";
            string filter = "";

            MRM.DANG.Model.TypeOfWork tow = new MRM.DANG.Model.TypeOfWork
            {
                Id = 1,
                Name = "Test Name",
                FiscalYear = 2015,
                TypeOfWorkCategoryId = 10,
                TypeOfWorkStatus = new TypeOfWorkStatus()
            };
            WBSFiscalYear_Channel wBSFiscalYear_Channel = new WBSFiscalYear_Channel
            {
                WBSNumber = "12345.123.234.001"
            };
            //tow.WBSFiscalYear_Channel = wBSFiscalYear_Channel;
            List<Model.BudgetbyCategoryRollup> rollups = new List<Model.BudgetbyCategoryRollup>();
            Model.BudgetbyCategoryRollup rollupOnAir = new Model.BudgetbyCategoryRollup
            {
                FiscalYear = 2015,
                BudgetTypeName = "On Air"
            };
            Model.BudgetbyCategoryRollup rollupOffAir = new Model.BudgetbyCategoryRollup
            {
                FiscalYear = 2015,
                BudgetTypeName = "Off Air"
            };
            Model.BudgetbyCategoryRollup rollupPaidMedia = new Model.BudgetbyCategoryRollup
            {
                FiscalYear = 2015,
                BudgetTypeName = "Paid Media"
            };
            rollups.Add(rollupOnAir);
            rollups.Add(rollupOffAir);
            rollups.Add(rollupPaidMedia);
            List<CategoryBudgetRollupViewModel> BudgetRollUps = new List<CategoryBudgetRollupViewModel>();
            CategoryBudgetRollupViewModel budgetRollup = new CategoryBudgetRollupViewModel()
            {
                towId = 1
            };
            BudgetRollUps.Add(budgetRollup);
            BudgetTypeOfWorkViewModel model = new BudgetTypeOfWorkViewModel();
            model.BudgetRollups = BudgetRollUps;

            #endregion

            //Mock
            #region  Mock
            //Budget Service Mock
            mockbudgetService.Setup(x => x.BudgetTypeOfWork(It.IsAny<int>())).Returns(new MRM.DANG.Model.TypeOfWork());
            mockbudgetService.Setup(x => x.BudgetsByCategoryRollup(It.IsAny<int>(), It.IsAny<int>())).Returns(rollups);

            //TypeofService Mock
            mocktypeOfWorkRepository.Setup(x => x.GetBudgetTypeOfWorkModel(It.IsAny<int>())).Returns(tow);
            mocktypeOfWorkRepository.Setup(x => x.GetBudgetPreviousFYTypeOfWorkModel(It.IsAny<int>())).Returns(new PreviousFYTOWCustomModel());

            //BudgetByCategoryRollupRepository Service Mock
            mockBudgetByCategoryRollupRepository.Setup(x => x.BudgetByCategoryRollupForTypeOfWork(It.IsAny<int>(), It.IsAny<int>())).Returns(rollups);

            var budgetService = new BudgetServiceMock(unitOfWork: null, budget: null, channel: null, userChannel: null,
            loggerService: null, iTypeOfWork: mocktypeOfWorkRepository.Object, iFamilyProductIssueTowDeliverableRepository: null,
            budgetByChannel: null, iBudgetTypeTOWRepository: null, budgetByCategory: null
            , iBudgetByCategoryRollup: mockBudgetByCategoryRollupRepository.Object, _ITypeOfWork_DeliverableCategory: null,
             _iForecastBudgetTypeCalendarTOWRepository: null, _iCalendarRepository: null,
             iTypeOfWorkCategoryRepository: null, iWBSFiscalYear_ChannelRepository: null, iBudgetTypeRepository: null);

            //Budget Controller Mock
            var BudgetController = new BudgetController(userService: null, loggerService: null, budgetService: budgetService,
            unitOfWork: null, productService: null, deliverableService: null, deliverableServiceV2: null, propertyService: null);


            #endregion

            //Assertions
            #region Assertions
            //Assert BudgetService Method Call
            var towResult = budgetService.BudgetTypeOfWork(typeOfWorkId);

            mockbudgetService.Verify();
            mockBudgetByCategoryRollupRepository.Verify(); //Asserts service calls

            Assert.IsNotNull(tow);//Result is not Null
            Assert.AreEqual(tow, towResult);//Asserting the expected return object with dummy data
            Assert.AreEqual(tow.Id, towResult.Id);//Assert matching the return data with my input

            //Assert BudgetsByCategoryRollup
            var rollupResult = budgetService.BudgetsByCategoryRollup(tow.Id);
            Assert.IsNotNull(rollupResult);
            Assert.AreEqual(rollups, rollupResult);
            Assert.AreEqual(rollups.Count(), rollupResult.Count());
            //TO Do review Assertions  
            //model = BudgetController.BudgetTypeOfWork(typeOfWorkId, FY, filter, mrmUserId, networkLogin);
            //Assert.IsNotNull(model);
            //Assert.AreEqual(model.BudgetRollups.Count(), 4);

            //Assertions After applying filter
            //filter = "On Air" + "|" + "" + "|" + "" + "|" + "false"; //Passing test data to filter for OnAir records
            //model = BudgetController.BudgetTypeOfWork(typeOfWorkId, FY, filter, mrmUserId, networkLogin);
            //Assert.AreEqual(model.BudgetRollups.Count(), 2);
            #endregion
        }

        [TestMethod]
        public void Filter_TowAudit_Tests()
        {
            //Set Data
            #region Data
            int towID = 1;
            int mrmUserId = 25;
            string networkLogin = "abc";
            string FY = "2015";
            string filter = "";

            DataSourceResult result = new DataSourceResult();
            TowAuditTrailExportModel towAuditViewModel = new TowAuditTrailExportModel() { };
            List<TowAuditTrailExportModel> lstAuditModel = new List<TowAuditTrailExportModel>();

            TowAuditTrailExportModel towAuditViewModelOnAir = new TowAuditTrailExportModel
            {
                BudgetType = "On Air"
            };
            TowAuditTrailExportModel towAuditViewModelOffAir = new TowAuditTrailExportModel
            {
                BudgetType = "Off Air"
            };
            TowAuditTrailExportModel towAuditViewModelPaidMedia = new TowAuditTrailExportModel
            {
                BudgetType = "Paid Media"
            };

            lstAuditModel.Add(towAuditViewModelOnAir);
            lstAuditModel.Add(towAuditViewModelOffAir);
            lstAuditModel.Add(towAuditViewModelPaidMedia);
            #endregion

            //Mock
            #region  Mock
            //Type of Service Mock
            mocktypeOfWorkRepository.Setup(x => x.GetTowAuditExport(It.IsAny<int>())).Returns(lstAuditModel.AsQueryable());

            //Budget Service instance
            var budgetService = new BudgetServiceMock(unitOfWork: null, budget: null, channel: null, userChannel: null,
          loggerService: null, iTypeOfWork: mocktypeOfWorkRepository.Object, iFamilyProductIssueTowDeliverableRepository: null,
            budgetByChannel: null, iBudgetTypeTOWRepository: null, budgetByCategory: null
            , iBudgetByCategoryRollup: null, _ITypeOfWork_DeliverableCategory: null,
             _iForecastBudgetTypeCalendarTOWRepository: null, _iCalendarRepository: null,
             iTypeOfWorkCategoryRepository: null, iWBSFiscalYear_ChannelRepository: null, iBudgetTypeRepository: null);

            //Budget Controller Mock
            var BudgetController = new BudgetController(userService: null, loggerService: null, budgetService: budgetService,
            unitOfWork: null, productService: null, deliverableService: null, deliverableServiceV2: null, propertyService: null);

            #endregion

            //Assertions
            #region Assertions
            //Assert BudgetService Method Call
            var towAuditResults = budgetService.GetTowAuditExport(towID);

            mockbudgetService.Verify();
            Assert.IsNotNull(towAuditResults);//Result is not Null
            Assert.AreEqual(towAuditResults.Count(), 3);

            var filteredModel = towAuditResults.Where(x => x.BudgetType == "On Air");
            Assert.IsNotNull(towAuditResults);//Result is not Null
            Assert.AreEqual(filteredModel.Count(), 1);

            //Assertion of Controller method
            filter = "" + "|" + "Off Air" + "|" + "" + "|" + "false"; //Passing test data to filter for OnAir records
            var offAirResult = BudgetController.GetTowAuditKendo(towID, filter, mrmUserId, networkLogin, "");
            Assert.IsNotNull(offAirResult.Data);
            Assert.AreEqual(offAirResult.Total, 1);

            filter = "";
            var noFilterResult = BudgetController.GetTowAuditKendo(towID, filter, mrmUserId, networkLogin, "");
            Assert.IsNotNull(noFilterResult.Data);
            Assert.AreEqual(noFilterResult.Total, 3);

            #endregion
        }

        #region MRM-65( Get all External Type of Work Details)
        [TestMethod]
        public void GetExternalTypeofWorks()
        {
            //Set Data
            #region Data

            DeliverableBudgetSummary ds1 = new DeliverableBudgetSummary
            {
                TypeOfWorkId = 1,
                TypeOfWorkName = "RDMA",
                TypeOfWorkCategoryId = 1,
                TypeOfWorkCategoryName = "External",
                MarketingGroupChannelId = 1,
                FiscalYear = "2016",
                EstimateCompleteAmount = 11700,
                FullWBSNumber = "001"
            };

            DeliverableBudgetSummary ds2 = new DeliverableBudgetSummary
            {
                TypeOfWorkId = 2,
                TypeOfWorkName = "NBT",
                TypeOfWorkCategoryId = 1,
                TypeOfWorkCategoryName = "External",
                MarketingGroupChannelId = 1,
                FiscalYear = "2016",
                EstimateCompleteAmount = 11323,
                FullWBSNumber = "002"
            };

            DeliverableBudgetSummary ds3 = new DeliverableBudgetSummary
            {
                TypeOfWorkId = 3,
                TypeOfWorkName = "RD Insider",
                TypeOfWorkCategoryId = 1,
                TypeOfWorkCategoryName = "External",
                MarketingGroupChannelId = 2,
                FiscalYear = "2015",
                EstimateCompleteAmount = 12363,
                FullWBSNumber = "003"

            };
            DeliverableBudgetSummary ds4 = new DeliverableBudgetSummary
            {
                TypeOfWorkId = 4,
                TypeOfWorkName = "RD Birthday",
                TypeOfWorkCategoryId = 1,
                TypeOfWorkCategoryName = "External",
                MarketingGroupChannelId = 2,
                FiscalYear = "2015",
                EstimateCompleteAmount = 165789
                ,
                FullWBSNumber = "004"
            };
            List<DeliverableBudgetSummary> dbsList = new List<DeliverableBudgetSummary>();
            dbsList.Add(ds1); dbsList.Add(ds2); dbsList.Add(ds3); dbsList.Add(ds4);
            #endregion

            //Mock
            #region  Mock
            //Type of Service Mock
            mocktypeOfWorkRepository.Setup(x => x.GetExternalTypeofWorks(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((ch, fy) =>
                (ch == 0) ? dbsList.Where(p => p.FiscalYear == fy.ToString()).ToList() :
                dbsList.Where(p => p.FiscalYear == fy.ToString() && p.MarketingGroupChannelId.Value == ch).ToList()
                );
            var mockbudgetService = new BudgetServiceMock(iTypeOfWork: mocktypeOfWorkRepository.Object);
            //Budget Controller Mock
            var BudgetController = new BudgetControllerMock(budgetservice: mockbudgetService);

            #endregion

            //Assertions
            #region Assertions

            int channelId = 0;
            int FiscalYear = 2016;
            string LoginUser = @"swna\TestLogin";

            //Get All Records when channelId=0 and current Year(2016)
            var result = BudgetController.GetExternalTypeofWorks(channelId, FiscalYear, LoginUser);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<TypeOfWorkViewModel>));
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.All(p => FiscalYear == 2016)); // all records should be fiscal year as 2016

            //filter by ChannelId and Fiscalyear
            channelId = 2; FiscalYear = 2015;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<TypeOfWorkViewModel>));
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.All(p => FiscalYear == 2015)); // all records should be fiscalyear:2015
            Assert.IsTrue(result.All(p => channelId == 2)); // all records should be with channel=2


            #endregion
        }
        #endregion

        #region MRM-148
        [TestMethod]
        public void Get_TypeOfWork_ViewModel_Tests()
        {
            #region Data
            int typeOfWorkId = 1234;
            int mrmUserId = 25;
            string networkLogin = "abc";
            string Company = "Company 1";
            string Business = "Business 1";
            
            var flashSnapshot = new List<FlashSnapshotModel>();
            flashSnapshot.Add(new FlashSnapshotModel()
            {
                ChannelId = 0,
                SnapshotType = 0,
                BudgetId = 0,
                Name = "Snapshot 1",
                Year = "2016",
                Quarter = " "


            });
            var forecasts = new List<TypeOfWorkForecastModel>();
            forecasts.Add(new TypeOfWorkForecastModel()
            {

                Months = new List<ForecastAmountModel>(),
                Quarters = new List<QuartersAmountModel>()
            });
            var tow = new MRM.DANG.Model.TypeOfWork
            {
                Id = typeOfWorkId,
                Name = "Test Name",
                FiscalYear = 2015,
                TypeOfWorkCategoryId = 10
            };
            WBSFiscalYear_Channel wBSFiscalYear_Channel = new WBSFiscalYear_Channel
            {
                WBSNumber = "12345.123.234.001"
            };
            //tow.WBSFiscalYear_Channel = wBSFiscalYear_Channel;

            TypeOfWorkModel tOfVm = new TypeOfWorkModel();
            tOfVm.ExternalWBS = null;
            tOfVm.Company = Company;
            tOfVm.Business = Business;
            tOfVm.Id = typeOfWorkId;
            tOfVm.TypeOfWorkForecasts = forecasts;
            #endregion

            #region  Mock
            mockbudgetService.Setup(x => x.BudgetTypeOfWork(It.IsAny<int>())).Returns(new MRM.DANG.Model.TypeOfWork());
            mockbudgetService.Setup(x => x.BudgetsByCategoryRollup(It.IsAny<int>(), It.IsAny<int>())).Returns(new List<BudgetbyCategoryRollup>());
            mockbudgetService.Setup(x => x.GetFiscalYearByIdAndChannel(It.IsAny<int>(), It.IsAny<int>())).Returns("");
            mockbudgetService.Setup(x => x.BudgetTowFlashSnapshot(It.IsAny<int>())).Returns(new List<FlashSnapshotModel>());
            //TypeofService Mock
            mocktypeOfWorkRepository.Setup(x => x.GetBudgetTypeOfWorkModel(It.IsAny<int>())).Returns(tow);
            mocktypeOfWorkRepository.Setup(x => x.GetFiscalYearByIdAndChannel(It.IsAny<int>(), It.IsAny<int>())).Returns(new WBSFiscalYear_Channel() { FiscalYear = "2015" });
            mocktypeOfWorkRepository.Setup(x => x.GetRatingList()).Returns(new List<Rating>());
            mocktypeOfWorkRepository.Setup(x => x.GetTypeOfWorkCategories()).Returns(new List<TypeOfWorkCategory>());
            mocktypeOfWorkRepository.Setup(x => x.GetTypeOfWorkStatusList()).Returns(new List<TypeOfWorkStatus>());
            mocktypeOfWorkRepository.Setup(x => x.GetAllFiscalYearList()).Returns(new List<WBSFiscalYear_Channel>());
            mocktypeOfWorkRepository.Setup(x => x.GetBudgetTowFlashSnapshot(It.IsAny<int>())).Returns(flashSnapshot);

            //BudgetByCategoryRollupRepository Service Mock
            mockBudgetByCategoryRollupRepository.Setup(x => x.BudgetByCategoryRollupForTypeOfWork(It.IsAny<int>(), It.IsAny<int>())).Returns(new List<BudgetbyCategoryRollup>());

            var userChannels = new List<MRMUser_Channel>();
            userChannels.Add(new MRMUser_Channel() { ChannelId = 1, Channel = new Channel() { Name = "Channel 1" } });
            userChannels.Add(new MRMUser_Channel() { ChannelId = 2, Channel = new Channel() { Name = "Channel 2" } });

            mocktypeOfWorkRepository.Setup(x => x.GetTypeOfWorkModel(It.IsAny<int>())).Returns(tOfVm);
            mockUserChannelRepository.Setup(x => x.UserChannels(It.IsAny<int>())).Returns(userChannels);

            var budgetService = new BudgetServiceMock(userChannel: mockUserChannelRepository.Object, iTypeOfWork: mocktypeOfWorkRepository.Object,
            iBudgetByCategoryRollup: mockBudgetByCategoryRollupRepository.Object);


            //Product Service Mock
            mockProductFamilyRepository.Setup(x => x.GetAll()).Returns(new List<ProductFamily>().AsQueryable<ProductFamily>());
            mockproductService.Setup(x => x.GetProductFamilies()).Returns(new List<ProductFamily>());

            var productService = new ProductServiceMock(productFamilyRepository: mockProductFamilyRepository.Object);

            //Deliverable Service Mock  
            mockdeliverableCategoryRepository.Setup(x => x.GetAll()).Returns(new List<DeliverableCategory>().AsQueryable<DeliverableCategory>());
            mockdeliverableService.Setup(x => x.GetAllDeliverableCategories()).Returns(new List<DeliverableCategory>());

            var deliverableService = new DeliverableServiceMock(deliverableCategoryRepository: mockdeliverableCategoryRepository.Object);

            var BudgetController = new BudgetController(userService: null, loggerService: null, budgetService: budgetService,
            unitOfWork: null, productService: productService, deliverableService: deliverableService, deliverableServiceV2: null, propertyService: null);
            #endregion

            #region Assertions
            mockbudgetService.Verify();
            mockproductService.Verify();
            mockdeliverableService.Verify();

            //Verify TOW ViewModel
            var towModel = budgetService.GetTypeOfWork(typeOfWorkId);
            Assert.IsNotNull(towModel);
            Assert.AreEqual(Company, towModel.Company);
            Assert.AreEqual(Business, towModel.Business);

            var towtOfVm = BudgetController.GetTypeOfWork(typeOfWorkId, mrmUserId, networkLogin);
            Assert.IsNotNull(towtOfVm);
            Assert.AreEqual(Company, towtOfVm.Company);
            Assert.AreEqual(Business, towtOfVm.Business);

            //Check Channels

            Assert.AreEqual(towtOfVm.ChannelList.Count(), 2);

            //Check Channel 1
            var channelListArray = towtOfVm.ChannelList.ToArray<SelectListItem>();
            Assert.AreEqual("1", channelListArray[0].Value);
            Assert.AreEqual("Channel 1", channelListArray[0].Text);

            //Check Channel 2
            Assert.AreEqual("2", channelListArray[1].Value);
            Assert.AreEqual("Channel 2", channelListArray[1].Text);
            // Check FlashSnapshot
            Assert.AreEqual(1, towtOfVm.FlashSnapshot.Count());
            Assert.AreEqual("Snapshot 1", towtOfVm.FlashSnapshot[0].Name);
            Assert.AreEqual("2016", towtOfVm.FlashSnapshot[0].Year);
            Assert.AreEqual(0, towtOfVm.FlashSnapshot[0].SnapshotType);
            #endregion
        }
        #endregion

        #region MRM-128
        [TestMethod]
        public void GetBudgetsByCategorySummary_Tests()
        {
            #region Data
            int channelid = 1, mrmUserId = 25; string fy = "2016", budgettype = "On Air,Off Air,Paid Media", networkLogin = "abc";
            string[] total = budgettype.Split(',').ToArray();
            BudgetByCategorySumViewModel summarylist1 = new BudgetByCategorySumViewModel
            {
                Category = "ABC",
                ActualAmount = 5256,
                CommittedAmount = 3000,
                AllocatedBudgetAmount = 2500,
                BudgetAmount = 4000,
                EstimateToComplete = 4500,
                EstimatedFinalCost = 5000,
                OverUnder = 50

            };
            BudgetByCategorySumViewModel summarylist2 = new BudgetByCategorySumViewModel
            {
                Category = "Aquired Movies",
                ActualAmount = 6456,
                CommittedAmount = 4000,
                AllocatedBudgetAmount = 3500,
                BudgetAmount = 5000,
                EstimateToComplete = 5500,
                EstimatedFinalCost = 4000,
                OverUnder = 100

            };

            List<BudgetbyCategoryRollup> rollups = new List<BudgetbyCategoryRollup>();
            rollups.Add(new BudgetbyCategoryRollup { ActualAmount = 5254, CategoryName = "ABC", CategoryId = 1, BudgetTypeName = "On Air" });
            rollups.Add(new BudgetbyCategoryRollup { ActualAmount = 4250, CategoryName = "Aquired Movies", CategoryId = 2, BudgetTypeName = "Off Air" });
            rollups.Add(new BudgetbyCategoryRollup { ActualAmount = 6250, CategoryName = "Aquired Series", CategoryId = 3, BudgetTypeName = "On Air" });
            rollups.Add(new BudgetbyCategoryRollup { ActualAmount = 7285, CategoryName = "Ad Sales", CategoryId = 4, BudgetTypeName = "Paid Media" });
            List<TypeOfWorkBudgetModel> BudgetTypes = new List<TypeOfWorkBudgetModel>();
            BudgetTypes.Add(new TypeOfWorkBudgetModel { BudgetTypeId = 1, BudgetTypeName = "On Air" });
            BudgetTypes.Add(new TypeOfWorkBudgetModel { BudgetTypeId = 2, BudgetTypeName = "Off Air" });
            BudgetTypes.Add(new TypeOfWorkBudgetModel { BudgetTypeId = 3, BudgetTypeName = "Paid Media" });
            BudgetTypes.Add(new TypeOfWorkBudgetModel { BudgetTypeId = 4, BudgetTypeName = "Brain Design" });
            List<BudgetByCategorySumViewModel> categorysummary = new List<BudgetByCategorySumViewModel>();
            categorysummary.Add(summarylist1);
            categorysummary.Add(summarylist2);
            List<BudgetbyCategoryRollup> Categoryrollup = new List<BudgetbyCategoryRollup>();
            Categoryrollup.Add(new BudgetbyCategoryRollup { ActualAmount = 5241, BudgetAmount = 450, BudgetTypeId = 1, BudgetTypeName = "On Air", CategoryId = 1, CategoryName = "ABC" });
            Categoryrollup.Add(new BudgetbyCategoryRollup { ActualAmount = 3250, BudgetAmount = 350, BudgetTypeId = 1, BudgetTypeName = "On Air", CategoryId = 2, CategoryName = "XYZ" });
            Categoryrollup.Add(new BudgetbyCategoryRollup { ActualAmount = 4250, BudgetAmount = 300, BudgetTypeId = 2, BudgetTypeName = "Off Air", CategoryId = 3, CategoryName = "PQR" });
            Categoryrollup.Add(new BudgetbyCategoryRollup { ActualAmount = 3525, BudgetAmount = 200, BudgetTypeId = 3, BudgetTypeName = "Paid Media", CategoryId = 4, CategoryName = "MNO" });
            IQueryable<BudgetbyCategoryRollup> rollupslist = Categoryrollup.AsQueryable();
            #endregion
            #region Mock            
            mockbudgetService.Setup(x => x.GetBudgetsByCategory(It.IsAny<int>(), It.IsAny<string>())).Returns(rollups);
            mockbudgetService.Setup(x => x.GetTypeOfWorkBudgets()).Returns(BudgetTypes);
            mockBudgetByCategoryRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<BudgetbyCategoryRollup, bool>>>())).Returns(rollupslist);
            mocktypeOfWorkRepository.Setup(x => x.GetTypeOfWorkBudgets()).Returns(BudgetTypes);

            //Budget Service Mock
            var budgetService = new BudgetServiceMock(iTypeOfWork: mocktypeOfWorkRepository.Object, budgetByCategory: mockBudgetByCategoryRepository.Object);
            var BudgetController = new BudgetControllerMock(budgetservice: budgetService);

            #endregion
            //Assertions
            #region Assertions
            Assert.IsNotNull(BudgetTypes);
            Assert.IsTrue(BudgetTypes.Count == 4);
            Assert.IsTrue(BudgetTypes.ElementAt(0).BudgetTypeId == 1);
            Assert.IsTrue(BudgetTypes.ElementAt(1).BudgetTypeName == "Off Air");
            Assert.IsTrue(BudgetTypes.ElementAt(2).BudgetTypeId == 3);

            Assert.IsNotNull(categorysummary);
            var rollupsresult = budgetService.GetBudgetsByCategory(channelid, fy);
            Assert.IsNotNull(rollupsresult);

            mockbudgetService.Verify();
            mockBudgetByCategoryRepository.Verify();

            rollupsresult = rollupsresult.Where(p => total.Contains(p.BudgetTypeName)).ToList();
            Assert.IsNotNull(rollupsresult);

            categorysummary = BudgetController.GetBudgetsByCategorySummary(1, budgettype, fy, mrmUserId, networkLogin);
            Assert.IsNotNull(categorysummary);

            categorysummary = BudgetController.GetBudgetsByCategorySummary(1, budgettype, fy, mrmUserId, networkLogin);
            Assert.IsNotNull(categorysummary);


            #endregion
        }
        #endregion

        #region MRM-650
        [TestMethod]
        public void GetTowFiscalYearList_Test()
        {
            #region Data            
            int mrmUserId = 25;
            string networkLogin = "abc";            

            var towList = new List<Model.TypeOfWork>();
            towList.Add(new Model.TypeOfWork {
                Id = 1,
                ChannelId = 1,
                FiscalYear = 2014,
                Description = "Tow 1",
                Name = "Tow 1"
            });

            towList.Add(new Model.TypeOfWork
            {
                Id = 2,
                ChannelId = 1,
                FiscalYear = 2014,
                Description = "Tow 2",
                Name = "Tow 2"
            });

            towList.Add(new Model.TypeOfWork
            {
                Id = 3,
                ChannelId = 1,
                FiscalYear = 2015,
                Description = "Tow 3",
                Name = "Tow 3"
            });

            towList.Add(new Model.TypeOfWork
            {
                Id = 4,
                ChannelId = 2,
                FiscalYear = 2015,
                Description = "Tow 4",
                Name = "Tow 4"
            });

            towList.Add(new Model.TypeOfWork
            {
                Id = 5,
                ChannelId = 2,
                FiscalYear = 2016,
                Description = "Tow 5",
                Name = "Tow 5"
            });

            towList.Add(new Model.TypeOfWork
            {
                Id = 6,
                ChannelId = 3,
                FiscalYear = 2016,
                Description = "Tow 6",
                Name = "Tow 6"
            });
            
            towList.Add(new Model.TypeOfWork
            {
                Id = 8,
                ChannelId = 3,
                FiscalYear = 0,
                Description = "Tow 8",
                Name = "Tow 8"
            });                       

            #endregion

            #region  Mock

            mocktypeOfWorkRepository.Setup(x => x.GetFiscalYearList(It.IsAny<bool>())).Returns(towList);            

            var budgetService = new BudgetServiceMock(userChannel: mockUserChannelRepository.Object, iTypeOfWork: mocktypeOfWorkRepository.Object,
            iBudgetByCategoryRollup: mockBudgetByCategoryRollupRepository.Object);
            
            var budgetController = new BudgetController(userService: null, loggerService: null, budgetService: budgetService,
            unitOfWork: null, productService: null, deliverableService: null, deliverableServiceV2: null, propertyService: null);
            #endregion

            #region Assertions
            mockbudgetService.Verify();
            
            var result = budgetController.GetTowFiscalYearList(mrmUserId,false, networkLogin);
            Assert.IsTrue(result.Count() == 3);
            Assert.IsFalse(result.Contains(new SelectListItem {Text="0",Value="0" }));
            
            #endregion
        }
        #endregion

        #region MRM-158
        [TestMethod]
        public void GetAllWBSElementHistory_Test()
        {
            #region Data            
            hWBSElementModel hWBSElementModel1 = new hWBSElementModel()
            {
                FullWBSNumber="1234567.001.001",
                BudgetAmount=500,
                Comments="Test1",
                HistoryInsert=DateTime.Now,
                LastAction="Reflow Audit",
                Q1ForecastAmount=0,
                Q2ForecastAmount =0,
                Q3ForecastAmount =20,
                Q4ForecastAmount =60,
                EstimateCompleteAmount=450,
                TypeOfWorkName="Reflow 1"
            };
            hWBSElementModel hWBSElementModel2 = new hWBSElementModel()
            {
                FullWBSNumber = "1234567.001.002",
                BudgetAmount = 700,
                Comments = "Test2",
                HistoryInsert = DateTime.UtcNow,
                LastAction = "Edit Type Of Work",
                Q1ForecastAmount = 0,
                Q2ForecastAmount = 0,
                Q3ForecastAmount = 40,
                Q4ForecastAmount = 900,
                EstimateCompleteAmount = 730,
                TypeOfWorkName = "TOW 1"
            };
            List<hWBSElementModel> hWBSElementModelList = new List<hWBSElementModel>();
            hWBSElementModelList.Add(hWBSElementModel1);
            hWBSElementModelList.Add(hWBSElementModel2);
            #endregion

            #region  Mock
            mockWBSElementRepository.Setup(x => x.GetAllWBSElementHistory(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(hWBSElementModelList);
            #endregion

            #region Service
            var budgetService = new BudgetServiceMock(iBudgetTypeTOWRepository: mockWBSElementRepository.Object);
            #endregion

            #region Controller
            var budgetController = new BudgetControllerMock(budgetservice: budgetService);
            #endregion

            #region Result
            var result = budgetController.GetAllWBSElementHistory(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());
            #endregion

            #region Assertions
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.ElementAt(0).BudgetAmount == 500);
            Assert.IsTrue(result.ElementAt(1).TypeOfWorkName =="TOW 1");
            Assert.IsTrue(result.ElementAt(0).Q3ForecastAmount ==20);
            Assert.IsTrue(result.ElementAt(1).Q4ForecastAmount ==900);
            Assert.IsTrue(result.ElementAt(0).EstimateCompleteAmount ==450);
            Assert.IsTrue(result.ElementAt(1).LastAction == "Edit Type Of Work");
            Assert.IsTrue(result.ElementAt(0).LastAction == "Reflow Audit");
            Assert.IsTrue(result.ElementAt(1).FullWBSNumber == "1234567.001.002");
            #endregion
        }
        #endregion

        #region MRM-1712
        [TestMethod]
        public void GetWBSByBudgetGroupFyNTow_ShouldReturnOneRecord()
        {
            #region Data Setup
            int fiscalYear = 2016;

            Disney.MRM.DANG.Model.TypeOfWork towList1 = new Disney.MRM.DANG.Model.TypeOfWork()
            {
                Id = 5001,
                FiscalYear = fiscalYear,
                TypeOfWorkCategoryId = 999,
                Name = "N1",
                ChannelId = 1
            };
            Disney.MRM.DANG.Model.TypeOfWork towList2 = new Disney.MRM.DANG.Model.TypeOfWork()
            {
                Id = 5002,
                FiscalYear = fiscalYear,
                TypeOfWorkCategoryId = 888,
                Name = "N2",
                ChannelId = 2
            };
            Disney.MRM.DANG.Model.TypeOfWork towList3 = new Disney.MRM.DANG.Model.TypeOfWork()
            {
                Id = 5003,
                FiscalYear = fiscalYear,
                TypeOfWorkCategoryId = 777,
                Name = "N3",
                ChannelId = 2
            };
            Disney.MRM.DANG.Model.TypeOfWork testList = new Disney.MRM.DANG.Model.TypeOfWork()
            {
                Id = 5003,
                FiscalYear = 2017,
                TypeOfWorkCategoryId = 777,
                Name = "N3",
                ChannelId = 2
            };

            List<WBSElement> wbsElementList = new List<WBSElement>();

            WBSElement wbsElement1 = new WBSElement()
            {
               Id = 10,
               TypeOfWork = towList1,
               ExternalWBSFlag = true,
               FullWBSNumber = "123",
               BudgetTypeId = 2,
               TypeOfWorkId = 3
            };

            WBSElement wbsElement2 = new WBSElement()
            {
                Id = 20,
                TypeOfWork = towList1,
                ExternalWBSFlag = false,
                FullWBSNumber = "456",
                BudgetTypeId = 1,
                TypeOfWorkId = 4 
            };

            WBSElement wbsElement3 = new WBSElement()
            {
                Id = 30,
                TypeOfWork = towList1,
                ExternalWBSFlag = false,
                FullWBSNumber = "789",
                BudgetTypeId = 2,
                TypeOfWorkId = 5
            }; 


            wbsElementList.Add(wbsElement1);
            wbsElementList.Add(wbsElement2);
            wbsElementList.Add(wbsElement3);
            #endregion

            #region  Mock
            mockWBSElementRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<WBSElement, bool>>>())).Returns(wbsElementList);
            #endregion

            #region Service
            var budgetService = new BudgetServiceMock(iBudgetTypeTOWRepository: mockWBSElementRepository.Object);
            var result = budgetService.GetWBSByBudgetGroupFyNTow(false, 1, 2016, 4, 1);
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.ElementAt(0).Text.Equals("456"));
            #endregion
        }
        #endregion
    }
}
