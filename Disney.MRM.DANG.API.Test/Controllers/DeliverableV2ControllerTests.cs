using System;
using System.Collections.Generic;
using Disney.MRM.DANG.API.Contracts;
using Disney.MRM.DANG.API.Managers;
using Disney.MRM.DANG.API.Managers.Deliverable;
using Disney.MRM.DANG.DataAccess;
using Disney.MRM.DANG.Repository;
using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.Service.Contracts;
using Disney.MRM.DANG.Model;
using Disney.MRM.DANG.Service.Implementations;
using Disney.MRM.DANG.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Linq.Expressions;
using Disney.MRM.DANG.API.Test.MockObject.Service;
using Moq;
using Disney.MRM.DANG.API.Controllers;
using Disney.MRM.DANG.ViewModel.Deliverable;
using Disney.MRM.DANG.ViewModel;
using Disney.MRM.DANG.ViewModel.BulkUpdate;
using AutoMapper;
using Disney.MRM.DANG.API.Test.MockObject.Manager;
using Disney.MRM.DANG.Core.Models.Intergration.Common;
using Disney.MRM.DANG.API.Test.MockObject.Controller;
using Disney.MRM.DANG.API.Test.MockObject.Director;
using System.Text;

namespace Disney.MRM.DANG.API.Test.Controllers
{
    [TestClass]
    public class DeliverableV2ControllerTests
    {
        #region Constants
        const int MRM_USER_ID = 540;
        const string NETWORK_LOGIN = "swna\\TestLogin";
        #endregion

        #region private variables
        private Moq.MockRepository mockRepository;
        private Moq.Mock<IDeliverableServiceV2> mockDeliverableSvcV2;
        private Moq.Mock<ITrackService> mockTrackService;
        private Moq.Mock<IDeliverableRepository> mockDeliverableRepository;
        private Moq.Mock<IDeliverableInternationalDetailRepository> mockIntlDetailDeliverableRepository;
        private Moq.Mock<IActivityTypeActivityStatusRepository> mockActivityTypeActivityStatusRepository;
        private Moq.Mock<IActivityStatusRepository> mockActivityStatusRepository;
        private Moq.Mock<IUnitOfWork> mockUnitOfWork;
        private Moq.Mock<ITrackActivityElementRepository> mockTrackActivityElementRepository;
        private Moq.Mock<IActivityTypeActivityStatusService> mockActivityTypeActivityStatusService;
        private Moq.Mock<ITrackTypeRepository> mockTrackTypeRepositry;
        private Moq.Mock<IAssetGroupChannelHouseAdvertiserRepository> mockAssetGroupChannelHouseAdvertiserRepository;
        private Moq.Mock<IMasterVendorViewRepository> mockmasterVendorViewRepository;
        private Moq.Mock<IDropDownListService> mockDropDownListService;
        private Moq.Mock<IDeliverableBudgetManager> mockDeliverableBudgetManager;
        private Moq.Mock<IDeliverableManager> mockDeliverableManager;
        private Moq.Mock<IDeliverableServiceV2> mockDeliverableServiceV2;
        private Moq.Mock<IDeliverableBudgetService> mockDeliverableBudgetService;
        private Moq.Mock<IDeliverableGeneralInfoManager> mockDeliverableGeneralInfoManager;
        private Moq.Mock<IDeliverableBudgetRepository> mockDeliverableBudgetRepository;
        private Moq.Mock<IDeliverableSummeryofBudgetRepository> mockDeliverableSummeryofBudgetRepository;
        private Moq.Mock<IDeliverable_BusinessAreaRepository> mockDeliverable_BusinessAreaRepository;
        private Moq.Mock<IOffAirDesignRepository> mockOffAirDesignRepository;
        private Moq.Mock<IDeliverableSecondaryTargetRepostiory> mockDeliverableSecondaryTargetRepostiory;
        private Moq.Mock<IDeliverableUserTitleMrmUserRepository> mockDeliverableUserTitleMrmUserRepository;
        private Moq.Mock<IUserTitleRepository> mockUserTitleRepository;
        private Moq.Mock<IDeliverable_TalentRepository> mockDeliverable_TalentRepository;
        private Moq.Mock<IDeliverableGroupRepository> mockDeliverableGroupRepository;
        private Moq.Mock<IDeliverable_WBSElementRepository> mockDeliverable_WBSElementRepository;
        private Moq.Mock<IWBSElementRepository> mockWBSElementRepository;
        private Moq.Mock<IChannelRepository> mockChannelRepository;
        private Moq.Mock<IUserRepository> mockUserRepository;
        private Moq.Mock<IDeliverableDirector> mockDeliverableDirector;
        private Moq.Mock<IIntergrationManager> mockIIntergrationManager;
        private Moq.Mock<IUserService> mockUserservice;
        private Moq.Mock<ITalentRepository> mockTalentRepository;
        private Moq.Mock<IDeliverableDateTypeRepository> mockDeliverableDateTypeRepository;
        private Moq.Mock<IContractRequest_DeliverableRepository> mockContractrequest_DeliverableRepository;
        
        private Moq.Mock<IDeliverable_VendorRepository> mockDeliverableVendorRepository;
        private Moq.Mock<IHomeService> mockHomeService;
        #endregion

        [TestInitialize]
        public void ClassInit()
        {
            mockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
            mockDeliverableSvcV2 = mockRepository.Create<IDeliverableServiceV2>();
            mockTrackService = mockRepository.Create<ITrackService>();
            mockDeliverableRepository = mockRepository.Create<IDeliverableRepository>();
            mockIntlDetailDeliverableRepository = mockRepository.Create<IDeliverableInternationalDetailRepository>();
            mockActivityTypeActivityStatusRepository = mockRepository.Create<IActivityTypeActivityStatusRepository>();
            mockActivityStatusRepository = mockRepository.Create<IActivityStatusRepository>();
            mockUnitOfWork = mockRepository.Create<IUnitOfWork>();
            mockTrackActivityElementRepository = mockRepository.Create<ITrackActivityElementRepository>();
            mockActivityTypeActivityStatusService = mockRepository.Create<IActivityTypeActivityStatusService>();
            mockTrackTypeRepositry = mockRepository.Create<ITrackTypeRepository>();
            mockAssetGroupChannelHouseAdvertiserRepository = mockRepository.Create<IAssetGroupChannelHouseAdvertiserRepository>();
            mockmasterVendorViewRepository = mockRepository.Create<IMasterVendorViewRepository>();
            mockDropDownListService = mockRepository.Create<IDropDownListService>();
            mockDeliverableBudgetManager = mockRepository.Create<IDeliverableBudgetManager>();
            mockDeliverableManager = mockRepository.Create<IDeliverableManager>();
            mockDeliverableServiceV2 = mockRepository.Create<IDeliverableServiceV2>();
            mockDeliverableBudgetService = mockRepository.Create<IDeliverableBudgetService>();
            mockDeliverableGeneralInfoManager = mockRepository.Create<IDeliverableGeneralInfoManager>();
            mockDeliverableBudgetRepository = mockRepository.Create<IDeliverableBudgetRepository>();
            mockDeliverableSummeryofBudgetRepository = mockRepository.Create<IDeliverableSummeryofBudgetRepository>();
            mockDeliverable_BusinessAreaRepository = mockRepository.Create<IDeliverable_BusinessAreaRepository>();
            mockOffAirDesignRepository = mockRepository.Create<IOffAirDesignRepository>();
            mockDeliverableSecondaryTargetRepostiory = mockRepository.Create<IDeliverableSecondaryTargetRepostiory>();
            mockDeliverableUserTitleMrmUserRepository = mockRepository.Create<IDeliverableUserTitleMrmUserRepository>();
            mockUserTitleRepository = mockRepository.Create<IUserTitleRepository>();
            mockDeliverable_TalentRepository = mockRepository.Create<IDeliverable_TalentRepository>();
            mockDeliverableGroupRepository = mockRepository.Create<IDeliverableGroupRepository>();
            mockDeliverable_WBSElementRepository = mockRepository.Create<IDeliverable_WBSElementRepository>();
            mockWBSElementRepository = mockRepository.Create<IWBSElementRepository>();
            mockChannelRepository = mockRepository.Create<IChannelRepository>();
            mockUserRepository = mockRepository.Create<IUserRepository>();
            mockDeliverableDirector = mockRepository.Create<IDeliverableDirector>();
            mockIIntergrationManager = mockRepository.Create<IIntergrationManager>();
            mockUserservice = mockRepository.Create<IUserService>();
            mockTalentRepository = mockRepository.Create<ITalentRepository>();
            mockDeliverableDateTypeRepository = mockRepository.Create<IDeliverableDateTypeRepository>();
            mockContractrequest_DeliverableRepository = mockRepository.Create<IContractRequest_DeliverableRepository>();
            mockDeliverableVendorRepository = mockRepository.Create<IDeliverable_VendorRepository>();
            mockHomeService = mockRepository.Create<IHomeService>();
        }

        [TestMethod]
        public void UpdateTracks_HouseIDAssignment_ShouldReturnNoDeliverableException()
        {
            //Arrange
            string expectedErrorMessage = null;

            Deliverable testDeliverable = new Deliverable()
            {
                Id = -1,
                DeliverableStatusId = 2, //Planned
                DeliverableTypeId = 2,//Bumper
                ProducingDepartmentId = 84,//MRM Team
                Name = "Disney Channel Video Deliverable",
                //ChannelId = 1,//Disney Channel
                DeliverableGroupId = 1,//Video
                IsOffChannelFlag = false,
                BroadcastChannelId = 1, //Disney Channel
                AssetGroupId = 3,//Disney Interstitials
                AdvertiserId = 7,//Disney Generic Promo
            };

            //Act
            try
            {
                var trackService = new TrackServiceMock();
                List<TrackElement> testIntegrationTracks;
                trackService.SaveTracks(testDeliverable.Id, "networklogin", false, out testIntegrationTracks);
            }
            catch (Exception ex)
            {
                expectedErrorMessage = ex.Message;
            }

            //Assert
            Assert.IsNotNull(expectedErrorMessage);
            Assert.AreEqual("Deliverable Id must be more then 0 in SaveTracks method.", expectedErrorMessage);

        }

        [TestMethod]
        public void UpdateTracks_HouseIDAssignment_ShouldReturnTrackElement2Last()
        {
            #region Data Setup
            //ActivityType of TRACK, ActivityStatus of IN-PROCESS,ActivityStatusCategory of LINE
            int testActivityTypeActivityStatusInProcessId = 110;//In-Process
            int testActivityTypeActivityStatusCancelledId = 111;//Cancelled

            DeliverableGroup testDeliverableGroup = new DeliverableGroup()
            {
                Code = "VID"//Video
            };

            Channel testChannel = new Channel()
            {
                Code = "DC"//Disney Channel
            };

            AssetGroup_Channel_House_Advertiser testAssetGroupChannelHouseAdv_First = new AssetGroup_Channel_House_Advertiser()
            {
                Id = 42,
                ChannelId = 5,
                HousePrefix = "I",
                CurrentHouseNumber = 1003,
                AssetGroupId = 9,
                AdvertiserId = 13
            };
            AssetGroup_Channel_House_Advertiser testAssetGroupChannelHouseAdv_Middle = new AssetGroup_Channel_House_Advertiser()
            {
                Id = 42,
                ChannelId = 5,
                HousePrefix = "I",
                CurrentHouseNumber = 1002,
                AssetGroupId = 9,
                AdvertiserId = 13
            };

            AssetGroup_Channel_House_Advertiser testAssetGroupChannelHouseAdv_Last = new AssetGroup_Channel_House_Advertiser()
            {
                Id = 42,
                ChannelId = 5,
                HousePrefix = "I",
                CurrentHouseNumber = 1001,
                AssetGroupId = 9,
                AdvertiserId = 13
            };

            var testAssetGrpChannelHouseAdv = new List<AssetGroup_Channel_House_Advertiser>() { testAssetGroupChannelHouseAdv_Middle, testAssetGroupChannelHouseAdv_Last, testAssetGroupChannelHouseAdv_First };

            ActivityStatus testActivityStatus = new ActivityStatus()
            {
                IsActiveFlag = true,
                Code = Core.Constants.ActivityStatusCode.IN_PROCESS//In-Process
            };

            DeliverableStatus testDeliverableStatus = new DeliverableStatus()
            {
                Code = Constants.DeliverableStatusCode.PLANNED//Planned
            };

            TrackType testTextedTrackType = new TrackType()
            {
                Id = 12,
                Name = "Texted",
                IsActiveFlag = true,
                Code = "TXTD",
                IsHouseNumberRequiredFlag = true
            };

            DeliverableInternationalDetail testDeliverableInternationalDetail = new DeliverableInternationalDetail
            {
                Id = 1,
                DeliverableId = 1,
                Delivery = false
            };

            ICollection<DeliverableInternationalDetail> testDeliverableInternationalDetailList = new HashSet<DeliverableInternationalDetail>();
            testDeliverableInternationalDetailList.Add(testDeliverableInternationalDetail);

            Deliverable testDeliverable = new Deliverable()
            {
                Id = 1,
                DeliverableStatus = testDeliverableStatus,
                DeliverableStatusId = 2, //Planned
                DeliverableTypeId = 2,//Bumper
                ProducingDepartmentId = 84,//MRM Team
                Name = "Disney Channel Video Deliverable",
                //ChannelId = 1,//Disney Channel
                DeliverableGroupId = 1,//Video
                DeliverableGroup = testDeliverableGroup,
                Channel = testChannel,
                IsOffChannelFlag = false,
                BroadcastChannelId = 1, //Disney Channel
                AssetGroupId = 3,//Disney Interstitials
                AdvertiserId = 7,//Disney Generic Promo
                DeliverableInternationalDetail = testDeliverableInternationalDetailList
            };

            TrackElement testTrackElement1 = new TrackElement()
            {
                Id = 1,
                TagName = "TE Unit Test 1",
                Name = "TE Unit Test Should Be Middle",
                StartDate = new DateTime(2016, 06, 12),
                HouseNumber = null,
                TrackTypeId = 12, //Texted Track
                DeliverableId = 1,
                ActivityStatusId = 39,//In Process
                IsActiveFlag = true,
                IsInternationalFlag = false,
                TrackType = testTextedTrackType
            };
            TrackElement testTrackElement2 = new TrackElement()
            {
                Id = 2,
                TagName = "TE Unit Test 2",
                Name = "TE Unit Test Should Be Last",
                StartDate = (DateTime?)null,
                HouseNumber = null,
                TrackTypeId = 12, //Texted Track
                DeliverableId = 1,
                ActivityStatusId = 39,//In Process
                IsActiveFlag = true,
                IsInternationalFlag = false,
                TrackType = testTextedTrackType
            };
            TrackElement testTrackElement3 = new TrackElement()
            {
                Id = 3,
                TagName = "TE Unit Test 3",
                Name = "TE Unit Test Should Be First",
                StartDate = new DateTime(2016, 06, 01),
                HouseNumber = null,
                TrackTypeId = 12, //Texted Track
                DeliverableId = 1,
                ActivityStatusId = 39,//In Process
                IsActiveFlag = true,
                IsInternationalFlag = false,
                TrackType = testTextedTrackType
            };

            //Build Texted Track List
            List<TrackElement> testTextedTrackElementList = new List<TrackElement> { };
            testTextedTrackElementList.Add(testTrackElement2);
            testTextedTrackElementList.Add(testTrackElement1);
            testTextedTrackElementList.Add(testTrackElement3);
            IQueryable<TrackElement> testTextedTrackElementQueryable = testTextedTrackElementList.AsQueryable();

            //Empty List
            List<TrackElement> testEmptyTrackElementList = new List<TrackElement> { };
            IQueryable<TrackElement> testEmptyTrackElementQueryable = testEmptyTrackElementList.AsQueryable();

            #endregion

            #region Mocking
            mockTrackActivityElementRepository.SetupSequence(tae => tae.GetAll())
                .Returns(testEmptyTrackElementQueryable)
                .Returns(testTextedTrackElementQueryable);

            mockTrackActivityElementRepository.Setup(tae => tae.Update(It.IsAny<TrackElement>()));

            mockDeliverableRepository.Setup(del => del.GetDeliverableById(It.IsAny<int>())).Returns(testDeliverable);

            mockActivityTypeActivityStatusService.SetupSequence(act => act.GetActivityStatusIdByActvityType(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(testActivityTypeActivityStatusCancelledId)
                .Returns(testActivityTypeActivityStatusInProcessId);

            mockActivityTypeActivityStatusService.Setup(act => act.GetActivityStatusByStatusId(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(testActivityStatus);

            mockTrackTypeRepositry.Setup(tt => tt.GetById(It.IsAny<int>())).Returns(testTextedTrackType);

            mockAssetGroupChannelHouseAdvertiserRepository.Setup(del => del.GetMany(It.IsAny<Expression<Func<AssetGroup_Channel_House_Advertiser, bool>>>())).Returns(
           (Expression<Func<AssetGroup_Channel_House_Advertiser, bool>> expr) => testAssetGrpChannelHouseAdv);

            mockAssetGroupChannelHouseAdvertiserRepository.Setup(cha => cha.Add(It.IsAny<AssetGroup_Channel_House_Advertiser>()))
                                               .Returns((AssetGroup_Channel_House_Advertiser)null);

            mockAssetGroupChannelHouseAdvertiserRepository.Setup(cha => cha.Update(It.IsAny<AssetGroup_Channel_House_Advertiser>()));

            mockUnitOfWork.Setup(uow => uow.Commit());

            #endregion

            #region Act
            //Act
            var trackService = new TrackServiceMock(itrackRepository: mockTrackActivityElementRepository.Object, ideliverableRepository: mockDeliverableRepository.Object,
                                                    iassetGroupChannelHouseAdvertiserRepository: mockAssetGroupChannelHouseAdvertiserRepository.Object,
                                                    iactivityTypeActivityStatusService: mockActivityTypeActivityStatusService.Object, iunitOfWork: mockUnitOfWork.Object);

            List<TrackElement> testIntegrationTracks;
            trackService.SaveTracks(testDeliverable.Id, "networklogin", false, out testIntegrationTracks);

            #endregion

            #region Assert
            //Assert
            Assert.IsNotNull(testIntegrationTracks);
            var result = testIntegrationTracks.LastOrDefault();
            Assert.IsNotNull(result);
            Assert.IsTrue(testIntegrationTracks.Count == 3);
            Assert.IsTrue(result.Name.Equals("TE Unit Test Should Be Last"));
            Assert.AreEqual(result.HouseNumber,"I1006");
            #endregion
        }

        [TestMethod]
        public void UpdateTracks_HouseIDAssignment_ShouldReturnTrackElement3First()
        {
            #region Data Setup
            //ActivityType of TRACK, ActivityStatus of IN-PROCESS,ActivityStatusCategory of LINE
            int testActivityTypeActivityStatusInProcessId = 110;//In-Process
            int testActivityTypeActivityStatusCancelledId = 111;//Cancelled

            DeliverableGroup testDeliverableGroup = new DeliverableGroup()
            {
                Code = "VID"//Video
            };

            Channel testChannel = new Channel()
            {
                Code = "DC"//Disney Channel
            };

            AssetGroup_Channel_House_Advertiser testAssetGroupChannelHouseAdv_First = new AssetGroup_Channel_House_Advertiser()
            {
                Id = 42,
                ChannelId = 5,
                HousePrefix = "I",
                CurrentHouseNumber = 1003,
                AssetGroupId = 9,
                AdvertiserId = 13
            };
            AssetGroup_Channel_House_Advertiser testAssetGroupChannelHouseAdv_Middle = new AssetGroup_Channel_House_Advertiser()
            {
                Id = 42,
                ChannelId = 5,
                HousePrefix = "I",
                CurrentHouseNumber = 1002,
                AssetGroupId = 9,
                AdvertiserId = 13
            };

            AssetGroup_Channel_House_Advertiser testAssetGroupChannelHouseAdv_Last = new AssetGroup_Channel_House_Advertiser()
            {
                Id = 42,
                ChannelId = 5,
                HousePrefix = "I",
                CurrentHouseNumber = 1001,
                AssetGroupId = 9,
                AdvertiserId = 13
            };

            var testAssetGrpChannelHouseAdv = new List<AssetGroup_Channel_House_Advertiser>() { testAssetGroupChannelHouseAdv_Middle, testAssetGroupChannelHouseAdv_Last, testAssetGroupChannelHouseAdv_First };

            ActivityStatus testActivityStatus = new ActivityStatus()
            {
                IsActiveFlag = true,
                Code = Core.Constants.ActivityStatusCode.IN_PROCESS//In-Process
            };

            DeliverableStatus testDeliverableStatus = new DeliverableStatus()
            {
                Code = Constants.DeliverableStatusCode.PLANNED//Planned
            };

            TrackType testTextedTrackType = new TrackType()
            {
                Id = 12,
                Name = "Texted",
                IsActiveFlag = true,
                Code = "TXTD",
                IsHouseNumberRequiredFlag = true
            };

            DeliverableInternationalDetail testDeliverableInternationalDetail = new DeliverableInternationalDetail
            {
                Id = 1,
                DeliverableId = 1,
                Delivery = false
            };

            ICollection<DeliverableInternationalDetail> testDeliverableInternationalDetailList = new HashSet<DeliverableInternationalDetail>();
            testDeliverableInternationalDetailList.Add(testDeliverableInternationalDetail);

            Deliverable testDeliverable = new Deliverable()
            {
                Id = 1,
                DeliverableStatus = testDeliverableStatus,
                DeliverableStatusId = 2, //Planned
                DeliverableTypeId = 2,//Bumper
                ProducingDepartmentId = 84,//MRM Team
                Name = "Disney Channel Video Deliverable",
                //ChannelId = 1,//Disney Channel
                DeliverableGroupId = 1,//Video
                DeliverableGroup = testDeliverableGroup,
                Channel = testChannel,
                IsOffChannelFlag = false,
                BroadcastChannelId = 1, //Disney Channel
                AssetGroupId = 3,//Disney Interstitials
                AdvertiserId = 7,//Disney Generic Promo
                DeliverableInternationalDetail = testDeliverableInternationalDetailList
            };

            TrackElement testTrackElement1 = new TrackElement()
            {
                Id = 1,
                TagName = "TE Unit Test 1",
                Name = "TE Unit Test 1",
                StartDate = new DateTime(2016, 06, 12),
                HouseNumber = null,
                TrackTypeId = 12, //Texted Track
                DeliverableId = 1,
                ActivityStatusId = 39,//In Process
                IsActiveFlag = true,
                IsInternationalFlag = false,
                TrackType = testTextedTrackType
            };
            TrackElement testTrackElement2 = new TrackElement()
            {
                Id = 2,
                TagName = "TE Unit Test 2",
                Name = "TE Unit Test 2",
                StartDate = (DateTime?)null,
                HouseNumber = null,
                TrackTypeId = 12, //Texted Track
                DeliverableId = 1,
                ActivityStatusId = 39,//In Process
                IsActiveFlag = true,
                IsInternationalFlag = false,
                TrackType = testTextedTrackType
            };
            TrackElement testTrackElement3 = new TrackElement()
            {
                Id = 3,
                TagName = "TE Unit Test 3",
                Name = "TE Unit Test Should Be First",
                StartDate = new DateTime(2016, 06, 01),
                HouseNumber = null,
                TrackTypeId = 12, //Texted Track
                DeliverableId = 1,
                ActivityStatusId = 39,//In Process
                IsActiveFlag = true,
                IsInternationalFlag = false,
                TrackType = testTextedTrackType
            };

            //Build Texted Track List
            List<TrackElement> testTextedTrackElementList = new List<TrackElement> { };
            testTextedTrackElementList.Add(testTrackElement2);
            testTextedTrackElementList.Add(testTrackElement1);
            testTextedTrackElementList.Add(testTrackElement3);
            IQueryable<TrackElement> testTextedTrackElementQueryable = testTextedTrackElementList.AsQueryable();

            //Empty List
            List<TrackElement> testEmptyTrackElementList = new List<TrackElement> { };
            IQueryable<TrackElement> testEmptyTrackElementQueryable = testEmptyTrackElementList.AsQueryable();

            #endregion

            #region Mocking
            mockTrackActivityElementRepository.SetupSequence(tae => tae.GetAll())
                .Returns(testEmptyTrackElementQueryable)
                .Returns(testTextedTrackElementQueryable);

            mockTrackActivityElementRepository.Setup(tae => tae.Update(It.IsAny<TrackElement>()));

            mockDeliverableRepository.Setup(del => del.GetDeliverableById(It.IsAny<int>())).Returns(testDeliverable);

            List<TrackElement> testIntegrationTracks;

            mockActivityTypeActivityStatusService.SetupSequence(act => act.GetActivityStatusIdByActvityType(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(testActivityTypeActivityStatusCancelledId)
                .Returns(testActivityTypeActivityStatusInProcessId);

            mockActivityTypeActivityStatusService.Setup(act => act.GetActivityStatusByStatusId(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(testActivityStatus);

            mockTrackTypeRepositry.Setup(tt => tt.GetById(It.IsAny<int>())).Returns(testTextedTrackType);

            mockAssetGroupChannelHouseAdvertiserRepository.Setup(del => del.GetMany(It.IsAny<Expression<Func<AssetGroup_Channel_House_Advertiser, bool>>>())).Returns(
            (Expression<Func<AssetGroup_Channel_House_Advertiser, bool>> expr) => testAssetGrpChannelHouseAdv);

            mockAssetGroupChannelHouseAdvertiserRepository.Setup(cha => cha.Add(It.IsAny<AssetGroup_Channel_House_Advertiser>()))
                                               .Returns((AssetGroup_Channel_House_Advertiser)null);

            mockAssetGroupChannelHouseAdvertiserRepository.Setup(cha => cha.Update(It.IsAny<AssetGroup_Channel_House_Advertiser>()));

            mockUnitOfWork.Setup(uow => uow.Commit());
            #endregion

            #region Act
            //Act
            var trackService = new TrackServiceMock(itrackRepository: mockTrackActivityElementRepository.Object, ideliverableRepository: mockDeliverableRepository.Object,
                                                    iassetGroupChannelHouseAdvertiserRepository: mockAssetGroupChannelHouseAdvertiserRepository.Object,
                                                    iactivityTypeActivityStatusService: mockActivityTypeActivityStatusService.Object, iunitOfWork: mockUnitOfWork.Object);
            trackService.SaveTracks(testDeliverable.Id, "networklogin", false, out testIntegrationTracks);

            #endregion

            #region Assert
            //Assert
            Assert.IsNotNull(testIntegrationTracks);
            var result = testIntegrationTracks.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.IsTrue(testIntegrationTracks.Count == 3);
            Assert.IsTrue(result.Name.Equals("TE Unit Test Should Be First"));
            #endregion
        }

        [TestMethod]
        public void UpdateTracks_HouseIDAssignment_ShouldSetTrackID4InternationalFlagFalse()
        {
            #region Data Setup
            //ActivityType of TRACK, ActivityStatus of IN-PROCESS,ActivityStatusCategory of LINE
            int testActivityTypeActivityStatusInProcessId = 110;//In-Process
            int testActivityTypeActivityStatusCancelledId = 111;//Cancelled

            DeliverableGroup testDeliverableGroup = new DeliverableGroup()
            {
                Code = "VID"//Video
            };

            Channel testChannel = new Channel()
            {
                Code = "DC"//Disney Channel
            };

            AssetGroup_Channel_House_Advertiser testAssetGroupChannelHouseAdv_First = new AssetGroup_Channel_House_Advertiser()
            {
                Id = 42,
                ChannelId = 5,
                HousePrefix = "I",
                CurrentHouseNumber = 1003,
                AssetGroupId = 9,
                AdvertiserId = 13
            };
            AssetGroup_Channel_House_Advertiser testAssetGroupChannelHouseAdv_Middle = new AssetGroup_Channel_House_Advertiser()
            {
                Id = 42,
                ChannelId = 5,
                HousePrefix = "I",
                CurrentHouseNumber = 1002,
                AssetGroupId = 9,
                AdvertiserId = 13
            };

            AssetGroup_Channel_House_Advertiser testAssetGroupChannelHouseAdv_Last = new AssetGroup_Channel_House_Advertiser()
            {
                Id = 42,
                ChannelId = 5,
                HousePrefix = "I",
                CurrentHouseNumber = 1001,
                AssetGroupId = 9,
                AdvertiserId = 13
            };

            var testAssetGrpChannelHouseAdv = new List<AssetGroup_Channel_House_Advertiser>() { testAssetGroupChannelHouseAdv_Middle, testAssetGroupChannelHouseAdv_Last, testAssetGroupChannelHouseAdv_First };

            ActivityStatus testActivityStatus = new ActivityStatus()
            {
                IsActiveFlag = true,
                Code = Core.Constants.ActivityStatusCode.IN_PROCESS//In-Process
            };

            DeliverableStatus testDeliverableStatus = new DeliverableStatus()
            {
                Code = Constants.DeliverableStatusCode.PLANNED//Planned
            };

            TrackType testTextedTrackType = new TrackType()
            {
                Id = 12,
                Name = "Texted",
                IsActiveFlag = true,
                Code = Constants.TrackTypeCode.TEXTED,
                IsHouseNumberRequiredFlag = true
            };

            TrackType testTextlessTrackType = new TrackType()
            {
                Id = 12,
                Name = "TextLess",
                IsActiveFlag = true,
                Code = Constants.TrackTypeCode.TEXTLESS,
                IsHouseNumberRequiredFlag = true
            };

            DeliverableInternationalDetail testDeliverableInternationalDetail = new DeliverableInternationalDetail
            {
                Id = 1,
                DeliverableId = 1,
                Delivery = true
            };

            ICollection<DeliverableInternationalDetail> testDeliverableInternationalDetailList = new HashSet<DeliverableInternationalDetail>();
            testDeliverableInternationalDetailList.Add(testDeliverableInternationalDetail);

            Deliverable testDeliverable = new Deliverable()
            {
                Id = 1,
                DeliverableStatus = testDeliverableStatus,
                DeliverableStatusId = 2, //Planned
                DeliverableTypeId = 2,//Bumper
                ProducingDepartmentId = 84,//MRM Team
                Name = "Disney Channel Video Deliverable",
                //ChannelId = 1,//Disney Channel
                DeliverableGroupId = 1,//Video
                DeliverableGroup = testDeliverableGroup,
                Channel = testChannel,
                IsOffChannelFlag = false,
                BroadcastChannelId = 1, //Disney Channel
                AssetGroupId = 3,//Disney Interstitials
                AdvertiserId = 7,//Disney Generic Promo
                DeliverableInternationalDetail = testDeliverableInternationalDetailList
            };

            TrackElement testTrackElement1 = new TrackElement()
            {
                Id = 1,
                TagName = "TE Unit Test 1",
                Name = "TE Unit Test 1",
                StartDate = new DateTime(2016, 06, 12),
                HouseNumber = null,
                TrackTypeId = 12, //Texted Track
                DeliverableId = 1,
                ActivityStatusId = 39,//In Process
                IsActiveFlag = true,
                IsInternationalFlag = false,
                TrackType = testTextedTrackType
            };
            TrackElement testTrackElement2 = new TrackElement()
            {
                Id = 2,
                TagName = "TE Unit Test 2",
                Name = "TE Unit Test 2",
                StartDate = new DateTime(2016, 06, 02),
                HouseNumber = null,
                TrackTypeId = 11, //Textless Track
                DeliverableId = 1,
                ActivityStatusId = 39,//In Process
                IsActiveFlag = true,
                IsInternationalFlag = false,
                TrackType = testTextlessTrackType
            };
            TrackElement testTrackElement3 = new TrackElement()
            {
                Id = 3,
                TagName = "TE Unit Test 3",
                Name = "TE Unit Test Should Be First",
                StartDate = new DateTime(2016, 06, 01),
                HouseNumber = null,
                TrackTypeId = 11, //Textless Track
                DeliverableId = 1,
                ActivityStatusId = 39,//In Process
                IsActiveFlag = true,
                IsInternationalFlag = false,
                TrackType = testTextlessTrackType
            };

            TrackElement testTrackElement4 = new TrackElement()
            {
                Id = 4,
                TagName = "TE Unit Test 4",
                Name = "TE Unit Test 4",
                StartDate = (DateTime?)null,
                HouseNumber = null,
                TrackTypeId = 11, //Textless Track
                DeliverableId = 1,
                ActivityStatusId = 39,//In Process
                IsActiveFlag = true,
                IsInternationalFlag = false,
                TrackType = testTextlessTrackType
            };

            //Build Texted Track List
            List<TrackElement> testTextedTrackElementList = new List<TrackElement> { };
            testTextedTrackElementList.Add(testTrackElement2);
            testTextedTrackElementList.Add(testTrackElement1);
            testTextedTrackElementList.Add(testTrackElement3);
            testTextedTrackElementList.Add(testTrackElement4);
            IQueryable<TrackElement> testTextedTrackElementQueryable = testTextedTrackElementList.AsQueryable();

            //Empty List
            List<TrackElement> testEmptyTrackElementList = new List<TrackElement> { };
            IQueryable<TrackElement> testEmptyTrackElementQueryable = testEmptyTrackElementList.AsQueryable();

            #endregion

            #region Mocking
            mockTrackActivityElementRepository.SetupSequence(tae => tae.GetAll())
                .Returns(testEmptyTrackElementQueryable)
                .Returns(testTextedTrackElementQueryable);

            mockTrackActivityElementRepository.Setup(tae => tae.Update(It.IsAny<TrackElement>()));

            mockDeliverableRepository.Setup(del => del.GetDeliverableById(It.IsAny<int>())).Returns(testDeliverable);

            List<TrackElement> testIntegrationTracks;

            mockActivityTypeActivityStatusService.SetupSequence(act => act.GetActivityStatusIdByActvityType(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(testActivityTypeActivityStatusCancelledId)
                .Returns(testActivityTypeActivityStatusInProcessId);

            mockActivityTypeActivityStatusService.Setup(act => act.GetActivityStatusByStatusId(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(testActivityStatus);

            mockTrackTypeRepositry.Setup(tt => tt.GetById(It.IsAny<int>())).Returns(testTextedTrackType);

            mockAssetGroupChannelHouseAdvertiserRepository.Setup(del => del.GetMany(It.IsAny<Expression<Func<AssetGroup_Channel_House_Advertiser, bool>>>())).Returns(
           (Expression<Func<AssetGroup_Channel_House_Advertiser, bool>> expr) => testAssetGrpChannelHouseAdv);

            mockAssetGroupChannelHouseAdvertiserRepository.Setup(cha => cha.Add(It.IsAny<AssetGroup_Channel_House_Advertiser>()))
                                               .Returns((AssetGroup_Channel_House_Advertiser)null);

            mockAssetGroupChannelHouseAdvertiserRepository.Setup(cha => cha.Update(It.IsAny<AssetGroup_Channel_House_Advertiser>()));

            mockUnitOfWork.Setup(uow => uow.Commit());
            #endregion

            #region Act
            //Act
            var trackService = new TrackServiceMock(itrackRepository: mockTrackActivityElementRepository.Object, ideliverableRepository: mockDeliverableRepository.Object,
                                                    iassetGroupChannelHouseAdvertiserRepository: mockAssetGroupChannelHouseAdvertiserRepository.Object,
                                                    iactivityTypeActivityStatusService: mockActivityTypeActivityStatusService.Object, iunitOfWork: mockUnitOfWork.Object);
            trackService.SaveTracks(testDeliverable.Id, "networklogin", false, out testIntegrationTracks);

            #endregion

            #region Assert
            //Assert
            Assert.IsNotNull(testIntegrationTracks);
            Assert.IsTrue(testIntegrationTracks.ElementAt(3).IsInternationalFlag == false);
            #endregion
        }

        [TestMethod]
        public void GetDeliverableInternationalByDeliverableId_ShouldSetDeliveryToTrue()
        {
            DeliverableInternationalDetail dIntlDetailResult = null;

            #region Data Setup
            //Arrange
            ActivityType testActivityTypeJR = new ActivityType()
            {
                Code = "INTLJ",
                Name = "International Jellyroll",
                IsActiveFlag = true
            };

            ActivityType testActivityTypeDCWMAX = new ActivityType()
            {
                Code = "INTLD",
                Name = "International DCWMAX",
                IsActiveFlag = true
            };

            ActivityStatus testActivityStatus = new ActivityStatus()
            {
                IsActiveFlag = true,
                Code = "PEND"//Pending
            };

            ActivityStatusCategory testActivityCategory = new ActivityStatusCategory()
            {
                Code = "HDR"
            };

            ActivityType_ActivityStatus testJRPendingActivityTypeActivityStatus = new ActivityType_ActivityStatus()
            {
                IsActiveFlag = true,
                ActivityStatus = testActivityStatus,
                ActivityStatusCategory = testActivityCategory,
                ActivityType = testActivityTypeJR
            };

            ActivityType_ActivityStatus testDCWMAXPendingActivityTypeActivityStatus = new ActivityType_ActivityStatus()
            {
                IsActiveFlag = true,
                ActivityStatus = testActivityStatus,
                ActivityStatusCategory = testActivityCategory,
                ActivityType = testActivityTypeDCWMAX
            };

            DeliverableGroup testDeliverableGroup = new DeliverableGroup()
            {
                Code = "OFF"//Non-Video
            };

            Channel testChannel = new Channel()
            {
                Code = "DC"//Disney Channel
            };

            Deliverable testDeliverable = new Deliverable()
            {
                Id = 1,
                DeliverableTypeId = 94,//Affiliate Marketing
                ProducingDepartmentId = 84,//MRM Team
                Name = "Disney Channel Non-Video Deliverable",
                DeliverableGroupId = 3,//Non-Video
                DeliverableGroup = testDeliverableGroup
            };

            #endregion

            #region Mocking
            mockDeliverableRepository.Setup(del => del.GetSingle(It.IsAny<Expression<Func<Deliverable, bool>>>())).Returns(
            (Expression<Func<Deliverable, bool>> expr) => testDeliverable);

            mockIntlDetailDeliverableRepository.SetupSequence(intl => intl.GetSingle(It.IsAny<Expression<Func<DeliverableInternationalDetail, bool>>>()))
            .Returns(null)
            .Returns(new DeliverableInternationalDetail());

            mockIntlDetailDeliverableRepository.Setup(intl => intl.Add(It.IsAny<DeliverableInternationalDetail>()))
                                               .Returns(new DeliverableInternationalDetail())
                                               .Callback<DeliverableInternationalDetail>(intl => dIntlDetailResult = intl);

            mockActivityTypeActivityStatusRepository.SetupSequence(activity => activity.GetSingle(It.IsAny<Expression<Func<ActivityType_ActivityStatus, bool>>>()))
                                                    .Returns(testJRPendingActivityTypeActivityStatus)
                                                    .Returns(testDCWMAXPendingActivityTypeActivityStatus);

            mockActivityStatusRepository.Setup(activityStatus => activityStatus.GetSingle(It.IsAny<Expression<Func<ActivityStatus, bool>>>())).Returns(
            (Expression<Func<ActivityStatus, bool>> expr) => testActivityStatus);

            mockChannelRepository.Setup(channel => channel.GetSingle(It.IsAny<Expression<Func<Channel, bool>>>())).Returns(
            (Expression<Func<Channel, bool>> expr) => testChannel);

            var deliverableServiceV2 = new DeliverableServiceV2Mock(deliverableRepository: mockDeliverableRepository.Object, deliverableInternationalDetailRepository: mockIntlDetailDeliverableRepository.Object, activityTypeActivityStatusRepository: mockActivityTypeActivityStatusRepository.Object,
                                                                    activityStatusRepository: mockActivityStatusRepository.Object, iunitOfWork: mockUnitOfWork.Object, channelRepository: mockChannelRepository.Object);


            mockUnitOfWork.Setup(uow => uow.Commit());
            #endregion

            //Act
            var result = deliverableServiceV2.GetDeliverableInternationalDetailByDeliverableId(testDeliverable.Id, 484, "networklogin");

            //Assert
            Assert.IsTrue(dIntlDetailResult.Delivery);
        }

        [TestMethod]
        public void GetDeliverableInternationalByDeliverableId_ShouldSetDeliveryToFalse()
        {
            DeliverableInternationalDetail dIntlDetailResult = null;

            #region Data Setup
            //Arrange
            ActivityType testActivityTypeJR = new ActivityType()
            {
                Code = "INTLJ",
                Name = "International Jellyroll",
                IsActiveFlag = true
            };

            ActivityType testActivityTypeDCWMAX = new ActivityType()
            {
                Code = "INTLD",
                Name = "International DCWMAX",
                IsActiveFlag = true
            };

            ActivityStatus testActivityStatus = new ActivityStatus()
            {
                IsActiveFlag = true,
                Code = "PEND"//Pending
            };

            ActivityStatusCategory testActivityCategory = new ActivityStatusCategory()
            {
                Code = "HDR"
            };

            ActivityType_ActivityStatus testJRPendingActivityTypeActivityStatus = new ActivityType_ActivityStatus()
            {
                IsActiveFlag = true,
                ActivityStatus = testActivityStatus,
                ActivityStatusCategory = testActivityCategory,
                ActivityType = testActivityTypeJR
            };

            ActivityType_ActivityStatus testDCWMAXPendingActivityTypeActivityStatus = new ActivityType_ActivityStatus()
            {
                IsActiveFlag = true,
                ActivityStatus = testActivityStatus,
                ActivityStatusCategory = testActivityCategory,
                ActivityType = testActivityTypeDCWMAX
            };

            DeliverableGroup testDeliverableGroup = new DeliverableGroup()
            {
                Code = "VID"//Video
            };

            Channel testChannel = new Channel()
            {
                Code = "ROM"//Radio Disney
            };

            Deliverable testDeliverable = new Deliverable()
            {
                Id = 1,
                DeliverableTypeId = 94,//Affiliate Marketing
                ProducingDepartmentId = 84,//MRM Team
                Name = "Radio Disney Video Deliverable",
                DeliverableGroupId = 2,//Video
                DeliverableGroup = testDeliverableGroup,
            };

            #endregion

            #region Mocking
            mockDeliverableRepository.Setup(del => del.GetSingle(It.IsAny<Expression<Func<Deliverable, bool>>>())).Returns(
            (Expression<Func<Deliverable, bool>> expr) => testDeliverable);

            mockIntlDetailDeliverableRepository.SetupSequence(intl => intl.GetSingle(It.IsAny<Expression<Func<DeliverableInternationalDetail, bool>>>()))
            .Returns(null)
            .Returns(new DeliverableInternationalDetail());

            mockIntlDetailDeliverableRepository.Setup(intl => intl.Add(It.IsAny<DeliverableInternationalDetail>()))
                                               .Returns(new DeliverableInternationalDetail())
                                               .Callback<DeliverableInternationalDetail>(intl => dIntlDetailResult = intl);

            mockActivityTypeActivityStatusRepository.SetupSequence(activity => activity.GetSingle(It.IsAny<Expression<Func<ActivityType_ActivityStatus, bool>>>()))
                                                    .Returns(testJRPendingActivityTypeActivityStatus)
                                                    .Returns(testDCWMAXPendingActivityTypeActivityStatus);

            mockActivityStatusRepository.Setup(activityStatus => activityStatus.GetSingle(It.IsAny<Expression<Func<ActivityStatus, bool>>>())).Returns(
            (Expression<Func<ActivityStatus, bool>> expr) => testActivityStatus);

            mockChannelRepository.Setup(channel => channel.GetSingle(It.IsAny<Expression<Func<Channel, bool>>>())).Returns(
            (Expression<Func<Channel, bool>> expr) => testChannel);

            var deliverableServiceV2 = new DeliverableServiceV2Mock(deliverableRepository: mockDeliverableRepository.Object, deliverableInternationalDetailRepository: mockIntlDetailDeliverableRepository.Object, activityTypeActivityStatusRepository: mockActivityTypeActivityStatusRepository.Object,
                                                                    activityStatusRepository: mockActivityStatusRepository.Object, iunitOfWork: mockUnitOfWork.Object, channelRepository: mockChannelRepository.Object);

            mockUnitOfWork.Setup(uow => uow.Commit());
            #endregion

            //Act
            var result = deliverableServiceV2.GetDeliverableInternationalDetailByDeliverableId(testDeliverable.Id, 0, "networklogin");

            //Assert
            Assert.IsFalse(dIntlDetailResult.Delivery);
        }

        #region MRM-164
        //MRM-164 : Initial Budget Amount is saved from SaveDeliverableBudget method in Manager
        [TestMethod]
        public void SaveDeliverableBudget_IntialBudgetAmount_Manager()
        {
            #region Data Setup

            DeliverableMultiWBSModel deliverableMultiWBSModel = new DeliverableMultiWBSModel()
            {
                InitialBudgetAmount = 123,
                DeliverableId = 140000,
                PrimaryWBSElementId = 2,
                IsLaunchFlag = false
            };
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                InitialBudgetAmount = 123,
                DeliverableId = 140000,
                IsLaunchFlag = true,
                PrimaryWBSElementId = 2, //wbsElement2.Id
            };

            DeliverableMultiWBSViewModel deliverableMultiWBSViewModelout = new DeliverableMultiWBSViewModel();

            DeliverableBudget deliverableBudget1 = new DeliverableBudget()
            {
                Id = 1,
                CreatedDateTime = DateTime.Now,
                DeliverableBudgetUniqueID = new Guid()
            };

            DeliverableBudget deliverableBudget2 = new DeliverableBudget()
            {
                Id = 1,
                CreatedDateTime = DateTime.Now,
                DeliverableBudgetUniqueID = new Guid()
            };

            List<DeliverableBudget> deliverableBudgetList = new List<DeliverableBudget>();
            deliverableBudgetList.Add(deliverableBudget1);
            deliverableBudgetList.Add(deliverableBudget2);

            deliverableMultiWBSModel.Budgets = deliverableBudgetList;
            #endregion

            #region Mocking

            //deliverableServiceV2

            Deliverable testDeliverable = null;
            mockDeliverableServiceV2.Setup(x => x.GetDeliverableById(It.IsAny<int>(), It.IsAny<string>())).Returns(testDeliverable);

            mockDeliverableBudgetManager.SetupSequence(a => a.SaveDeliverableBudget(It.IsAny<DeliverableBudgetViewModel>(), It.IsAny<int>(), It.IsAny<string>()))
           .Returns(new Core.Models.Intergration.Common.IntergrationOutBound())
           .Returns(null);

            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>())).Returns(1);

            mockDeliverableBudgetService.Setup(x => x.SaveDeliverableBudget(It.IsAny<DeliverableMultiWBSModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(deliverableMultiWBSModel);
            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>())).Returns(1);

            #endregion

            var DeliverableBudgetManagerMock = new DeliverableBudgetManagerMock(deliverableBudgetService: mockDeliverableBudgetService.Object
                , deliverableServiceV2: mockDeliverableServiceV2.Object);

            //Act
            var result = DeliverableBudgetManagerMock.SaveDeliverableBudget(deliverableMultiWBSViewModel, 540, "testlogin",out deliverableMultiWBSViewModelout);

            Assert.IsTrue(result != null);
        }

        //MRM-164 : Initial Budget Amount is saved from SaveDeliverableBudget method in Service
        [TestMethod]
        public void SaveDeliverableBudget_IntialBudgetAmount_Service()
        {
            #region Data Setup

            DeliverableMultiWBSModel deliverableMultiWBSModel = new DeliverableMultiWBSModel()
            {
                InitialBudgetAmount = 123,
                DeliverableId = 140000,
                PrimaryWBSElementId = 2,
                IsLaunchFlag = false,
                Budgets = new List<DeliverableBudget>()
            };
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                InitialBudgetAmount = 123,
                DeliverableId = 140000,
                IsLaunchFlag = true,
                PrimaryWBSElementId = 2, //wbsElement2.Id
            };
            Deliverable testDeliverable = new Deliverable()
            {
                Id = 140000,
                DeliverableTypeId = 94,//Affiliate Marketing
                ProducingDepartmentId = 84,//MRM Team
                Name = "Radio Disney Video Deliverable",
                DeliverableGroupId = 2,//Video
                ClipDeliverMasterVendorId = 108,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true
            };
            WBSElement wbsElement1 = new WBSElement() { Id = 1, FullWBSNumber = "123456.011.001", TypeOfWorkId = 1000, };
            DeliverableBudget deliverableBud1 = new DeliverableBudget()
            {
                Id = 1,
                DeliverableId = 140000,
                MasterVendorId = 1212,
                WBSElementId = wbsElement1.Id,
                ProductionMethodTypeId = 1,
            };
            MRMUser mrmUser = new MRMUser()
            {
                Id=1255,
                FirstName ="Test",
                LastName = "User"
            };
            #endregion

            #region Mocking

            //deliverableServiceV2



            List<DeliverableBudget> BudgetLists = new List<DeliverableBudget>();
            BudgetLists.Add(deliverableBud1);
            mockDeliverableRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(testDeliverable);
            mockDeliverableRepository.Setup(x => x.Update(It.IsAny<Deliverable>()));
            mockDeliverableBudgetRepository.Setup(x => x.Delete(It.IsAny<DeliverableBudget>()));
            mockDeliverableBudgetRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableBudget, bool>>>())).Returns(BudgetLists);
            mockDeliverableBudgetRepository.Setup(x => x.Add(It.IsAny<DeliverableBudget>())).Returns(deliverableBud1);
            mockUnitOfWork.Setup(x => x.Commit());

            mockDeliverableBudgetManager.SetupSequence(a => a.SaveDeliverableBudget(It.IsAny<DeliverableBudgetViewModel>(), It.IsAny<int>(), It.IsAny<string>()))
           .Returns(new Core.Models.Intergration.Common.IntergrationOutBound())
           .Returns(null);

            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>())).Returns(1);

            mockDeliverableBudgetService.Setup(x => x.SaveDeliverableBudget(It.IsAny<DeliverableMultiWBSModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(deliverableMultiWBSModel);
            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>())).Returns(1);
            mockUserRepository.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(mrmUser);

            #endregion

            var DeliverableBudgetServiceMock = new DeliverableBudgetServiceMock(
                unitOfWork: mockUnitOfWork.Object,
                deliverableRepository: mockDeliverableRepository.Object, UserRepository: mockUserRepository.Object,
                deliverableBudgetRepository: mockDeliverableBudgetRepository.Object);

            //Act
            var result = DeliverableBudgetServiceMock.SaveDeliverableBudget(deliverableMultiWBSModel, "SWNA//TestLogin", 0);

            //Check object null or not
            Assert.IsTrue(result != null);

            //check valid object not
            Assert.IsInstanceOfType(result, typeof(DeliverableMultiWBSModel));

            //Check intital Budget Amount is correct or not
            Assert.AreEqual(result.InitialBudgetAmount, 123);
        }

        //MRM-164 : Initial Budget Amount is saved from SaveDeliverableBudget method. 
        [TestMethod]
        public void SaveDeliverableBudget()
        {
            #region Data Setup

            DeliverableBudgetViewModel deliverableBudgetViewModel = new DeliverableBudgetViewModel();
            deliverableBudgetViewModel.ProducingDepartmentId = 84;//MRM Team
            deliverableBudgetViewModel.FiscalYearId = 36;
            deliverableBudgetViewModel.BudgetGroupId = 1;
            deliverableBudgetViewModel.TowId = 3939;
            deliverableBudgetViewModel.BudgetTypeId = 3;
            deliverableBudgetViewModel.WBSNumber = "1129058.001.003";
            deliverableBudgetViewModel.DeliverableId = 14028104;

            #endregion

            #region Mocking

            mockDeliverableBudgetManager.SetupSequence(a => a.SaveDeliverableBudget(It.IsAny<DeliverableBudgetViewModel>(), It.IsAny<int>(), It.IsAny<string>()))
           .Returns(new Core.Models.Intergration.Common.IntergrationOutBound())
           .Returns(null);

            #endregion

            var deliverableV2Controller = new DeliverableV2Controller(userService: null, loggerService: null, deliverableManager: null,
                deliverablePropertyManager: null, deliverableCommentManager: null,
                deliverableBudgetManager: mockDeliverableBudgetManager.Object,
                deliverableGeneralInfoManager: null, offAirActivityManager: null, invoiceManager: null,
                journalEntryManager: null, trackManager: null, paidMediaActivityManager: null, graphicActivityManager: null, scriptManager: null,
                deliverableService: null, deliverableBulkUpdateManager: null, workOrderManager: null, deliverableDirector: null,
                titleCategoryRepository: null);

            //Act
            var result = deliverableV2Controller.SaveDeliverableBudget(deliverableBudgetViewModel, 540, "testlogin");

            //Assert
            Assert.IsTrue(result.Success);
            mockDeliverableBudgetManager.Verify(a => a.SaveDeliverableBudget(It.IsAny<DeliverableBudgetViewModel>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once());

            result = deliverableV2Controller.SaveDeliverableBudget(deliverableBudgetViewModel, 540, "testlogin");
            Assert.IsTrue(result.Success);
            Assert.IsTrue((result.Data as DeliverableSaveModel) == null);
        }
        #endregion

        #region MRM-54 : Client LOB
        [TestMethod]
        public void SaveClientLobField_SingleLOBSuccessTest()
        {
            #region Data Setup

            DeliverableGeneralInfoViewModel deliverableGeneralInfoViewModel = new DeliverableGeneralInfoViewModel();
            List<int> clientLobList = new List<int>(new int[] { 666 });
            deliverableGeneralInfoViewModel.ClientLOBList = clientLobList;

            DeliverableSaveModel deliverableSaveModel = new DeliverableSaveModel()
            {
                id = 1,
                IntegrationOutBound = new Core.Models.Intergration.Common.IntergrationOutBound(),
                timestamp = DateTime.Now.ToShortTimeString()
            };

            #endregion

            #region Mocking
            mockDeliverableManager.Setup(a => a.SaveGeneralInfoViewModel(It.IsAny<DeliverableGeneralInfoViewModel>(), It.IsAny<int>(), It.IsAny<string>(), true))
            .Returns(deliverableSaveModel);
            #endregion

            var deliverableV2Controller = new DeliverableV2Controller(userService: null, loggerService: null,
                deliverableManager: mockDeliverableManager.Object,
               deliverablePropertyManager: null, deliverableCommentManager: null,
               deliverableBudgetManager: null,
               deliverableGeneralInfoManager: null, offAirActivityManager: null, invoiceManager: null,
               journalEntryManager: null, trackManager: null, paidMediaActivityManager: null, graphicActivityManager: null, scriptManager: null,
               deliverableService: null, deliverableBulkUpdateManager: null, workOrderManager: null, deliverableDirector: null,
               titleCategoryRepository: null);

            //Act
            var result = deliverableV2Controller.SaveGeneralInfo(deliverableGeneralInfoViewModel, 540, "testlogin");

            //Assert
            Assert.IsTrue(result.Success && (result.Data as DeliverableSaveModel) != null);
        }

        [TestMethod]
        public void SaveGeneralInfoViewModelTest()
        {
            #region Data
            int mrmUserId = 999,nwid1=55,nwid2=56;
            string networkLogin = "SWNA\\TestLogin";
            string voiceOverArtist = "TestCR";
            List<string> listVO = new List<string>();
            listVO.Add(voiceOverArtist);
            List<int> NwList = new List<int>();
            NwList.Add(nwid1);
            NwList.Add(nwid2);
            DeliverableGeneralInfoViewModel generalinfo = new DeliverableGeneralInfoViewModel()
            {
                CreatedBy="556",
                AssigneeId=1,
                VoiceOverArtistIds= listVO,
                UpdateById = mrmUserId,
                UpdateDate = DateTime.UtcNow,
                PlannedLengthCode="Other",
                PlannedLengthOtherValue="10",
                DeliverableGroupCode="CR",
                DeliverableId=145632,
                DescriptionText="Test",
                FormOfDeliverableText="TestCR",
                ProjectId="CR1",
                PaymentTermId=15,
                OtherTermText="TestContr",
                CommunicationChannel="CRChannel",
                NetworkIds= NwList,
                FinDelDate=DateTime.UtcNow,
                ShootFrom= DateTime.UtcNow,
                ShootTo= DateTime.Today,
                AssignedDate=DateTime.Today
            };

            DeliverableDateType deldatetype = new DeliverableDateType()
            {
              Code="CR",
              CreatedBy=556,
              LastUpdatedBy=556,
              CreatedDateTime=DateTime.Now,
            };
            List<DeliverableDateType> deldatetypelist = new List<DeliverableDateType>();
            deldatetypelist.Add(deldatetype);
            List<DeliverableGeneralInfoViewModel> generalinfolist = new List<DeliverableGeneralInfoViewModel>();
            generalinfolist.Add(generalinfo);
            Talent talent = new Talent()
            {
                Id=1,
                Name="TestCR",
                CreatedBy=556,                
                IsActiveFlag = true,                
                CreatedDateTime = DateTime.UtcNow,
                LastUpdatedBy = 556,
                LastUpdatedDateTime = DateTime.UtcNow
            };
            DeliverableSaveModel deliverableSaveModel = new DeliverableSaveModel()
            {
                id = 1,
                IntegrationOutBound = new Core.Models.Intergration.Common.IntergrationOutBound(),
                timestamp = DateTime.Now.ToShortTimeString()
            };
            IntergrationOutBound intoutbound = new IntergrationOutBound()
            {
                DefaultWorkOrders=new Core.Models.Intergration.MP.WorkOrderRequest()
            };
            Mapper.CreateMap<DeliverableGeneralInfoViewModel, Deliverable>();
            Mapper.CreateMap<List<DeliverableDateTypeFieldViewModel>, List<DeliverableDate>>();
            #endregion
            #region Mocking
            mockDeliverableServiceV2.Setup(x => x.AddVoiceOverArtist(It.IsAny<string>(), It.IsAny<int>())).Returns(talent.Id);
            mockDeliverableDateTypeRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableDateType, bool>>>())).Returns(deldatetypelist);
            mockDeliverableServiceV2.Setup(x => x.GetDeliverableDateTypeByCode(It.IsAny<string>())).Returns(deldatetype);
            mockTalentRepository.Setup(x => x.Add(It.IsAny<Talent>())).Returns(talent);
            mockTalentRepository.Setup(x => x.Update(It.IsAny<Talent>()));
            mockDeliverableManager.Setup(a => a.SaveGeneralInfoViewModel(It.IsAny<DeliverableGeneralInfoViewModel>(), It.IsAny<int>(), It.IsAny<string>(), true))
           .Returns(deliverableSaveModel);
            mockDeliverableManager.Setup(a => a.SaveDeliverableDates(It.IsAny<List<DeliverableDateTypeFieldViewModel>>(), It.IsAny<int>(), It.IsAny<string>()))
           .Returns(intoutbound);
            mockDeliverableServiceV2.Setup(x => x.SaveTypeOfContents(It.IsAny<int>(), It.IsAny<List<int>>(), It.IsAny<int>()));
            mockDeliverableManager.Setup(a => a.SaveTypeOfContents(It.IsAny<int>(), It.IsAny<List<int>>(), It.IsAny<int>()));
            mockUnitOfWork.Setup(x => x.Commit());
            #endregion
            var deliverablecontroller = new DeliverableV2ControllerMock(deliverableManager: mockDeliverableManager.Object);
            //Act
            var result = deliverablecontroller.SaveGeneralInfo(generalinfo, 540, "testlogin");

            //Assert
            Assert.IsTrue(result.Success && result.Data  != null);

        }

        [TestMethod]
        public void SaveClientLobField_MultipleLOBSuccessTest()
        {
            #region Data Setup

            DeliverableGeneralInfoViewModel deliverableGeneralInfoViewModel = new DeliverableGeneralInfoViewModel();
            List<int> clientLobList = new List<int>(new int[] { 1, 2, 3 });
            deliverableGeneralInfoViewModel.ClientLOBList = clientLobList;

            DeliverableSaveModel deliverableSaveModel = new DeliverableSaveModel()
            {
                id = 1,
                IntegrationOutBound = new Core.Models.Intergration.Common.IntergrationOutBound(),
                timestamp = DateTime.Now.ToShortTimeString()
            };

            #endregion

            #region Mocking
            mockDeliverableManager.Setup(a => a.SaveGeneralInfoViewModel(It.IsAny<DeliverableGeneralInfoViewModel>(), It.IsAny<int>(), It.IsAny<string>(), true))
            .Returns(deliverableSaveModel);
            #endregion

            var deliverableV2Controller = new DeliverableV2Controller(userService: null, loggerService: null,
                deliverableManager: mockDeliverableManager.Object,
               deliverablePropertyManager: null, deliverableCommentManager: null,
               deliverableBudgetManager: null,
               deliverableGeneralInfoManager: null, offAirActivityManager: null, invoiceManager: null,
               journalEntryManager: null, trackManager: null, paidMediaActivityManager: null, graphicActivityManager: null, scriptManager: null,
               deliverableService: null, deliverableBulkUpdateManager: null, workOrderManager: null, deliverableDirector: null,
               titleCategoryRepository: null);

            //Act
            var result = deliverableV2Controller.SaveGeneralInfo(deliverableGeneralInfoViewModel, 540, "testlogin");

            //Assert
            Assert.IsTrue(result.Success && (result.Data as DeliverableSaveModel) != null);
        }

        [TestMethod]
        public void SaveClientLobField_NoLOBSuccessTest()
        {
            #region Data Setup

            DeliverableGeneralInfoViewModel deliverableGeneralInfoViewModel = new DeliverableGeneralInfoViewModel();
            List<int> clientLobList = new List<int>(new int[] { });
            deliverableGeneralInfoViewModel.ClientLOBList = clientLobList;

            DeliverableSaveModel deliverableSaveModel = new DeliverableSaveModel()
            {
                id = 1,
                IntegrationOutBound = new Core.Models.Intergration.Common.IntergrationOutBound(),
                timestamp = DateTime.Now.ToShortTimeString()
            };

            #endregion

            #region Mocking
            mockDeliverableManager.Setup(a => a.SaveGeneralInfoViewModel(It.IsAny<DeliverableGeneralInfoViewModel>(), It.IsAny<int>(), It.IsAny<string>(), true))
            .Returns(deliverableSaveModel);
            #endregion

            var deliverableV2Controller = new DeliverableV2Controller(userService: null, loggerService: null,
                deliverableManager: mockDeliverableManager.Object,
               deliverablePropertyManager: null, deliverableCommentManager: null,
               deliverableBudgetManager: null,
               deliverableGeneralInfoManager: null, offAirActivityManager: null, invoiceManager: null,
               journalEntryManager: null, trackManager: null, paidMediaActivityManager: null, graphicActivityManager: null, scriptManager: null,
               deliverableService: null, deliverableBulkUpdateManager: null, workOrderManager: null, deliverableDirector: null,
               titleCategoryRepository: null);

            //Act
            var result = deliverableV2Controller.SaveGeneralInfo(deliverableGeneralInfoViewModel, 540, "testlogin");

            //Assert
            Assert.IsTrue(result.Success && (result.Data as DeliverableSaveModel) != null);
        }

        [TestMethod]
        public void GetDeliverableGeneralInfoViewModel_ClientLOBList()
        {
            #region Data Setup

            int mrmUserId = 999;
            string networkLogin = "SWNA\\TestLogin";

            DeliverableGroup testDeliverableGroup = new DeliverableGroup()
            {
                Code = "VID"//Video
            };

            Channel testChannel = new Channel()
            {
                Code = "ROM"//Radio Disney
            };

            Deliverable testDeliverable = new Deliverable()
            {
                Id = 1,
                DeliverableTypeId = 94,//Affiliate Marketing
                ProducingDepartmentId = 84,//MRM Team
                Name = "Radio Disney Video Deliverable",
                //ChannelId = 10,//Radio Disney
                DeliverableGroupId = 2,//Video
                DeliverableGroup = testDeliverableGroup,
                Channel = testChannel
            };

            //Client LOB Data
            DeliverableGeneralInfoViewModel deliverableGeneralInfoViewModel = new DeliverableGeneralInfoViewModel();
            deliverableGeneralInfoViewModel.ClientLOBList = new List<int>() { 1, 2, 3 };

            //Client LOB Empty Data
            DeliverableGeneralInfoViewModel deliverableGeneralInfoViewModel_Empty = new DeliverableGeneralInfoViewModel();
            deliverableGeneralInfoViewModel_Empty.ClientLOBList = new List<int>() { };

            List<int> clientLobTestListData = new List<int> { 1, 2, 3 };

            List<int> clientLobTestListDataNotEqual = new List<int> { 1, 2, 3, 4 };

            List<int> clientLobTestListDataEmpty = new List<int> { };




            #endregion

            #region Mocking

            mockDeliverableGeneralInfoManager.Setup(x => x.GetDeliverableGeneralInfoViewModel(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(deliverableGeneralInfoViewModel);
            mockDeliverableServiceV2.Setup(x => x.GetDeliverableById(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(testDeliverable);
            #endregion

            var deliverableV2Controller = new DeliverableV2Controller(userService: null, loggerService: null,
                deliverableManager: null,
               deliverablePropertyManager: null, deliverableCommentManager: null,
               deliverableBudgetManager: null,
               deliverableGeneralInfoManager: mockDeliverableGeneralInfoManager.Object, offAirActivityManager: null, invoiceManager: null,
               journalEntryManager: null, trackManager: null, paidMediaActivityManager: null, graphicActivityManager: null, scriptManager: null,
               deliverableService: null, deliverableBulkUpdateManager: null, workOrderManager: null, deliverableDirector: null,
               titleCategoryRepository: null);
            //Act
            deliverableGeneralInfoViewModel = deliverableV2Controller.GetDeliverableGeneralInfoViewModel(testDeliverable.Id, mrmUserId, networkLogin);

            //Assert
            CollectionAssert.AreEqual(deliverableGeneralInfoViewModel.ClientLOBList, clientLobTestListData);
            CollectionAssert.AreNotEqual(deliverableGeneralInfoViewModel.ClientLOBList, clientLobTestListDataNotEqual);
            //For Client LOB list Empty and Test Data empty
            CollectionAssert.AreEqual(deliverableGeneralInfoViewModel_Empty.ClientLOBList, clientLobTestListDataEmpty);
            //For Client LOB list Empty and Test Data NOT Empty
            CollectionAssert.AreNotEqual(deliverableGeneralInfoViewModel_Empty.ClientLOBList, clientLobTestListData);
        }
        #endregion

        #region MRM-163 -  Sorting Clip and Deliver Vendor Values
        [TestMethod]
        public void GetMasterVendor_ShouldSortClipAndDeliverableVendorValuesAscAndDesc()
        {
            #region Data Setup
            bool isMeditorOnly = true;
            string networkLogin = "SWNA\\Testlogin";

            List<MasterVendor1> masterVendorList = new List<MasterVendor1>();
            masterVendorList.Add(new MasterVendor1 { Id = 1, VendorTypeCode = "VTC1", MasterVendorName = "AA", MediatorVendorFlag = true });
            masterVendorList.Add(new MasterVendor1 { Id = 2, VendorTypeCode = "VTC2", MasterVendorName = "BB", MediatorVendorFlag = false });
            masterVendorList.Add(new MasterVendor1 { Id = 3, VendorTypeCode = "VTC3", MasterVendorName = "CC", MediatorVendorFlag = false });
            masterVendorList.Add(new MasterVendor1 { Id = 4, VendorTypeCode = "VTC4", MasterVendorName = "DD", MediatorVendorFlag = true });
            masterVendorList.Add(new MasterVendor1 { Id = 5, VendorTypeCode = "VTC5", MasterVendorName = "EE", MediatorVendorFlag = true });
            masterVendorList.Add(new MasterVendor1 { Id = 6, VendorTypeCode = "VTC6", MasterVendorName = "FF", MediatorVendorFlag = false });

            IQueryable<MasterVendor1> masterVendorListQueryable = masterVendorList.AsQueryable();

            #endregion

            #region Mocking
            mockmasterVendorViewRepository.Setup(x => x.GetAll()).Returns(masterVendorListQueryable);
            mockDropDownListService.Setup(x => x.GetMasterVendor(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(masterVendorListQueryable);

            #endregion

            #region Mapper
            Mapper.CreateMap<MasterVendor1, DropDownViewModel>()
             .ForMember(i => i.Id, m => m.MapFrom(u => u.Id))
             .ForMember(i => i.Code, m => m.MapFrom(u => u.VendorTypeCode))
             .ForMember(i => i.ToolTip, m => m.MapFrom(u => u.MasterVendorName))
             .ForMember(i => i.Description, m => m.MapFrom(u => u.Id.ToString()));
            #endregion

            DropDownApiController dropDownApiController = new DropDownApiController(userService: null,
                loggerService: null, dropDownListService: mockDropDownListService.Object, homeService: mockHomeService.Object);


            DropDownListViewModel result = dropDownApiController.GetMasterVendor(It.IsAny<string>(), It.IsAny<bool>());

            #region Assert

            //Sort Order -  Ascending
            Assert.AreEqual(true, result.DropDownList[0].ToolTip.StartsWith("A"));
            //Sort Order -  Descending
            Assert.AreNotEqual(true, result.DropDownList[5].ToolTip.StartsWith("A"));

            #endregion
        }
        #endregion

        #region MRM-54 :  Saving Client LOB list
        [TestMethod]
        public void SaveGeneralInfo_SaveClientLOBListTest()
        {
            #region Data Setup
            DeliverableGroup deliverableGroup = new DeliverableGroup()
            {
                Id = 1,
                Code = "OAD"
            };
            Deliverable_Talent deliverable_Talent1 = new Deliverable_Talent()
            {
                Id = 1,
                DeliverableId = 140000
            };
            Deliverable_Talent deliverable_Talent2 = new Deliverable_Talent()
            {
                Id = 1,
                DeliverableId = 140000
            };
            List<Deliverable_Talent> deliverable_TalentList = new List<Deliverable_Talent>();
            deliverable_TalentList.Add(deliverable_Talent1);
            deliverable_TalentList.Add(deliverable_Talent2);
            DeliverableStatus deliverableStatus = new DeliverableStatus()
            {
                Id = 1,
                Code = "INPCC"
            };
            TrackElement trackElement1 = new TrackElement()
            {
                Id = 1,
                Name = "te1"
            };
            TrackElement trackElement2 = new TrackElement()
            {
                Id = 2,
                Name = "te2"
            };
            List<TrackElement> trackElement = new List<TrackElement>();
            trackElement.Add(trackElement1);
            trackElement.Add(trackElement2);
            DeliverableSaveModel deliverableSaveModel = new DeliverableSaveModel();
            deliverableSaveModel.id = 1;
            Deliverable_SecondaryTarget deliverable_SecondaryTarget1 = new Deliverable_SecondaryTarget()
            {
                Id = 1,
                DeliverableId = 140000
            };
            Deliverable_SecondaryTarget deliverable_SecondaryTarget2 = new Deliverable_SecondaryTarget()
            {
                Id = 2,
                DeliverableId = 140001
            };
            List<Deliverable_SecondaryTarget> deliverable_SecondaryTargetList = new List<Deliverable_SecondaryTarget>();
            deliverable_SecondaryTargetList.Add(deliverable_SecondaryTarget1);
            deliverable_SecondaryTargetList.Add(deliverable_SecondaryTarget2);
            Deliverable_UserTitle_MRMUser deliverable_UserTitle_MRMUser1 = new Deliverable_UserTitle_MRMUser()
            {
                Id = 1,
                MRMUserId = 99,
                DeliverableId = 1
            };
            Deliverable_UserTitle_MRMUser deliverable_UserTitle_MRMUser2 = new Deliverable_UserTitle_MRMUser()
            {
                Id = 2,
                MRMUserId = 88
            };
            Deliverable_UserTitle_MRMUser deliverable_UserTitle_MRMUserTest = new Deliverable_UserTitle_MRMUser()
            {
                Id = 3,
                MRMUserId = 100
            };
            List<Deliverable_UserTitle_MRMUser> deliverable_UserTitle_MRMUserList = new List<Deliverable_UserTitle_MRMUser>();
            deliverable_UserTitle_MRMUserList.Add(deliverable_UserTitle_MRMUser1);
            deliverable_UserTitle_MRMUserList.Add(deliverable_UserTitle_MRMUser2);
            IQueryable<Deliverable_UserTitle_MRMUser> deliverable_UserTitle_MRMUserListQueryable = deliverable_UserTitle_MRMUserList.AsQueryable();
            Deliverable_BusinessArea deliverableBusinessArea1 = new Deliverable_BusinessArea()
            {
                BusinessAreaId = 1,
                DeliverableId = 140000,
                Id = 100
            };
            Deliverable_BusinessArea deliverableBusinessArea2 = new Deliverable_BusinessArea()
            {
                BusinessAreaId = 2,
                DeliverableId = 140001,
                Id = 101
            };
            List<Deliverable_BusinessArea> iDeliverableBusinessArea = new List<Deliverable_BusinessArea>();
            iDeliverableBusinessArea.Add(deliverableBusinessArea1);
            iDeliverableBusinessArea.Add(deliverableBusinessArea2);
            Deliverable testDeliverable = new Deliverable()
            {
                Id = 1,
                DeliverableTypeId = 94,//Affiliate Marketing
                ProducingDepartmentId = 84,//MRM Team
                Name = "Radio Disney Video Deliverable",
                //ChannelId = 10,//Radio Disney
                DeliverableGroupId = 2,//Video
                DeliverableGroup = deliverableGroup,
                Deliverable_Talent = deliverable_TalentList,
                DeliverableStatus = deliverableStatus,
                Deliverable_BusinessArea = iDeliverableBusinessArea
            };
            deliverableBusinessArea1.Deliverable = testDeliverable;
            deliverableBusinessArea2.Deliverable = testDeliverable;
            OffAirDesign offAirDesign = new OffAirDesign()
            {
                DeliverableId = testDeliverable.Id,
                AssigneeMRMUserId = 1
            };
            testDeliverable.OffAirDesign = offAirDesign;
            testDeliverable.Deliverable_UserTitle_MRMUser = deliverable_UserTitle_MRMUserList;
            List<int> iQuerableList = new List<int>();
            iQuerableList.Add(1);
            iQuerableList.Add(2);
            IQueryable<int> iQuerableListQueryable = iQuerableList.AsQueryable();
            UserTitle userTitle = new UserTitle()
            {
                Id = 1,
            };
            #endregion

            #region Mocking
            mockDeliverableServiceV2.Setup(x => x.SaveGeneralInfo(It.IsAny<Deliverable>(), It.IsAny<string>(), out trackElement, It.IsAny<bool>()))
                .Returns(testDeliverable);
            mockDeliverableRepository.Setup(x => x.GetDeliverableById(It.IsAny<int>()))
                .Returns(testDeliverable);
            mockDeliverableRepository.Setup(x => x.Update(It.IsAny<Deliverable>()));
            mockDeliverableRepository.Setup(x => x.SaveConractRequest(It.IsAny<ContractRequest>())).Returns(It.IsAny<int>());
            mockDeliverable_BusinessAreaRepository.Setup(x => x.Delete(It.IsAny<Deliverable_BusinessArea>()));
            mockDeliverable_BusinessAreaRepository.Setup(x => x.Add(It.IsAny<Deliverable_BusinessArea>())).Returns(deliverableBusinessArea1);
            mockOffAirDesignRepository.Setup(x => x.GetSingle(It.IsAny<Expression<Func<OffAirDesign, bool>>>()))
                .Returns(offAirDesign);
            mockOffAirDesignRepository.Setup(x => x.Add(It.IsAny<OffAirDesign>())).Returns(offAirDesign);
            mockDeliverableSecondaryTargetRepostiory.Setup(x => x.GetMany(It.IsAny<Expression<Func<Deliverable_SecondaryTarget, bool>>>()))
                .Returns(deliverable_SecondaryTargetList);
            mockDeliverableSecondaryTargetRepostiory.Setup(x => x.Add(It.IsAny<Deliverable_SecondaryTarget>()));
            mockDeliverableSecondaryTargetRepostiory.Setup(x => x.Delete(It.IsAny<Deliverable_SecondaryTarget>()));
            mockDeliverableUserTitleMrmUserRepository.Setup(x => x.GetAll()).Returns(deliverable_UserTitle_MRMUserListQueryable);
            mockDeliverableUserTitleMrmUserRepository.Setup(x => x.GetSingle(It.IsAny<Expression<Func<Deliverable_UserTitle_MRMUser, bool>>>()))
                .Returns(deliverable_UserTitle_MRMUser2);
            mockDeliverableUserTitleMrmUserRepository.Setup(x => x.Update(deliverable_UserTitle_MRMUser2));
            mockDeliverableUserTitleMrmUserRepository.Setup(x => x.Delete(It.IsAny<Expression<Func<Deliverable_UserTitle_MRMUser, bool>>>()));
            mockUserTitleRepository.Setup(x => x.GetSingle(It.IsAny<Expression<Func<UserTitle, bool>>>())).Returns(userTitle);
            mockDeliverable_TalentRepository.Setup(x => x.Add(deliverable_Talent1));
            mockDeliverable_TalentRepository.Setup(x => x.Delete(deliverable_Talent1));
            mockDeliverableGroupRepository.Setup(x => x.GetSingle(It.IsAny<Expression<Func<DeliverableGroup, bool>>>()))
                .Returns(deliverableGroup);
            mockUnitOfWork.Setup(x => x.Commit());
            #endregion

            #region Service
            var deliverableServiceV2 = new DeliverableServiceV2Mock(deliverableRepository: mockDeliverableRepository.Object,deliverableUserTitleMrmUserRepository: mockDeliverableUserTitleMrmUserRepository.Object,
                    deliverableSecondaryTargetRepository: mockDeliverableSecondaryTargetRepostiory.Object,
                    deliverableTalentRepository: mockDeliverable_TalentRepository.Object, offAirDesignRepository: mockOffAirDesignRepository.Object, userTitleRepository: mockUserTitleRepository.Object, deliverableGroupRepository: mockDeliverableGroupRepository.Object,
                    deliverable_BusinessAreaRepository: mockDeliverable_BusinessAreaRepository.Object, iunitOfWork: mockUnitOfWork.Object);
            #endregion

            var result = deliverableServiceV2.SaveGeneralInfo(testDeliverable, "SWNA\\TestLogin", out trackElement, true);

            #region Assert
            var result1 = result.Deliverable_BusinessArea.LastOrDefault();
            Assert.IsNotNull(result1);
            Assert.AreEqual(result1.BusinessAreaId, deliverableBusinessArea2.BusinessAreaId);
            #endregion
        }
        #endregion

        #region MRM-165  - Passing null deliverable Id should Fail
        [TestMethod]
        public void GetDeliverableMultiWBSByDeliverableId_NoDeliverableIdException()
        {
            #region Data Setup
            DeliverableGroup testDeliverableGroup = new DeliverableGroup()
            {
                Code = "VID"//Video
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
                Id = 1,
                DeliverableTypeId = 94,//Affiliate Marketing
                ProducingDepartmentId = 84,//MRM Team
                Name = "Radio Disney Video Deliverable",
                //ChannelId = 10,//Radio Disney
                DeliverableGroupId = 2,//Video
                DeliverableGroup = testDeliverableGroup,
                Channel = testChannel,
            };
            DeliverableBudget deliverableBudget1 = new DeliverableBudget()
            {
                DeliverableId = 1,
                Id = 100,
                WBSElement = wbsElement1,
                Deliverable = testDeliverable
            };
            DeliverableBudget deliverableBudget2 = new DeliverableBudget()
            {
                DeliverableId = 2,
                Id = 101,
                WBSElement = wbsElement2,
                Deliverable = testDeliverable,
            };
            List<DeliverableBudget> deliverableBudgetList = new List<DeliverableBudget>();
            deliverableBudgetList.Add(deliverableBudget1);
            deliverableBudgetList.Add(deliverableBudget2);
            bool exceptionCaught = false;
            #endregion

            #region Mocking
            mockDeliverableServiceV2.Setup(x => x.GetDeliverableById(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(testDeliverable);
            mockDeliverableRepository.Setup(x => x.GetDeliverableById(It.IsAny<int>()))
                .Returns(testDeliverable);
            mockDeliverableBudgetService.Setup(x => x.GetDeliverableBudgetsByDeliverableId(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(deliverableBudgetList);
            mockDeliverableBudgetRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableBudget, bool>>>()))
                .Returns(deliverableBudgetList);
            #endregion
            try
            {
                var deliverableBudgetManager = new DeliverableBudgetManager(
                deliverableBudgetService: null, userSerive: null, intergrationManager: null, deliverableServiceV2: null,
                dropDownListService: null, DeliverableBudgetService: mockDeliverableBudgetService.Object);

                var result = deliverableBudgetManager.GetDeliverableMultiWBSByDeliverableId(It.IsAny<int>(), 1, "SWNA\\TestLogin");
            }
            catch (Exception ex)
            {
                exceptionCaught = true;
            }
            #region Assert
            Assert.IsTrue(exceptionCaught);
            #endregion
        }
        #endregion

        #region Get List of Internal WBS Elements
        [TestMethod]
        public void GetDeliverableMultiWBSByDeliverableId_GetMultipleInternalWBSElements()
        {
            #region Data Setup

            Guid UniqueId1 = Guid.NewGuid();
            Guid UniqueId2 = Guid.NewGuid();

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
                PrimaryDeliverableBudgetUniqueID = UniqueId2,
                ClipDeliverMasterVendorId = 108,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true
            };
            DeliverableBudget deliverableBud1 = new DeliverableBudget()
            {
                Id = 1,
                DeliverableId = 140000,
                MasterVendorId = 12,
                WBSElementId = wbsElement1.Id,
                DeliverableBudgetUniqueID = UniqueId1
            };
            deliverableBud1.WBSElement = wbsElement1;
            deliverableBud1.Deliverable = testDeliverable;
            DeliverableBudget deliverableBud2 = new DeliverableBudget()
            {
                Id = 2,
                DeliverableId = 140000,
                MasterVendorId = 25,
                WBSElementId = wbsElement2.Id,
                DeliverableBudgetUniqueID = UniqueId2
            };
            deliverableBud2.WBSElement = wbsElement2;
            deliverableBud2.Deliverable = testDeliverable;
            deliverableBud2.TypeOfWork = typeOfWork;
            List<DeliverableBudget> deliverableBudgetList = new List<DeliverableBudget>();
            deliverableBudgetList.Add(deliverableBud1);
            deliverableBudgetList.Add(deliverableBud2);
            DeliverableBudgetViewModel deliverableBudget1 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                WBSNumber = "123456.123.001",
                MasterVendorId = 10,
                isPrimary = true,
                BudgetGroupId = 55,
                DeliverableBudgetId = 1,
                WBSElementId = 2,
                UserId = 852,
                DeliverableBudgetUniqueID = UniqueId1
            };
            DeliverableBudgetViewModel deliverableBudget2 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                MasterVendorId = 15,
                isPrimary = false,
                BudgetGroupId = 77,
                DeliverableBudgetId = 2,
                WBSNumber = "123456.123.002",
                DeliverableBudgetUniqueID = UniqueId2
            };
            List<DeliverableBudgetViewModel> deliverableBudgetViewModelList = new List<DeliverableBudgetViewModel>();
            deliverableBudgetViewModelList.Add(deliverableBudget1);
            deliverableBudgetViewModelList.Add(deliverableBudget2);
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                DeliverableId = 140000,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true,
                PrimaryDeliverableBudgetUniqueID = UniqueId2,
                ClipDeliverMasterVendorId = 100,
                ProducingDepartmentId = 108,
                MarketingGroupChannelId = 1,
                Budgets = deliverableBudgetViewModelList
            };
            #endregion

            #region Mocking
            mockDeliverableServiceV2.Setup(x => x.GetDeliverableById(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(testDeliverable);
            mockDeliverableRepository.Setup(x => x.GetDeliverableById(It.IsAny<int>()))
                .Returns(testDeliverable);
            mockDeliverableBudgetService.Setup(x => x.GetDeliverableBudgetsByDeliverableId(It.IsAny<int>(), It.IsAny<string>()))
              .Returns(deliverableBudgetList);
            mockDeliverableBudgetRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableBudget, bool>>>()))
             .Returns(deliverableBudgetList);
            mockDeliverableBudgetManager.Setup(x => x.GetDeliverableMultiWBSByDeliverableId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(deliverableMultiWBSViewModel);
            #endregion

            var deliverableBudgetManager = new DeliverableBudgetManager(
            deliverableBudgetService: mockDeliverableBudgetService.Object, userSerive: null, intergrationManager: null, deliverableServiceV2: mockDeliverableServiceV2.Object,
            dropDownListService: null, DeliverableBudgetService: mockDeliverableBudgetService.Object);

            #region Service
            DeliverableMultiWBSViewModel result = deliverableBudgetManager.GetDeliverableMultiWBSByDeliverableId(140000, 1, "SWNA\\TestLogin");
            #endregion

            #region Assert
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.Budgets != null);
            Assert.IsTrue(result.DeliverableId == 140000);
            Assert.IsFalse(result.Budgets.ElementAt(0).isExternal == true);
            Assert.IsTrue(result.Budgets.ElementAt(1).isPrimary == true);
            Assert.IsTrue(result.Budgets.Count == 2);
            Assert.IsFalse(result.Budgets.ElementAt(0).TowId == 1001);
            Assert.IsTrue(result.Budgets.ElementAt(0).WBSNumber == "123456.011.001");
            Assert.IsTrue(result.Budgets.ElementAt(1).TowId == 1001);
            Assert.IsTrue(result.InitialBudgetAmount == 99);
            Assert.IsTrue(result.ProducingDepartmentId == 84);
            Assert.IsFalse(result.ClipDeliverMasterVendorId != 108);
            Assert.IsTrue(result.Budgets.ElementAt(1).DeliverableId == 140000);
            //Assert.IsTrue(result.Budgets.ElementAt(0).UserId == 852);
            Assert.IsFalse(result.IsLaunchFlag == false);
            #endregion
        }
        #endregion

        #region MRM-215 -  Save Method Of Production
        [TestMethod]
        public void SaveDeliverableBudget_SaveMethodOfProductionTrue()
        {
            #region Data Setup
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
            DeliverableBudget deliverableBud1 = new DeliverableBudget()
            {
                Id = 1,
                DeliverableId = 140000,
                MasterVendorId = 12,
                WBSElementId = wbsElement1.Id
            };
            deliverableBud1.WBSElement = wbsElement1;
            deliverableBud1.Deliverable = testDeliverable;
            DeliverableBudget deliverableBud2 = new DeliverableBudget()
            {
                Id = 2,
                DeliverableId = 140000,
                MasterVendorId = 25,
                WBSElementId = wbsElement2.Id
            };
            deliverableBud2.WBSElement = wbsElement2;
            deliverableBud2.Deliverable = testDeliverable;
            deliverableBud2.TypeOfWork = typeOfWork;
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
            List<int> pmoList = new List<int>();
            pmoList.Add(productionMethodTypeList.ElementAt(0).Id);
            pmoList.Add(productionMethodTypeList.ElementAt(1).Id);
            DeliverableBudgetViewModel deliverableBudget1 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                WBSNumber = "123456.123.001",
                MasterVendorId = 10,
                isPrimary = true,
                BudgetGroupId = 55,
                DeliverableBudgetId = 1,
                WBSElementId = 2,
                UserId = 852
            };
            deliverableBudget1.ProductionMethodTypeIds = pmoList;
            DeliverableBudgetViewModel deliverableBudget2 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                MasterVendorId = 15,
                isPrimary = false,
                BudgetGroupId = 77,
                DeliverableBudgetId = 2,
                WBSNumber = "123456.123.002"
            };
            deliverableBudget2.ProductionMethodTypeIds = pmoList;
            List<DeliverableBudgetViewModel> deliverableBudgetViewModelList = new List<DeliverableBudgetViewModel>();
            deliverableBudgetViewModelList.Add(deliverableBudget1);
            deliverableBudgetViewModelList.Add(deliverableBudget2);
            DeliverableMultiWBSModel deliverableMultiWBSModel = new DeliverableMultiWBSModel()
            {
                DeliverableId = 140000,
                PrimaryWBSElementId = 2,
                IsLaunchFlag = false
            };
            deliverableMultiWBSModel.Budgets = deliverableBudgetList;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                DeliverableId = 140000,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true,
                PrimaryWBSElementId = 2, //wbsElement2.Id
                ClipDeliverMasterVendorId = 100,
                ProducingDepartmentId = 108,
                MarketingGroupChannelId = 1,
                Budgets = deliverableBudgetViewModelList
            };
            deliverableMultiWBSViewModel.Budgets = deliverableBudgetViewModelList;
            Deliverable_WBSElement deliverable_WBSElement = new Deliverable_WBSElement()
            {
                DeliverableId = 14000,
                Id = 1,
            };
            deliverable_WBSElement.Deliverable = testDeliverable;
            deliverable_WBSElement.WBSElement = wbsElement1;
            deliverable_WBSElement.WBSElement = wbsElement2;
            #endregion

            #region Mocking
            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>()))
                .Returns(deliverableMultiWBSViewModel.Budgets.Where(i => i.isPrimary).FirstOrDefault().WBSElementId);
            mockDeliverableBudgetService.Setup(x => x.SaveDeliverableBudget(deliverableMultiWBSModel, "SWNA\\TestLogin", 0, 99))
                .Returns(deliverableMultiWBSModel);
            mockDeliverableRepository.Setup(x => x.GetById(It.IsAny<int>()))
              .Returns(testDeliverable);
            mockDeliverableBudgetRepository.Setup(x => x.Delete(It.IsAny<DeliverableBudget>()));
            mockDeliverableBudgetRepository.Setup(x => x.Add(It.IsAny<DeliverableBudget>()));
            mockUnitOfWork.Setup(x => x.Commit());
            mockDeliverable_WBSElementRepository.Setup(x => x.Update(It.IsAny<Deliverable_WBSElement>()));
            mockDeliverable_WBSElementRepository.Setup(x => x.Add(It.IsAny<Deliverable_WBSElement>()))
                .Returns(deliverable_WBSElement);
            mockDeliverableRepository.Setup(x => x.Update(It.IsAny<Deliverable>()));
            mockDeliverableBudgetRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableBudget, bool>>>()))
                .Returns(deliverableBudgetList);
            #endregion

            #region Manager
            var deliverableBudgetManager = new DeliverableBudgetManager(deliverableBudgetService: mockDeliverableBudgetService.Object, userSerive: null,
                 intergrationManager: null, deliverableServiceV2: null, dropDownListService: null, DeliverableBudgetService: mockDeliverableBudgetService.Object);
            #endregion

            #region Assert
            Assert.IsTrue(deliverableBudgetManager != null);
            #endregion
        }
        #endregion

        #region MRM-216 Save Part
        //MRM-216 -  SaveVendorsPostiveCaseOneVendorOneMOP
        [TestMethod]
        public void SaveVendorsPostiveCaseOneVendorOneMOP()
        {
            #region Data Setup
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
            DeliverableBudget deliverableBud1 = new DeliverableBudget()
            {
                Id = 1,
                DeliverableId = 140000,
                MasterVendorId = 1212,
                WBSElementId = wbsElement1.Id,
                ProductionMethodTypeId = 1,

            };
            deliverableBud1.WBSElement = wbsElement1;
            deliverableBud1.Deliverable = testDeliverable;
            DeliverableBudget deliverableBud2 = new DeliverableBudget()
            {
                Id = 2,
                DeliverableId = 140000,
                MasterVendorId = 1313,
                WBSElementId = wbsElement2.Id,
                ProductionMethodTypeId = 1,
            };
            deliverableBud2.WBSElement = wbsElement2;
            deliverableBud2.Deliverable = testDeliverable;
            deliverableBud2.TypeOfWork = typeOfWork;
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

            DeliverableMOP MOP1 = new DeliverableMOP { ProductionMethod = "Post House", ProductionMethodTypeId = 1, Vendors = new List<MOPVendor>() };
            MOPVendor MOPVendor1 = new MOPVendor { MasterVendorId = 1212, Actual = 100, Committed = 100, EstimateCompleteAmount = 100, EstimatedFinalCost = 100, Name = "Vendor1", PHApplyPercent = 10 };
            MOP1.Vendors.Add(MOPVendor1);

            DeliverableMOP MOP2 = new DeliverableMOP { ProductionMethod = "Post House", ProductionMethodTypeId = 1, Vendors = new List<MOPVendor>() };
            MOPVendor MOPVendor2 = new MOPVendor { MasterVendorId = 1313, Actual = 100, Committed = 100, EstimateCompleteAmount = 100, EstimatedFinalCost = 100, Name = "Vendor2", PHApplyPercent = 10 };
            MOP2.Vendors.Add(MOPVendor2);

            List<ProductionMethodType> productionMethodTypeList = new List<ProductionMethodType>();
            productionMethodTypeList.Add(productionMethodType1);
            productionMethodTypeList.Add(productionMethodType2);
            DeliverableBudgetViewModel deliverableBudget1 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                WBSNumber = "123456.123.001",
                MasterVendorId = 10,
                isPrimary = true,
                BudgetGroupId = 55,
                DeliverableBudgetId = 1,
                WBSElementId = 2,
                UserId = 852,
                ProductionMethods = new List<DeliverableMOP>()
            };
            deliverableBudget1.ProductionMethods.Add(MOP1);
            DeliverableBudgetViewModel deliverableBudget2 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                MasterVendorId = 15,
                isPrimary = false,
                BudgetGroupId = 77,
                DeliverableBudgetId = 2,
                WBSNumber = "123456.123.002",
                ProductionMethods = new List<DeliverableMOP>()
            };
            deliverableBudget2.ProductionMethods.Add(MOP2);
            List<DeliverableBudgetViewModel> deliverableBudgetViewModelList = new List<DeliverableBudgetViewModel>();
            deliverableBudgetViewModelList.Add(deliverableBudget1);
            deliverableBudgetViewModelList.Add(deliverableBudget2);
            DeliverableMultiWBSModel deliverableMultiWBSModel = new DeliverableMultiWBSModel()
            {
                DeliverableId = 140000,
                PrimaryWBSElementId = 2,
                IsLaunchFlag = false
            };
            deliverableMultiWBSModel.Budgets = deliverableBudgetList;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                DeliverableId = 140000,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true,
                PrimaryWBSElementId = 2, //wbsElement2.Id
                ClipDeliverMasterVendorId = 100,
                ProducingDepartmentId = 108,
                MarketingGroupChannelId = 1,
                Budgets = deliverableBudgetViewModelList
            };
            deliverableMultiWBSViewModel.Budgets = deliverableBudgetViewModelList;
            Deliverable_WBSElement deliverable_WBSElement = new Deliverable_WBSElement()
            {
                DeliverableId = 14000,
                Id = 1,
            };
            deliverable_WBSElement.Deliverable = testDeliverable;
            deliverable_WBSElement.WBSElement = wbsElement1;
            deliverable_WBSElement.WBSElement = wbsElement2;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModelout = new DeliverableMultiWBSViewModel();
            #endregion

            #region Mocking
            Deliverable DeliverableNull = null;
            mockDeliverableServiceV2.Setup(x => x.GetDeliverableById(It.IsAny<int>(), It.IsAny<string>())).Returns(DeliverableNull);

            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>()))
                .Returns(deliverableMultiWBSViewModel.Budgets.Where(i => i.isPrimary).FirstOrDefault().WBSElementId);
            mockDeliverableBudgetService.Setup(x => x.SaveDeliverableBudget(It.IsAny<DeliverableMultiWBSModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(deliverableMultiWBSModel);
            mockDeliverableRepository.Setup(x => x.GetById(It.IsAny<int>()))
              .Returns(testDeliverable);
            mockDeliverableBudgetRepository.Setup(x => x.Delete(It.IsAny<DeliverableBudget>()));
            mockDeliverableBudgetRepository.Setup(x => x.Add(It.IsAny<DeliverableBudget>()));
            mockUnitOfWork.Setup(x => x.Commit());
            mockDeliverable_WBSElementRepository.Setup(x => x.Update(It.IsAny<Deliverable_WBSElement>()));
            mockDeliverable_WBSElementRepository.Setup(x => x.Add(It.IsAny<Deliverable_WBSElement>()))
                .Returns(deliverable_WBSElement);
            mockDeliverableRepository.Setup(x => x.Update(It.IsAny<Deliverable>()));
            mockDeliverableBudgetRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableBudget, bool>>>()))
                .Returns(deliverableBudgetList);
            #endregion

            #region Manager
            var deliverableBudgetManager = new DeliverableBudgetManagerMock(deliverableBudgetService: mockDeliverableBudgetService.Object,
                deliverableServiceV2: mockDeliverableServiceV2.Object, DeliverableBudgetService: mockDeliverableBudgetService.Object);
            testDeliverable = null;
            var oBudetModel = deliverableBudgetManager.SaveDeliverableBudget(deliverableMultiWBSViewModel, 99, "SWNA\\TestLogin", out deliverableMultiWBSViewModelout);
            #endregion

            #region Assert

            Assert.IsTrue(oBudetModel != null);

            #endregion

        }

        //MRM-216 -  SaveVendorsPostiveCaseOneMOPMultipleVendors
        [TestMethod]
        public void SaveVendorsPostiveCaseOneMOPMultipleVendors()
        {
            #region Data Setup
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
            DeliverableBudget deliverableBud1 = new DeliverableBudget()
            {
                Id = 1,
                DeliverableId = 140000,
                MasterVendorId = 1212,
                WBSElementId = wbsElement1.Id,
                ProductionMethodTypeId = 1,

            };
            deliverableBud1.WBSElement = wbsElement1;
            deliverableBud1.Deliverable = testDeliverable;
            DeliverableBudget deliverableBud2 = new DeliverableBudget()
            {
                Id = 2,
                DeliverableId = 140000,
                MasterVendorId = 1313,
                WBSElementId = wbsElement2.Id,
                ProductionMethodTypeId = 1,
            };
            deliverableBud2.WBSElement = wbsElement2;
            deliverableBud2.Deliverable = testDeliverable;
            deliverableBud2.TypeOfWork = typeOfWork;
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

            DeliverableMOP MOP1 = new DeliverableMOP { ProductionMethod = "Post House", ProductionMethodTypeId = 1, Vendors = new List<MOPVendor>() };
            MOPVendor MOPVendor1 = new MOPVendor { MasterVendorId = 1212, Actual = 100, Committed = 100, EstimateCompleteAmount = 100, EstimatedFinalCost = 100, Name = "Vendor1", PHApplyPercent = 10 };
            MOPVendor MOPVendor2 = new MOPVendor { MasterVendorId = 1212, Actual = 100, Committed = 100, EstimateCompleteAmount = 100, EstimatedFinalCost = 100, Name = "Vendor2", PHApplyPercent = 10 };
            MOPVendor MOPVendor3 = new MOPVendor { MasterVendorId = 1212, Actual = 100, Committed = 100, EstimateCompleteAmount = 100, EstimatedFinalCost = 100, Name = "Vendor3", PHApplyPercent = 10 };
            MOPVendor MOPVendor4 = new MOPVendor { MasterVendorId = 1212, Actual = 100, Committed = 100, EstimateCompleteAmount = 100, EstimatedFinalCost = 100, Name = "Vendor4", PHApplyPercent = 10 };
            MOP1.Vendors.Add(MOPVendor1);
            MOP1.Vendors.Add(MOPVendor2);
            MOP1.Vendors.Add(MOPVendor3);
            MOP1.Vendors.Add(MOPVendor4);



            List<ProductionMethodType> productionMethodTypeList = new List<ProductionMethodType>();
            productionMethodTypeList.Add(productionMethodType1);
            productionMethodTypeList.Add(productionMethodType2);
            DeliverableBudgetViewModel deliverableBudget1 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                WBSNumber = "123456.123.001",
                MasterVendorId = 10,
                isPrimary = true,
                BudgetGroupId = 55,
                DeliverableBudgetId = 1,
                WBSElementId = 2,
                UserId = 852,
                ProductionMethods = new List<DeliverableMOP>()
            };
            deliverableBudget1.ProductionMethods.Add(MOP1);

            List<DeliverableBudgetViewModel> deliverableBudgetViewModelList = new List<DeliverableBudgetViewModel>();
            deliverableBudgetViewModelList.Add(deliverableBudget1);
            DeliverableMultiWBSModel deliverableMultiWBSModel = new DeliverableMultiWBSModel()
            {
                DeliverableId = 140000,
                PrimaryWBSElementId = 2,
                IsLaunchFlag = false
            };
            deliverableMultiWBSModel.Budgets = deliverableBudgetList;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                DeliverableId = 140000,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true,
                PrimaryWBSElementId = 2, //wbsElement2.Id
                ClipDeliverMasterVendorId = 100,
                ProducingDepartmentId = 108,
                MarketingGroupChannelId = 1,
                Budgets = deliverableBudgetViewModelList
            };
            deliverableMultiWBSViewModel.Budgets = deliverableBudgetViewModelList;
            Deliverable_WBSElement deliverable_WBSElement = new Deliverable_WBSElement()
            {
                DeliverableId = 14000,
                Id = 1,
            };
            deliverable_WBSElement.Deliverable = testDeliverable;
            deliverable_WBSElement.WBSElement = wbsElement1;
            deliverable_WBSElement.WBSElement = wbsElement2;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModelout = new DeliverableMultiWBSViewModel();
            #endregion

            #region Mocking
            Deliverable DeliverableNull = null;
            mockDeliverableServiceV2.Setup(x => x.GetDeliverableById(It.IsAny<int>(), It.IsAny<string>())).Returns(DeliverableNull);

            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>()))
                .Returns(deliverableMultiWBSViewModel.Budgets.Where(i => i.isPrimary).FirstOrDefault().WBSElementId);
            mockDeliverableBudgetService.Setup(x => x.SaveDeliverableBudget(It.IsAny<DeliverableMultiWBSModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(deliverableMultiWBSModel);
            mockDeliverableRepository.Setup(x => x.GetById(It.IsAny<int>()))
              .Returns(testDeliverable);
            mockDeliverableBudgetRepository.Setup(x => x.Delete(It.IsAny<DeliverableBudget>()));
            mockDeliverableBudgetRepository.Setup(x => x.Add(It.IsAny<DeliverableBudget>()));
            mockUnitOfWork.Setup(x => x.Commit());
            mockDeliverable_WBSElementRepository.Setup(x => x.Update(It.IsAny<Deliverable_WBSElement>()));
            mockDeliverable_WBSElementRepository.Setup(x => x.Add(It.IsAny<Deliverable_WBSElement>()))
                .Returns(deliverable_WBSElement);
            mockDeliverableRepository.Setup(x => x.Update(It.IsAny<Deliverable>()));
            mockDeliverableBudgetRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableBudget, bool>>>()))
                .Returns(deliverableBudgetList);
            #endregion

            #region Manager
            var deliverableBudgetManager = new DeliverableBudgetManagerMock(deliverableBudgetService: mockDeliverableBudgetService.Object,
                deliverableServiceV2: mockDeliverableServiceV2.Object, DeliverableBudgetService: mockDeliverableBudgetService.Object);
            testDeliverable = null;
            var oBudetModel = deliverableBudgetManager.SaveDeliverableBudget(deliverableMultiWBSViewModel, 99, "SWNA\\TestLogin", out deliverableMultiWBSViewModelout);
            #endregion

            #region Assert
            Assert.IsTrue(oBudetModel != null);

            #endregion

        }

        //MRM-216 -  SaveVendorsPostiveCaseOneMOPMultipleVendors1
        [TestMethod]
        public void SaveVendorsPostiveCaseMultipleVendorsMultipleMOPs()
        {
            #region Data Setup
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
            DeliverableBudget deliverableBud1 = new DeliverableBudget()
            {
                Id = 1,
                DeliverableId = 140000,
                MasterVendorId = 1212,
                WBSElementId = wbsElement1.Id,
                ProductionMethodTypeId = 1,

            };
            deliverableBud1.WBSElement = wbsElement1;
            deliverableBud1.Deliverable = testDeliverable;
            DeliverableBudget deliverableBud2 = new DeliverableBudget()
            {
                Id = 2,
                DeliverableId = 140000,
                MasterVendorId = 1313,
                WBSElementId = wbsElement2.Id,
                ProductionMethodTypeId = 1,
            };
            deliverableBud2.WBSElement = wbsElement2;
            deliverableBud2.Deliverable = testDeliverable;
            deliverableBud2.TypeOfWork = typeOfWork;
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

            DeliverableMOP MOP1 = new DeliverableMOP { ProductionMethod = "Post House", ProductionMethodTypeId = 1, Vendors = new List<MOPVendor>() };
            MOPVendor MOPVendor1 = new MOPVendor { MasterVendorId = 1212, Actual = 100, Committed = 100, EstimateCompleteAmount = 100, EstimatedFinalCost = 100, Name = "Vendor1", PHApplyPercent = 10 };
            MOPVendor MOPVendor2 = new MOPVendor { MasterVendorId = 1212, Actual = 100, Committed = 100, EstimateCompleteAmount = 100, EstimatedFinalCost = 100, Name = "Vendor2", PHApplyPercent = 10 };
            MOPVendor MOPVendor3 = new MOPVendor { MasterVendorId = 1212, Actual = 100, Committed = 100, EstimateCompleteAmount = 100, EstimatedFinalCost = 100, Name = "Vendor3", PHApplyPercent = 10 };
            MOPVendor MOPVendor4 = new MOPVendor { MasterVendorId = 1212, Actual = 100, Committed = 100, EstimateCompleteAmount = 100, EstimatedFinalCost = 100, Name = "Vendor4", PHApplyPercent = 10 };
            MOP1.Vendors.Add(MOPVendor1);
            MOP1.Vendors.Add(MOPVendor2);
            MOP1.Vendors.Add(MOPVendor3);
            MOP1.Vendors.Add(MOPVendor4);


            DeliverableMOP MOP2 = new DeliverableMOP { ProductionMethod = "Creative Service", ProductionMethodTypeId = 2, Vendors = new List<MOPVendor>() };
            MOP2.Vendors.Add(MOPVendor1);
            MOP2.Vendors.Add(MOPVendor2);
            MOP2.Vendors.Add(MOPVendor3);
            MOP2.Vendors.Add(MOPVendor4);


            List<ProductionMethodType> productionMethodTypeList = new List<ProductionMethodType>();
            productionMethodTypeList.Add(productionMethodType1);
            productionMethodTypeList.Add(productionMethodType2);
            DeliverableBudgetViewModel deliverableBudget1 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                WBSNumber = "123456.123.001",
                MasterVendorId = 10,
                isPrimary = true,
                BudgetGroupId = 55,
                DeliverableBudgetId = 1,
                WBSElementId = 2,
                UserId = 852,
                ProductionMethods = new List<DeliverableMOP>()
            };
            deliverableBudget1.ProductionMethods.Add(MOP1);

            List<DeliverableBudgetViewModel> deliverableBudgetViewModelList = new List<DeliverableBudgetViewModel>();
            deliverableBudgetViewModelList.Add(deliverableBudget1);
            DeliverableMultiWBSModel deliverableMultiWBSModel = new DeliverableMultiWBSModel()
            {
                DeliverableId = 140000,
                PrimaryWBSElementId = 2,
                IsLaunchFlag = false
            };
            deliverableMultiWBSModel.Budgets = deliverableBudgetList;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                DeliverableId = 140000,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true,
                PrimaryWBSElementId = 2, //wbsElement2.Id
                ClipDeliverMasterVendorId = 100,
                ProducingDepartmentId = 108,
                MarketingGroupChannelId = 1,
                Budgets = deliverableBudgetViewModelList
            };
            deliverableMultiWBSViewModel.Budgets = deliverableBudgetViewModelList;
            Deliverable_WBSElement deliverable_WBSElement = new Deliverable_WBSElement()
            {
                DeliverableId = 14000,
                Id = 1,
            };
            deliverable_WBSElement.Deliverable = testDeliverable;
            deliverable_WBSElement.WBSElement = wbsElement1;
            deliverable_WBSElement.WBSElement = wbsElement2;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModelout = new DeliverableMultiWBSViewModel();
            #endregion

            #region Mocking
            Deliverable DeliverableNull = null;
            mockDeliverableServiceV2.Setup(x => x.GetDeliverableById(It.IsAny<int>(), It.IsAny<string>())).Returns(DeliverableNull);

            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>()))
                .Returns(deliverableMultiWBSViewModel.Budgets.Where(i => i.isPrimary).FirstOrDefault().WBSElementId);
            mockDeliverableBudgetService.Setup(x => x.SaveDeliverableBudget(It.IsAny<DeliverableMultiWBSModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(deliverableMultiWBSModel);
            mockDeliverableRepository.Setup(x => x.GetById(It.IsAny<int>()))
              .Returns(testDeliverable);
            mockDeliverableBudgetRepository.Setup(x => x.Delete(It.IsAny<DeliverableBudget>()));
            mockDeliverableBudgetRepository.Setup(x => x.Add(It.IsAny<DeliverableBudget>()));
            mockUnitOfWork.Setup(x => x.Commit());
            mockDeliverable_WBSElementRepository.Setup(x => x.Update(It.IsAny<Deliverable_WBSElement>()));
            mockDeliverable_WBSElementRepository.Setup(x => x.Add(It.IsAny<Deliverable_WBSElement>()))
                .Returns(deliverable_WBSElement);
            mockDeliverableRepository.Setup(x => x.Update(It.IsAny<Deliverable>()));
            mockDeliverableBudgetRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableBudget, bool>>>()))
                .Returns(deliverableBudgetList);
            #endregion

            #region Manager
            var deliverableBudgetManager = new DeliverableBudgetManagerMock(deliverableBudgetService: mockDeliverableBudgetService.Object,
                deliverableServiceV2: mockDeliverableServiceV2.Object, DeliverableBudgetService: mockDeliverableBudgetService.Object);
            testDeliverable = null;
            var oBudetModel = deliverableBudgetManager.SaveDeliverableBudget(deliverableMultiWBSViewModel, 99, "SWNA\\TestLogin",out deliverableMultiWBSViewModelout);
            #endregion

            #region Assert
            Assert.IsTrue(oBudetModel != null);
            #endregion

        }

        //MRM-216 - Save Vendors for Deliverable Service
        [TestMethod]
        public void SaveVendorsPositiveCaseOneVendorOneMOPForDeliverableService()
        {
            #region Data Setup
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
            DeliverableBudget deliverableBud1 = new DeliverableBudget()
            {
                Id = 1,
                DeliverableId = 140000,
                MasterVendorId = 1212,
                WBSElementId = wbsElement1.Id,
                ProductionMethodTypeId = 1,

            };
            deliverableBud1.WBSElement = wbsElement1;
            deliverableBud1.Deliverable = testDeliverable;
            DeliverableBudget deliverableBud2 = new DeliverableBudget()
            {
                Id = 2,
                DeliverableId = 140000,
                MasterVendorId = 1313,
                WBSElementId = wbsElement2.Id,
                ProductionMethodTypeId = 1,
            };
            deliverableBud2.WBSElement = wbsElement2;
            deliverableBud2.Deliverable = testDeliverable;
            deliverableBud2.TypeOfWork = typeOfWork;
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

            DeliverableMOP MOP1 = new DeliverableMOP { ProductionMethod = "Post House", ProductionMethodTypeId = 1, Vendors = new List<MOPVendor>() };
            MOPVendor MOPVendor1 = new MOPVendor { MasterVendorId = 1212, Actual = 100, Committed = 100, EstimateCompleteAmount = 100, EstimatedFinalCost = 100, Name = "Vendor1", PHApplyPercent = 10 };
            MOP1.Vendors.Add(MOPVendor1);

            DeliverableMOP MOP2 = new DeliverableMOP { ProductionMethod = "Post House", ProductionMethodTypeId = 1, Vendors = new List<MOPVendor>() };
            MOPVendor MOPVendor2 = new MOPVendor { MasterVendorId = 1313, Actual = 100, Committed = 100, EstimateCompleteAmount = 100, EstimatedFinalCost = 100, Name = "Vendor2", PHApplyPercent = 10 };
            MOP2.Vendors.Add(MOPVendor2);

            List<ProductionMethodType> productionMethodTypeList = new List<ProductionMethodType>();
            productionMethodTypeList.Add(productionMethodType1);
            productionMethodTypeList.Add(productionMethodType2);
            DeliverableBudgetViewModel deliverableBudget1 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                WBSNumber = "123456.123.001",
                MasterVendorId = 10,
                isPrimary = true,
                BudgetGroupId = 55,
                DeliverableBudgetId = 1,
                WBSElementId = 2,
                UserId = 852,
                ProductionMethods = new List<DeliverableMOP>()
            };
            deliverableBudget1.ProductionMethods.Add(MOP1);
            DeliverableBudgetViewModel deliverableBudget2 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                MasterVendorId = 15,
                isPrimary = false,
                BudgetGroupId = 77,
                DeliverableBudgetId = 2,
                WBSNumber = "123456.123.002",
                ProductionMethods = new List<DeliverableMOP>()
            };
            deliverableBudget2.ProductionMethods.Add(MOP2);
            List<DeliverableBudgetViewModel> deliverableBudgetViewModelList = new List<DeliverableBudgetViewModel>();
            deliverableBudgetViewModelList.Add(deliverableBudget1);
            deliverableBudgetViewModelList.Add(deliverableBudget2);
            DeliverableMultiWBSModel deliverableMultiWBSModel = new DeliverableMultiWBSModel()
            {
                DeliverableId = 140000,
                PrimaryWBSElementId = 2,
                IsLaunchFlag = false
            };
            deliverableMultiWBSModel.Budgets = deliverableBudgetList;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                DeliverableId = 140000,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true,
                PrimaryWBSElementId = 2, //wbsElement2.Id
                ClipDeliverMasterVendorId = 100,
                ProducingDepartmentId = 108,
                MarketingGroupChannelId = 1,
                Budgets = deliverableBudgetViewModelList
            };
            deliverableMultiWBSViewModel.Budgets = deliverableBudgetViewModelList;
            Deliverable_WBSElement deliverable_WBSElement = new Deliverable_WBSElement()
            {
                DeliverableId = 14000,
                Id = 1,
            };
            deliverable_WBSElement.Deliverable = testDeliverable;
            deliverable_WBSElement.WBSElement = wbsElement1;
            deliverable_WBSElement.WBSElement = wbsElement2;

            MRMUser mrmUser = new MRMUser()
            {
                Id = 1234,
                FirstName = "Test",
                LastName = "User"
            };
            #endregion

            #region Mocking
            Deliverable DeliverableNull = null;
            mockDeliverableServiceV2.Setup(x => x.GetDeliverableById(It.IsAny<int>(), It.IsAny<string>())).Returns(DeliverableNull);

            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>()))
                .Returns(deliverableMultiWBSViewModel.Budgets.Where(i => i.isPrimary).FirstOrDefault().WBSElementId);
            mockDeliverableBudgetService.Setup(x => x.SaveDeliverableBudget(It.IsAny<DeliverableMultiWBSModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(deliverableMultiWBSModel);
            mockDeliverableRepository.Setup(x => x.GetById(It.IsAny<long>()))
              .Returns(testDeliverable);
            mockDeliverableRepository.Setup(x => x.Update(It.IsAny<Deliverable>()));
            mockDeliverableBudgetRepository.Setup(x => x.Delete(It.IsAny<DeliverableBudget>()));
            mockDeliverableBudgetRepository.Setup(x => x.Add(It.IsAny<DeliverableBudget>()))
                .Returns(deliverableBud1);
            mockUnitOfWork.Setup(x => x.Commit());
            mockDeliverable_WBSElementRepository.Setup(x => x.Update(It.IsAny<Deliverable_WBSElement>()));
            mockDeliverable_WBSElementRepository.Setup(x => x.Add(It.IsAny<Deliverable_WBSElement>()))
                .Returns(deliverable_WBSElement);
            // mockDeliverableRepository.Setup(x => x.Update(It.IsAny<Deliverable>()));
            mockDeliverableBudgetRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableBudget, bool>>>()))
                .Returns(deliverableBudgetList);
            mockDeliverableBudgetRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(deliverableBud1);
            mockDeliverableBudgetRepository.Setup(x => x.UpdateDeliverableBudget(It.IsAny<DeliverableBudget>()));
            mockUserRepository.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(mrmUser);

            #endregion

            #region Service
            var deliverableBudgetService = new DeliverableBudgetServiceMock(deliverableRepository: mockDeliverableRepository.Object,
                deliverableBudgetRepository: mockDeliverableBudgetRepository.Object,
                Deliverable_WBSElementRepository: mockDeliverable_WBSElementRepository.Object, UserRepository: mockUserRepository.Object,
                unitOfWork: mockUnitOfWork.Object);
            var oBudetModel = deliverableBudgetService.SaveDeliverableBudget(deliverableMultiWBSModel, "SWNA\\TestLogin", 0, 99);
            #endregion

            #region Assert

            Assert.IsTrue(oBudetModel != null);

            #endregion
        }
        #endregion

        #region MRM-243
        //Get List of External WBS Elements
        [TestMethod]
        public void GetDeliverableMultiWBSByDeliverableId_GetMultipleExternalWBSElements()
        {
            #region Data Setup

            Guid UniqueId1 = Guid.NewGuid();
            Guid UniqueId2 = Guid.NewGuid();
            DeliverableGroup testDeliverableGroup = new DeliverableGroup()
            {
                Code = "VID"//Video
            };
            TypeOfWork typeOfWork = new TypeOfWork()
            {
                Name = "Type Of Work",
                Id = 166,
                FiscalYear = 2016
            };
            Channel testChannel = new Channel()
            {
                Code = "ROM"//Radio Disney
            };

            BusinessArea businessArea1 = new BusinessArea()
            {
                Id = 101,
                CompanyId = 1
            };

            BusinessArea businessArea2 = new BusinessArea()
            {
                Id = 102,
                CompanyId = 2
            };

            WBSElement wbsElement1 = new WBSElement()
            {
                Id = 1,
                FullWBSNumber = "ABC_External_1",
                TypeOfWorkId = 5000,
                ExternalBusinessAreaId = 101,
                ExternalWBSFlag = true,
                BusinessArea = businessArea1
            };
            WBSElement wbsElement2 = new WBSElement()
            {
                Id = 2,
                FullWBSNumber = "ABC_External_2",
                TypeOfWorkId = 5000,
                ExternalBusinessAreaId = 102,
                ExternalWBSFlag = true,
                BusinessArea = businessArea2
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
                PrimaryDeliverableBudgetUniqueID = UniqueId2,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true
            };

            wbsElement2.TypeOfWork = typeOfWork;
            DeliverableBudget deliverableBud1 = new DeliverableBudget()
            {
                Id = 1,
                DeliverableId = 140000,
                MasterVendorId = 12,
                WBSElementId = wbsElement1.Id,
                DeliverableBudgetUniqueID = UniqueId1

            };
            wbsElement1.TypeOfWork = typeOfWork;
            deliverableBud1.WBSElement = wbsElement1;
            deliverableBud1.Deliverable = testDeliverable;
            deliverableBud1.TypeOfWork = typeOfWork;

            DeliverableBudget deliverableBud2 = new DeliverableBudget()
            {
                Id = 2,
                DeliverableId = 140000,
                MasterVendorId = 25,
                WBSElementId = wbsElement2.Id,
                DeliverableBudgetUniqueID = UniqueId2
            };
            deliverableBud2.WBSElement = wbsElement2;
            deliverableBud2.Deliverable = testDeliverable;
            deliverableBud2.TypeOfWork = typeOfWork;
            List<DeliverableBudget> deliverableBudgetList = new List<DeliverableBudget>();
            deliverableBudgetList.Add(deliverableBud1);
            deliverableBudgetList.Add(deliverableBud2);
            DeliverableBudgetViewModel deliverableBudget1 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                WBSNumber = "External_ABC_1",
                MasterVendorId = 10,
                isPrimary = true,
                DeliverableBudgetId = 1,
                WBSElementId = 1,
                UserId = 852,
                DeliverableBudgetUniqueID = UniqueId1
            };
            DeliverableBudgetViewModel deliverableBudget2 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                MasterVendorId = 15,
                isPrimary = false,
                BudgetGroupId = 77,
                DeliverableBudgetId = 2,
                WBSNumber = "External_ABC_2",
                WBSElementId = 2,
                UserId = 852,
                DeliverableBudgetUniqueID = UniqueId2
            };
            List<DeliverableBudgetViewModel> deliverableBudgetViewModelList = new List<DeliverableBudgetViewModel>();
            deliverableBudgetViewModelList.Add(deliverableBudget1);
            deliverableBudgetViewModelList.Add(deliverableBudget2);
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                DeliverableId = 140000,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true,
                PrimaryDeliverableBudgetUniqueID = UniqueId2,
                ClipDeliverMasterVendorId = 100,
                ProducingDepartmentId = 108,
                MarketingGroupChannelId = 1,
                Budgets = deliverableBudgetViewModelList
            };
            #endregion

            #region Mocking
            mockDeliverableServiceV2.Setup(x => x.GetDeliverableById(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(testDeliverable);
            mockDeliverableRepository.Setup(x => x.GetDeliverableById(It.IsAny<int>()))
                .Returns(testDeliverable);

            mockDeliverableBudgetRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableBudget, bool>>>()))
             .Returns(deliverableBudgetList);
            //         mockDeliverableBudgetManager.Setup(x => x.GetDeliverableMultiWBSByDeliverableId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
            //             .Returns(deliverableMultiWBSViewModel);
            #endregion

            var deliverableBudgetService = new DeliverableBudgetServiceMock(deliverableBudgetRepository: mockDeliverableBudgetRepository.Object);

            var deliverableBudgetManager = new DeliverableBudgetManagerMock(
            deliverableBudgetService: deliverableBudgetService, userSerive: null, intergrationManager: null, deliverableServiceV2: mockDeliverableServiceV2.Object,
            dropDownListService: null);

            #region Service
            DeliverableMultiWBSViewModel result = deliverableBudgetManager.GetDeliverableMultiWBSByDeliverableId(140000, 1, "SWNA\\TestLogin");
            #endregion

            #region Assert
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.Budgets != null);
            Assert.IsTrue(result.DeliverableId == 140000);
            Assert.IsTrue(result.Budgets.ElementAt(0).isExternal == true);
            Assert.IsTrue(result.Budgets.ElementAt(1).isPrimary == true);
            Assert.IsTrue(result.Budgets.Count == 2);
            Assert.IsTrue(result.Budgets.ElementAt(0).TowId == 5000);
            Assert.IsTrue(result.Budgets.ElementAt(0).WBSNumber == "ABC_External_1");
            Assert.IsTrue(result.Budgets.ElementAt(1).TowId == 5000);
            Assert.IsTrue(result.InitialBudgetAmount == 99);
            Assert.IsTrue(result.ProducingDepartmentId == 84);
            Assert.IsFalse(result.ClipDeliverMasterVendorId != 108);
            Assert.IsTrue(result.Budgets.ElementAt(1).DeliverableId == 140000);
            //Assert.IsTrue(result.Budgets.ElementAt(0).UserId == 852);
            Assert.IsTrue(result.Budgets[0].FiscalYearId > 0);
            Assert.IsTrue(result.Budgets[0].TowId > 0);
            Assert.IsTrue(result.Budgets[0].ExternalBusinessAreaId > 0);
            Assert.IsTrue(result.Budgets[0].ExternalCompanyId > 0);
            Assert.IsTrue(result.Budgets[0].isExternal == true);

            // Duplicate combination of tow and business area
            Assert.IsFalse(result.Budgets[0].TowId == result.Budgets[1].TowId
                && result.Budgets[0].ExternalBusinessAreaId == result.Budgets[1].ExternalBusinessAreaId
                );

            Assert.IsFalse(result.IsLaunchFlag == false);
            #endregion
        }

        //MRM-243 : SaveExternalDeliverableBudget. 
        [TestMethod]
        public void SaveExternalDeliverableBudget()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<Disney.MRM.DANG.API.AutoMapper.DomainToViewModelMappingProfile>();
                x.AddProfile<Disney.MRM.DANG.API.AutoMapper.ViewModelToDomainMappingProfile>();
            });

            #region Data Setup
            #region Deliverable
            var deliverable = new Deliverable();
            deliverable.Id = 1000;
            deliverable.Name = "External WBS Test cases";
            #endregion

            #region DeliverableBudget
            var deliverableBudget = new DeliverableBudget();
            deliverableBudget.Id = 101;
            deliverableBudget.DeliverableId = 14028807;
            deliverableBudget.tmp_ExternalWBSFlag = true;
            deliverableBudget.CreatedBy = MRM_USER_ID;
            #endregion

            #region DeliverableBudgetViewModel
            DeliverableBudgetViewModel deliverableBudgetViewModel = new DeliverableBudgetViewModel();
            deliverableBudgetViewModel.ProducingDepartmentId = 84;//MRM Team
            deliverableBudgetViewModel.FiscalYearId = 2016;
            deliverableBudgetViewModel.TowId = 5572;
            deliverableBudgetViewModel.ExternalBusinessAreaId = 3;
            deliverableBudgetViewModel.WBSNumber = "ABC_External_1";
            deliverableBudgetViewModel.DeliverableId = 14028807;
            deliverableBudgetViewModel.DeliverableBudgetId = 0;
            deliverableBudgetViewModel.WBSElementId = 5000;
            deliverableBudgetViewModel.EstimateAmount = 1000;
            deliverableBudgetViewModel.CommittedAmount = 800;
            deliverableBudgetViewModel.ActualAmount = 800;
            deliverableBudgetViewModel.ProductionAmount = 800;
            deliverableBudgetViewModel.CreativeAmount = 800;
            deliverableBudgetViewModel.CreatedBy = MRM_USER_ID;
            #endregion

            #region WBSElement
            WBSElement elem = new WBSElement();
            elem.Id = 5000;
            elem.FullWBSNumber = deliverableBudgetViewModel.WBSNumber;
            elem.ExternalWBSFlag = true;
            #endregion
            #endregion

            #region Mocking
            #region Unit Of Work
            mockUnitOfWork.Setup(uow => uow.Commit());
            #endregion

            #region Deliverable Repository
            mockDeliverableRepository.Setup(x => x.GetDeliverableById(It.IsAny<int>()))
                .Returns(deliverable);
            #endregion

            #region WBS Element Repository
            // Get Single in WBS Element Repository
            mockWBSElementRepository.Setup(x => x.GetSingle(It.IsAny<Expression<Func<WBSElement, bool>>>()))
                            .Returns(elem);
            #endregion

            #region Deliverable Budget Repository
            // Get Single
            mockDeliverableBudgetRepository
                .Setup(x => x.GetSingle(It.IsAny<Expression<Func<DeliverableBudget, bool>>>()))
                .Returns(deliverableBudget);

            // Get Budget By Id
            mockDeliverableBudgetRepository
                .Setup(x => x.GetBudgetById(It.IsAny<int>())).Returns(It.IsAny<DeliverableBudget>());

            // Add Deliverable Budget
            mockDeliverableBudgetRepository.Setup(x => x.Add(It.IsAny<DeliverableBudget>()))
                .Returns(deliverableBudget);

            // Update Budget
            mockDeliverableBudgetRepository.Setup(x => x.UpdateBudget(It.IsAny<DeliverableBudget>()));

            #endregion

            #region DeliverableServiceV2

            var deliverableServiceV2 = new DeliverableServiceV2Mock(deliverableRepository: mockDeliverableRepository.Object);

            #endregion

            #region BudgetService
            var budgetService = new BudgetServiceMock(unitOfWork: mockUnitOfWork.Object,
                iBudgetTypeTOWRepository: mockWBSElementRepository.Object
                );
            #endregion

            #region DeliverableBudgetService
            var deliverableBudgetService = new DeliverableBudgetServiceMock(unitOfWork: mockUnitOfWork.Object,
                  deliverableBudgetRepository: mockDeliverableBudgetRepository.Object
                );

            #endregion

            #region DeliverableBudgetManager
            var deliverableBudgetManager = new DeliverableBudgetManagerMock(
                deliverableBudgetService: deliverableBudgetService,
                deliverableServiceV2: deliverableServiceV2
                );
            #endregion

            // Check if External
            var wbs = budgetService.GetExternalWBS((int)deliverableBudgetViewModel.TowId,
                (int)deliverableBudgetViewModel.ExternalBusinessAreaId);

            Assert.IsTrue(wbs == deliverableBudgetViewModel.WBSNumber);

            #endregion

            //Act
            var result = deliverableBudgetManager.SaveDeliverableBudget(deliverableBudgetViewModel, MRM_USER_ID, NETWORK_LOGIN);

            //Assert
            Assert.IsTrue(result is IntergrationOutBound);


        }
        #endregion

        #region MRM-61
        [TestMethod]
        public void GetDeliverableSummeryofBudgetRepository_Test()
        {

            #region Datasetup
            DeliverableBudgetSummary deliverableID = new DeliverableBudgetSummary
            {
                DeliverableId = 14029501
            };
            List<DeliverableBudgetSummary> deliverableBudgetSummary = new List<DeliverableBudgetSummary>();
            List<DeliverableSummeryofBudgetViewModel> deliverableBudgetSummaryViewmodel = new List<DeliverableSummeryofBudgetViewModel>();
            deliverableBudgetSummary.Add(new DeliverableBudgetSummary { DeliverableId = 14029501, ProductionMethodTypeName = "pmo1", MasterVendorId = 1, ActualAmount = 50 });
            deliverableBudgetSummary.Add(new DeliverableBudgetSummary { DeliverableId = 14029501, ProductionMethodTypeName = "pmo2", MasterVendorId = 2, ActualAmount = 100 });
            deliverableBudgetSummary.Add(new DeliverableBudgetSummary { DeliverableId = 14029501, ProductionMethodTypeName = "pmo3", MasterVendorId = 3, ActualAmount = 150 });
            IQueryable<DeliverableBudgetSummary> deliverableBudgetListQueryable = deliverableBudgetSummary.AsQueryable();
            #endregion

            #region mocking
            mockDeliverableBudgetService.Setup(x => x.GetDeliverableSummeryofCost(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(deliverableBudgetSummary);
            mockDeliverableSummeryofBudgetRepository.Setup(x => x.GetAll())
                .Returns(deliverableBudgetListQueryable);
            mockDeliverableBudgetManager.Setup(x => x.GetDeliverableSummeryofCost(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
               .Returns(deliverableBudgetSummaryViewmodel);
            #endregion

            #region Mapper
            Mapper.CreateMap<DeliverableBudgetSummary, DeliverableSummeryofBudgetViewModel>();
            #endregion

            var deliverableBudgetServiceMock = new DeliverableBudgetServiceMock(DeliverableSummeryofBudgetRepository: mockDeliverableSummeryofBudgetRepository.Object);
            var deliverableBudgetManagerMock = new DeliverableBudgetManagerMock(
            deliverableBudgetService: mockDeliverableBudgetService.Object, userSerive: null, intergrationManager: null, deliverableServiceV2: mockDeliverableServiceV2.Object,
            dropDownListService: null, DeliverableBudgetService: mockDeliverableBudgetService.Object);

            #region service
            List<DeliverableSummeryofBudgetViewModel> result = deliverableBudgetManagerMock.GetDeliverableSummeryofCost(deliverableID.DeliverableId, MRM_USER_ID, NETWORK_LOGIN);
            #endregion

            #region Assert
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.ElementAt(0).MasterVendorId == 1);
            Assert.IsTrue(result.ElementAt(1).ProductionMethodTypeName == "pmo2");
            Assert.IsTrue(result.ElementAt(2).ActualAmount == 150);
            #endregion

        }
        #endregion

        #region MRM-767


        //MRM-767 Validation Internal WBS Scenario 1 (Duplicate FiscalYear,Channel and TowId)
        [TestMethod]
        public void CheckValidationforDuplicateInternalWBS_Scenario1()
        {
            #region Data Setup
            DeliverableGroup testDeliverableGroup = new DeliverableGroup() { Id = 2, Name = "Video", Code = "VID" };//Video
            TypeOfWork typeOfWork = new TypeOfWork() { Name = "Type Of Work", Id = 166 };
            Channel testChannel = new Channel() { Code = "ROM" };//Radio Disney

            Deliverable testDeliverable = new Deliverable()
            {
                Id = 140000,
                DeliverableTypeId = 94,//Affiliate Marketing
                ProducingDepartmentId = 84,//MRM Team
                Name = "Radio Disney Video Deliverable",
                DeliverableGroupId = 2,//Video
                DeliverableGroup = testDeliverableGroup,
                Channel = testChannel,
                ClipDeliverMasterVendorId = 108,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true
            };
            DeliverableBudget deliverableBud1 = new DeliverableBudget() { Id = 1, DeliverableId = 140000, tmp_FiscalYear = 2016, tmp_BudgetGroupChannelId = 1, tmp_TypeOfWorkId = 1, tmp_ExternalWBSFlag = false };
            deliverableBud1.Deliverable = testDeliverable;
            deliverableBud1.TypeOfWork = typeOfWork;
            DeliverableBudget deliverableBud2 = new DeliverableBudget() { Id = 2, DeliverableId = 140000, tmp_FiscalYear = 2016, tmp_BudgetGroupChannelId = 1, tmp_TypeOfWorkId = 1, tmp_ExternalWBSFlag = false };
            deliverableBud2.Deliverable = testDeliverable;
            deliverableBud2.TypeOfWork = typeOfWork;

            List<DeliverableBudget> deliverableBudgetList = new List<DeliverableBudget>();
            deliverableBudgetList.Add(deliverableBud1);
            deliverableBudgetList.Add(deliverableBud2);

            DeliverableBudgetViewModel deliverableBudget1 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                DeliverableBudgetId = 1,
                isPrimary = true,
                FiscalYearId = 2016,
                BudgetTypeId = 1,
                TowId = 1,
                isExternal = false,
                tmp_ExternalWBSFlag = false
            };
            DeliverableBudgetViewModel deliverableBudget2 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                DeliverableBudgetId = 2,
                isPrimary = false,
                FiscalYearId = 2016,
                BudgetTypeId = 1,
                TowId = 1,
                isExternal = false,
                tmp_ExternalWBSFlag = false
            };

            List<DeliverableBudgetViewModel> deliverableBudgetViewModelList = new List<DeliverableBudgetViewModel>();
            deliverableBudgetViewModelList.Add(deliverableBudget1);
            deliverableBudgetViewModelList.Add(deliverableBudget2);

            DeliverableMultiWBSModel deliverableMultiWBSModel = new DeliverableMultiWBSModel() { DeliverableId = 140000, PrimaryWBSElementId = 1, IsLaunchFlag = false };
            deliverableMultiWBSModel.Budgets = deliverableBudgetList;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                DeliverableId = 140000,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true,
                PrimaryWBSElementId = 2, //wbsElement2.Id
                ClipDeliverMasterVendorId = 100,
                ProducingDepartmentId = 108,
                MarketingGroupChannelId = 1,
                Budgets = deliverableBudgetViewModelList
            };
            deliverableMultiWBSViewModel.Budgets = deliverableBudgetViewModelList;
            Deliverable_WBSElement deliverable_WBSElement = new Deliverable_WBSElement()
            {
                DeliverableId = 14000,
                Id = 1,
            };
            deliverable_WBSElement.Deliverable = testDeliverable;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModelout = new DeliverableMultiWBSViewModel();
            #endregion

            #region Mocking
            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>()))
                .Returns(deliverableMultiWBSViewModel.Budgets.Where(i => i.isPrimary).FirstOrDefault().WBSElementId);
            mockDeliverableBudgetService.Setup(x => x.SaveDeliverableBudget(deliverableMultiWBSModel, "SWNA\\TestLogin", 0, 99))
                .Returns(deliverableMultiWBSModel);
            mockDeliverableRepository.Setup(x => x.GetById(It.IsAny<int>()))
              .Returns(testDeliverable);
            mockDeliverableBudgetRepository.Setup(x => x.Delete(It.IsAny<DeliverableBudget>()));
            mockDeliverableBudgetRepository.Setup(x => x.Add(It.IsAny<DeliverableBudget>()));
            mockUnitOfWork.Setup(x => x.Commit());
            mockDeliverable_WBSElementRepository.Setup(x => x.Update(It.IsAny<Deliverable_WBSElement>()));
            mockDeliverable_WBSElementRepository.Setup(x => x.Add(It.IsAny<Deliverable_WBSElement>()))
                .Returns(deliverable_WBSElement);
            mockDeliverableRepository.Setup(x => x.Update(It.IsAny<Deliverable>()));
            mockDeliverableBudgetRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableBudget, bool>>>()))
                .Returns(deliverableBudgetList);
            #endregion

            #region Manager

            var DeliverableBudgetMgr = new DeliverableBudgetManagerMock(deliverableBudgetService: mockDeliverableBudgetService.Object);
            bool isError = false;
            string Msg = string.Empty;
            try
            {
                IntergrationOutBound oIntergrationOutBound = DeliverableBudgetMgr.SaveDeliverableBudget(deliverableMultiWBSViewModel, 1, "",out deliverableMultiWBSViewModelout);
            }
            catch (Exception ex)
            {
                isError = true;
                Msg = ex.Message;
            }
            #endregion

            #region Assert
            Assert.IsTrue(isError);
            Assert.IsTrue(Msg.Contains("Please remove Duplicate WBS Details"));
            #endregion

        }


        //MRM-767 Validation Internal WBS Scenario 2 (Unique WBS Records)
        [TestMethod]
        public void CheckValidationforDuplicateInternalWBS_Scenario2()
        {
            #region Data Setup
            DeliverableGroup testDeliverableGroup = new DeliverableGroup() { Id = 2, Name = "Video", Code = "VID" };//Video
            TypeOfWork typeOfWork = new TypeOfWork() { Name = "Type Of Work", Id = 166 };
            Channel testChannel = new Channel() { Code = "ROM" };//Radio Disney

            Deliverable testDeliverable = new Deliverable()
            {
                Id = 140000,
                DeliverableTypeId = 94,//Affiliate Marketing
                ProducingDepartmentId = 84,//MRM Team
                Name = "Radio Disney Video Deliverable",
                DeliverableGroupId = 2,//Video
                DeliverableGroup = testDeliverableGroup,
                Channel = testChannel,
                ClipDeliverMasterVendorId = 108,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true
            };
            DeliverableBudget deliverableBud1 = new DeliverableBudget() { Id = 1, DeliverableId = 140000, tmp_FiscalYear = 2016, tmp_BudgetGroupChannelId = 1, tmp_TypeOfWorkId = 1, tmp_ExternalWBSFlag = false };
            deliverableBud1.Deliverable = testDeliverable;
            deliverableBud1.TypeOfWork = typeOfWork;
            DeliverableBudget deliverableBud2 = new DeliverableBudget() { Id = 2, DeliverableId = 140000, tmp_FiscalYear = 2015, tmp_BudgetGroupChannelId = 1, tmp_TypeOfWorkId = 2, tmp_ExternalWBSFlag = false };
            deliverableBud2.Deliverable = testDeliverable;
            deliverableBud2.TypeOfWork = typeOfWork;

            List<DeliverableBudget> deliverableBudgetList = new List<DeliverableBudget>();
            deliverableBudgetList.Add(deliverableBud1);
            deliverableBudgetList.Add(deliverableBud2);

            DeliverableBudgetViewModel deliverableBudget1 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                DeliverableBudgetId = 1,
                isPrimary = true,
                FiscalYearId = 2016,
                BudgetTypeId = 1,
                TowId = 1,
                isExternal = false,
                tmp_ExternalWBSFlag = false
            };
            DeliverableBudgetViewModel deliverableBudget2 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                DeliverableBudgetId = 2,
                isPrimary = false,
                FiscalYearId = 2015,
                BudgetTypeId = 1,
                TowId = 2,
                isExternal = false,
                tmp_ExternalWBSFlag = false
            };

            List<DeliverableBudgetViewModel> deliverableBudgetViewModelList = new List<DeliverableBudgetViewModel>();
            deliverableBudgetViewModelList.Add(deliverableBudget1);
            deliverableBudgetViewModelList.Add(deliverableBudget2);

            DeliverableMultiWBSModel deliverableMultiWBSModel = new DeliverableMultiWBSModel() { DeliverableId = 140000, PrimaryWBSElementId = 1, IsLaunchFlag = false };
            deliverableMultiWBSModel.Budgets = deliverableBudgetList;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                DeliverableId = 140000,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true,
                PrimaryWBSElementId = 2, //wbsElement2.Id
                ClipDeliverMasterVendorId = 100,
                ProducingDepartmentId = 108,
                MarketingGroupChannelId = 1,
                Budgets = deliverableBudgetViewModelList
            };
            deliverableMultiWBSViewModel.Budgets = deliverableBudgetViewModelList;
            Deliverable_WBSElement deliverable_WBSElement = new Deliverable_WBSElement()
            {
                DeliverableId = 14000,
                Id = 1,
            };
            deliverable_WBSElement.Deliverable = testDeliverable;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModelout = new DeliverableMultiWBSViewModel();
            #endregion

            #region Mocking
            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>()))
                .Returns(deliverableMultiWBSViewModel.Budgets.Where(i => i.isPrimary).FirstOrDefault().WBSElementId);
            mockDeliverableBudgetService.Setup(x => x.SaveDeliverableBudget(It.IsAny<DeliverableMultiWBSModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(deliverableMultiWBSModel);
            mockDeliverableRepository.Setup(x => x.GetById(It.IsAny<int>()))
              .Returns(testDeliverable);
            mockDeliverableBudgetRepository.Setup(x => x.Delete(It.IsAny<DeliverableBudget>()));
            mockDeliverableBudgetRepository.Setup(x => x.Add(It.IsAny<DeliverableBudget>()));
            mockUnitOfWork.Setup(x => x.Commit());
            mockDeliverable_WBSElementRepository.Setup(x => x.Update(It.IsAny<Deliverable_WBSElement>()));
            mockDeliverable_WBSElementRepository.Setup(x => x.Add(It.IsAny<Deliverable_WBSElement>()))
                .Returns(deliverable_WBSElement);
            mockDeliverableRepository.Setup(x => x.Update(It.IsAny<Deliverable>()));
            mockDeliverableBudgetRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableBudget, bool>>>()))
                .Returns(deliverableBudgetList);
            #endregion

            #region Manager

            var DeliverableBudgetMgr = new DeliverableBudgetManagerMock(deliverableBudgetService: mockDeliverableBudgetService.Object);
            bool isError = false;
            string Msg = string.Empty;
            try
            {
                IntergrationOutBound oIntergrationOutBound = DeliverableBudgetMgr.SaveDeliverableBudget(deliverableMultiWBSViewModel, 1, "", out deliverableMultiWBSViewModelout);
            }
            catch (Exception ex)
            {
                isError = true;
                Msg = ex.Message;
            }
            #endregion

            #region Assert
            Assert.IsTrue(isError == false);
            #endregion

        }



        //MRM-767 Validation External WBS Scenario 3 (Duplicate FiscalYear,Channel and TowId)
        [TestMethod]
        public void CheckValidationforDuplicateExternalWBS_Scenario3()
        {
            #region Data Setup
            DeliverableGroup testDeliverableGroup = new DeliverableGroup() { Id = 2, Name = "Video", Code = "VID" };//Video
            TypeOfWork typeOfWork = new TypeOfWork() { Name = "Type Of Work", Id = 166 };
            Channel testChannel = new Channel() { Code = "ROM" };//Radio Disney

            Deliverable testDeliverable = new Deliverable()
            {
                Id = 140000,
                DeliverableTypeId = 94,//Affiliate Marketing
                ProducingDepartmentId = 84,//MRM Team
                Name = "Radio Disney Video Deliverable",
                DeliverableGroupId = 2,//Video
                DeliverableGroup = testDeliverableGroup,
                Channel = testChannel,
                ClipDeliverMasterVendorId = 108,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true
            };
            DeliverableBudget deliverableBud1 = new DeliverableBudget() { Id = 1, DeliverableId = 140000, tmp_FiscalYear = 2016, tmp_ExternalBusinessAreaId = 1, tmp_ExternalCompanyId = 1, tmp_TypeOfWorkId = 1, tmp_ExternalWBSFlag = true };
            deliverableBud1.Deliverable = testDeliverable;
            deliverableBud1.TypeOfWork = typeOfWork;
            DeliverableBudget deliverableBud2 = new DeliverableBudget() { Id = 2, DeliverableId = 140000, tmp_FiscalYear = 2016, tmp_ExternalBusinessAreaId = 1, tmp_ExternalCompanyId = 1, tmp_TypeOfWorkId = 1, tmp_ExternalWBSFlag = true };
            deliverableBud2.Deliverable = testDeliverable;
            deliverableBud2.TypeOfWork = typeOfWork;

            List<DeliverableBudget> deliverableBudgetList = new List<DeliverableBudget>();
            deliverableBudgetList.Add(deliverableBud1);
            deliverableBudgetList.Add(deliverableBud2);

            DeliverableBudgetViewModel deliverableBudget1 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                DeliverableBudgetId = 1,
                isPrimary = true,
                FiscalYearId = 2016,
                ExternalBusinessAreaId = 1,
                ExternalCompanyId = 1,
                TowId = 1,
                isExternal = true,
                tmp_ExternalWBSFlag = true
            };
            DeliverableBudgetViewModel deliverableBudget2 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                DeliverableBudgetId = 2,
                isPrimary = false,
                FiscalYearId = 2016,
                ExternalBusinessAreaId = 1,
                ExternalCompanyId = 1,
                TowId = 1,
                isExternal = true,
                tmp_ExternalWBSFlag = true
            };

            List<DeliverableBudgetViewModel> deliverableBudgetViewModelList = new List<DeliverableBudgetViewModel>();
            deliverableBudgetViewModelList.Add(deliverableBudget1);
            deliverableBudgetViewModelList.Add(deliverableBudget2);

            DeliverableMultiWBSModel deliverableMultiWBSModel = new DeliverableMultiWBSModel() { DeliverableId = 140000, PrimaryWBSElementId = 1, IsLaunchFlag = false };
            deliverableMultiWBSModel.Budgets = deliverableBudgetList;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                DeliverableId = 140000,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true,
                PrimaryWBSElementId = 2, //wbsElement2.Id
                ClipDeliverMasterVendorId = 100,
                ProducingDepartmentId = 108,
                MarketingGroupChannelId = 1,
                Budgets = deliverableBudgetViewModelList
            };
            deliverableMultiWBSViewModel.Budgets = deliverableBudgetViewModelList;
            Deliverable_WBSElement deliverable_WBSElement = new Deliverable_WBSElement()
            {
                DeliverableId = 14000,
                Id = 1,
            };
            deliverable_WBSElement.Deliverable = testDeliverable;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModelout = new DeliverableMultiWBSViewModel();
            #endregion

            #region Mocking
            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>()))
                .Returns(deliverableMultiWBSViewModel.Budgets.Where(i => i.isPrimary).FirstOrDefault().WBSElementId);
            mockDeliverableBudgetService.Setup(x => x.SaveDeliverableBudget(deliverableMultiWBSModel, "SWNA\\TestLogin", 0, 99))
                .Returns(deliverableMultiWBSModel);
            mockDeliverableRepository.Setup(x => x.GetById(It.IsAny<int>()))
              .Returns(testDeliverable);
            mockDeliverableBudgetRepository.Setup(x => x.Delete(It.IsAny<DeliverableBudget>()));
            mockDeliverableBudgetRepository.Setup(x => x.Add(It.IsAny<DeliverableBudget>()));
            mockUnitOfWork.Setup(x => x.Commit());
            mockDeliverable_WBSElementRepository.Setup(x => x.Update(It.IsAny<Deliverable_WBSElement>()));
            mockDeliverable_WBSElementRepository.Setup(x => x.Add(It.IsAny<Deliverable_WBSElement>()))
                .Returns(deliverable_WBSElement);
            mockDeliverableRepository.Setup(x => x.Update(It.IsAny<Deliverable>()));
            mockDeliverableBudgetRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableBudget, bool>>>()))
                .Returns(deliverableBudgetList);
            #endregion

            #region Manager

            var DeliverableBudgetMgr = new DeliverableBudgetManagerMock(deliverableBudgetService: mockDeliverableBudgetService.Object);
            bool isError = false;
            string Msg = string.Empty;
            try
            {
                IntergrationOutBound oIntergrationOutBound = DeliverableBudgetMgr.SaveDeliverableBudget(deliverableMultiWBSViewModel, 1, "", out deliverableMultiWBSViewModelout);
            }
            catch (Exception ex)
            {
                isError = true;
                Msg = ex.Message;
            }
            #endregion

            #region Assert
            Assert.IsTrue(isError);
            Assert.IsTrue(Msg.Contains("Please remove Duplicate WBS Details"));
            #endregion

        }


        //MRM-767 Validation External WBS Scenario 4 (Unique WBS Records)
        [TestMethod]
        public void CheckValidationforDuplicateExternalWBS_Scenario4()
        {
            #region Data Setup
            DeliverableGroup testDeliverableGroup = new DeliverableGroup() { Id = 2, Name = "Video", Code = "VID" };//Video
            TypeOfWork typeOfWork = new TypeOfWork() { Name = "Type Of Work", Id = 166 };
            Channel testChannel = new Channel() { Code = "ROM" };//Radio Disney

            Deliverable testDeliverable = new Deliverable()
            {
                Id = 140000,
                DeliverableTypeId = 94,//Affiliate Marketing
                ProducingDepartmentId = 84,//MRM Team
                Name = "Radio Disney Video Deliverable",
                DeliverableGroupId = 2,//Video
                DeliverableGroup = testDeliverableGroup,
                Channel = testChannel,
                ClipDeliverMasterVendorId = 108,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true
            };
            DeliverableBudget deliverableBud1 = new DeliverableBudget() { Id = 1, DeliverableId = 140000, tmp_FiscalYear = 2016, tmp_ExternalCompanyId = 1, tmp_ExternalBusinessAreaId = 2, tmp_TypeOfWorkId = 1, tmp_ExternalWBSFlag = true };
            deliverableBud1.Deliverable = testDeliverable;
            deliverableBud1.TypeOfWork = typeOfWork;
            DeliverableBudget deliverableBud2 = new DeliverableBudget() { Id = 2, DeliverableId = 140000, tmp_FiscalYear = 2015, tmp_ExternalCompanyId = 2, tmp_ExternalBusinessAreaId = 2, tmp_TypeOfWorkId = 2, tmp_ExternalWBSFlag = true };
            deliverableBud2.Deliverable = testDeliverable;
            deliverableBud2.TypeOfWork = typeOfWork;

            List<DeliverableBudget> deliverableBudgetList = new List<DeliverableBudget>();
            deliverableBudgetList.Add(deliverableBud1);
            deliverableBudgetList.Add(deliverableBud2);

            DeliverableBudgetViewModel deliverableBudget1 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                DeliverableBudgetId = 1,
                isPrimary = true,
                FiscalYearId = 2016,
                TowId = 1,
                ExternalCompanyId = 1,
                ExternalBusinessAreaId = 2,
                isExternal = true,
                tmp_ExternalWBSFlag = true
            };
            DeliverableBudgetViewModel deliverableBudget2 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                DeliverableBudgetId = 2,
                isPrimary = false,
                FiscalYearId = 2015,
                TowId = 1,
                ExternalCompanyId = 2,
                ExternalBusinessAreaId = 1,
                isExternal = false,
                tmp_ExternalWBSFlag = false
            };

            List<DeliverableBudgetViewModel> deliverableBudgetViewModelList = new List<DeliverableBudgetViewModel>();
            deliverableBudgetViewModelList.Add(deliverableBudget1);
            deliverableBudgetViewModelList.Add(deliverableBudget2);

            DeliverableMultiWBSModel deliverableMultiWBSModel = new DeliverableMultiWBSModel() { DeliverableId = 140000, PrimaryWBSElementId = 1, IsLaunchFlag = false };
            deliverableMultiWBSModel.Budgets = deliverableBudgetList;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                DeliverableId = 140000,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true,
                PrimaryWBSElementId = 2, //wbsElement2.Id
                ClipDeliverMasterVendorId = 100,
                ProducingDepartmentId = 108,
                MarketingGroupChannelId = 1,
                Budgets = deliverableBudgetViewModelList
            };
            deliverableMultiWBSViewModel.Budgets = deliverableBudgetViewModelList;
            Deliverable_WBSElement deliverable_WBSElement = new Deliverable_WBSElement()
            {
                DeliverableId = 14000,
                Id = 1,
            };
            deliverable_WBSElement.Deliverable = testDeliverable;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModelout = new DeliverableMultiWBSViewModel();
            #endregion

            #region Mocking
            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>()))
                .Returns(deliverableMultiWBSViewModel.Budgets.Where(i => i.isPrimary).FirstOrDefault().WBSElementId);
            mockDeliverableBudgetService.Setup(x => x.SaveDeliverableBudget(It.IsAny<DeliverableMultiWBSModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(deliverableMultiWBSModel);
            mockDeliverableRepository.Setup(x => x.GetById(It.IsAny<int>()))
              .Returns(testDeliverable);
            mockDeliverableBudgetRepository.Setup(x => x.Delete(It.IsAny<DeliverableBudget>()));
            mockDeliverableBudgetRepository.Setup(x => x.Add(It.IsAny<DeliverableBudget>()));
            mockUnitOfWork.Setup(x => x.Commit());
            mockDeliverable_WBSElementRepository.Setup(x => x.Update(It.IsAny<Deliverable_WBSElement>()));
            mockDeliverable_WBSElementRepository.Setup(x => x.Add(It.IsAny<Deliverable_WBSElement>()))
                .Returns(deliverable_WBSElement);
            mockDeliverableRepository.Setup(x => x.Update(It.IsAny<Deliverable>()));
            mockDeliverableBudgetRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableBudget, bool>>>()))
                .Returns(deliverableBudgetList);
            #endregion

            #region Manager

            var DeliverableBudgetMgr = new DeliverableBudgetManagerMock(deliverableBudgetService: mockDeliverableBudgetService.Object);
            bool isError = false;
            string Msg = string.Empty;
            try
            {
                IntergrationOutBound oIntergrationOutBound = DeliverableBudgetMgr.SaveDeliverableBudget(deliverableMultiWBSViewModel, 1, "",out deliverableMultiWBSViewModelout);
            }
            catch (Exception ex)
            {
                isError = true;
                Msg = ex.Message;
            }
            #endregion

            #region Assert
            Assert.IsTrue(isError == false);
            #endregion

        }


        //MRM-767 -  Save Deliverable Budget with out WBSElement
        [TestMethod]
        public void SaveDeliverableBudget_WithoutWBSElement()
        {
            #region Data Setup
            DeliverableGroup testDeliverableGroup = new DeliverableGroup() { Code = "VID" };
            TypeOfWork typeOfWork = new TypeOfWork() { Name = "Type Of Work", Id = 166 };
            Channel testChannel = new Channel() { Code = "ROM" };//Radio Disney
            Guid DeliverableBudgetUniqueID1 = Guid.NewGuid();
            Guid DeliverableBudgetUniqueID2 = Guid.NewGuid();
            Deliverable testDeliverable = new Deliverable()
            {
                Id = 140000,
                DeliverableTypeId = 94,//Affiliate Marketing
                ProducingDepartmentId = 84,//MRM Team
                Name = "Radio Disney Video Deliverable",
                DeliverableGroupId = 2,//Video
                DeliverableGroup = testDeliverableGroup,
                Channel = testChannel,
                PrimaryDeliverableBudgetUniqueID = DeliverableBudgetUniqueID1,
                ClipDeliverMasterVendorId = 108,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true
            };

            DeliverableBudget deliverableBud1 = new DeliverableBudget()
            {
                Id = 1,
                DeliverableId = 140000,
                MasterVendorId = 12,
                tmp_TypeOfWorkId = 1,
                tmp_FiscalYear = 2016,
                tmp_BudgetTypeId = 3,
                DeliverableBudgetUniqueID = DeliverableBudgetUniqueID1
            };
            deliverableBud1.Deliverable = testDeliverable;
            DeliverableBudget deliverableBud2 = new DeliverableBudget()
            {
                Id = 2,
                DeliverableId = 140000,
                MasterVendorId = 25,
                tmp_TypeOfWorkId = 2,
                tmp_FiscalYear = 2016,
                tmp_BudgetTypeId = 4,
                DeliverableBudgetUniqueID = DeliverableBudgetUniqueID2
            };
            deliverableBud2.Deliverable = testDeliverable;
            deliverableBud2.TypeOfWork = typeOfWork;
            List<DeliverableBudget> deliverableBudgetList = new List<DeliverableBudget>();
            deliverableBudgetList.Add(deliverableBud1);
            deliverableBudgetList.Add(deliverableBud2);
            ProductionMethodType productionMethodType1 = new ProductionMethodType() { Id = 1, Code = "PH", Name = "Post House" };
            ProductionMethodType productionMethodType2 = new ProductionMethodType() { Id = 2, Code = "CS", Name = "Creative Services" };
            List<ProductionMethodType> productionMethodTypeList = new List<ProductionMethodType>();
            productionMethodTypeList.Add(productionMethodType1);
            productionMethodTypeList.Add(productionMethodType2);

            List<int> pmoList = new List<int>();
            pmoList.Add(productionMethodTypeList.ElementAt(0).Id);
            pmoList.Add(productionMethodTypeList.ElementAt(1).Id);
            DeliverableBudgetViewModel deliverableBudget1 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                WBSNumber = "123456.123.001",
                MasterVendorId = 10,
                isPrimary = true,
                TowId = 1,
                BudgetTypeId = 3,
                FiscalYearId = 2016,
                BudgetGroupId = 55,
                DeliverableBudgetId = 1,
                UserId = 852,
                DeliverableBudgetUniqueID = DeliverableBudgetUniqueID1
            };
            deliverableBudget1.ProductionMethodTypeIds = pmoList;
            DeliverableBudgetViewModel deliverableBudget2 = new DeliverableBudgetViewModel()
            {
                DeliverableId = 140000,
                MasterVendorId = 15,
                isPrimary = false,
                TowId = 2,
                BudgetTypeId = 4,
                FiscalYearId = 2016,
                BudgetGroupId = 77,
                DeliverableBudgetId = 2,
                DeliverableBudgetUniqueID = DeliverableBudgetUniqueID2
            };
            deliverableBudget2.ProductionMethodTypeIds = pmoList;
            List<DeliverableBudgetViewModel> deliverableBudgetViewModelList = new List<DeliverableBudgetViewModel>();
            deliverableBudgetViewModelList.Add(deliverableBudget1);
            deliverableBudgetViewModelList.Add(deliverableBudget2);
            DeliverableMultiWBSModel deliverableMultiWBSModel = new DeliverableMultiWBSModel()
            {
                DeliverableId = 140000,
                PrimaryDeliverableBudgetUniqueID = DeliverableBudgetUniqueID1,
                IsLaunchFlag = false
            };
            deliverableMultiWBSModel.Budgets = deliverableBudgetList;
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                DeliverableId = 140000,
                InitialBudgetAmount = 99,
                IsLaunchFlag = true,
                PrimaryDeliverableBudgetUniqueID = DeliverableBudgetUniqueID1,
                ClipDeliverMasterVendorId = 100,
                ProducingDepartmentId = 108,
                MarketingGroupChannelId = 1,
                Budgets = deliverableBudgetViewModelList
            };
            deliverableMultiWBSViewModel.Budgets = deliverableBudgetViewModelList;
            Deliverable_WBSElement deliverable_WBSElement = new Deliverable_WBSElement()
            {
                DeliverableId = 14000,
                Id = 1,
            };
            deliverable_WBSElement.Deliverable = testDeliverable;
            MRMUser mrmUser = new MRMUser()
            {
                Id=1234,
                FirstName = "Test",
                LastName = "User"
            };
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModelout = new DeliverableMultiWBSViewModel();
            #endregion

            #region Mocking
            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>()))
                .Returns(deliverableMultiWBSViewModel.Budgets.Where(i => i.isPrimary).FirstOrDefault().WBSElementId);
            mockDeliverableBudgetService.Setup(x => x.SaveDeliverableBudget(It.IsAny<DeliverableMultiWBSModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                  .Returns(deliverableMultiWBSModel);
            mockDeliverableRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(testDeliverable);
            mockDeliverableBudgetRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(deliverableBud1);
            mockDeliverableBudgetRepository.Setup(x => x.UpdateDeliverableBudget(It.IsAny<DeliverableBudget>()));
            mockDeliverableBudgetRepository.Setup(x => x.Delete(It.IsAny<DeliverableBudget>()));
            mockDeliverableBudgetRepository.Setup(x => x.Add(It.IsAny<DeliverableBudget>()));
            mockDeliverableBudgetRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<DeliverableBudget, bool>>>()))
                .Returns(deliverableBudgetList);
            mockUnitOfWork.Setup(x => x.Commit());
            mockDeliverable_WBSElementRepository.Setup(x => x.Update(It.IsAny<Deliverable_WBSElement>()));
            mockDeliverable_WBSElementRepository.Setup(x => x.Add(It.IsAny<Deliverable_WBSElement>()))
                .Returns(deliverable_WBSElement);
            mockDeliverableRepository.Setup(x => x.Update(It.IsAny<Deliverable>()));
            mockUserRepository.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(mrmUser);
            #endregion

            #region Manager

            var DeliverableBudgetMgr = new DeliverableBudgetManagerMock(deliverableBudgetService: mockDeliverableBudgetService.Object);
            var DeliverableServiceMgr = new DeliverableBudgetServiceMock(deliverableRepository: mockDeliverableRepository.Object,
                                                            deliverableBudgetRepository: mockDeliverableBudgetRepository.Object, UserRepository:mockUserRepository.Object,
                                                            unitOfWork: mockUnitOfWork.Object);
            bool isError = false;
            IntergrationOutBound oIntergrationOutBound = null;
            DeliverableMultiWBSModel result = null;
            string Username = "SWNA//TAKAS031'";
            int mrmUserId = 3254;
            try
            {
                //Manager Level
                oIntergrationOutBound = DeliverableBudgetMgr.SaveDeliverableBudget(deliverableMultiWBSViewModel, mrmUserId, Username,out deliverableMultiWBSViewModelout);


                //Service Level
                result = DeliverableServiceMgr.SaveDeliverableBudget(deliverableMultiWBSModel, Username);

            }
            catch (Exception ex)
            {
                isError = true;
            }

            #endregion

            #region Assert
            Assert.IsTrue(DeliverableBudgetMgr != null);
            Assert.IsTrue(isError == false);
            Assert.IsTrue(oIntergrationOutBound != null);
            Assert.IsInstanceOfType(oIntergrationOutBound, typeof(IntergrationOutBound));


            //Service Level
            Assert.IsTrue(result != null);
            Assert.IsInstanceOfType(result, typeof(DeliverableMultiWBSModel));
            Assert.IsTrue(result.Budgets.Count > 0);
            Assert.AreEqual(result.PrimaryDeliverableBudgetUniqueID, DeliverableBudgetUniqueID1);


            #endregion

        }

        #endregion

        #region MRM-159

        //Get Deliverable Search 
        [TestMethod]
        public void GetDeliverableSearch()
        {
            #region Data Setup
            #region DeliverableSearch_Result
            List<DeliverableSearch_Result> resultList = new List<DeliverableSearch_Result>();
            List<DeliverableSearch_Result> expectedResultList = new List<DeliverableSearch_Result>();

            var result1 = new DeliverableSearch_Result();

            result1.DeliverableId = 1001;
            result1.DeliverableName = "Deliverable1";
            result1.MarketingGroupChannelName = "Disney Channel";
            result1.MarketingGroupChannelId = 3;
            result1.FullWBSNumber = "112.00.11.1";
            result1.FiscalYear = 2016;
            result1.DeliverableStatusName = "In Progress";
            result1.DeliverableGroupName = "Off Air";
            result1.OWP_MasterVendorNames = "Vendor1";

            resultList.Add(result1);
            expectedResultList.Add(result1);

            var result2 = new DeliverableSearch_Result();

            result2.DeliverableId = 1002;
            result2.DeliverableName = "Deliverable2";
            result2.MarketingGroupChannelName = "Disney XD";
            result2.MarketingGroupChannelId = 2;
            result2.FullWBSNumber = "112.00.11.2";
            result2.FiscalYear = 2016;
            result2.DeliverableStatusName = "In Progress";
            result2.DeliverableGroupName = "Off Air";
            result2.OWP_MasterVendorNames = "Vendor2";
            resultList.Add(result2);

            var result3 = new DeliverableSearch_Result();

            result3.DeliverableId = 1003;
            result3.DeliverableName = "Deliverable3";
            result3.MarketingGroupChannelName = "Disney Channel";
            result3.MarketingGroupChannelId = 3;
            result3.FullWBSNumber = "112.00.11.1";
            result3.FiscalYear = 2016;
            result3.DeliverableStatusName = "In Progress";
            result3.DeliverableGroupName = "Off Air";
            result3.OWP_MasterVendorNames = "Vendor3";
            resultList.Add(result3);
            expectedResultList.Add(result3);

            #endregion

            #region BulkUpdateDeliverableSearchViewModel
            var inputModel = new BulkUpdatDeliverableSearchViewModel();
            inputModel.MarketingGroupIds = new List<int>(new int[] { 3, 5, 7 });
            inputModel.WBSId = 1000;
            inputModel.SearchContextCode = "BUDM";
            inputModel.ExcludeDeliverableIds = null;
            inputModel.TowIds = null;
            inputModel.WriterProducerIds = null;
            inputModel.DeliverableIds = null;
            inputModel.BroadcastChannelIds = null;
            inputModel.BudgetGroupChannelIds = null;
            inputModel.DeliverableName = string.Empty;
            #endregion

            #endregion

            #region Mocking
            //   Nullable< DateTime > lastUpdatedDate, string budgetGroupChannelIds, string searchContextCode, string excludeDeliverableIds,int? wbsId

            mockDeliverableRepository.Setup(x => x.GetDeliverableSearchResults(It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<int?>(),
                                                                               It.IsAny<System.DateTime?>(),
                                                                               It.IsAny<System.DateTime?>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<System.DateTime?>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<int?>()
                ))
                .Returns(expectedResultList.AsQueryable());

            #endregion

            #region Service
            var deliverableService = new DeliverableServiceV2Mock(deliverableRepository: mockDeliverableRepository.Object);

            var result = deliverableService.GetBulkUpdateDeliverablesSearchResults(inputModel).ToList();
            #endregion

            #region Assert
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result[0].DeliverableId == 1001);
            Assert.IsTrue(result[1].FullWBSNumber == "112.00.11.1");

            #endregion
        }

        //Get Deliverable Search 
        [TestMethod]
        public void GetDeliverableSearchWBS()
        {
            #region Data Setup
            #region DeliverableSearchWBS_Result
            List<DeliverableSearchWBS_Result> resultList = new List<DeliverableSearchWBS_Result>();
            List<DeliverableSearchWBS_Result> expectedResultList = new List<DeliverableSearchWBS_Result>();

            var result1 = new DeliverableSearchWBS_Result();

            result1.DeliverableId = 1001;
            result1.DeliverableName = "Deliverable1";
            result1.MarketingGroupChannelName = "Disney Channel";
            result1.MarketingGroupChannelId = 3;
            result1.FullWBSNumber = "112.00.11.1";
            result1.FiscalYear = 2016;
            result1.DeliverableStatusName = "In Progress";
            result1.TypeOfWorkName = "Composing";
            result1.TypeOfWorkCategoryName = "Orchestra";
            result1.mop_vendorNames = "Music World";

            resultList.Add(result1);
            expectedResultList.Add(result1);

            var result2 = new DeliverableSearchWBS_Result();

            result2.DeliverableId = 1002;
            result2.DeliverableName = "Deliverable2";
            result2.MarketingGroupChannelName = "Disney Junior";
            result2.MarketingGroupChannelId = 5;
            result2.FullWBSNumber = "112.00.11.2";
            result2.FiscalYear = 2016;
            result2.DeliverableStatusName = "In Progress";
            result2.TypeOfWorkName = "Composing";
            result2.TypeOfWorkCategoryName = "Orchestra";
            result2.mop_vendorNames = "Music World";
            resultList.Add(result2);

            var result3 = new DeliverableSearchWBS_Result();

            result3.DeliverableId = 1003;
            result3.DeliverableName = "Deliverable3";
            result3.MarketingGroupChannelName = "Disney Channel";
            result3.MarketingGroupChannelId = 3;
            result3.FullWBSNumber = "112.00.11.1";
            result3.FiscalYear = 2016;
            result3.DeliverableStatusName = "In Progress";
            result3.TypeOfWorkName = "Composing";
            result3.TypeOfWorkCategoryName = "Orchestra";
            result3.mop_vendorNames = "Music World";

            resultList.Add(result3);
            expectedResultList.Add(result3);

            #endregion

            #region BulkUpdateDeliverableSearchViewModel
            var inputModel = new BulkUpdatDeliverableSearchViewModel();
            inputModel.MarketingGroupIds = new List<int>(new int[] { 3, 5, 7 });
            inputModel.WBSId = 1000;
            inputModel.SearchContextCode = "BUDM";
            inputModel.ExcludeDeliverableIds = null;
            inputModel.TowIds = null;
            inputModel.WriterProducerIds = null;
            inputModel.DeliverableIds = null;
            inputModel.BroadcastChannelIds = null;
            inputModel.BudgetGroupChannelIds = null;
            inputModel.DeliverableName = string.Empty;
            #endregion

            #endregion

            #region Mocking
            //   Nullable< DateTime > lastUpdatedDate, string budgetGroupChannelIds, string searchContextCode, string excludeDeliverableIds,int? wbsId

            mockDeliverableRepository.Setup(x => x.GetDeliverableWBSSearchResults(It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<int?>(),
                                                                               It.IsAny<System.DateTime?>(),
                                                                               It.IsAny<System.DateTime?>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<System.DateTime?>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<string>(),
                                                                               It.IsAny<int?>()
                ))
                .Returns(expectedResultList.AsQueryable());

            #endregion

            #region Service
            var deliverableService = new DeliverableServiceV2Mock(deliverableRepository: mockDeliverableRepository.Object);

            var result = deliverableService.GetBulkUpdateDeliverablesWBSSearchResults(inputModel).ToList();
            #endregion

            #region Assert
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result[0].DeliverableId == 1001);
            Assert.IsTrue(result[1].FullWBSNumber == "112.00.11.1");

            #endregion
        }

        #endregion

        #region MRM-160
         [TestMethod]
        public void SaveBulkUpdateDeliverablesWBS_Test()
        {
            #region Data Setup
            List<DeliverableWBSSearchViewModel> resultList = new List<DeliverableWBSSearchViewModel>();
            Guid test1 = new Guid();
            Guid test2 = new Guid();
            var result1 = new DeliverableWBSSearchViewModel();
            result1.DeliverableId = 1001;
            result1.DeliverableName = "Deliverable1";
            result1.MarketingGroupChannelName = "Disney Channel";
            result1.MarketingGroupChannelId = 3;
            result1.FullWBSNumber = "112.00.11.1";
            result1.FiscalYear = 2016;
            result1.DeliverableStatusName = "In Progress";
            result1.MOP = "Internal";
            result1.MOPCode = "INTRN";
            result1.WBSIdOld = 1;
            result1.WBSId = 99;
            result1.BudgetGroupChannelCode = "Disney Channel";
            result1.BudgetGroupChannelId = 1;
            result1.BudgetTypeId = 1;
            result1.DeliverableBudgetId = 555;
            result1.deliverableBudgetUID = test1;
            result1.IsWBSExternal = false;
            result1.TypeOfWorkId = 745;
            result1.mop_vendorIds = 11;
            result1.timestamp = Encoding.ASCII.GetBytes("0x00000000012A0127");
            result1.mop_vendorNames = "mv1,mv2";
            

            resultList.Add(result1);
                        
            var result2 = new DeliverableWBSSearchViewModel();
            result2.DeliverableId = 1002;
            result2.DeliverableName = "Deliverable2";
            result2.MarketingGroupChannelName = "Disney XD";
            result2.MarketingGroupChannelId = 2;
            result2.FullWBSNumber = "112.00.11.2";
            result2.FiscalYear = 2016;
            result2.DeliverableStatusName = "In Progress";
            result2.MOP = "Contract Request";
            result2.MOPCode = "CR";
            result2.WBSIdOld = 2;
            result2.WBSId = 98;
            result2.BudgetGroupChannelCode = "Disney Channel";
            result2.BudgetGroupChannelId = 1;
            result2.BudgetTypeId = 1;
            result2.DeliverableBudgetId = 597;
            result2.deliverableBudgetUID = test2;
            result2.IsWBSExternal = false;
            result2.TypeOfWorkId = 1145;
            result2.mop_vendorIds = 33;
            result2.mop_vendorNames = "mv3,mv4";
            result2.timestamp = Encoding.ASCII.GetBytes("0x00000000012A0127");
            resultList.Add(result2);

            Deliverable testDeliverable = new Deliverable()
            {
                Id = 1001,
                timestamp= Encoding.ASCII.GetBytes("0x00000000012A0127"),
                ClipDeliverMasterVendorId=408,
                InitialBudgetAmount=1453,
            };
            testDeliverable.PrimaryDeliverableBudgetUniqueID = test1;

            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                InitialBudgetAmount = 123,
                DeliverableId = 1001,
                IsLaunchFlag = true,
                PrimaryDeliverableBudgetUniqueID = new Guid()
            };
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModelout = new DeliverableMultiWBSViewModel();

            IntergrationOutBound intergrationOutBound = new IntergrationOutBound();
            #endregion

            #region Mocking
            mockDeliverableServiceV2.Setup(x => x.GetDeliverableById(It.IsAny<int>(), It.IsAny<string>())).Returns(testDeliverable);
            mockDeliverableGeneralInfoManager.Setup(x => x.GetDeliverableGeneralInfoViewModel(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(()=>null);
            mockDeliverableManager.Setup(x=>x.SaveGeneralInfoViewModel(It.IsAny<DeliverableGeneralInfoViewModel>(),
                It.IsAny<int>(),It.IsAny<string>(),true)).Returns(()=>null);
            mockDeliverableBudgetManager.Setup(x => x.SaveDeliverableBudget(It.IsAny<DeliverableMultiWBSViewModel>(), It.IsAny<int>()
                , It.IsAny<string>(),out deliverableMultiWBSViewModelout))
                .Returns(()=>null);
            mockIIntergrationManager.Setup(x => x.GetPromoPlacementForAssetManagement(It.IsAny<Deliverable>(), It.IsAny<string>())).Returns(() => null);
            mockIIntergrationManager.Setup(x => x.GetDeliverableMediaPulse(It.IsAny<Deliverable>(), It.IsAny<string>()))
                .Returns(()=>null);
            mockIIntergrationManager.Setup(x => x.GetDefaultWorkOrdersMediaPulse(It.IsAny<Deliverable>(), It.IsAny<string>()))
                .Returns(() => null);
            #endregion

            #region Director
            var controller = new DeliverableDirectorMock(deliverableService:mockDeliverableServiceV2.Object,
                deliverableGeneralInfoManager: mockDeliverableGeneralInfoManager.Object,
                deliverableManager:mockDeliverableManager.Object,
                budgetManager: mockDeliverableBudgetManager.Object,
                intergrationManager: mockIIntergrationManager.Object);
            List<DeliverableSaveModel> result = controller.SaveBulkUpdateDeliverablesWBS(resultList,495,1000,"SWNA\\TestLogin");
            #endregion

            #region Assert
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.ElementAt(0).id == result1.DeliverableId);
            Assert.IsTrue(result.ElementAt(1).timestamp == Convert.ToBase64String(result1.timestamp));
            #endregion
        }

        #endregion

        #region MRM-549
        [TestMethod]
        public void GetBudgetUsers_Test()
        {
            #region DataSetup
            int mrmUserId = 999;
            string networkLogin = "SWNA\\TestLogin";
            MRMUser users = new MRMUser()
            {
                DepartmentId = 1,
                FirstName = "Test",
                LastName="BudgetUser",
                Id=120                
            };
            Department dept1 = new Department()
            {
               Id=1,
               Name="MRMUSER"
            };

            users.Department = dept1;
          
            List<MRMUser> userslist = new List<MRMUser>();
            userslist.Add(users);
            
            #endregion
            #region Mocking
            mockUserRepository.Setup(x => x.GetAll()).Returns(userslist.AsQueryable());
            mockUserservice.Setup(x => x.GetBudgetUsers()).Returns(userslist);
            #endregion
            #region Service
            var userservice = new UserServiceMock(_userRepository: mockUserRepository.Object);

            var deliverableV2Controller = new DeliverableV2ControllerMock(userService: mockUserservice.Object);

            var result = userservice.GetBudgetUsers();
           var usersdata = deliverableV2Controller.GetBudgetUsers(mrmUserId,networkLogin);
            #endregion
            #region Assets
            Assert.IsTrue(usersdata != null);           
           
            #endregion
        }

        [TestMethod]
        public void GetLegalUsers_Test()
        {
            #region DataSetup
            int mrmUserId = 999;
            string networkLogin = "SWNA\\TestLogin";
            MRMUser users = new MRMUser()
            {
                DepartmentId = 2,
                FirstName = "Test",
                LastName = "LegalUser",
                Id = 121,
                UserTitleId = 5
                                
            };
            Department dept1 = new Department()
            {
                Id = 1,
                Name = "MRMUSER"
            };
            UserTitle usertitle1 = new UserTitle()
            {
                Id = 5,
                Code = "LGL"
            };
            users.Department = dept1;
            users.UserTitle = usertitle1;

            List<MRMUser> userslist = new List<MRMUser>();
            userslist.Add(users);

            #endregion
            #region Mocking
            mockUserRepository.Setup(x => x.GetAll()).Returns(userslist.AsQueryable());
            mockUserservice.Setup(x => x.GetLegalUsers()).Returns(userslist);
            #endregion
            #region Service
            var userservice = new UserServiceMock(_userRepository: mockUserRepository.Object);
            var deliverableV2Controller = new DeliverableV2ControllerMock(userService: mockUserservice.Object);

            var result = userservice.GetLegalUsers();
            var usersdata = deliverableV2Controller.GetLegalUsers(mrmUserId, networkLogin);
            #endregion
            #region Assets
            Assert.IsTrue(usersdata != null);           
            
            #endregion
        }
        #endregion

        #region MRM-548
        [TestMethod]
        public void GetUserDetailsByUserId_Test()
        {
            #region DataSetup
            string networkLogin = "SWNA\\TestLogin";
            MRMUser users = new MRMUser()
            {
                Id = 562,
                EmailAddress = "Srinivasa.Reddy.X.Manukonda.-ND@abc.com"
            };
            MRMUser objTestData = new MRMUser()
            {
                EmailAddress = "Srinivasa.Reddy.X.Manukonda.-ND@abc.com"
            };

            List<MRMUser> userslist = new List<MRMUser>();
            userslist.Add(users);

            #endregion
            #region Mocking
            mockUserRepository.Setup(x => x.GetAll()).Returns(userslist.AsQueryable());
            mockUserservice.Setup(x => x.GetMRMUserByUserId(users.Id)).Returns(userslist);
            #endregion
            #region Service
            var userservice = new UserServiceMock(_userRepository: mockUserRepository.Object);

            var deliverableV2Controller = new DeliverableV2ControllerMock(userService: mockUserservice.Object);

            var result = userservice.GetMRMUserByUserId(users.Id);
            var usersdata = deliverableV2Controller.GetMRMUserByUserId(Convert.ToString(result.FirstOrDefault().Id), networkLogin);
            #endregion
            #region Assets
            Assert.IsFalse(usersdata == null);
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.ElementAt(0).EmailAddress == objTestData.EmailAddress);
            #endregion
        }
        #endregion

        #region MRM-672
        [TestMethod]
        public void GetLinkCRDeliverableTest()
        {
            #region Data           
            string networkLogin = "SWNA\\TestLogin";
            List<DropDownViewModel> Crdeliverable = new List<DropDownViewModel>();
            DeliverableGroup delgroup = new DeliverableGroup()
            {
                Code="CR",
                CreatedBy=556,
                Name="TestCR",
                Id=3
            };
            DeliverableBudget delbudget = new DeliverableBudget()
            {
                CreatedBy=556,
                Id=254,
                MasterVendorId=61,
                CostCenterId=20
            };
            List<DeliverableBudget> delbudgetlist = new List<DeliverableBudget>();
            delbudgetlist.Add(delbudget);
            ContractRequest crt = new ContractRequest()
            {
                Id=1456235,
                CreatedBy=556
            };
            ContractRequest_Deliverable crtdel = new ContractRequest_Deliverable()
            {
                Id=1,
                ContractRequest=crt,
                CreatedBy=556,
                DeliverableId=1456235
            };
            List<ContractRequest_Deliverable> crtdellist = new List<ContractRequest_Deliverable>();
            Deliverable deliverable1 = new Deliverable()
            {
                Id=1456235,
                CreatedBy=556,
                DeliverableGroup= delgroup,
                DeliverableBudget= delbudgetlist,
                Name="TestCR",
                ContractRequest=crt,
                ContractRequest_Deliverable= crtdellist

            };
            List<Deliverable> deliverablelist = new List<Deliverable>();
            deliverablelist.Add(deliverable1);
            #endregion
            #region Mocking
            mockDeliverableManager.Setup(a => a.GetLinkCRDeliverable(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
           .Returns(Crdeliverable);
            mockDeliverableServiceV2.Setup(a => a.GetLinkCRDeliverable(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
           .Returns(Crdeliverable);
            mockDeliverableRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<Deliverable, bool>>>()))
               .Returns(deliverablelist);
            mockDeliverableRepository.Setup(x => x.GetById(It.IsAny<long>()))
               .Returns(deliverable1);
            #endregion
            var deliverablecontroller = new DeliverableV2ControllerMock(deliverableManager: mockDeliverableManager.Object);
            var deliverableServiceV2 = new DeliverableServiceV2Mock(deliverableRepository: mockDeliverableRepository.Object);
            //Act
            Crdeliverable = deliverablecontroller.GetLinkCRDeliverable(deliverable1.Name, deliverable1.Id, "25", networkLogin);            
            var result = deliverableServiceV2.GetLinkCRDeliverable(deliverable1.Name, deliverable1.Id, "25", networkLogin);

            //Assert
            Assert.IsTrue(result.Count== 1);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void LinkContractRequestToDeliverableTest()
        {
            #region Data
            int mrmUserId = 999, ContractRequestId= 14030325, DeliverableId= 14032497;
            string networkLogin = "SWNA\\TestLogin";
            #endregion
            #region Mocking
            mockDeliverableManager.Setup(a => a.LinkContractRequestToDeliverable(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
           .Returns(true);
            mockDeliverableServiceV2.Setup(a => a.LinkContractRequestToDeliverable(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
           .Returns(true);
            mockContractrequest_DeliverableRepository.Setup(a => a.LinkContractRequestToDeliverable(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
           .Returns(true);
            #endregion
            var deliverablecontroller = new DeliverableV2ControllerMock(deliverableManager: mockDeliverableManager.Object);
            var deliverableServiceV2 = new DeliverableServiceV2Mock(ContractRequestDeliverableRepository: mockContractrequest_DeliverableRepository.Object);
            //Act
            var result = deliverablecontroller.LinkContractRequestToDeliverable(ContractRequestId, DeliverableId, mrmUserId, networkLogin);
            var contractrequestlink= deliverableServiceV2.LinkContractRequestToDeliverable(ContractRequestId, DeliverableId, mrmUserId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(contractrequestlink == true);
        }

        [TestMethod]
        public void DeleteLinkCRDeliverableTest()
        {
            #region Data
            int mrmUserId = 999, ContractRequestId = 14030325, DeliverableId = 14032498;
            string networkLogin = "SWNA\\TestLogin";
            #endregion
            #region Mocking
            mockDeliverableManager.Setup(a => a.DeleteLinkCRDeliverable(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
           .Returns(true);
            mockDeliverableServiceV2.Setup(a => a.DeleteLinkCRDeliverable(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
           .Returns(true);
            mockContractrequest_DeliverableRepository.Setup(a => a.DeleteLinkCRDeliverable(It.IsAny<int>(), It.IsAny<int>()))
           .Returns(true);
            #endregion
            var deliverablecontroller = new DeliverableV2ControllerMock(deliverableManager: mockDeliverableManager.Object);
            var deliverableServiceV2 = new DeliverableServiceV2Mock(ContractRequestDeliverableRepository: mockContractrequest_DeliverableRepository.Object);
            //Act
            var result = deliverablecontroller.DeleteLinkCRDeliverable(ContractRequestId, DeliverableId, mrmUserId, networkLogin);
            var deletecrresult = deliverableServiceV2.DeleteLinkCRDeliverable(ContractRequestId, DeliverableId, mrmUserId);
            //Assert            
            Assert.IsTrue(result == true);
            Assert.IsTrue(deletecrresult == true);
        }

        [TestMethod]
        public void GetContractRequestDeliverablesTest()
        {
            #region Data
            int mrmUserId = 999,DeliverableId = 14030325;
            string networkLogin = "SWNA\\TestLogin";
            ContractRequestDeliverableVM crdelvm = new ContractRequestDeliverableVM()
            {
                ContractRequestId= 14030325,
                ContractType="CR",
                FinalDate=DateTime.UtcNow,
                 Name="TestCR"
            };
            List<ContractRequestDeliverableVM> crdelvmlist = new List<ContractRequestDeliverableVM>();
            crdelvmlist.Add(crdelvm);
            DeliverableDateType deldatetype = new DeliverableDateType()
            {
                CreatedBy=556,
                Code="DEL"
            };
            DeliverableDate deldate = new DeliverableDate()
            {
                DeliverableId= 14030325,
                DeliverableDateTypeId=9,
                DeliverableDateType=deldatetype
            };
            List<DeliverableDate> deldatelist = new List<DeliverableDate>();
            Deliverable del = new Deliverable()
            {
                Id= 14030325,
                CreatedBy=556,
                DeliverableDate=deldatelist
            };

            ContractRequest cr = new ContractRequest()
            {
                CreatedBy = 556,
                Id = 14030325,
                ContractRequestProject = "Test Contract Request Project",
                ContractRequestPaymentTermId = 1,
                Deliverable = del
            };

            List<ContractRequest> crlist = new List<ContractRequest>();
            crlist.Add(cr);
            #endregion
            #region Mocking
            mockDeliverableManager.Setup(a => a.GetContractRequestDeliverables(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
           .Returns(crdelvmlist);
            mockDeliverableServiceV2.Setup(a => a.GetContractRequestDeliverables(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
           .Returns(crdelvmlist);
            mockContractrequest_DeliverableRepository.Setup(a => a.GetContractRequestDeliverables(It.IsAny<int>(), It.IsAny<int>()))
           .Returns(crlist);
            #endregion
            var deliverablecontroller = new DeliverableV2ControllerMock(deliverableManager: mockDeliverableManager.Object);
            var deliverableServiceV2 = new DeliverableServiceV2Mock(ContractRequestDeliverableRepository: mockContractrequest_DeliverableRepository.Object);
            //Act
            var result = deliverablecontroller.GetContractRequestDeliverables(DeliverableId, mrmUserId, networkLogin);
            var getcrresult = deliverableServiceV2.GetContractRequestDeliverables(DeliverableId, mrmUserId, networkLogin);
            //Assert            
            Assert.IsTrue(result.Count==1);
            Assert.IsTrue(result.ElementAt(0).ContractRequestId == 14030325);
            Assert.IsFalse(result.ElementAt(0).Name == "Test");
            Assert.IsTrue(getcrresult.Count > 0);
        }
        #endregion

        #region MRM-501
        [TestMethod]
        public void SaveCRVendors_Test()
        {
            #region Variables
            var mrmUserId = 100;
            var networkLogin = "swna\testLogin";
            var deliverableId = 101;
            #endregion

            #region Data SetUp
            #region ContractRequestVendorViewModel
            var lstContractRequestVendorViewModel = new List<ContractRequestVendorViewModel>()
            {
                new ContractRequestVendorViewModel
                {
                    Id = 1,
                    Address = "Pasadena",
                    DeliverableId = deliverableId,
                    FederalId = "Fed 1",
                    MasterVendorId = 1,
                    Name = "Vendor 1",
                    PhoneNumber = "1-800-1234",
                    SapVendorNumber = "123.456.7890",
                    ContactsCount = 0,
                    AwardedContract = false,
                    DidNotRespond = true,
                    VendorContacts = new List<CRVendorContactModel>() {
                        new CRVendorContactModel
                        {
                            ContactId = 1,
                            Notify = true,
                            Primary = true
                        },
                        new CRVendorContactModel
                        {
                            ContactId = 2,
                            Notify = true,
                            Primary = false
                        },
                        new CRVendorContactModel
                        {
                            ContactId = 3,
                            Notify = false,
                            Primary = false
                        }
                    }
                },
                new ContractRequestVendorViewModel
                {
                    Id = 2,
                    Address = "San Jose",
                    DeliverableId = deliverableId,
                    FederalId = "Fed 2",
                    MasterVendorId = 2,
                    Name = "Vendor 2",
                    PhoneNumber = "1-800-5678",
                    SapVendorNumber = "123.456.0987",
                    ContactsCount = 0,
                    AwardedContract = false,
                    DidNotRespond = false,
                    VendorContacts = new List<CRVendorContactModel>() {
                        new CRVendorContactModel
                        {
                            ContactId = 4,
                            Notify = true,
                            Primary = true
                        },
                        new CRVendorContactModel
                        {
                            ContactId = 5,
                            Notify = true,
                            Primary = false
                        },
                        new CRVendorContactModel
                        {
                            ContactId = 6,
                            Notify = false,
                            Primary = false
                        }
                    }
                },
                new ContractRequestVendorViewModel
                {
                    Id = 3,
                    Address = "SFO",
                    DeliverableId = deliverableId,
                    FederalId = "Fed 3",
                    MasterVendorId = 4,
                    Name = "Vendor 4",
                    PhoneNumber = "1-800-6543",
                    SapVendorNumber = "123.456.8790",
                    ContactsCount = 0,
                    AwardedContract = true,
                    DidNotRespond = false,
                    VendorContacts = new List<CRVendorContactModel>() {
                        new CRVendorContactModel
                        {
                            ContactId = 7,
                            Notify = true,
                            Primary = true
                        },
                        new CRVendorContactModel
                        {
                            ContactId = 8,
                            Notify = true,
                            Primary = false
                        },
                        new CRVendorContactModel
                        {
                            ContactId = 9,
                            Notify = false,
                            Primary = false
                        }
                    }
                }
            };
            #endregion

            #region Deliverable_MasterVendor          

            var lstDeliverableMasterVendor = new List<Deliverable_MasterVendor>()
            {
                new Deliverable_MasterVendor
                {
                    Id = 1,
                    DeliverableId = deliverableId,
                    MasterVendorId = 1,
                    AwardedContractFlag = false,
                    RespondedFlag = true
                },
                new Deliverable_MasterVendor
                {
                    Id = 2,
                    DeliverableId = deliverableId,
                    MasterVendorId = 2,
                    AwardedContractFlag = false,
                    RespondedFlag = false
                },
                new Deliverable_MasterVendor
                {
                    Id = 3,
                    DeliverableId = deliverableId,
                    MasterVendorId = 3,
                    AwardedContractFlag = true,
                    RespondedFlag = false
                },
            };
            #endregion


            UserTitle testUserTitle = new UserTitle { Id = 1, Code = "test'", Name = "test" };

            #endregion

            #region Mock
            mockDeliverableVendorRepository
                .Setup(x => x.GetMany(It.IsAny<Expression<Func<Deliverable_MasterVendor, bool>>>()))
                .Returns(lstDeliverableMasterVendor);
            mockDeliverableVendorRepository
                .Setup(x => x.Delete(It.IsAny<Expression<Func<Deliverable_MasterVendor, bool>>>()));

            mockDeliverableVendorRepository
                .Setup(x => x.GetSingle(It.IsAny<Expression<Func<Deliverable_MasterVendor, bool>>>()))
                .Returns(new Deliverable_MasterVendor
                {
                    Id = 3,
                    DeliverableId = deliverableId,
                    MasterVendorId = 3,
                    AwardedContractFlag = true,
                    RespondedFlag = false
                });

            mockDeliverableVendorRepository.Setup(x => x.Update(It.IsAny<Deliverable_MasterVendor>()));
            
            mockDeliverableVendorRepository
                .Setup(x => x.Add(It.IsAny<Deliverable_MasterVendor>())).Returns(It.IsAny<Deliverable_MasterVendor>());

            mockUserTitleRepository.Setup(del => del.GetSingle(It.IsAny<Expression<Func<UserTitle, bool>>>())).Returns(
           (Expression<Func<UserTitle, bool>> expr) => testUserTitle);

            mockDeliverableUserTitleMrmUserRepository.Setup(x => x.Delete(It.IsAny<Expression<Func<Deliverable_UserTitle_MRMUser, bool>>>()));
            mockDeliverableUserTitleMrmUserRepository.Setup(x => x.Add(It.IsAny<Deliverable_UserTitle_MRMUser>())).Returns(It.IsAny<Deliverable_UserTitle_MRMUser>()); 
            mockUnitOfWork.Setup(uow => uow.Commit());
            #endregion

            #region Services
            var deliverableServiceV2 = new DeliverableServiceV2Mock(
                deliverable_VendorRepository: mockDeliverableVendorRepository.Object,
                userTitleRepository: mockUserTitleRepository.Object,
                deliverableUserTitleMrmUserRepository: mockDeliverableUserTitleMrmUserRepository.Object,
                iunitOfWork: mockUnitOfWork.Object
                );
            #endregion

            #region Asserts
            var result = deliverableServiceV2.SaveCRVendor(lstContractRequestVendorViewModel, deliverableId, mrmUserId, networkLogin);

            Assert.IsTrue(result == true);

            #endregion

        }
        #endregion

        #region MRM-527
        [TestMethod]
        public void LinkContractRequestToNonCRDeliverable_Test()
        {
            #region DataSetup
            int ContractRequestId = 14030325, mrmUserId = 999, DeliverableId = 14032548;            
            string networkLogin = "SWNA\\TestLogin";
            DeliverableGroup delgroup = new DeliverableGroup()
            {
                Id=2,
                Code="VID",
                Name="Video"

            };

            DeliverableDateType deldatetype = new DeliverableDateType()
            {
                Id=9,
                Code="DEL",
                Name="Final Due"
            };

            DeliverableDate deldate = new DeliverableDate()
            {
                DeliverableDateType=deldatetype,
                DeliverableDateTypeId=9,
                DeliverableId= 14032548
            };
            List<DeliverableDate> deldatelist = new List<DeliverableDate>();
            deldatelist.Add(deldate);
            DeliverableStatus delstatus = new DeliverableStatus()
            {
                Id=1,
                Code="DRFT",
                Name="Draft"
            };

            Deliverable deliverable1 = new Deliverable()
            {
                Id= 14032548,
                Name= "teat cafd",
                DeliverableGroupId=2,
                DeliverableGroup=delgroup,
                DeliverableStatusId=1,
                DeliverableStatus=delstatus,
                DeliverableDate= deldatelist
            };            
            #endregion

            #region Mocking
            mockDeliverableManager.Setup(a => a.LinkContractRequestToNonCRDeliverable(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
           .Returns(deliverable1);
            mockDeliverableServiceV2.Setup(a => a.LinkContractRequestToNonCRDeliverable(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
           .Returns(deliverable1);
            mockContractrequest_DeliverableRepository.Setup(a => a.LinkContractRequestToNonCRDeliverable(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
           .Returns(deliverable1);
            #endregion

            var deliverablecontroller = new DeliverableV2ControllerMock(deliverableManager: mockDeliverableManager.Object);
            var deliverableServiceV2 = new DeliverableServiceV2Mock(ContractRequestDeliverableRepository: mockContractrequest_DeliverableRepository.Object);

            //Act
            var result = deliverablecontroller.LinkContractRequestToNonCRDeliverable(ContractRequestId, DeliverableId, mrmUserId, networkLogin);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ContractRequestId == 14032548);
        }

        [TestMethod]
        public void SaveLinkedDeliverables_Test()
        {
            #region DataSetup
            DeliverableSaveModel result = new DeliverableSaveModel();
            result.IntegrationOutBound = new IntergrationOutBound();
            int  mrmUserId = 999, DeliverableId = 14032548;
            string networkLogin = "SWNA\\TestLogin";
            ContractRequestDeliverableVM contractreqdelvm = new ContractRequestDeliverableVM()
            {
                ContractRequestId= 14030325,
                Name="Test"
            };
            List<ContractRequestDeliverableVM> contractreqdelvmlist = new List<ContractRequestDeliverableVM>();
            contractreqdelvmlist.Add(contractreqdelvm);
            DeliverableMultiWBSModel deliverableMultiWBSModel = new DeliverableMultiWBSModel()
            {
                InitialBudgetAmount = 123,
                DeliverableId = 140000,
                PrimaryWBSElementId = 2,
                IsLaunchFlag = false
            };
            DeliverableMultiWBSViewModel deliverableMultiWBSViewModel = new DeliverableMultiWBSViewModel()
            {
                InitialBudgetAmount = 123,
                DeliverableId = 140000,
                IsLaunchFlag = true,
                PrimaryWBSElementId = 2, //wbsElement2.Id
            };


            DeliverableGeneralInfoViewModel deliverableGeneralInfoViewModel = new DeliverableGeneralInfoViewModel()
            {
                AdvertiserId=22,
                AssigneeId=5,
                ChildDeliverableId=145236,
                CreatedBy="556",
                PrintAssigneeId=11,
                DeliverableId = 14523645,

            };
            OffAirDesignActivityViewModel offairdesact = new OffAirDesignActivityViewModel()
            {
                AssigneeId=6,
                DeliverableId=14523645,
                PrintAssigneeId = 8

            };
            Deliverable testDeliverable = new Deliverable()
            {
                Id = 14032548,
                Name = "teat cafd",
                DeliverableGroupId = 2,               
                DeliverableStatusId = 1
                
            };
            OffAirDesign offAirDesign = new OffAirDesign()
            {
                DeliverableId = testDeliverable.Id,
                AssigneeMRMUserId = 1
            };
            testDeliverable.OffAirDesign = offAirDesign;
            ViewModel.Deliverable.DeliverableViewModel delvm = new ViewModel.Deliverable.DeliverableViewModel()
            {
               GeneralInfo= deliverableGeneralInfoViewModel,
               OffAir= offairdesact
            };
            List<int> clientLobList = new List<int>(new int[] { 666 });
            deliverableGeneralInfoViewModel.ClientLOBList = clientLobList;

            DeliverableSaveModel deliverableSaveModel = new DeliverableSaveModel()
            {
                id = 1,
                IntegrationOutBound = new Core.Models.Intergration.Common.IntergrationOutBound(),
                timestamp = DateTime.Now.ToShortTimeString()
            };
            IntergrationOutBound intoutbound = new IntergrationOutBound()
            {
                DefaultWorkOrders = new Core.Models.Intergration.MP.WorkOrderRequest()
            };
            #endregion
            #region Mocking
            mockDeliverableManager.Setup(a => a.SaveGeneralInfoViewModel(It.IsAny<DeliverableGeneralInfoViewModel>(), It.IsAny<int>(), It.IsAny<string>(), true))
            .Returns(deliverableSaveModel);
            mockDeliverableServiceV2.Setup(x => x.GetDeliverableById(It.IsAny<int>(), It.IsAny<string>())).Returns(testDeliverable);
            mockDeliverableRepository.Setup(x => x.GetDeliverableById(It.IsAny<int>())).Returns(testDeliverable);
            mockDeliverableManager.Setup(a => a.SaveDeliverableDates(It.IsAny<List<DeliverableDateTypeFieldViewModel>>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(intoutbound);
            mockDeliverableManager.Setup(a => a.SaveLinkedDeliverables(It.IsAny<List<ContractRequestDeliverableVM>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()));
            mockDeliverableServiceV2.Setup(a => a.SaveLinkedDeliverables(It.IsAny<List<ContractRequestDeliverableVM>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()));
            mockDeliverableRepository.Setup(x => x.DeleteLinkedDeliverables(It.IsAny<int>()));
            mockDeliverableRepository.Setup(x => x.SaveLinkedDeliverables(It.IsAny<ContractRequest_Deliverable>()));
            mockDeliverableBudgetManager.SetupSequence(a => a.SaveDeliverableBudget(It.IsAny<DeliverableBudgetViewModel>(), It.IsAny<int>(), It.IsAny<string>()))
          .Returns(new Core.Models.Intergration.Common.IntergrationOutBound())
          .Returns(null);

            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>())).Returns(1);

            mockDeliverableBudgetService.Setup(x => x.SaveDeliverableBudget(It.IsAny<DeliverableMultiWBSModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(deliverableMultiWBSModel);
            mockDeliverableBudgetService.Setup(x => x.GetWBSElementId(It.IsAny<string>())).Returns(1);
            mockDeliverableManager.Setup(a => a.SaveDeliverablePrintActivity(It.IsAny<OffAirDesignActivityViewModel>(), It.IsAny<int>(), It.IsAny<string>()))
           .Returns(true);
            mockDeliverableServiceV2.Setup(a => a.SaveDeliverablePrintActivity(It.IsAny<OffAirDesign>(),It.IsAny<string>()))
           .Returns(true);
            mockOffAirDesignRepository.Setup(x => x.GetSingle(It.IsAny<Expression<Func<OffAirDesign, bool>>>()))
               .Returns(offAirDesign);
            mockOffAirDesignRepository.Setup(x => x.Add(It.IsAny<OffAirDesign>())).Returns(offAirDesign);
            mockUnitOfWork.Setup(uow => uow.Commit());
            #endregion

            
            var deliverableServiceV2 = new DeliverableServiceV2Mock(deliverableRepository: mockDeliverableRepository.Object, offAirDesignRepository: mockOffAirDesignRepository.Object, deliverableGroupRepository: mockDeliverableGroupRepository.Object,
                     iunitOfWork: mockUnitOfWork.Object);
            var controller = new DeliverableDirectorMock(deliverableService: mockDeliverableServiceV2.Object,
                deliverableGeneralInfoManager: mockDeliverableGeneralInfoManager.Object,
                deliverableManager: mockDeliverableManager.Object,
                budgetManager: mockDeliverableBudgetManager.Object,
                intergrationManager: mockIIntergrationManager.Object);

            //Assert

                mockDeliverableManager.Verify();
                mockDeliverableServiceV2.Verify();
                deliverableServiceV2.SaveLinkedDeliverables(contractreqdelvmlist, DeliverableId, mrmUserId, networkLogin);                               
                Assert.IsTrue(true);
                Assert.IsNotNull(delvm.OffAir.DeliverableId);
                Assert.IsTrue(contractreqdelvmlist.Count == 1);
                Assert.IsFalse(contractreqdelvmlist.ElementAt(0).Name == "TestCR");

        }

        #endregion
    }
}
