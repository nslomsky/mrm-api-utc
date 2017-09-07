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

namespace Disney.MRM.DANG.API.Test.Service
{
    [TestClass]
    public class IntegrationServiceTest
    {
        private Moq.MockRepository mockRepository;
        private Moq.Mock<IIntergrationService> mockIntergrationService;
        private Moq.Mock<IWorkOrderRepository> mockWorkOrderRepository;
        private Moq.Mock<IDeliverableRepository> mockDeliverableRepository;
        private Moq.Mock<ICalendarRepository> mockCalendarRepository;
        private Moq.Mock<IDeliverableBudgetRepository> mockDeliverableBudgetRepository;
        private Moq.Mock<IWorkOrderTransactionRepositry> mockWorkOrderTransactionRepositry;
        private Moq.Mock<IInvoiceLineRepository> mockInvoiceLineRepository;
        private Moq.Mock<IUnitOfWork> mockUnitOfWork;
        private Moq.Mock<ITypeOfWorkRepository> mockTypeOfWorkRepository;
        private Moq.Mock<IWBSFiscalYear_ChannelRepository> mockWBSFiscalYear_ChannelRepository;
        private Moq.Mock<IUserRepository> mockUserRepository;


        [TestInitialize]
        public void ClassInit()
        {
            mockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
            mockIntergrationService = mockRepository.Create<IIntergrationService>();
            mockWorkOrderRepository = mockRepository.Create<IWorkOrderRepository>();
            mockDeliverableRepository = mockRepository.Create<IDeliverableRepository>();
            mockCalendarRepository = mockRepository.Create<ICalendarRepository>();
            mockDeliverableBudgetRepository = mockRepository.Create<IDeliverableBudgetRepository>();
            mockWorkOrderTransactionRepositry = mockRepository.Create<IWorkOrderTransactionRepositry>();
            mockInvoiceLineRepository = mockRepository.Create<IInvoiceLineRepository>();
            mockUnitOfWork = mockRepository.Create<IUnitOfWork>();
            mockTypeOfWorkRepository = mockRepository.Create<ITypeOfWorkRepository>();
            mockWBSFiscalYear_ChannelRepository = mockRepository.Create<IWBSFiscalYear_ChannelRepository>();
            mockUserRepository = mockRepository.Create<IUserRepository>();
        }
       // Contract Request - EDMX fix
       // MRM-212 : FY Cross Over
        [TestMethod]
        public void UpsertBillingWorkOrderTransactions_ForFY2017()
        {
            #region Data Setup
            WorkOrderTransaction workOrderTransaction1 = new WorkOrderTransaction()
            {
                TransactionNumber = "11111111",
                BillingAmount = 5000,
                CreatedBy = 496,
                Id = 1,
                WorkOrderId = 1,
                BeginDate = new DateTime(2017, 5, 19)
            };
            WorkOrderTransaction workOrderTransactionExist = new WorkOrderTransaction()
            {
                TransactionNumber = "22222222",
                BillingAmount = 3000,
                CreatedBy = 496,
                Id = 2,
                BeginDate = new DateTime(2016, 5, 19)
            };
            List<WorkOrderTransaction> workOrderTransactionList = new List<WorkOrderTransaction>();
            workOrderTransactionList.Add(workOrderTransaction1);
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
                MasterVendorId = 418,
                Deliverable = testDeliverable,
                PHApplyPercent = 40
            };
            DeliverableBudget deliverableBudget2 = new DeliverableBudget()
            {
                DeliverableId = 1,
                Id = 101,
                WBSElement = wbsElement2,
                MasterVendorId = 418,
                Deliverable = testDeliverable,
            };
            WorkOrder workOrder = new WorkOrder()
            {
                WorkOrderNumber = 1122,
                Id = 1,
                VendorId = 418,
                DeliverableId = 1,
                Deliverable = testDeliverable
            };
            TypeOfWork tow = new TypeOfWork()
            {
                Id = 1,
                ChannelId = 1,
                FiscalYear = 2016
            };
            WBSFiscalYear_Channel wBSFiscalYear_Channel = new WBSFiscalYear_Channel()
            {
                FiscalYear = "2016",
                Id = 52,
                WBSNumber = "12345",
                CreatedBy = 496
            };
            List<WorkOrder> workOrderList = new List<WorkOrder>();
            workOrderList.Add(workOrder);
            List<Deliverable> deliverableList = new List<Deliverable>();
            deliverableList.Add(testDeliverable);
            List<DeliverableBudget> deliverableBudgetList = new List<DeliverableBudget>();
            deliverableBudgetList.Add(deliverableBudget1);
            tow.DeliverableBudget = deliverableBudgetList;
            List<TypeOfWork> towList = new List<TypeOfWork>();
            towList.Add(tow);
            List<WBSFiscalYear_Channel> wbsFiscalYear_ChannelList = new List<WBSFiscalYear_Channel>();
            wbsFiscalYear_ChannelList.Add(wBSFiscalYear_Channel);
            //wBSFiscalYear_Channel.TypeOfWork = towList;
            List<int> deliverableBudgetIdList = new List<int>();
            deliverableBudgetIdList.Add(deliverableBudget1.Id);
            Calendar calendar = new Calendar()
            {
                Id = 1,
                CalendarYear = "2016",
                FiscalYear = "2017"
            };
            MRMUser user = new MRMUser()
            {
                Id = 496,
                UserName = "SWNA\\TestLogin"
            };
            #endregion

            #region Mocking
            mockWorkOrderTransactionRepositry.Setup(i => i.GetSingle(It.IsAny<Expression<Func<WorkOrderTransaction, bool>>>()))
                .Returns(workOrderTransactionExist);
            mockWorkOrderRepository.Setup(i => i.GetAll()).Returns(workOrderList.AsQueryable());
            mockDeliverableRepository.Setup(i => i.GetAll()).Returns(deliverableList.AsQueryable());
            mockDeliverableBudgetRepository.Setup(i => i.GetAll()).Returns(deliverableBudgetList.AsQueryable());
            mockTypeOfWorkRepository.Setup(i => i.GetAll()).Returns(towList.AsQueryable());
            mockDeliverableRepository.Setup(i => i.CreateCrossoverDeliverable(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .Returns(deliverableBudgetIdList.AsQueryable());
            mockDeliverableBudgetRepository.Setup(i => i.GetByDeliverableBudgetId(It.IsAny<int>()))
                .Returns(deliverableBudget1);
            mockDeliverableBudgetRepository.Setup(i => i.Update(It.IsAny<DeliverableBudget>()));
            mockWorkOrderTransactionRepositry.Setup(i => i.Add(It.IsAny<WorkOrderTransaction>()));
            mockWorkOrderTransactionRepositry.Setup(i => i.Update(It.IsAny<WorkOrderTransaction>()));
            mockInvoiceLineRepository.Setup(i => i.GetSingle(It.IsAny<Expression<Func<InvoiceLine, bool>>>()))
                .Returns(() => null);
            mockUnitOfWork.Setup(i => i.Commit());
            mockUserRepository.Setup(i => i.GetSingle(It.IsAny<Expression<Func<MRMUser, bool>>>())).Returns(user);
            mockCalendarRepository.Setup(i => i.GetSingle(It.IsAny<Expression<Func<Calendar, bool>>>())).Returns(calendar);
            mockWBSFiscalYear_ChannelRepository.Setup(i => i.GetAll()).Returns(wbsFiscalYear_ChannelList.AsQueryable());
            #endregion

            #region Service 
            var integrationService = new IntegrationServiceMock(workOrderRepository: mockWorkOrderRepository.Object,
                iBillingWorkOrderTransactionRepositry: mockWorkOrderTransactionRepositry.Object,
                iDeliverableRepository: mockDeliverableRepository.Object, iDeliverableBudgetRepository: mockDeliverableBudgetRepository.Object,
                iTypeOfWorkRepository: mockTypeOfWorkRepository.Object, iInvoiceLineRepository: mockInvoiceLineRepository.Object,
                unitOfWork: mockUnitOfWork.Object, iUserRepository: mockUserRepository.Object, iCalendarRepository: mockCalendarRepository.Object,
                iWBSFiscalYear_ChannelRepository: mockWBSFiscalYear_ChannelRepository.Object);

            bool result = integrationService.UpsertBillingWorkOrderTransactions(workOrderTransactionList);
            #endregion

            #region Asserts
            Assert.IsTrue(result == true);
            #endregion
        }

    }
}

