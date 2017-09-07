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
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System;
using Disney.MRM.DANG.ViewModel;
using Disney.MRM.DANG.API.Test.MockObject.Controller;
using Disney.MRM.DANG.API.ManualMapper;

namespace Disney.MRM.DANG.API.Test.Controllers
{
    [TestClass]
    public class FinanceControllerTest
    {
        private Moq.MockRepository mockRepository;
        private Moq.Mock<IFinanceService> mockfinanceservice;
        private Moq.Mock<IApInvoiceBatchRepository> mockApInvoiceBatchRepository;
        private Moq.Mock<IInvoiceHeaderRepository> mockInvoiceHeaderRepository;
        private Moq.Mock<IUnitOfWork> mockIUnitOfWork;
        private Moq.Mock<IInvoiceLineRepository> mockIInvoiceLineRepository;
        private Moq.Mock<IFinanceService> mockFinanceService;
        private Moq.Mock<IWBS_DeliverablesRepository> mockWBS_DeliverablesRepository;
        [TestInitialize]
        public void ClassInit()
        {
            mockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
            mockfinanceservice = mockRepository.Create<IFinanceService>();
            mockApInvoiceBatchRepository = mockRepository.Create<IApInvoiceBatchRepository>();
            mockInvoiceHeaderRepository = mockRepository.Create<IInvoiceHeaderRepository>();
            mockIUnitOfWork = mockRepository.Create<IUnitOfWork>();
            mockIInvoiceLineRepository = mockRepository.Create<IInvoiceLineRepository>();
            mockFinanceService = mockRepository.Create<IFinanceService>();
            mockWBS_DeliverablesRepository = mockRepository.Create<IWBS_DeliverablesRepository>();
        }

        //MRM-210 :  Batch process created successfully
        [TestMethod]
        public void CreateBatch_ShouldReturnSuccessTest()
        {

            #region Data
            int mrmUserId = 25;
            List<int> channelist = new List<int>() { 1, 2, 3 };
            List<int> budgettypelist = new List<int>() { 1, 2 };

            SqlParameter userChannels = new SqlParameter("@ChannelIds", SqlDbType.NVarChar);
            userChannels.Value = channelist;

            SqlParameter userBudgetTypes = new SqlParameter("@BudgetTypeID", SqlDbType.NVarChar);
            userBudgetTypes.Value = budgettypelist;

            SqlParameter userId = new SqlParameter("@UserID", SqlDbType.Int);
            userId.Value = mrmUserId;

            SqlParameter invoiceHeaderId = new SqlParameter("@InvoiceHeaderId", SqlDbType.Int);
            invoiceHeaderId.Value = 119;

            List<SqlParameter> result = new List<SqlParameter>();
            result.Add(userChannels);
            result.Add(userBudgetTypes);
            result.Add(userId);
            result.Add(invoiceHeaderId);

            ApUploadUserChoiceModel apUploadUserChoiceModel = new ApUploadUserChoiceModel()
            {
                UserId = mrmUserId,
                UserSelectedChannelList = channelist,
                UserSelectedBudgetTypeList = budgettypelist
            };
            UserMessageModel usermodel = new UserMessageModel();
            MRM.DANG.Model.UserMessageModel userMessage_SuccessCase = new Model.UserMessageModel
            {
                StatusNumber = 0,
                IsSuccess = true,
                Message = "Batch Created Succesfuly"
            };
            #endregion

            #region  Mock
            mockApInvoiceBatchRepository.Setup(x => x.CreateBatch(It.IsAny<ApUploadUserChoiceModel>()))
                .Returns(userMessage_SuccessCase);

            #endregion

            #region Service
            var financeService = new FinanceServiceMock(
                apInvoiceBatchRepository: mockApInvoiceBatchRepository.Object);
            UserMessageModel userMessage = financeService.CreateBatch(apUploadUserChoiceModel);
            #endregion

            #region Assertions
            Assert.IsFalse(userMessage.Message == "No approved invoices exist for the for selected channels and budget type.");
            Assert.IsTrue(userMessage.Message == "Batch Created successfully.");
            Assert.IsTrue(userMessage.StatusNumber == 0);
            Assert.IsTrue(userMessage.IsSuccess == true);
            #endregion

        }

        //MRM-210 :  No Approved Invoices
        [TestMethod]
        public void CreateBatch_ShouldReturnNoApprovedTest()
        {

            #region Data
            int mrmUserId = 25;
            List<int> channelist = new List<int>() { 1, 2, 3 };
            List<int> budgettypelist = new List<int>() { 1, 2 };

            SqlParameter userChannels = new SqlParameter("@ChannelIds", SqlDbType.NVarChar);
            userChannels.Value = channelist;

            SqlParameter userBudgetTypes = new SqlParameter("@BudgetTypeID", SqlDbType.NVarChar);
            userBudgetTypes.Value = budgettypelist;

            SqlParameter userId = new SqlParameter("@UserID", SqlDbType.Int);
            userId.Value = mrmUserId;

            SqlParameter invoiceHeaderId = new SqlParameter("@InvoiceHeaderId", SqlDbType.Int);
            invoiceHeaderId.Value = 119;

            List<SqlParameter> result = new List<SqlParameter>();
            result.Add(userChannels);
            result.Add(userBudgetTypes);
            result.Add(userId);
            result.Add(invoiceHeaderId);

            ApUploadUserChoiceModel apUploadUserChoiceModel = new ApUploadUserChoiceModel()
            {
                UserId = mrmUserId,
                UserSelectedChannelList = channelist,
                UserSelectedBudgetTypeList = budgettypelist
            };

            MRM.DANG.Model.UserMessageModel userMessage_NoApproved = new Model.UserMessageModel
            {
                StatusNumber = 51000,
                IsSuccess = false,
                IsWarning = true,
                Message = "Bad Batch - 0 Line Count"
            };


            #endregion

            #region  Mock
            mockApInvoiceBatchRepository.Setup(x => x.CreateBatch(It.IsAny<ApUploadUserChoiceModel>()))
                .Returns(userMessage_NoApproved);
            #endregion

            #region Service
            var financeService = new FinanceServiceMock(
                apInvoiceBatchRepository: mockApInvoiceBatchRepository.Object);
            UserMessageModel userMessage = financeService.CreateBatch(It.IsAny<ApUploadUserChoiceModel>());
            #endregion

            #region Assertions
            Assert.IsFalse(userMessage.IsWarning == false);
            Assert.IsTrue(userMessage.StatusNumber == 51000);
            Assert.IsTrue(userMessage.Message == "No approved invoices exist for the for selected channels and budget type.");
            #endregion

        }

        //MRM-210 :  Error on Batch Creation
        [TestMethod]
        public void CreateBatch_ShouldReturnErrorTest()
        {

            #region Data
            int mrmUserId = 25;
            List<int> channelist = new List<int>() { 1, 2, 3 };
            List<int> budgettypelist = new List<int>() { 1, 2 };

            SqlParameter userChannels = new SqlParameter("@ChannelIds", SqlDbType.NVarChar);
            userChannels.Value = channelist;

            SqlParameter userBudgetTypes = new SqlParameter("@BudgetTypeID", SqlDbType.NVarChar);
            userBudgetTypes.Value = budgettypelist;

            SqlParameter userId = new SqlParameter("@UserID", SqlDbType.Int);
            userId.Value = mrmUserId;

            SqlParameter invoiceHeaderId = new SqlParameter("@InvoiceHeaderId", SqlDbType.Int);
            invoiceHeaderId.Value = 119;

            List<SqlParameter> result = new List<SqlParameter>();
            result.Add(userChannels);
            result.Add(userBudgetTypes);
            result.Add(userId);
            result.Add(invoiceHeaderId);

            ApUploadUserChoiceModel apUploadUserChoiceModel = new ApUploadUserChoiceModel()
            {
                UserId = mrmUserId,
            };
            MRM.DANG.Model.UserMessageModel userMessage_Error = new Model.UserMessageModel
            {
                IsSuccess = false,
                IsWarning = false
            };

            bool exceptionCaught = false;
            UserMessageModel userMessage = new UserMessageModel();
            #endregion

            #region  Mock
            mockApInvoiceBatchRepository.Setup(x => x.CreateBatch(It.IsAny<ApUploadUserChoiceModel>()))
                .Returns(userMessage_Error);
            #endregion

            #region Service
            var financeService = new FinanceServiceMock(
                apInvoiceBatchRepository: mockApInvoiceBatchRepository.Object);
            try
            {
                userMessage = financeService.CreateBatch(It.IsAny<ApUploadUserChoiceModel>());
            }
            catch (Exception ex)
            {
                exceptionCaught = true;
            }

            #endregion

            #region Assertions
            Assert.IsFalse(userMessage.IsSuccess == true);
            Assert.IsTrue(userMessage.IsWarning == false);
            Assert.IsTrue(exceptionCaught == true);
            #endregion

        }

        //MRM-197
        [TestMethod]
        public void AddUpdateInvoice_Test()
        {
            #region Data
            int mrmUserId = 1;
            string networkLogin = "abc";
            InvoiceViewModel viewModel = new InvoiceViewModel()
            {
                Id = 0,
                Vendor = 142,
                PaymentTerm = 9,
                Status = 2,
                InvoiceNumber = "123",
                InvoiceDate = DateTime.Now,
                PostingDate = DateTime.Now,
                Comments = "Invoice Creation Comments",
                WebUser = 123,//mrmuserId,
                timestamp = null
            };
            List<InvoiceLineViewModel> lstInvoiceLines = new List<InvoiceLineViewModel>();
            InvoiceLineViewModel invoiceLine1 = new InvoiceLineViewModel()
            {
                ContractNumber = "1234",
                PurchaseOrderNumber = "Abcd1234",
                Description = "Test InvoiceLine1 to insert",
                Comment = "Test Comment to insert",
                Amount = 250,
                DeliverableBudgetId = 129289,
                GLAccountId = 50,
                Id = 1,
                ProductionMethodType = 2,
                WBSElementId = 3250,
                WBSNumber = "1146",
                Coding = "",
                ChannelCostCenterId = 21,
                ChannelProfitCenterId = null,

            };
            InvoiceLineViewModel invoiceLine2 = new InvoiceLineViewModel()
            {
                ContractNumber = "2345",
                PurchaseOrderNumber = "Abcd2345",
                Description = "Test InvoiceLine2 to insert",
                Comment = "Test Comment to insert",
                Amount = 124,
                DeliverableBudgetId = 13458,
                GLAccountId = 50,
                Id = 1,
                ProductionMethodType = 1,
                WBSElementId = 3250,
                WBSNumber = "1146",
                Coding = "",
                ChannelCostCenterId = null,
                ChannelProfitCenterId = 21
            };
            Vendor vendor = new Vendor()
            {
                Name = "TestVendor1",
                Id = 1,
            };
            InvoiceStatus status = new InvoiceStatus()
            {
                Id = 1,
                Name = "In Progress"
            };

            lstInvoiceLines.Add(invoiceLine1);
            lstInvoiceLines.Add(invoiceLine2);
            viewModel.InvoiceLines = lstInvoiceLines;
            InvoiceHeader theInvoice = FinanceMapper.ToInvoice(viewModel);
            theInvoice.Vendor = vendor;
            theInvoice.InvoiceStatus = status;
            InvoiceHeader theInvoice1 = FinanceMapper.ToInvoice(viewModel);
            theInvoice1.Id = 999;
            #endregion

            #region  Mock
            //invoiceHeaderRepository  Mock
            mockInvoiceHeaderRepository.Setup(x => x.Add(It.IsAny<InvoiceHeader>())).Returns(theInvoice);
            //iunit Mock
            mockIUnitOfWork.Setup(x => x.Commit());
            //IInvoiceLine Repository Mock
            mockIInvoiceLineRepository.Setup(x => x.Add(It.IsAny<InvoiceLine>())).Returns(() => null);
            //FinanceService Service Mock
            mockFinanceService.Setup(x => x.AddInvoice(It.IsAny<InvoiceHeader>())).Returns(theInvoice);
            mockFinanceService.Setup(x => x.GetInvoiceById(It.IsAny<int>())).Returns(theInvoice);
            this.mockInvoiceHeaderRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(theInvoice);
            this.mockInvoiceHeaderRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(theInvoice1);
            mockInvoiceHeaderRepository.Setup(x=>x.GetInvoiceById(It.IsAny<int>())).Returns(theInvoice);
            mockIInvoiceLineRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<InvoiceLine, bool>>>()))
            .Returns(new List<InvoiceLine>());
            mockIInvoiceLineRepository.Setup(x => x.GetById(It.IsAny<long>()))
            .Returns(new InvoiceLine());
            mockInvoiceHeaderRepository.Setup(x => x.Update(It.IsAny<InvoiceHeader>()));
            mockIInvoiceLineRepository.Setup(x => x.Update(It.IsAny<InvoiceLine>()));
            //mock to finance Service
            FinanceServiceMock financeService = new FinanceServiceMock(
           invoiceHeaderRepository: mockInvoiceHeaderRepository.Object, unitOfWork: mockIUnitOfWork.Object, invoiceLineRepository: mockIInvoiceLineRepository.Object);

            //Finance Controller Mock
            var FinanceController = new FinanceControllerMock(financeServicee: financeService);
            #endregion

            #region Assertions
            mockInvoiceHeaderRepository.Verify();
            mockIUnitOfWork.Verify();
            mockIInvoiceLineRepository.Verify();
            mockFinanceService.Verify();

            //Mapping the viewmodel to actual Table
            var serviceResult = financeService.AddInvoice(theInvoice);
            Assert.IsNotNull(serviceResult);
            Assert.IsNotNull(serviceResult.InvoiceLine);
            Assert.AreEqual(serviceResult.Id, 0);//The actual Id is auto generated after insertion done in DB.
            //As we are passing 0 as invoice headerId for the purpose of insertion here we recieve the same.

            var controllerResult = FinanceController.AddUpdateInvoice(viewModel, mrmUserId, networkLogin);
            Assert.IsNotNull(controllerResult);
            Assert.AreEqual(controllerResult.Success, true);
            Assert.IsNotNull(controllerResult.Data);

            //Service N Controller Assertions for Updating Invoice
            viewModel.Id =999;
            
            //Mapping the viewmodel to actual Table
            var serviceResultForUpdate = financeService.UpdateInvoice(theInvoice1);
            Assert.IsNotNull(serviceResultForUpdate);
            Assert.IsNotNull(serviceResultForUpdate.InvoiceLine);
            Assert.AreEqual(serviceResultForUpdate.Id, 999);//Here we will recieve the same invoice header id we passed for update

            var controllerUpdateResult = FinanceController.AddUpdateInvoice(viewModel, mrmUserId, networkLogin);
            Assert.IsNotNull(controllerUpdateResult);
            Assert.AreEqual(controllerUpdateResult.Success, true);
            Assert.IsNotNull(controllerUpdateResult.Data);
            #endregion
        }
        //MRM-211
        [TestMethod]
        public void ReconciliationVendorsTest()
        {
            #region Data         
            WBS_Deliverables wbs_deliverable = new WBS_Deliverables()
            {
                DeliverableId = 14000023,
                FullWBSNumber = "1147614.001.001",
            };
            List<WBS_Deliverables> wbs_deliverables = new List<WBS_Deliverables>();
            wbs_deliverables.Add(wbs_deliverable);
            wbs_deliverables.Add(new WBS_Deliverables { FullWBSNumber = "1147614.003.001", DeliverableId = 14000000, MasterVendorId = 1 });
            wbs_deliverables.Add(new WBS_Deliverables { FullWBSNumber = "1147614.022.001", DeliverableId = 14000000, MasterVendorId = 2 });
            wbs_deliverables.Add(new WBS_Deliverables { FullWBSNumber = "1147614.022.001", DeliverableId = 14000000, MasterVendorId = 3 });
            #endregion
            #region Mock
            mockfinanceservice.Setup(x => x.ReconciliationVendors(It.IsAny<int>(), It.IsAny<string>())).Returns(wbs_deliverables);
            mockWBS_DeliverablesRepository.Setup(x => x.ReconciliationVendors(It.IsAny<int>(), It.IsAny<string>())).Returns(wbs_deliverables);

            //Finance Service Mock
            var financeservicemock = new FinanceServiceMock(_iWBS_DeliverablesRepository: mockWBS_DeliverablesRepository.Object);
            //Finance Controller Mock
            var FinanceController = new FinanceControllerMock(financeServicee: mockFinanceService.Object);
            #endregion

            #region service
            List<WBS_Deliverables> result = financeservicemock.ReconciliationVendors(wbs_deliverable.DeliverableId, wbs_deliverable.FullWBSNumber);
            #endregion

            #region Assertions 
            mockWBS_DeliverablesRepository.Verify();
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.ElementAt(2).FullWBSNumber == "1147614.022.001");
            Assert.IsTrue(result.ElementAt(2).DeliverableId == 14000000);
            Assert.IsTrue(result.ElementAt(2).MasterVendorId == 2);
            #endregion

        }

        //MRM-211
        [TestMethod]
        public void ReconciliationMethodOfProductionTest()
        {
            #region Data
            WBS_Deliverables wbs_deliverable = new WBS_Deliverables()
            {
                DeliverableId = 14000023,
                FullWBSNumber = "1147614.001.001",
                MasterVendorId = 1
            };
            List<WBS_Deliverables> wbs_deliverables = new List<WBS_Deliverables>();
            wbs_deliverables.Add(wbs_deliverable);
            wbs_deliverables.Add(new WBS_Deliverables { FullWBSNumber = "1147614.003.001", DeliverableId = 14000000, MasterVendorId = 2, ProductionMethodTypeId = 5 });
            wbs_deliverables.Add(new WBS_Deliverables { FullWBSNumber = "1147614.022.001", DeliverableId = 14000012, MasterVendorId = 2, ProductionMethodTypeId = 6 });
            wbs_deliverables.Add(new WBS_Deliverables { FullWBSNumber = "1147614.022.001", DeliverableId = 14000032, MasterVendorId = 2, ProductionMethodTypeId = 4 });

            #endregion

            #region Mock
            mockfinanceservice.Setup(x => x.ReconciliationMethodOfProduction(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(wbs_deliverables);
            mockWBS_DeliverablesRepository.Setup(x => x.ReconciliationMethodOfProduction(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(wbs_deliverables);
            //Finance Service Mock
            var financeservicemock = new FinanceServiceMock(_iWBS_DeliverablesRepository: mockWBS_DeliverablesRepository.Object);
            //Finance Controller Mock
            var FinanceController = new FinanceControllerMock(financeServicee: mockFinanceService.Object);
            #endregion

            #region service
            List<WBS_Deliverables> result = financeservicemock.ReconciliationMethodOfProduction(wbs_deliverable.DeliverableId, wbs_deliverable.MasterVendorId ?? default(int), wbs_deliverable.FullWBSNumber);
            #endregion

            #region Assertions
            mockWBS_DeliverablesRepository.Verify();
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.ElementAt(1).MasterVendorId == 2);
            Assert.IsTrue(result.ElementAt(2).ProductionMethodTypeId == 6);
            #endregion

        }

        [TestMethod]
        public void GetBudgetCommitedDetailsWithoutInvoiceDates_Test()
        {
            #region Data
            Vendor vendor1 = new Vendor()
            {
                Id = 66,
                Name = "KMP"
            };
            Vendor vendor2 = new Vendor()
            {
                Id = 99,
                Name = "SS"
            };
            InvoiceStatus invoiceStatus = new InvoiceStatus()
            {
                Id = 1,
                Name = "In Process",
                Code = "INPCC"
            };
            InvoiceHeader invoiceHeader1 = new InvoiceHeader()
            {
                Id = 200,
                InvoiceNumber = "111",
                InvoiceStatus = invoiceStatus,
                Comments = "UTC Test",
                Vendor = vendor1
            };
            InvoiceHeader invoiceHeader2 = new InvoiceHeader()
            {
                Id = 100,
                InvoiceNumber = "222",
                InvoiceStatus = invoiceStatus,
                Comments = "UTC Test 2",
                Vendor = vendor2
            };
            InvoiceLine invoiceLine1 = new InvoiceLine()
            {
                Id = 1,
                InvoiceHeaderId = 100,
                Amount = 99,
                LineDescription = "Invoice Line 1",
                GLAccountId = 22
            };
            InvoiceLine invoiceLine2 = new InvoiceLine()
            {
                Id = 2,
                InvoiceHeaderId = 200,
                Amount = 635,
                LineDescription = "Invoice Line 2",
                GLAccountId = 78
            };
            List<InvoiceLine> invoiceLineList = new List<InvoiceLine>();
            invoiceLineList.Add(invoiceLine1);
            invoiceLineList.Add(invoiceLine2);
            #endregion

            #region Mock
            mockfinanceservice.Setup(x => x.GetInvoiceLinesByWBSId(It.IsAny<int>()))
                .Returns(invoiceLineList);
            mockfinanceservice.Setup(x => x.GetInvoiceLinesByDeliverableandWBS(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(invoiceLineList);
            mockfinanceservice.SetupSequence(x => x.GetInvoiceById(It.IsAny<int>()))
                .Returns(invoiceHeader1)
                .Returns(invoiceHeader2);
            //Finance Controller Mock
            var FinanceController = new FinanceControllerMock(financeServicee: mockfinanceservice.Object);
            #endregion

            #region Service
            List<BudgetCommittedAmountDetailsViewModel> result = FinanceController.GetBudgetCommitedDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            #endregion

            #region Assertions
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.ElementAt(0).Description == "Invoice Line 1");
            Assert.IsTrue(result.ElementAt(1).Description == "Invoice Line 2");
            Assert.IsTrue(result.ElementAt(0).HeaderComments == "UTC Test");
            Assert.IsTrue(result.ElementAt(1).HeaderComments == "UTC Test 2");
            Assert.IsTrue(result.ElementAt(0).Vendor == "KMP");
            Assert.IsTrue(result.ElementAt(1).Vendor == "SS");
            Assert.IsTrue(result.ElementAt(0).InlineAmount == 99);
            Assert.IsTrue(result.ElementAt(1).InlineAmount == 635);
            Assert.IsFalse(result.ElementAt(1).Status == "Approved");
            #endregion
        }

        [TestMethod]
        public void GetBudgetCommitedDetailsWithInvoiceDates_Test()
        {
            #region Data
            Vendor vendor1 = new Vendor()
            {
                Id = 66,
                Name = "KMP"
            };
            Vendor vendor2 = new Vendor()
            {
                Id = 99,
                Name = "SS"
            };
            InvoiceStatus invoiceStatus = new InvoiceStatus()
            {
                Id = 1,
                Name = "In Process",
                Code = "INPCC"
            };
            InvoiceHeader invoiceHeader1 = new InvoiceHeader()
            {
                Id = 200,
                InvoiceNumber = "111",
                InvoiceDate = new DateTime(2016,9,1),
                InvoiceStatus = invoiceStatus,
                Comments = "UTC Test",
                Vendor = vendor1
            };
            InvoiceHeader invoiceHeader2 = new InvoiceHeader()
            {
                Id = 100,
                InvoiceNumber = "222",
                InvoiceDate = new DateTime(2016,9,2),
                InvoiceStatus = invoiceStatus,
                Comments = "UTC Test 2",
                Vendor = vendor2
            };
            InvoiceLine invoiceLine1 = new InvoiceLine()
            {
                Id = 1,
                InvoiceHeaderId = 100,
                Amount = 99,
                LineDescription = "Invoice Line 1",
                GLAccountId = 22
            };
            InvoiceLine invoiceLine2 = new InvoiceLine()
            {
                Id = 2,
                InvoiceHeaderId = 200,
                Amount = 635,
                LineDescription = "Invoice Line 2",
                GLAccountId = 78
            };
            List<InvoiceLine> invoiceLineList = new List<InvoiceLine>();
            invoiceLineList.Add(invoiceLine1);
            invoiceLineList.Add(invoiceLine2);
            #endregion

            #region Mock
            mockfinanceservice.Setup(x => x.GetInvoiceLinesByWBSId(It.IsAny<int>()))
                .Returns(invoiceLineList);
            mockfinanceservice.Setup(x => x.GetInvoiceLinesByDeliverableandWBS(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(invoiceLineList);
            mockfinanceservice.SetupSequence(x => x.GetInvoiceById(It.IsAny<int>()))
                .Returns(invoiceHeader1)
                .Returns(invoiceHeader2);
            //Finance Controller Mock
            var FinanceController = new FinanceControllerMock(financeServicee: mockfinanceservice.Object);
            #endregion

            #region Service
            List<BudgetCommittedAmountDetailsViewModel> result = FinanceController.GetBudgetCommitedDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            #endregion

            #region Assertions
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.ElementAt(0).Description == "Invoice Line 2");
            Assert.IsTrue(result.ElementAt(1).Description == "Invoice Line 1");
            Assert.IsTrue(result.ElementAt(0).HeaderComments == "UTC Test 2");
            Assert.IsTrue(result.ElementAt(1).HeaderComments == "UTC Test");
            Assert.IsTrue(result.ElementAt(0).Vendor == "SS");
            Assert.IsTrue(result.ElementAt(1).Vendor == "KMP");
            Assert.IsTrue(result.ElementAt(0).InlineAmount == 635);
            Assert.IsTrue(result.ElementAt(1).InlineAmount == 99);
            Assert.IsFalse(result.ElementAt(1).Status == "Approved");
            #endregion
        }

        [TestMethod]
        public void GetBudgetActualDetailss_Test()
        {
            #region Data
            TypeOfWork typeOfWork1 = new TypeOfWork()
            {
                Id = 88,
                FiscalYear = 2016
            };
            WBSElement wBSElement1 = new WBSElement()
            {
                Id = 4569,
                TypeOfWork = typeOfWork1
            };
            SAPActualsFileRow sAPActualsFileRow1 = new SAPActualsFileRow()
            {
                DocumentNumber = "55",
                OffsettingAccountNo = "778899",
                NameOfOffsettingAccountGKONT_LTXT = "OffSet 1",
                CostElement = "CE1",
                CostElementDescription = "CE1 + Desc",
            };
            TypeOfWork typeOfWork2 = new TypeOfWork()
            {
                Id = 99,
                FiscalYear = 2017
            };
            WBSElement wBSElement2 = new WBSElement()
            {
                Id = 9657,
                TypeOfWork = typeOfWork2
            };
            SAPActualsFileRow sAPActualsFileRow2 = new SAPActualsFileRow()
            {
                DocumentNumber = "63",
                OffsettingAccountNo = "112233",
                NameOfOffsettingAccountGKONT_LTXT = "OffSet 2",
                CostElement = "CE2",
                CostElementDescription = "CE2 + Desc",
            };
            ActualsReconciliation actualsReconciliation1 = new ActualsReconciliation()
            {
                Id = 1,
                Name = "Recon 1",
                PostingDate = DateTime.Now,
                ActualAmount = 1001,
                SAPActualsFileRow = sAPActualsFileRow1,
                WBSElement = wBSElement1
            };
            ActualsReconciliation actualsReconciliation2 = new ActualsReconciliation()
            {
                Id = 2,
                Name = "Recon 2",
                PostingDate = DateTime.Now,
                ActualAmount = 9009,
                SAPActualsFileRow = sAPActualsFileRow2,
                WBSElement = wBSElement2
            };
            List<ActualsReconciliation> actualsReconciliationList = new List<ActualsReconciliation>();
            actualsReconciliationList.Add(actualsReconciliation1);
            actualsReconciliationList.Add(actualsReconciliation2);
            #endregion

            #region Mock
            mockfinanceservice.Setup(x => x.GetActualsByWBS(It.IsAny<int>()))
                .Returns(actualsReconciliationList);
            mockfinanceservice.Setup(x => x.GetActualsByDeliverableandWBS(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(actualsReconciliationList);
            //Finance Controller Mock
            var FinanceController = new FinanceControllerMock(financeServicee: mockfinanceservice.Object);
            #endregion

            #region Service
            List<BudgetActualsAmountDetailsViewModel> result = FinanceController.GetBudgetActualDetailss(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            #endregion

            #region Assertions
            Assert.IsFalse(result == null);
            Assert.IsTrue(result.ElementAt(0).CostElementGLAccount == "CE1");
            Assert.IsTrue(result.ElementAt(1).CostElementGLAccount == "CE2");
            Assert.IsTrue(result.ElementAt(0).FiscalYear == "2016");
            Assert.IsTrue(result.ElementAt(1).FiscalYear == "2017");
            Assert.IsTrue(result.ElementAt(0).OffsettingAccountName == "OffSet 1");
            Assert.IsTrue(result.ElementAt(1).OffsettingAccountName == "OffSet 2");
            Assert.IsTrue(result.ElementAt(0).SAPDocumentNumber == "55");
            Assert.IsTrue(result.ElementAt(1).SAPDocumentNumber == "63");
            Assert.IsTrue(result.ElementAt(0).OffsettingAccountNumber == "778899");
            Assert.IsTrue(result.ElementAt(1).OffsettingAccountNumber == "112233");
            #endregion
        }
    }
}
