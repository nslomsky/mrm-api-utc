using Disney.MRM.DANG.API.Controllers;
using Disney.MRM.DANG.Core.Contracts;
using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.Model;
using Disney.MRM.DANG.Service.Contracts;
using Disney.MRM.DANG.Service.Implementations;
using Disney.MRM.DANG.ViewModel.Deliverable;
using Disney.MRM.DANG.API.Test.MockObject.Service;
using Disney.MRM.DANG.API.Test.MockObject.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Disney.MRM.DANG.ViewModel;

namespace Disney.MRM.DANG.API.Test.Controllers
{
    [TestClass]
    public class DropDownApiControllerTest
    {
        #region Constants
        const string NETWORK_LOGIN = "swna\testlogin";
        #endregion
        private Moq.MockRepository mockRepository;
        private Moq.Mock<IUserService> mockUserService;
        private Moq.Mock<ILogService> mockLoggerService;
        private Moq.Mock<IDropDownListService> mockDropDownListService;
        private Moq.Mock<IChannelDeliverableGroupRepository> mockChannelDeliverableGroupRepository;
        private Moq.Mock<IWBSFiscalYear_ChannelRepository> mockWBSFiscalYear_ChannelRepository;
        private Moq.Mock<ITypeOfWorkRepository> mockTypeOfWorkRepository;
        private Moq.Mock<IDropDownListRepository> mockDropDownListRepository;
        private Moq.Mock<IWBSElementRepository> mockWBSElementRepository;
        private Moq.Mock<IBusinessAreaRepository> mockBusinessAreaRepository;
        private Moq.Mock<IProductionMethodTypeRepository> mockProductionMethodTypeRepository;
        private Moq.Mock<IVendorRepository> mockVendorRepository;
        private Moq.Mock<IHomeService> mockHomeService;

        [TestInitialize]
        public void ClassInit()
        {
            mockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
            mockUserService = mockRepository.Create<IUserService>();
            mockLoggerService = mockRepository.Create<ILogService>();
            mockDropDownListService = mockRepository.Create<IDropDownListService>();
            mockChannelDeliverableGroupRepository = mockRepository.Create<IChannelDeliverableGroupRepository>();
            mockWBSFiscalYear_ChannelRepository = mockRepository.Create<IWBSFiscalYear_ChannelRepository>();
            mockTypeOfWorkRepository = mockRepository.Create<ITypeOfWorkRepository>();
            mockDropDownListRepository = mockRepository.Create<IDropDownListRepository>();
            mockWBSElementRepository = mockRepository.Create<IWBSElementRepository>();
            mockBusinessAreaRepository = mockRepository.Create<IBusinessAreaRepository>();
            mockProductionMethodTypeRepository = mockRepository.Create<IProductionMethodTypeRepository>();
            mockVendorRepository = mockRepository.Create<IVendorRepository>();
            mockHomeService = mockRepository.Create<IHomeService>();
        }

        [TestMethod]
        public void GetTowsByMarketingGroupIdFiscalYearAndTowCategoryId_ShouldSucceedWhenTOWCategoryNull()
        {
            #region Data Setup

            //Arrange
            int? testFiscalYear = 2016;
            int? testMarketingGroupId = 8; // OffAirDesign
            int? testTowCategoryId = null; // MRM-309 removes towCategory parameter


            TypeOfWork tow = new TypeOfWork
            {
                TypeOfWorkStatus = new TypeOfWorkStatus { Code = Core.Constants.TypeOfWorkStatus.InProcess },
                TypeOfWorkStatusId = 2, // InProcess
                ChannelId = 8,
                Channel = new Channel { Id = 8 },
                FiscalYear = 2016,
                Id = 5588,
                Name = "DCWW",
                TypeOfWorkCategoryId = 35
            };

            List<TypeOfWork> listTOW = new List<TypeOfWork>();
            listTOW.Add(tow);

            WBSFiscalYear_Channel testWBSFiscalYear_Channel = new WBSFiscalYear_Channel
            {
                ChannelId = 8,
                FiscalYear = "2016",
                WBSNumber = "114764",
                Channel = new Channel { Name = "Off Air Design" }
            };

            #endregion

            #region Mocking

            mockWBSFiscalYear_ChannelRepository.Setup(wbsFY => wbsFY.GetSingle(It.IsAny<Expression<Func<WBSFiscalYear_Channel, bool>>>()))
                                               .Returns(testWBSFiscalYear_Channel);

            mockTypeOfWorkRepository.Setup(tows => tows.GetAll())
                                    .Returns(listTOW.AsQueryable());

            var dropDownListService = new DropDownListServiceMock(typeOfWorkRepository: mockTypeOfWorkRepository.Object,
                                                                  wBSFiscalYear_ChannelRepository: mockWBSFiscalYear_ChannelRepository.Object);
            #endregion

            //Act
            var result = dropDownListService.GetTowsByMarketingGroupIdFiscalYearAndTowCategoryId(testMarketingGroupId,
                                                                                                 testFiscalYear,
                                                                                                 testTowCategoryId).FirstOrDefault();

            //Assert
            Assert.IsTrue(result != null &&
                          result.TypeOfWorkStatus.Code != Core.Constants.TypeOfWorkStatus.Draft &&
                          result.TypeOfWorkStatusId == tow.TypeOfWorkStatusId &&
                          result.Id == tow.Id &&
                          result.TypeOfWorkCategoryId == tow.TypeOfWorkCategoryId &&
                          result.ChannelId == tow.ChannelId &&
                          result.Name == tow.Name);
        }

        //MRM-266 : Get Channel_Deliverable data for default values. 
        [TestMethod]
        public void getChannelDeliverableGroupsSuccessTest()
        {
            Channel_DeliverableGroupViewModel channel_DeliverableGroupViewModelData = new Channel_DeliverableGroupViewModel()
            {
                ChannelId = 1,
                DeliverableGroupId = 5,
                DefaultGLAccountId = 10,
                DefaultProfitCenterId = 15,
                DefaultCostCenterId = 20
            };

            Channel_DeliverableGroupViewModel channel_DeliverableGroupViewModelTestData = new Channel_DeliverableGroupViewModel()
            {
                ChannelId = 1,
                DeliverableGroupId = 3,
                DefaultGLAccountId = 10,
                DefaultProfitCenterId = 15,
                DefaultCostCenterId = 20
            };

            Channel_DeliverableGroup channel_DeliverableGroup1 = new Channel_DeliverableGroup()
            {
                ChannelId = 1,
                DeliverableGroupId = 5,
                IsOffChannelFlag = false,
                DefaultGLAccountId = 10,
                DefaultProfitCenterId = 15,
                DefaultCostCenterId = 20
            };

            Channel_DeliverableGroup channel_DeliverableGroup2 = new Channel_DeliverableGroup()
            {
                ChannelId = 2,
                DeliverableGroupId = 2,
                IsOffChannelFlag = true,
                DefaultGLAccountId = 10,
                DefaultProfitCenterId = 15,
                DefaultCostCenterId = 20
            };

            Channel_DeliverableGroup channel_DeliverableGroup3 = new Channel_DeliverableGroup()
            {
                ChannelId = 3,
                DeliverableGroupId = 7,
                DefaultGLAccountId = 10,
                DefaultProfitCenterId = 15,
                DefaultCostCenterId = 20
            };

            List<Channel_DeliverableGroup> channel_DeliverableGroupList = new List<Channel_DeliverableGroup>();
            channel_DeliverableGroupList.Add(channel_DeliverableGroup1);
            channel_DeliverableGroupList.Add(channel_DeliverableGroup2);
            channel_DeliverableGroupList.Add(channel_DeliverableGroup3);

            mockDropDownListService.Setup(a => a.GetChannelDeliverableGroups(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>()))
            .Returns(channel_DeliverableGroup1);

            mockChannelDeliverableGroupRepository.Setup(a => a.GetMany(It.IsAny<Expression<Func<Channel_DeliverableGroup, bool>>>()))
                .Returns(channel_DeliverableGroupList);


            var dropDownListService = new DropDownListServiceMock(ChannelDeliverableGroupRepository: mockChannelDeliverableGroupRepository.Object);


            var controller = new DropDownApiController(mockUserService.Object, mockLoggerService.Object, dropDownListService: mockDropDownListService.Object, homeService: mockHomeService.Object);
            var result = controller.GetChannelDeliverableGroups(1, 5, false, "test");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ChannelId == 1);
            Assert.IsTrue(result.DeliverableGroupId == 5);

            var result1 = controller.GetChannelDeliverableGroups(2, 2, true, "test");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ChannelId == 1);
            Assert.IsTrue(result.DeliverableGroupId == 5);



        }

        //MRM-165 - Get Type Of Work list
        [TestMethod]
        public void GetTowsByMarketingGroupIdAndCategoryId_GetListOfTOWTest()
        {
            #region Data Setup
            TypeOfWorkStatus typeOfWorkStatus = new TypeOfWorkStatus()
            {
                Id = 1,
                Code = "INPCC"
            };
            TypeOfWork towList1 = new TypeOfWork()
            {
                Id = 1,
                FiscalYear = 2016,
                TypeOfWorkCategoryId = 999,
                ChannelId = 1,
                //WBSFiscalYearChannelId = 2016,
                Name = "N1",
            };
            TypeOfWork towList2 = new TypeOfWork()
            {
                Id = 2,
                FiscalYear = 2016,
                TypeOfWorkCategoryId = 888,
                ChannelId = 1,
                //WBSFiscalYearChannelId = 2016,
                Name = "N2"
            };
            TypeOfWork towList3 = new TypeOfWork()
            {
                Id = 3,
                FiscalYear = 2016,
                TypeOfWorkCategoryId = 777,
                ChannelId = 1,
                //WBSFiscalYearChannelId = 2016,
                Name = "N3"
            };
            TypeOfWork testList = new TypeOfWork()
            {
                Id = 3,
                FiscalYear = 2016,
                TypeOfWorkCategoryId = 777,
                ChannelId = 1,
                //WBSFiscalYearChannelId = 2016,
                Name = "N3"
            };
            //Empty Type Of Work List
            TypeOfWork testList_Empty = new TypeOfWork() { };
            List<TypeOfWork> typeOfWorkData = new List<TypeOfWork>();
            typeOfWorkData.Add(towList1);
            typeOfWorkData.Add(towList2);
            typeOfWorkData.Add(towList3);
            List<TypeOfWork> typeOfWorkTestData = new List<TypeOfWork>();
            typeOfWorkTestData.Add(testList);
            IQueryable<TypeOfWork> typeOfWorkDataQueryable = typeOfWorkData.AsQueryable();
            towList3.TypeOfWorkStatus = typeOfWorkStatus;
            towList2.TypeOfWorkStatus = typeOfWorkStatus;
            towList1.TypeOfWorkStatus = typeOfWorkStatus;
            WBSFiscalYear_Channel wbsFiscalYear_Channel = new WBSFiscalYear_Channel();
            wbsFiscalYear_Channel.Id = 2016;
            wbsFiscalYear_Channel.FiscalYear = "2016";
            wbsFiscalYear_Channel.ChannelId = 99;
            #endregion

            #region Mocking
            mockWBSFiscalYear_ChannelRepository.Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(wbsFiscalYear_Channel);
            mockTypeOfWorkRepository.Setup(x => x.GetAll())
                .Returns(typeOfWorkDataQueryable);
            #endregion

            #region Service
            var dropDownListService = new DropDownListServiceMock(typeOfWorkRepository: mockTypeOfWorkRepository.Object,
                                                wBSFiscalYear_ChannelRepository: mockWBSFiscalYear_ChannelRepository.Object,
                                                ChannelDeliverableGroupRepository: mockChannelDeliverableGroupRepository.Object);
            #endregion

            //Act
            IQueryable<TypeOfWork> result = dropDownListService.GetTowsByMarketingGroupIdAndCategoryId(
                1,
                Convert.ToInt32(wbsFiscalYear_Channel.FiscalYear),
                testList.Id);

            //Assert
            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.ElementAt(1).Id == 2);
            Assert.IsTrue(result.ElementAt(2).Name == testList.Name);
            Assert.IsFalse(result == null);
        }

        //MRM-165 - Get Fiscal Years By Channel
        [TestMethod]
        public void GetFiscalYearsByChannel_GetFiscalYearListTest()
        {
            #region Data Setup
            Channel_FiscalYear fiscalYearList1 = new Channel_FiscalYear()
            {
                Id = 1,
                FiscalYear = "2014",
                ChannelId = 10
            };
            Channel_FiscalYear fiscalYearList2 = new Channel_FiscalYear()
            {
                Id = 2,
                FiscalYear = "2015",
                ChannelId = 10
            };
            Channel_FiscalYear fiscalYearList3 = new Channel_FiscalYear()
            {
                Id = 3,
                FiscalYear = "2016",
                ChannelId = 20
            };
            Channel_FiscalYear fiscalYearTestList = new Channel_FiscalYear()
            {
                Id = 3,
                FiscalYear = "2016",
                ChannelId = 15
            };
            //Empty Type Of Work List
            Channel_FiscalYear testList_Empty = new Channel_FiscalYear() { };
            List<Channel_FiscalYear> fiscalYearData = new List<Channel_FiscalYear>();
            fiscalYearData.Add(fiscalYearList1);
            fiscalYearData.Add(fiscalYearList2);
            fiscalYearData.Add(fiscalYearList3);
            List<Channel_FiscalYear> fiscalYearTestData = new List<Channel_FiscalYear>();
            fiscalYearTestData.Add(fiscalYearTestList);
            #endregion

            #region Mocking
            mockTypeOfWorkRepository.Setup(x => x.GetFiscalYearList(It.IsAny<int>()))
                .Returns(fiscalYearData);
            #endregion

            #region Service
            var dropDownListService = new DropDownListServiceMock(typeOfWorkRepository: mockTypeOfWorkRepository.Object, ChannelDeliverableGroupRepository: mockChannelDeliverableGroupRepository.Object);
            #endregion

            //Act
            List<Channel_FiscalYear> result = dropDownListService.GetFiscalYearsByChannel(10);

            //Assert
            //As the list is retrieved in Descending order
            Assert.IsTrue(result.ElementAt(0).Id == 1);
            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.ElementAt(2).FiscalYear == "2016");
        }

        //MRM-215 - Retrive All Method Of Productions from database
        [TestMethod]
        public void GetDropDown_GetMethodOfProductionValues()
        {

            #region Data Setup
            DropDownList dropDown1 = new DropDownList()
            {
                Code = "PH", //Post House
                Id = 1,
                Text = "Post House",
                SortOrder = 0
            };
            dropDown1.DropDownIndex = "Method Of Production";
            DropDownList dropDown2 = new DropDownList()
            {
                Code = "CR",
                Id = 2,
                Text = "Contract Request",
                SortOrder = 0
            };
            dropDown2.DropDownIndex = "Method Of Production";
            List<DropDownList> dropDownList = new List<DropDownList>();
            dropDownList.Add(dropDown1);
            dropDownList.Add(dropDown2);
            #endregion

            #region Mocking
            mockDropDownListService.Setup(x => x.GetDropDownList(It.IsAny<string>()))
                .Returns(dropDownList.AsQueryable());
            mockDropDownListRepository.Setup(x => x.GetAll())
                .Returns(dropDownList.AsQueryable());
            #endregion

            #region Service
            var dropDownListService = new DropDownListServiceMock(idropDownListRepository: mockDropDownListRepository.Object);
            #endregion

            #region Act
            IQueryable<DropDownList> result = dropDownListService.GetDropDownList(It.IsAny<string>());
            #endregion

            #region Assert
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.All(i => i.SortOrder == 0));
            Assert.IsTrue(result.ElementAt(0).Text == "Post House");
            Assert.IsTrue(result.ElementAt(1).Text == "Contract Request");
            #endregion

        }

        #region MRM-243
        //MRM-243 - Get External Type Of Work list
        [TestMethod]
        public void GetExternalTows()
        {
            #region Data Setup
            int fiscalYear = 2016;

            #region TypeOfWork
            TypeOfWork towList1 = new TypeOfWork()
            {
                Id = 5001,
                FiscalYear = fiscalYear,
                TypeOfWorkCategoryId = 999,
                Name = "N1"
            };
            TypeOfWork towList2 = new TypeOfWork()
            {
                Id = 5002,
                FiscalYear = fiscalYear,
                TypeOfWorkCategoryId = 888,
                Name = "N2"
            };
            TypeOfWork towList3 = new TypeOfWork()
            {
                Id = 5003,
                FiscalYear = fiscalYear,
                TypeOfWorkCategoryId = 777,
                Name = "N3"
            };
            TypeOfWork testList = new TypeOfWork()
            {
                Id = 5003,
                FiscalYear = fiscalYear,
                TypeOfWorkCategoryId = 777,
                Name = "N3"
            };
            #endregion

            #region WBSElement
            List<WBSElement> wbsElementList = new List<WBSElement>();

            WBSElement wbsElement1 = new WBSElement() { };
            wbsElement1.TypeOfWork = towList1;
            wbsElement1.ExternalWBSFlag = true;
            wbsElement1.ExternalBusinessAreaId = 1;

            WBSElement wbsElement2 = new WBSElement() { };
            wbsElement2.TypeOfWork = towList2;
            wbsElement2.ExternalWBSFlag = true;
            wbsElement2.ExternalBusinessAreaId = 1;

            WBSElement wbsElement3 = new WBSElement() { };
            wbsElement3.TypeOfWork = towList3;
            wbsElement3.ExternalWBSFlag = true;
            wbsElement3.ExternalBusinessAreaId = 2;

            wbsElementList.Add(wbsElement1);
            wbsElementList.Add(wbsElement2);
            wbsElementList.Add(wbsElement3);
            #endregion

            //Empty Type Of Work List
            TypeOfWork testList_Empty = new TypeOfWork() { };
            List<TypeOfWork> typeOfWorkData = new List<TypeOfWork>();
            typeOfWorkData.Add(towList1);
            typeOfWorkData.Add(towList2);
            typeOfWorkData.Add(towList3);
            List<TypeOfWork> typeOfWorkTestData = new List<TypeOfWork>();
            typeOfWorkTestData.Add(testList);
            IQueryable<TypeOfWork> typeOfWorkDataQueryable = typeOfWorkData.AsQueryable();
            IQueryable<WBSElement> wbsElementDataQueryable = wbsElementList.AsQueryable();
            #endregion

            #region Mocking           
            mockWBSElementRepository.Setup(x => x.GetAll())
                           .Returns(wbsElementDataQueryable);

            mockTypeOfWorkRepository.Setup(x => x.GetAll())
                .Returns(typeOfWorkDataQueryable);
            #endregion

            #region Service
            var dropDownListService = new DropDownListServiceMock(typeOfWorkRepository: mockTypeOfWorkRepository.Object,
                                                wBSFiscalYear_ChannelRepository: null,
                                                ChannelDeliverableGroupRepository: null,
                                                wbsElementRepository: mockWBSElementRepository.Object);
            #endregion

            //Act
            IQueryable<TypeOfWork> result = dropDownListService.GetExternalTows(fiscalYear);

            //Assert
            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.ElementAt(1).Id == 5002);
            Assert.IsTrue(result.ElementAt(2).Name == testList.Name);
            Assert.IsFalse(result == null);
        }

        [TestMethod]
        public void GetExternalFiscalYears()
        {
            #region Data Setup

            #region FiscalYear
            List<int> fyList = new List<int>();
            fyList.Add(2015);
            fyList.Add(2016);
            #endregion

            #region TypeOfWork
            TypeOfWork towList1 = new TypeOfWork()
            {
                Id = 5001,
                FiscalYear = 2015,
                TypeOfWorkCategoryId = 999,
                Name = "N1"
            };
            TypeOfWork towList2 = new TypeOfWork()
            {
                Id = 5002,
                FiscalYear = 2016,
                TypeOfWorkCategoryId = 888,
                Name = "N2"
            };
            TypeOfWork towList3 = new TypeOfWork()
            {
                Id = 5003,
                FiscalYear = 2016,
                TypeOfWorkCategoryId = 777,
                Name = "N3"
            };
            #endregion

            #region WBSElement
            List<WBSElement> wbsElementList = new List<WBSElement>();

            WBSElement wbsElement1 = new WBSElement() { };
            wbsElement1.TypeOfWork = towList1;
            wbsElement1.ExternalWBSFlag = true;
            wbsElement1.ExternalBusinessAreaId = 1;

            WBSElement wbsElement2 = new WBSElement() { };
            wbsElement2.TypeOfWork = towList2;
            wbsElement2.ExternalWBSFlag = true;
            wbsElement2.ExternalBusinessAreaId = 1;

            WBSElement wbsElement3 = new WBSElement() { };
            wbsElement3.TypeOfWork = towList3;
            wbsElement3.ExternalWBSFlag = true;
            wbsElement3.ExternalBusinessAreaId = 2;

            wbsElementList.Add(wbsElement1);
            wbsElementList.Add(wbsElement2);
            wbsElementList.Add(wbsElement3);
            #endregion

            //Empty Type Of Work List
            TypeOfWork testList_Empty = new TypeOfWork() { };
            List<TypeOfWork> typeOfWorkData = new List<TypeOfWork>();
            typeOfWorkData.Add(towList1);
            typeOfWorkData.Add(towList2);
            typeOfWorkData.Add(towList3);
            List<TypeOfWork> typeOfWorkTestData = new List<TypeOfWork>();
            #endregion

            #region Mocking                       

            mockTypeOfWorkRepository.Setup(x => x.GetExternalFiscalYearList())
                .Returns(fyList);
            #endregion

            #region Service
            var dropDownListService = new DropDownListServiceMock(typeOfWorkRepository: mockTypeOfWorkRepository.Object,
                                                wBSFiscalYear_ChannelRepository: null,
                                                ChannelDeliverableGroupRepository: null,
                                                wbsElementRepository: mockWBSElementRepository.Object);
            #endregion

            //Act
            List<int> result = dropDownListService.GetExternalFiscalYears();

            //Assert
            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.ElementAt(1) == 2016);
            Assert.IsFalse(result == null || !result.Any());

        }

        [TestMethod]
        public void GetExternalCompany()
        {
            #region Data Setup
            var dropDownIndex = "Company";

            #region Company
            var ddList1 = new DropDownList();
            ddList1.Id = 1;
            ddList1.Text = "Company 1";
            ddList1.Value = "1";
            ddList1.Code = "001";
            ddList1.Description = "Company 1";
            ddList1.DropDownIndex = dropDownIndex;

            var ddList2 = new DropDownList();
            ddList2.Id = 2;
            ddList2.Text = "Company 2";
            ddList2.Value = "2";
            ddList2.Code = "002";
            ddList2.Description = "Company 2";
            ddList2.DropDownIndex = dropDownIndex;

            var ddList3 = new DropDownList();
            ddList3.Id = 3;
            ddList3.Text = "Company 3";
            ddList3.Value = "3";
            ddList3.Code = "003";
            ddList3.Description = "Company 3";
            ddList3.DropDownIndex = dropDownIndex;

            List<DropDownList> ddList = new List<DropDownList>();
            ddList.Add(ddList1);
            ddList.Add(ddList2);
            ddList.Add(ddList3);

            #endregion


            #endregion

            #region Mocking                       
            mockDropDownListRepository.Setup(x => x.GetAll()).Returns(ddList.AsQueryable<DropDownList>);

            #endregion

            #region Service
            var dropDownListService = new DropDownListServiceMock(
                                                idropDownListRepository: mockDropDownListRepository.Object
                                                );
            #endregion

            //Act
            var result = dropDownListService.GetDropDownList(NETWORK_LOGIN);

            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.ElementAt(0).Code == "001");
            Assert.IsTrue(result.ElementAt(0).Text == "Company 1");
        }

        [TestMethod]
        public void GetExternalBusinessAreas()
        {
            #region Data Setup

            var companyId_1 = 1; var companyId_2 = 2;

            #region Business Area
            var ba1 = new BusinessArea();
            ba1.Id = 1;
            ba1.Name = "Area 1";
            ba1.Code = "001";
            ba1.Description = "Business Area 1";
            ba1.CompanyId = companyId_1;

            var ba2 = new BusinessArea();
            ba2.Id = 2;
            ba2.Name = "Area 2";
            ba2.Code = "002";
            ba2.Description = "Business Area 2";
            ba2.CompanyId = companyId_1;

            var ba3 = new BusinessArea();
            ba3.Id = 3;
            ba3.Name = "Area 3";
            ba3.Code = "003";
            ba3.Description = "Business Area 3";
            ba3.CompanyId = companyId_2;

            List<BusinessArea> baList = new List<BusinessArea>();
            baList.Add(ba1);
            baList.Add(ba2);
            baList.Add(ba3);

            #endregion


            #endregion

            #region Mocking                       
            mockBusinessAreaRepository.Setup(x => x.GetAll()).Returns(baList.AsQueryable<BusinessArea>);

            #endregion

            #region Service
            var dropDownListService = new DropDownListServiceMock(
                                                businessAreaRepository: mockBusinessAreaRepository.Object
                                                );
            #endregion

            //Act
            var result = dropDownListService.GetBusinessAreaDropDown();

            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.ElementAt(0).Code == "001");
            Assert.IsTrue(result.ElementAt(0).Name == "Area 1");
            Assert.IsTrue(result.ElementAt(0).CompanyId == companyId_1);
        }
        #endregion

        //MRM -216 :  Get Vendors by Method Of Production
        [TestMethod]
        public void GetVendorsByMethodofProduction_GetVendorsForPostHouseTrueTest()
        {
            #region Data Setup
            MOPVendor mOPVendor1 = new MOPVendor()
            {
                MasterVendorId = 1,
                Name = "Keep Me Posted"
            };
            MOPVendor mOPVendor2 = new MOPVendor()
            {
                MasterVendorId = 2,
                Name = "Creative Services"
            };
            List<MOPVendor> mOPVendorList = new List<MOPVendor>();
            mOPVendorList.Add(mOPVendor1);
            mOPVendorList.Add(mOPVendor2);
            DropDownViewModel dropDownViewModel1 = new DropDownViewModel()
            {
                Id = 1,
                Enabled = true,

            };
            DeliverableMOP deliverableMOP = new DeliverableMOP()
            {
                isInternal = true,
                isPostHouse = true
            };
            deliverableMOP.Vendors = mOPVendorList;
            DeliverableGroup testDeliverableGroup = new DeliverableGroup()
            {
                Code = "VID"//Video
            };
            TypeOfWork typeOfWork = new TypeOfWork()
            {
                Name = "Type Of Work",
                Id = 166
            };
            Channel testChannel = new Channel()
            {
                Code = "ROM"//Radio Disney
            };
            WBSElement wbsElement1 = new WBSElement()
            {
                Id = 1,
                FullWBSNumber = "123456.011.001",
                TypeOfWorkId = 1000,
            };
            WBSElement wbsElement2 = new WBSElement()
            {
                Id = 2,
                FullWBSNumber = "123456.011.002",
                TypeOfWorkId = 1001,
            };
            Deliverable testDeliverable = new Deliverable()
            {
                Id = 140000,
                DeliverableTypeId = 94,//Affiliate Marketing
                ProducingDepartmentId = 84,//MRM Team
                Name = "Radio Disney Video Deliverable",
                //ChannelId = 10,//Radio Disney
                DeliverableGroupId = 2,//Video
                DeliverableGroup = testDeliverableGroup,
                Channel = testChannel,
                ClipDeliverMasterVendorId = 108,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true
            };
            MasterVendor masterVendor1 = new MasterVendor()
            {
                Id = 99,
                OtherCode = "Keep Me Posted"
            };
            MasterVendor masterVendor2 = new MasterVendor()
            {
                Id = 100,
                OtherCode = "Creative Services"
            };
            DeliverableBudget deliverableBud1 = new DeliverableBudget()
            {
                Id = 1,
                DeliverableId = 140000,
                MasterVendorId = 99,
                WBSElementId = wbsElement1.Id,
            };
            deliverableBud1.WBSElement = wbsElement1;
            deliverableBud1.Deliverable = testDeliverable;
            deliverableBud1.MasterVendor = masterVendor1;
            DeliverableBudget deliverableBud2 = new DeliverableBudget()
            {
                Id = 2,
                DeliverableId = 140000,
                MasterVendorId = 100,
                WBSElementId = wbsElement2.Id
            };
            deliverableBud2.WBSElement = wbsElement2;
            deliverableBud2.Deliverable = testDeliverable;
            deliverableBud2.TypeOfWork = typeOfWork;
            deliverableBud2.MasterVendor = masterVendor2;
            List<DeliverableBudget> deliverableBudgetList = new List<DeliverableBudget>();
            deliverableBudgetList.Add(deliverableBud1);
            deliverableBudgetList.Add(deliverableBud2);
            ProductionMethodType productionMethodType1 = new ProductionMethodType()
            {
                Id = 1,
                Code = "PH",
                Name = "Post House"
            };
            ProductionMethodType productionMethodType2 = new ProductionMethodType()
            {
                Id = 2,
                Code = "CS",
                Name = "Creative Services"
            };
            List<ProductionMethodType> productionMethodTypeList = new List<ProductionMethodType>();
            productionMethodTypeList.Add(productionMethodType1);
            productionMethodTypeList.Add(productionMethodType2);
            Channel_ProductionMethodType_WorkOrderType_Vendor channel_ProductionMethodType_WorkOrderType_Vendor1 = new Channel_ProductionMethodType_WorkOrderType_Vendor()
            {
                Id = 1,
                ChannelName = "Disney Channel",
                MasterVendorId = 99,
                MasterVendorName = "Keep Me Posted",
                ProductionMethodTypeId = productionMethodType1.Id
            };
            Channel_ProductionMethodType_WorkOrderType_Vendor channel_ProductionMethodType_WorkOrderType_Vendor2 = new Channel_ProductionMethodType_WorkOrderType_Vendor()
            {
                Id = 2,
                ChannelName = "Disney Channel",
                MasterVendorId = 100,
                MasterVendorName = "Creative Services",
                ProductionMethodTypeId = productionMethodType2.Id
            };
            List<Channel_ProductionMethodType_WorkOrderType_Vendor> channel_ProductionMethodType_WorkOrderType_VendorList = new List<Channel_ProductionMethodType_WorkOrderType_Vendor>();
            channel_ProductionMethodType_WorkOrderType_VendorList.Add(channel_ProductionMethodType_WorkOrderType_Vendor1);
            channel_ProductionMethodType_WorkOrderType_VendorList.Add(channel_ProductionMethodType_WorkOrderType_Vendor2);
            #endregion

            #region Mocking
            mockProductionMethodTypeRepository.Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(productionMethodType1);
            mockVendorRepository.Setup(x => x.GetVendorsByProductionMethodType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<Guid>(), out deliverableBudgetList))
                .Returns(channel_ProductionMethodType_WorkOrderType_VendorList);




            #endregion

            #region Service
            DeliverableMOP result = new DeliverableMOP();
            var dropDownListService = new DropDownListServiceMock(ProductionMethodTypeRepository: mockProductionMethodTypeRepository.Object,
                vendorRepository: mockVendorRepository.Object);
            result = dropDownListService.GetVendorsByMethodofProduction(140000, 1, 1, 1, Guid.NewGuid(), NETWORK_LOGIN);
            #endregion

            #region Assert
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.isPostHouse);
            //Default values for Post house are Keep Me Posted and Creative Services 
            Assert.IsTrue(result.VendorOptions.ElementAt(0).Text == "Keep Me Posted");
            Assert.IsTrue(result.VendorOptions.ElementAt(1).Text == "Creative Services");
            Assert.IsTrue(result.VendorOptions.Count == 2);
            Assert.IsTrue(result.ProductionMethodTypeId == 1);
            Assert.AreEqual(result.ProductionMethod, "Post House");
            #endregion
        }

        //MRM-216 : Get Vendors by Method Of Production Other than Post House and Internal
        [TestMethod]
        public void GetVendorsByMethodofProduction_GetVendorsOtherThanPHOrInternalTrueTest()
        {
            #region Data Setup
            MOPVendor mOPVendor1 = new MOPVendor()
            {
                MasterVendorId = 99,
                Name = "Keep Me Posted"
            };
            MOPVendor mOPVendor2 = new MOPVendor()
            {
                MasterVendorId = 100,
                Name = "Creative Services"
            };
            List<MOPVendor> mOPVendorList = new List<MOPVendor>();
            mOPVendorList.Add(mOPVendor1);
            mOPVendorList.Add(mOPVendor2);
            DropDownViewModel dropDownViewModel1 = new DropDownViewModel()
            {
                Id = 1,
                Enabled = true,

            };
            DeliverableMOP deliverableMOP = new DeliverableMOP()
            {
                isInternal = true,
                isPostHouse = true
            };
            deliverableMOP.Vendors = mOPVendorList;
            DeliverableGroup testDeliverableGroup = new DeliverableGroup()
            {
                Code = "VID"//Video
            };
            TypeOfWork typeOfWork = new TypeOfWork()
            {
                Name = "Type Of Work",
                Id = 166
            };
            Channel testChannel = new Channel()
            {
                Code = "ROM"//Radio Disney
            };
            WBSElement wbsElement1 = new WBSElement()
            {
                Id = 1,
                FullWBSNumber = "123456.011.001",
                TypeOfWorkId = 1000,
            };
            WBSElement wbsElement2 = new WBSElement()
            {
                Id = 2,
                FullWBSNumber = "123456.011.002",
                TypeOfWorkId = 1001,
            };
            Deliverable testDeliverable = new Deliverable()
            {
                Id = 140000,
                DeliverableTypeId = 94,//Affiliate Marketing
                ProducingDepartmentId = 84,//MRM Team
                Name = "Radio Disney Video Deliverable",
                //ChannelId = 10,//Radio Disney
                DeliverableGroupId = 2,//Video
                DeliverableGroup = testDeliverableGroup,
                Channel = testChannel,
                ClipDeliverMasterVendorId = 108,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true
            };
            MasterVendor masterVendor1 = new MasterVendor()
            {
                Id = 99,
                OtherCode = "Keep Me Posted"
            };
            MasterVendor masterVendor2 = new MasterVendor()
            {
                Id = 100,
                OtherCode = "Creative Services"
            };
            DeliverableBudget deliverableBud1 = new DeliverableBudget()
            {
                Id = 1,
                DeliverableId = 140000,
                MasterVendorId = 99,
                WBSElementId = wbsElement1.Id,
            };
            deliverableBud1.WBSElement = wbsElement1;
            deliverableBud1.Deliverable = testDeliverable;
            deliverableBud1.MasterVendor = masterVendor1;
            DeliverableBudget deliverableBud2 = new DeliverableBudget()
            {
                Id = 2,
                DeliverableId = 140000,
                MasterVendorId = 100,
                WBSElementId = wbsElement2.Id
            };
            deliverableBud2.WBSElement = wbsElement2;
            deliverableBud2.Deliverable = testDeliverable;
            deliverableBud2.TypeOfWork = typeOfWork;
            deliverableBud2.MasterVendor = masterVendor2;
            List<DeliverableBudget> deliverableBudgetList = new List<DeliverableBudget>();
            deliverableBudgetList.Add(deliverableBud1);
            deliverableBudgetList.Add(deliverableBud2);
            ProductionMethodType productionMethodType1 = new ProductionMethodType()
            {
                Id = 1,
                Code = "CR",
                Name = "Contract Request"
            };
            ProductionMethodType productionMethodType2 = new ProductionMethodType()
            {
                Id = 2,
                Code = "MISC",
                Name = "Miscellaneous"
            };
            List<ProductionMethodType> productionMethodTypeList = new List<ProductionMethodType>();
            productionMethodTypeList.Add(productionMethodType1);
            productionMethodTypeList.Add(productionMethodType2);
            DropDownList dropDown1 = new DropDownList()
            {
                Id = 99,
                Code = "KMP",
                Text = "Keep Me Posted"
            };
            DropDownList dropDown2 = new DropDownList()
            {
                Id = 100,
                Code = "CS",
                Text = "Creative Services"
            };
            List<DropDownList> dropDownList = new List<DropDownList>();
            dropDownList.Add(dropDown1);
            dropDownList.Add(dropDown2);
            Channel_ProductionMethodType_WorkOrderType_Vendor channel_ProductionMethodType_WorkOrderType_Vendor2 = new Channel_ProductionMethodType_WorkOrderType_Vendor()
            {
                Id = 2,
                ChannelName = "Disney Channel",
                MasterVendorId = 100,
                MasterVendorName = "Creative Services",
                ProductionMethodTypeId = productionMethodType2.Id
            };
            List<Channel_ProductionMethodType_WorkOrderType_Vendor> channel_ProductionMethodType_WorkOrderType_VendorList = new List<Channel_ProductionMethodType_WorkOrderType_Vendor>();
            channel_ProductionMethodType_WorkOrderType_VendorList.Add(channel_ProductionMethodType_WorkOrderType_Vendor2);
            #endregion

            #region Mocking
            mockProductionMethodTypeRepository.Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(productionMethodType1);
            mockVendorRepository.Setup(x => x.GetVendorsByProductionMethodType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<Guid>(), out deliverableBudgetList))
                .Returns(dropDownList);

            #endregion

            #region Service
            DeliverableMOP result = new DeliverableMOP();
            var dropDownListService = new DropDownListServiceMock(ProductionMethodTypeRepository: mockProductionMethodTypeRepository.Object,
                vendorRepository: mockVendorRepository.Object);
            result = dropDownListService.GetVendorsByMethodofProduction(140000, 1, 1, 1, Guid.NewGuid(), NETWORK_LOGIN);
            #endregion

            #region Assert
            Assert.IsFalse(result == null);
            Assert.IsFalse(result.isPostHouse);
            Assert.IsTrue(result.VendorOptions.Count == 2);
            Assert.IsTrue(result.ProductionMethodTypeId == 1);
            Assert.AreEqual(result.ProductionMethod, "Contract Request");
            Assert.IsTrue(result.VendorOptions.ElementAt(1).Text == "Creative Services");
            #endregion
        }

        #region MRM-159
        // MRM-159
        [TestMethod]
        public void GetAllActiveWBS()
        {
            #region Data Setup
            List<WBSElement> wbsList = new List<WBSElement>();

            var elem1 = new WBSElement();
            elem1.FullWBSNumber = "112.123.001";
            elem1.ExternalWBSFlag = false;
            elem1.TypeOfWorkId = 1;
            elem1.IsActiveFlag = true;
            wbsList.Add(elem1);

            var elem2 = new WBSElement();
            elem2.FullWBSNumber = "112.123.002";
            elem2.ExternalWBSFlag = false;
            elem2.TypeOfWorkId = 1;
            elem2.IsActiveFlag = true;
            wbsList.Add(elem2);

            var elem3 = new WBSElement();
            elem3.FullWBSNumber = "112.123.003";
            elem3.ExternalWBSFlag = false;
            elem3.TypeOfWorkId = 3;
            elem3.IsActiveFlag = true;
            wbsList.Add(elem3);

            var elem4 = new WBSElement();
            elem4.FullWBSNumber = "112.123.004";
            elem4.ExternalWBSFlag = false;
            elem4.TypeOfWorkId = 3;
            elem4.IsActiveFlag = true;
            wbsList.Add(elem4);

            var elem5 = new WBSElement();
            elem5.FullWBSNumber = "112.123.005";
            elem5.ExternalWBSFlag = false;
            elem5.TypeOfWorkId = 5;
            elem5.IsActiveFlag = true;
            wbsList.Add(elem5);

            var elem6 = new WBSElement();
            elem6.FullWBSNumber = "112.123.006";
            elem6.ExternalWBSFlag = false;
            elem6.TypeOfWorkId = 6;
            elem6.IsActiveFlag = false;
            wbsList.Add(elem6);

            #endregion

            #region Mock
            mockWBSElementRepository.Setup(x => x.GetAll()).Returns(wbsList.AsQueryable());
            #endregion

            #region Service
            var dropDownListService = new DropDownListServiceMock(wbsElementRepository: mockWBSElementRepository.Object);
            #endregion

            #region Controller
            var controller = new DropDownApiControllerMock(mockUserService.Object, mockLoggerService.Object, dropDownListService: dropDownListService);
            #endregion

            #region Call
            var result = controller.GetAllActiveWBS(NETWORK_LOGIN).DropDownList;
            #endregion

            #region Assert

            Assert.IsFalse(result == null);
            Assert.IsFalse(result.Count == 0);
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result[4].Text == "112.123.005");
            #endregion  


        }
        #endregion

        #region #1373
        //[TestMethod]
        //public void GetTowByWBSFiscalYearChannel_ShouldReturn1DropDownValue()
        //{
        //    #region Arrange
        //    TypeOfWork tow = new TypeOfWork
        //    {
        //        TypeOfWorkStatus = new TypeOfWorkStatus
        //        {
        //            Code = Core.Constants.TypeOfWorkStatus.InProcess
        //        },

        //        TypeOfWorkStatusId = 2, // InProcess

        //        WBSFiscalYearChannelId = 48,
        //        WBSFiscalYear_Channel = new WBSFiscalYear_Channel
        //        {
        //            Channel = new Channel
        //            {
        //                Id = 1,
        //                Code = "DC",
        //                Name = "Disney Channel"
        //            },
        //            FiscalYear = "2016",

        //        },
        //        Id = 5588,
        //        Name = "DC",
        //        TypeOfWorkCategoryId = 35
        //    };

        //    TypeOfWork tow1 = new TypeOfWork
        //    {
        //        TypeOfWorkStatus = new TypeOfWorkStatus
        //        {
        //            Code = Core.Constants.TypeOfWorkStatus.Draft
        //        },

        //        TypeOfWorkStatusId = 2, // InProcess

        //        WBSFiscalYearChannelId = 48,
        //        WBSFiscalYear_Channel = new WBSFiscalYear_Channel
        //        {
        //            Channel = new Channel
        //            {
        //                Id = 1,
        //                Code = "DC",
        //                Name = "Disney Channel"
        //            },
        //            FiscalYear = "2016",

        //        },
        //        Id = 5588,
        //        Name = "DC",
        //        TypeOfWorkCategoryId = 35
        //    };

        //    List<TypeOfWork> listTOW = new List<TypeOfWork>();
        //    listTOW.Add(tow);
        //    listTOW.Add(tow1);

        //    mockDropDownListService.Setup(x => x.GetTowsByMarketingGroupIdFiscalYearAndTowCategoryId(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>()))
        //        .Returns(listTOW.AsQueryable());

        //    #endregion

        //    #region Act
        //    var dropDownController = new DropDownApiControllerMock(dropDownListService: mockDropDownListService.Object);
        //    var result = dropDownController.GetTowByWBSFiscalYearChannel(1, 2016, "networkLogin").ToList();
        //    #endregion

        //    #region Assert
        //    Assert.IsFalse(result == null);
        //    Assert.IsTrue(result.Count == 1);
        //    #endregion
        //}

        //[TestMethod]
        //public void GetTowByWBSFiscalYearChannel_ShouldReturn2DropDownValues()
        //{
        //    #region Arrange
        //    TypeOfWork tow = new TypeOfWork
        //    {
        //        TypeOfWorkStatus = new TypeOfWorkStatus
        //        {
        //            Code = Core.Constants.TypeOfWorkStatus.InProcess
        //        },

        //        TypeOfWorkStatusId = 2, // InProcess

        //        WBSFiscalYearChannelId = 48,
        //        WBSFiscalYear_Channel = new WBSFiscalYear_Channel
        //        {
        //            Channel = new Channel
        //            {
        //                Id = 1,
        //                Code = "DC",
        //                Name = "Disney Channel"
        //            },
        //            FiscalYear = "2016",

        //        },
        //        Id = 5588,
        //        Name = "DC",
        //        TypeOfWorkCategoryId = 35
        //    };

        //    TypeOfWork tow1 = new TypeOfWork
        //    {
        //        TypeOfWorkStatus = new TypeOfWorkStatus
        //        {
        //            Code = Core.Constants.TypeOfWorkStatus.Draft
        //        },

        //        TypeOfWorkStatusId = 2, // InProcess

        //        WBSFiscalYearChannelId = 48,
        //        WBSFiscalYear_Channel = new WBSFiscalYear_Channel
        //        {
        //            Channel = new Channel
        //            {
        //                Id = 1,
        //                Code = "DC",
        //                Name = "Disney Channel"
        //            },
        //            FiscalYear = "2015",

        //        },
        //        Id = 5588,
        //        Name = "DC",
        //        TypeOfWorkCategoryId = 35
        //    };

        //    TypeOfWork tow2 = new TypeOfWork
        //    {
        //        TypeOfWorkStatus = new TypeOfWorkStatus
        //        {
        //            Code = Core.Constants.TypeOfWorkStatus.InProcess
        //        },

        //        TypeOfWorkStatusId = 2, // InProcess

        //        WBSFiscalYearChannelId = 48,
        //        WBSFiscalYear_Channel = new WBSFiscalYear_Channel
        //        {
        //            Channel = new Channel
        //            {
        //                Id = 1,
        //                Code = "DC",
        //                Name = "Disney Channel"
        //            },
        //            FiscalYear = "2016",

        //        },
        //        Id = 5588,
        //        Name = "DC",
        //        TypeOfWorkCategoryId = 35
        //    };

        //    List<TypeOfWork> listTOW = new List<TypeOfWork>();
        //    listTOW.Add(tow);
        //    listTOW.Add(tow1);
        //    listTOW.Add(tow2);

        //    mockDropDownListService.Setup(x => x.GetTowsByMarketingGroupIdFiscalYearAndTowCategoryId(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>()))
        //        .Returns(listTOW.AsQueryable());

        //    #endregion

        //    #region Act
        //    var dropDownController = new DropDownApiControllerMock(dropDownListService: mockDropDownListService.Object);
        //    var result = dropDownController.GetTowByWBSFiscalYearChannel(1, 2016, "networkLogin").ToList();
        //    #endregion

        //    #region Assert
        //    Assert.IsFalse(result == null);
        //    Assert.IsTrue(result.Count == 2);
        //    #endregion
        //}

        //[TestMethod]
        //public void GetTowByWBSFiscalYearChannel_ShouldReturn3DropDownValues()
        //{
        //    #region Arrange
        //    TypeOfWork tow = new TypeOfWork
        //    {
        //        TypeOfWorkStatus = new TypeOfWorkStatus
        //        {
        //            Code = Core.Constants.TypeOfWorkStatus.InProcess
        //        },

        //        TypeOfWorkStatusId = 2, // InProcess

        //        WBSFiscalYearChannelId = 48,
        //        WBSFiscalYear_Channel = new WBSFiscalYear_Channel
        //        {
        //            Channel = new Channel
        //            {
        //                Id = 1,
        //                Code = "DC",
        //                Name = "Disney Channel"
        //            },
        //            FiscalYear = "2016",

        //        },
        //        Id = 5588,
        //        Name = "DC",
        //        TypeOfWorkCategoryId = 35
        //    };

        //    TypeOfWork tow1 = new TypeOfWork
        //    {
        //        TypeOfWorkStatus = new TypeOfWorkStatus
        //        {
        //            Code = Core.Constants.TypeOfWorkStatus.Draft
        //        },

        //        TypeOfWorkStatusId = 2, // InProcess

        //        WBSFiscalYearChannelId = 48,
        //        WBSFiscalYear_Channel = new WBSFiscalYear_Channel
        //        {
        //            Channel = new Channel
        //            {
        //                Id = 1,
        //                Code = "DC",
        //                Name = "Disney Channel"
        //            },
        //            FiscalYear = "2015",

        //        },
        //        Id = 5588,
        //        Name = "DC",
        //        TypeOfWorkCategoryId = 35
        //    };

        //    TypeOfWork tow2 = new TypeOfWork
        //    {
        //        TypeOfWorkStatus = new TypeOfWorkStatus
        //        {
        //            Code = Core.Constants.TypeOfWorkStatus.InProcess
        //        },

        //        TypeOfWorkStatusId = 2, // InProcess

        //        WBSFiscalYearChannelId = 48,
        //        WBSFiscalYear_Channel = new WBSFiscalYear_Channel
        //        {
        //            Channel = new Channel
        //            {
        //                Id = 1,
        //                Code = "DC",
        //                Name = "Disney Channel"
        //            },
        //            FiscalYear = "2016",

        //        },
        //        Id = 5588,
        //        Name = "DC",
        //        TypeOfWorkCategoryId = 35
        //    };

        //    TypeOfWork tow3 = new TypeOfWork
        //    {
        //        TypeOfWorkStatus = new TypeOfWorkStatus
        //        {
        //            Code = Core.Constants.TypeOfWorkStatus.Planned
        //        },

        //        TypeOfWorkStatusId = 2, // InProcess

        //        WBSFiscalYearChannelId = 48,
        //        WBSFiscalYear_Channel = new WBSFiscalYear_Channel
        //        {
        //            Channel = new Channel
        //            {
        //                Id = 1,
        //                Code = "DC",
        //                Name = "Disney Channel"
        //            },
        //            FiscalYear = "2016",

        //        },
        //        Id = 5588,
        //        Name = "DC",
        //        TypeOfWorkCategoryId = 35
        //    };

        //    List<TypeOfWork> listTOW = new List<TypeOfWork>();
        //    listTOW.Add(tow);
        //    listTOW.Add(tow1);
        //    listTOW.Add(tow2);
        //    listTOW.Add(tow3);

        //    mockDropDownListService.Setup(x => x.GetTowsByMarketingGroupIdFiscalYearAndTowCategoryId(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>()))
        //        .Returns(listTOW.AsQueryable());

        //    #endregion

        //    #region Act
        //    var dropDownController = new DropDownApiControllerMock(dropDownListService: mockDropDownListService.Object);
        //    var result = dropDownController.GetTowByWBSFiscalYearChannel(1, 2016, "networkLogin").ToList();
        //    #endregion

        //    #region Assert
        //    Assert.IsFalse(result == null);
        //    Assert.IsTrue(result.Count == 3);
        //    #endregion
        //}

        //[TestMethod]
        //public void GetTowByWBSFiscalYearChannel_ShouldReturn0DropDownValues()
        //{
        //    #region Arrange

        //    TypeOfWork tow = new TypeOfWork
        //    {
        //        TypeOfWorkStatus = new TypeOfWorkStatus
        //        {
        //            Code = Core.Constants.TypeOfWorkStatus.Draft
        //        },

        //        TypeOfWorkStatusId = 2, // InProcess

        //        WBSFiscalYearChannelId = 48,
        //        WBSFiscalYear_Channel = new WBSFiscalYear_Channel
        //        {
        //            Channel = new Channel
        //            {
        //                Id = 1,
        //                Code = "DC",
        //                Name = "Disney Channel"
        //            },
        //            FiscalYear = "2015",

        //        },
        //        Id = 5588,
        //        Name = "DC",
        //        TypeOfWorkCategoryId = 35
        //    };

        //    List<TypeOfWork> listTOW = new List<TypeOfWork>();
        //    listTOW.Add(tow);

        //    mockDropDownListService.Setup(x => x.GetTowsByMarketingGroupIdFiscalYearAndTowCategoryId(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>()))
        //        .Returns(listTOW.AsQueryable());

        //    #endregion

        //    #region Act
        //    var dropDownController = new DropDownApiControllerMock(dropDownListService: mockDropDownListService.Object);
        //    var result = dropDownController.GetTowByWBSFiscalYearChannel(1, 2015, "networkLogin").ToList();
        //    #endregion

        //    #region Assert
        //    Assert.IsTrue(result.Count == 0);
        //    #endregion
        //}
        #endregion

        #region MRM-1027 GetDefaultMOPandVendorsTest
        /// <summary>
        /// For Test getting Method of Produciton and Vendors Based on  ChannelId(marketing Group)
        /// </summary>
        [TestMethod]
        public void GetDefaultMOPandVendorsTest()
        {
            #region Data Setup
            DeliverableMOP vm = new DeliverableMOP
            {
                ProductionMethodTypeId = 1,
                ProductionMethod = "Post House",
                Vendors = new List<MOPVendor>(),
                VendorOptions = new List<DropDownViewModel>(),
                isPostHouse = true
            };

            ProductionMethodType ProductionMethodType1 = new ProductionMethodType
            {
                Id = 1,
                Name = "Post House",
                Code = "PH"
            };

            vm.Vendors.Add(new MOPVendor { MasterVendorId = 173, Name = "Vendor1", EstimatedFinalCost = 0, Committed = 0, Actual = 0, EstimateCompleteAmount = 0 });
            string NETWORK_LOGIN = "swna//TestLogin";
            int DeliverableId = 12034;
            //int channelId = 7; //DCWW PMAC
            int channelId = 8; //Off Air Design
            Guid UniqueId = Guid.NewGuid();
            Channel_ProductionMethodType_MasterVendor chProdMV1 = new Channel_ProductionMethodType_MasterVendor { ChannelId = 7, ProductionMethodTypeId = 1, MasterVendorId = 1 };
            Channel_ProductionMethodType_MasterVendor chProdMV2 = new Channel_ProductionMethodType_MasterVendor { ChannelId = 8, ProductionMethodTypeId = 4, MasterVendorId = 173 };
            List<Channel_ProductionMethodType_MasterVendor> Channel_Mop_Vendors = new List<Channel_ProductionMethodType_MasterVendor>();
            Channel_Mop_Vendors.Add(chProdMV1);
            Channel_Mop_Vendors.Add(chProdMV2);
            #endregion

            #region Mock
            mockVendorRepository.Setup(x => x.GetDefaultMOPandVendors(It.IsAny<int>(),It.IsAny<int>()))
                .Returns((int y,int x) => Channel_Mop_Vendors.FirstOrDefault(p => p.ChannelId == x));
            mockProductionMethodTypeRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(ProductionMethodType1);
            #endregion

            #region Service
            var dropDownListService = new DropDownListServiceMock(vendorRepository: mockVendorRepository.Object,
                ProductionMethodTypeRepository: mockProductionMethodTypeRepository.Object);
            #endregion

            #region Controller
            var controller = new DropDownApiControllerMock(mockUserService.Object, mockLoggerService.Object, dropDownListService: dropDownListService);
            #endregion

            #region Call
            var result = controller.GetDefaultMOPandVendors(DeliverableId, channelId, UniqueId, NETWORK_LOGIN);
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DeliverableMOP));
            Assert.IsTrue(result.ProductionMethodTypeId == 4);
            Assert.IsTrue(result.Vendors.Any(p => p.MasterVendorId == 173));
            #endregion  
        }
        #endregion

        [TestMethod]
        public void GetAdvertiserDropDown_NotEqualSortOrders()
        {

            #region Arrange

            Advertiser advFirst = new Advertiser() { Id = 1,Name = "First Advertiser",Code = "First",Description = "First because Sort Order is least",SortOrder= 10};
            Advertiser advSecond = new Advertiser() { Id = 2, Name = "Second Advertiser", Code = "Second", Description = "Second because Name is befor the Third Advertiser. Sort Order is 99", SortOrder = 99 };
            Advertiser advLast = new Advertiser() { Id = 3, Name = "Third Advertiser", Code = "Third", Description = "Last becuase alphabetically after Second Advertiser. Sort Order is 99", SortOrder = 99 };

            AssetGroup_Channel_House_Advertiser assetGrpHouseAdvFirst = new AssetGroup_Channel_House_Advertiser() { Id = 1, Advertiser = advFirst, ChannelId = 1, AssetGroupId = 2 };
            AssetGroup_Channel_House_Advertiser assetGrpHouseAdvSecond = new AssetGroup_Channel_House_Advertiser() { Id = 2, Advertiser = advSecond, ChannelId = 1, AssetGroupId = 2 };
            AssetGroup_Channel_House_Advertiser assetGrpHouseAdvLast = new AssetGroup_Channel_House_Advertiser() { Id = 3, Advertiser = advLast,ChannelId = 1, AssetGroupId = 2 };
            // Should not be returned. Channel Id and Asset Group Id do not match where clause.
            AssetGroup_Channel_House_Advertiser assetGrpHouseAdvNotReturned = new AssetGroup_Channel_House_Advertiser() { Id = 4, ChannelId = 3, AssetGroupId = 3 };

            var assetGroupHouseAdvertiser = new List<AssetGroup_Channel_House_Advertiser>() {assetGrpHouseAdvFirst,assetGrpHouseAdvSecond,assetGrpHouseAdvLast};

            mockDropDownListService.Setup(x => x.GetAssetGroupsDropDown(It.IsAny<int>(), It.IsAny<string>())).Returns(assetGroupHouseAdvertiser.AsQueryable());
            #endregion Arrange

            #region Act

            var ddcontroller = new DropDownApiControllerMock(dropDownListService: mockDropDownListService.Object);
            var result = ddcontroller.GetAdvertiserDropDown(1, 2, 1, "username");
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.DropDownList.Count == 3);
            Assert.AreEqual(result.DropDownList.First().Text, "First Advertiser");
            Assert.AreEqual(result.DropDownList.Last().Text, "Third Advertiser");
            #endregion
        }

    }
}
