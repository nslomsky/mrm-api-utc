using Disney.MRM.DANG.Core.Contracts;
using Disney.MRM.DANG.Service.Contracts;
using Disney.MRM.DANG.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Disney.MRM.DANG.Model;
using Disney.MRM.DANG.Service.Implementations;
using Disney.MRM.DANG.API.Controllers;
using Disney.MRM.DANG.API.Test.MockObject.Service;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Linq.Expressions;
using System.Linq;
using Disney.MRM.DANG.ViewModel.Deliverable;
using Disney.MRM.DANG.Core.Models.Intergration.ESB;
using Disney.MRM.DANG.API.Test.MockObject.Manager;
using Disney.MRM.DANG.API.Test.MockObject.Managers;
using AutoMapper;

namespace Disney.MRM.DANG.API.Test.Manager
{
    [TestClass]
    public class IntergrationManagerTests
    {
        private Moq.MockRepository mockRepository;
        private Moq.Mock<IDeliverableServiceV2> mockDeliverableServiceV2;
        private Moq.Mock<IIntergrationService> mockIntegrationService;

        [TestInitialize]
        public void ClassInit()
        {
            mockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
            mockDeliverableServiceV2 = mockRepository.Create<IDeliverableServiceV2>();
            mockIntegrationService = mockRepository.Create<IIntergrationService>();
        }

        //MRM-213 - Get Mediator
        #region MRM-213
        [TestMethod]
        public void SentPrimaryTOWInfoToMediatorTest()
        {
            #region Data Setup
            DeliverableGroup testDeliverableGroup = new DeliverableGroup() { Code = "VID" };

            Model.TypeOfWork typeOfWork1 = new Model.TypeOfWork() { Name = "Type Of Work A", Id = 1100 };
            Model.TypeOfWork typeOfWork2 = new Model.TypeOfWork() { Name = "Type Of Work B", Id = 1101 };

            Channel testChannel = new Channel() { Code = "DC" };//Radio Disney

            Guid DeliverableBudgetUniqueID1 = Guid.NewGuid();
            Guid DeliverableBudgetUniqueID2 = Guid.NewGuid();

            WBSElement wbsElement1 = new WBSElement()
            {
                Id = 1,
                FullWBSNumber = "123456.011.001",
                TypeOfWorkId = 1100
            };
            WBSElement wbsElement2 = new WBSElement()
            {
                Id = 2,
                FullWBSNumber = "123456.011.002",
                TypeOfWorkId = 1101
            };

            wbsElement1.TypeOfWork = typeOfWork1;
            wbsElement2.TypeOfWork = typeOfWork2;

            DeliverableDate FPADate = new DeliverableDate()
            {
                Id = 1,
                DeliverableDateTypeId = 1,
                DateValue = new DateTime()
            };

            DeliverableDate DueDate = new DeliverableDate()
            {
                Id = 2,
                DeliverableDateTypeId = 9,
                DateValue = new DateTime()
            };

            List<DeliverableDate> deliverableDateList = new List<DeliverableDate>();
            deliverableDateList.Add(FPADate);
            deliverableDateList.Add(DueDate);
            ActivityStatus activityStatus = new ActivityStatus()
            {
                Id = 39,
                IsActiveFlag=true,
                Code = "INPCC",
            };
            TrackElement trk1 = new TrackElement()
            {
                Id = 1,
                ActivityStatusId = 39,
                HouseNumber = "MRM1403030766624",
                Name = "Textless Generic",
                ActualLength = 0.10m,
                IsActiveFlag = true
            };
            TrackElement trk2 = new TrackElement()
            {
                Id = 2,
                ActivityStatusId = 39,
                HouseNumber = "MRM1403030766625",
                Name = "International Textless",
                ActualLength = 0.10m,
                IsActiveFlag = true
            };
            DeliverableDate deliverableDate1 = new DeliverableDate()
            {
                Id = 1,
                DeliverableId = 140000
            };
            DeliverableDate deliverableDate2 = new DeliverableDate()
            {
                Id = 2,
                DeliverableId = 140000
            };

            DeliverableDateType deliverableDateType1 = new DeliverableDateType()
            {
                Id = 1,
                Code = "FPA"
            };

            DeliverableDateType deliverableDateType2 = new DeliverableDateType()
            {
                Id = 2,
                Code = "DEL"
            };

            FPADate.DeliverableDateType = deliverableDateType1;
            DueDate.DeliverableDateType = deliverableDateType2;
            Model.Deliverable testDeliverable = new Model.Deliverable()
            {
                Id = 140000,
                DeliverableTypeId = 94,//Affiliate Marketing
                ProducingDepartmentId = 84,//MRM Team
                Name = "Disney Channel Video Deliverable",
                DeliverableGroupId = 2,//Video
                DeliverableGroup = testDeliverableGroup,
                Channel = testChannel,
                DeliverableStatusId = 3,
                PlannedLengthId = 10,
                ClipDeliverMasterVendorId = 418
            };
            testDeliverable.PrimaryDeliverableBudgetUniqueID = DeliverableBudgetUniqueID1;
            testDeliverable.DeliverableDate = deliverableDateList;
            DeliverableStatus deliverableStatus = new DeliverableStatus()
            {
                Id = 2,
                Code = "INPCC",
                Name = "In Process"
            };
            testDeliverable.DeliverableStatus = deliverableStatus;

            DeliverableBudget deliverableBud1 = new DeliverableBudget()
            {
                Id = 1,
                DeliverableId = 140000,
                MasterVendorId = 12
            };
            deliverableBud1.DeliverableBudgetUniqueID = DeliverableBudgetUniqueID1;
            deliverableBud1.WBSElement = wbsElement1;

            DeliverableBudget deliverableBud2 = new DeliverableBudget()
            {
                Id = 2,
                DeliverableId = 140000,
                MasterVendorId = 25
            };
            deliverableBud2.DeliverableBudgetUniqueID = DeliverableBudgetUniqueID2;
            deliverableBud2.WBSElement = wbsElement2;

            List<DeliverableBudget> deliverableBudgetList = new List<DeliverableBudget>();
            deliverableBudgetList.Add(deliverableBud1);
            deliverableBudgetList.Add(deliverableBud2);

            testDeliverable.DeliverableBudget = deliverableBudgetList;

            ProductionMethodType productionMethodType1 = new ProductionMethodType() { Id = 1, Code = "PH", Name = "Post House" };
            ProductionMethodType productionMethodType2 = new ProductionMethodType() { Id = 2, Code = "CS", Name = "Creative Services" };

            List<ProductionMethodType> productionMethodTypeList = new List<ProductionMethodType>();
            productionMethodTypeList.Add(productionMethodType1);
            productionMethodTypeList.Add(productionMethodType2);

            List<int> pmoList = new List<int>();
            pmoList.Add(productionMethodTypeList.ElementAt(0).Id);
            pmoList.Add(productionMethodTypeList.ElementAt(1).Id);

            List<DeliverableDate> deldates = new List<DeliverableDate>();
            deldates.Add(FPADate);
            deldates.Add(DueDate);
            testDeliverable.DeliverableDate = deldates;

            List<TrackElement> lstTrk = new List<TrackElement>();
            lstTrk.Add(trk1);
            lstTrk.Add(trk2);
            testDeliverable.TrackElement = lstTrk;

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
                Budgets = deliverableBudgetViewModelList,
            };
            deliverableMultiWBSViewModel.Budgets = deliverableBudgetViewModelList;

            Deliverable_WBSElement deliverable_WBSElement = new Deliverable_WBSElement()
            {
                DeliverableId = 14000,
                Id = 1,
            };

            deliverable_WBSElement.Deliverable = testDeliverable;
            MasterVendor masterVendor = new MasterVendor()
            {
                Id = 1,
                SAPVendorId = 25,
                MediatorVendorId = "795"
            };
            PromoPlacementBody promoPlacementBody = new PromoPlacementBody();
            PromoPlacementBody promoPlacementBodyResult = new PromoPlacementBody();
            #endregion

            #region Mapper
            Mapper.CreateMap<Model.Deliverable, PromoPlacementBody>()
                .ForMember(i => i.ChannelCode, m => m.MapFrom(d => d.Channel.Code))
                .ForMember(i => i.DeliverableId, m => m.MapFrom(d => d.Id))
               .ForMember(i => i.DeliverableName, m => m.MapFrom(d => d.Name))
                .ForMember(i => i.DeliverableStatusCode, m => m.MapFrom(d => d.DeliverableStatus.Code))
                .ForMember(i => i.DeliverableTargetCode, m => m.MapFrom(d => d.Channel1.Code))
                .ForMember(i => i.DeliverableTypeCode, m => m.MapFrom(d => d.DeliverableType.Code))
                .ForMember(i => i.IsOffAir, m => m.MapFrom(d => d.IsOffChannelFlag))
                .ForMember(i => i.FinalDueDate,
                    m =>
                        m.MapFrom(
                            d =>
                                d.DeliverableDate.FirstOrDefault(
                                    i => i.DeliverableDateType.Code == Core.Constants.DeliverableDateType.Final_Due)
                                    .DateValue))
                .ForMember(i => i.FirstPromoAirDate,
                    m =>
                        m.MapFrom(
                            d =>
                                d.DeliverableDate.FirstOrDefault(
                                    i =>
                                        i.DeliverableDateType.Code ==
                                        Core.Constants.DeliverableDateType.FirstPromotionalAirDate).DateValue))
                .ForMember(i => i.IsReissue, m => m.MapFrom(d => d.IsReIssueFlag))
                //Updated for MRM-213
                .ForMember(i => i.TOWId, m => m.MapFrom(d => d.DeliverableBudget.Where(x => x.DeliverableBudgetUniqueID == d.PrimaryDeliverableBudgetUniqueID).FirstOrDefault().WBSElement.TypeOfWorkId.Value))
                .ForMember(i => i.TOWName, m => m.MapFrom(d => d.DeliverableBudget.Where(x => x.DeliverableBudgetUniqueID == d.PrimaryDeliverableBudgetUniqueID).FirstOrDefault().WBSElement.TypeOfWork.Name))
                .ForMember(i => i.WBSNumber, m => m.MapFrom(d => FormatWBS(d.DeliverableBudget.Where(x => x.DeliverableBudgetUniqueID == d.PrimaryDeliverableBudgetUniqueID).FirstOrDefault())))
                .ForMember(i => i.WriterProducer,
                    m => m.MapFrom(d => FormatOwnerName(Core.Constants.UserTitle.WriterProducer, d) == string.Empty
                        ? FormatOwnerName(Core.Constants.UserTitle.ProductionManager, d)
                        : FormatOwnerName(Core.Constants.UserTitle.WriterProducer, d)))

                .ForMember(i => i.WriterProducerId,
                    m => m.MapFrom(d => FormatNetworkLogin(Core.Constants.UserTitle.WriterProducer, d) == string.Empty
                        ? FormatOwnerName(Core.Constants.UserTitle.ProductionManager, d)
                        : FormatNetworkLogin(Core.Constants.UserTitle.WriterProducer, d)));

            #endregion

            #region Mocking
            mockDeliverableServiceV2.Setup(x => x.GetMasterVendorById(It.IsAny<int>()))
                    .Returns(masterVendor); // Check for MediatorVendorId for SAP Vendor ID -- logic in GetVendorSAP() in IntergrationManager.cs
            mockIntegrationService.Setup(x => x.GetCommentsForDeliverable(It.IsAny<int>()))
                .Returns(new List<Comment>());
            mockIntegrationService.Setup(x => x.GetTargetPlatforms(It.IsAny<List<int>>()))
                .Returns(new List<TargetPlatform>());
            #endregion

            #region Service
            promoPlacementBody = Mapper.Map<Model.Deliverable, PromoPlacementBody>(testDeliverable);
            var integrationMock = new IntergrationManagerMock(deliverableV2Service: mockDeliverableServiceV2.Object,intergrationService: mockIntegrationService.Object);
            promoPlacementBodyResult = integrationMock.GetPromoPlacementForAssetManagement(testDeliverable, "SWNA\\TestLogin");
            #endregion

            #region Assert
            Assert.IsFalse(promoPlacementBodyResult == null);
            Assert.IsTrue(promoPlacementBodyResult.DeliverableStatusCode == "INPCC");
            Assert.IsTrue(promoPlacementBodyResult.VendorNumber == masterVendor.MediatorVendorId);
            Assert.IsTrue(promoPlacementBodyResult.TOWName == typeOfWork1.Name);
            Assert.IsTrue(promoPlacementBodyResult.WBSNumber == wbsElement1.FullWBSNumber);
            #endregion
        }

        public string FormatOwnerName(string userTitleCode, Model.Deliverable deliverable)
        {
            string result = string.Empty;
            Deliverable_UserTitle_MRMUser user =
                deliverable.Deliverable_UserTitle_MRMUser.FirstOrDefault(
                    i => i.UserTitle != null && i.UserTitle.Code == userTitleCode);
            if (user == null)
            {
                return result;
            }
            if (user.MRMUser == null)
            {
                return result;
            }
            return user.MRMUser.FirstName + " " + user.MRMUser.LastName;
        }

        public string FormatWBS(DeliverableBudget budget)
        {
            string result = string.Empty;

            //MRM-213
            if (budget != null && budget.WBSElement != null)
            {
                return budget.WBSElement.FullWBSNumber;
            }

            return result;
        }

        public string FormatNetworkLogin(string userTitleCode, Model.Deliverable deliverable)
        {
            string result = string.Empty;
            Deliverable_UserTitle_MRMUser user =
                deliverable.Deliverable_UserTitle_MRMUser.FirstOrDefault(
                    i => i.UserTitle != null && i.UserTitle.Code == userTitleCode);
            if (user == null)
            {
                return result;
            }
            if (user.MRMUser == null)
            {
                return result;
            }
            return user.MRMUser.UserName;
        }
        #endregion
    }
}


