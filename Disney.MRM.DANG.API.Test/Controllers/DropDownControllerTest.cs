using Disney.MRM.DANG.API.Test.MockObject.Controller;
using Disney.MRM.DANG.API.Test.MockObject.Service;
using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.Model;
using Disney.MRM.DANG.Service.Contracts;
using Disney.MRM.DANG.Service.Implementations;
using Disney.MRM.DANG.ViewModel;
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
    public class DropDownControllerTest
    {
        private Moq.MockRepository mockRepository;
        private Moq.Mock<IProjectService> mockProjectService;
        private Moq.Mock<IUserService> mockUserService;
        private Moq.Mock<IContractRequestService> mockContractRequestService;
        private Moq.Mock<IBudgetService> mockBudgetService;
        private Moq.Mock<IApprovalService> mockApprovalService;
        private Moq.Mock<IInternationalService> mockInternationalService;
        private Moq.Mock<IDeliverableService> mockDeliverableService;
        private Moq.Mock<IDeliverableServiceV2> mockDeliverableServiceV2;
        private Moq.Mock<IFinanceService> mockFinanceService;
        private Moq.Mock<IWBS_DeliverablesRepository> mockWBS_DeliverablesRepository;

        [TestInitialize]
        public void ClassInit()
        {
            mockRepository = new MockRepository(MockBehavior.Loose) { DefaultValue = DefaultValue.Mock };
            mockProjectService = mockRepository.Create<IProjectService>();
            mockContractRequestService = mockRepository.Create<IContractRequestService>();
            mockBudgetService = mockRepository.Create<IBudgetService>();
            mockApprovalService = mockRepository.Create<IApprovalService>();
            mockInternationalService = mockRepository.Create<IInternationalService>();
            mockDeliverableService = mockRepository.Create<IDeliverableService>();
            mockDeliverableServiceV2 = mockRepository.Create<IDeliverableServiceV2>();
            mockFinanceService = mockRepository.Create<IFinanceService>();
            mockWBS_DeliverablesRepository = mockRepository.Create<IWBS_DeliverablesRepository>();
            mockUserService = mockRepository.Create<IUserService>();
        }

        [TestMethod]
        public void GetDeliverableForInvoice_Test()
        {
            //Set Data
            List<DropDownViewModel> lstdropDownViewModel = new List<DropDownViewModel>();
            DropDownViewModel drpDownModel = new DropDownViewModel()
            {

            };
            List<WBS_Deliverables> lstWBS = new List<WBS_Deliverables>();
            WBS_Deliverables wbs1 = new WBS_Deliverables()
            {
                DeliverableId = 1234,
                DeliverableName = "Deliverable1",
                ExternalWBSFlag = false
            };
            WBS_Deliverables wbs2 = new WBS_Deliverables()
            {
                DeliverableId = 2345,
                DeliverableName = "Deliverable2",
                ExternalWBSFlag = true
            };
            WBS_Deliverables wbs3 = new WBS_Deliverables()
            {
                DeliverableId = 3456,
                DeliverableName = "Deliverable3",
                ExternalWBSFlag = false
            };
            lstWBS.Add(wbs1);
            lstWBS.Add(wbs2);
            lstWBS.Add(wbs3);

            DropDownViewModel vm1 = new DropDownViewModel()
            {
                Text = "1234 ( Deliverable1 )",
                Value = "1234"
            };
            DropDownViewModel vm2 = new DropDownViewModel()
            {
                Text = "2345 ( Deliverable2 )",
                Value = "2345"
            };
            DropDownViewModel vm3 = new DropDownViewModel()
            {
                Text = "3456 ( ExternalDeliverable )",
                Value = "3456"
            };
            lstdropDownViewModel.Add(vm1);
            lstdropDownViewModel.Add(vm2);
            lstdropDownViewModel.Add(vm3);

            //Inputs 
            bool isExternal = false;
            string filter = "";
            int mrmUserid = 1234;
            string networkLogin = "Disney ABC";

            //Mock

            //FinanceService Service Mock
            mockFinanceService.Setup(x => x.GetDeliverablesForInvoiceLine(It.IsAny<bool>())).Returns(lstWBS);
            //iWBS_DeliverablesRepository Mock
            mockWBS_DeliverablesRepository.Setup(x => x.GetDeliverablesForInvoiceLine(It.IsAny<bool>())).Returns(lstWBS);

            var financeService = new FinanceServiceMock(_iWBS_DeliverablesRepository: mockWBS_DeliverablesRepository.Object);
            //Finance Controller Mock
            var DropDownController = new DropDownControllerMock(financeService: mockFinanceService.Object);


            //Assertions
            mockFinanceService.Verify();
            mockWBS_DeliverablesRepository.Verify();

            var serviceResult = financeService.GetDeliverablesForInvoiceLine(isExternal);


            #region "Assertion ofGetDeliverableForInvoice"
            Assert.IsNotNull(serviceResult);//Result is not Null
            Assert.AreEqual(serviceResult, lstWBS);//Asserting the expected return object with dummy data
            Assert.AreEqual(serviceResult.Count, 3);//Assert matching the return data with my input

            var controllerResult = DropDownController.GetDeliverableForInvoice(isExternal, filter, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResult);//Result is not Null
            Assert.AreEqual(controllerResult.Count, 3);

            isExternal = true;
            var controllerResultForExternalTrue = DropDownController.GetDeliverableForInvoice(isExternal, filter, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResultForExternalTrue);//Result is not Null
            Assert.AreEqual(controllerResultForExternalTrue.Count, 3);

            filter = "Deliverable1";
            var controllerResultForfiltereingonDeliverableText = DropDownController.GetDeliverableForInvoice(isExternal, filter, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResultForfiltereingonDeliverableText);//Result is not Null
            Assert.AreEqual(controllerResultForfiltereingonDeliverableText.Count, 1);

            #endregion

        }

        [TestMethod]
        public void GetMethodOFProductionByDeliverableId_Test()
        {
            //Set Data
            List<DropDownViewModel> lstdropDownViewModel = new List<DropDownViewModel>();
            DropDownViewModel drpDownModel = new DropDownViewModel()
            {

            };
            List<WBS_Deliverables> lstWBS = new List<WBS_Deliverables>();
            WBS_Deliverables wbs1 = new WBS_Deliverables()
            {
                DeliverableId = 1234,
                ExternalWBSFlag = false,
                ProductionMethodTypeId = 1,
                ProductionMethodTypeName = "Post House",
                DeliverableName = "Deliverable1"
            };
            WBS_Deliverables wbs2 = new WBS_Deliverables()
            {
                DeliverableId = 2345,
                DeliverableName = "Deliverable2",
                ExternalWBSFlag = true,
                ProductionMethodTypeId = 2,
                ProductionMethodTypeName = "Contract Request"

            };
            WBS_Deliverables wbs3 = new WBS_Deliverables()
            {
                DeliverableId = 3456,
                DeliverableName = "Deliverable3",
                ExternalWBSFlag = false,
                ProductionMethodTypeId = 3,
                ProductionMethodTypeName = "Miscellaneous"
            };
            lstWBS.Add(wbs1);
            lstWBS.Add(wbs2);
            lstWBS.Add(wbs3);

            DropDownViewModel vm1 = new DropDownViewModel()
            {
                Text = "Post House",
                Value = "1"
            };
            DropDownViewModel vm2 = new DropDownViewModel()
            {
                Text = "Contract Request",
                Value = "2"
            };
            DropDownViewModel vm3 = new DropDownViewModel()
            {
                Text = "Miscellaneous",
                Value = "3"
            };
            lstdropDownViewModel.Add(vm1);
            lstdropDownViewModel.Add(vm2);
            lstdropDownViewModel.Add(vm3);

            //Inputs 
            bool isExternal = false;
            string filter = "";
            int mrmUserid = 1234;
            string networkLogin = "Disney ABC";
            int deliverableId = 1234;
            int vendorid = 23;
            int wbsid = 212;

            #region Mock
            //Mock

            //FinanceService Service Mock
            mockFinanceService.Setup(x => x.GetMethodOFProductionByDeliverableId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(lstWBS);
            //iWBS_DeliverablesRepository Mock
            mockWBS_DeliverablesRepository.Setup(x => x.GetMethodOFProductionByDeliverableId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(lstWBS);

            var financeService = new FinanceServiceMock(
             _iWBS_DeliverablesRepository: mockWBS_DeliverablesRepository.Object);
            //Finance Controller Mock
            var DropDownController = new DropDownControllerMock(financeService: mockFinanceService.Object);

            #endregion

            //Assertions
            mockFinanceService.Verify();
            mockWBS_DeliverablesRepository.Verify();

            var serviceResult = financeService.GetMethodOFProductionByDeliverableId(deliverableId, vendorid, wbsid, isExternal);


            #region "Assertion of GetMethodOFProductionByDeliverableId"
            Assert.IsNotNull(serviceResult);//Result is not Null
            Assert.AreEqual(serviceResult, lstWBS);//Asserting the expected return object with dummy data
            Assert.AreEqual(serviceResult.Count, 3);//Assert matching the return data with my input

            var controllerResult = DropDownController.GetMethodOFProductionByDeliverableId(deliverableId, vendorid, wbsid, isExternal, filter, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResult);//Result is not Null
            Assert.AreEqual(controllerResult.Count, 3);

            isExternal = true;
            var controllerResultForExternalTrue = DropDownController.GetMethodOFProductionByDeliverableId(deliverableId, vendorid, wbsid, isExternal, filter, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResultForExternalTrue);//Result is not Null
            Assert.AreSame(controllerResultForExternalTrue[0].Text.Trim(), "Post House");

            filter = "deliverable1";
            var controllerResultForFilter = DropDownController.GetMethodOFProductionByDeliverableId(deliverableId, vendorid, wbsid, isExternal, filter, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResultForExternalTrue);//Result is not Null
            Assert.AreSame(controllerResultForExternalTrue[0].Text.Trim(), "Post House");
            #endregion
        }

        [TestMethod]
        public void GetWBSByDeliverableNMOP_Test()
        {
            //Set Data
            List<DropDownViewModel> lstdropDownViewModel = new List<DropDownViewModel>();
            DropDownViewModel drpDownModel = new DropDownViewModel()
            {

            };
            List<WBS_Deliverables> lstWBS = new List<WBS_Deliverables>();
            WBS_Deliverables wbs1 = new WBS_Deliverables()
            {
                DeliverableId = 1234,
                WBSElementId = 1,
                DeliverableName = "Deliverable1",
                FullWBSNumber = "11234.001.002"
            };
            WBS_Deliverables wbs2 = new WBS_Deliverables()
            {
                DeliverableId = 2345,
                WBSElementId = 2,
                FullWBSNumber = "12456.001.002",
                DeliverableName = "Deliverable2",
            };
            WBS_Deliverables wbs3 = new WBS_Deliverables()
            {
                DeliverableId = 3456,
                WBSElementId = 3,
                FullWBSNumber = "11234.003.001",
                DeliverableName = "Deliverable1",
            };
            lstWBS.Add(wbs1);
            lstWBS.Add(wbs2);
            lstWBS.Add(wbs3);

            DropDownViewModel vm1 = new DropDownViewModel()
            {
                Text = "11234.001.002",
                Value = "1"
            };
            DropDownViewModel vm2 = new DropDownViewModel()
            {
                Text = "12456.001.002",
                Value = "2"
            };
            DropDownViewModel vm3 = new DropDownViewModel()
            {
                Text = "11234.003.001",
                Value = "3"
            };
            lstdropDownViewModel.Add(vm1);
            lstdropDownViewModel.Add(vm2);
            lstdropDownViewModel.Add(vm3);

            //Inputs 
            bool isExternal = false;
            string filter = "";
            int mrmUserid = 1234;
            string networkLogin = "Disney ABC";
            int deliverableId = 1234;
            int mopId = 542;

            #region Mock
            //Mock

            //FinanceService Service Mock
            mockFinanceService.Setup(x => x.GetWBSByDeliverableNMOP(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(lstWBS);
            //iWBS_DeliverablesRepository Mock
            mockWBS_DeliverablesRepository.Setup(x => x.GetWBSByDeliverableNMOP(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(lstWBS);


            var financeService = new FinanceServiceMock(
             _iWBS_DeliverablesRepository: mockWBS_DeliverablesRepository.Object);
            //Finance Controller Mock
            var DropDownController = new DropDownControllerMock(financeService: mockFinanceService.Object);

            #endregion

            //Assertions
            mockFinanceService.Verify();
            mockWBS_DeliverablesRepository.Verify();

            var serviceResult = financeService.GetWBSByDeliverableNMOP(deliverableId, mopId, isExternal);


            #region "Assertion of GetWBSByDeliverableNMOP"
            Assert.IsNotNull(serviceResult);//Result is not Null
            Assert.AreEqual(serviceResult, lstWBS);//Asserting the expected return object with dummy data
            Assert.AreEqual(serviceResult.Count, 3);//Assert matching the return data with my input

            var controllerResult = DropDownController.GetWBSByDeliverableNMOP(deliverableId, mopId, isExternal, filter, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResult);//Result is not Null
            Assert.AreEqual(controllerResult.Count, 3);

            isExternal = true;
            var controllerResultForExternalTrue = DropDownController.GetWBSByDeliverableNMOP(deliverableId, mopId, isExternal, filter, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResultForExternalTrue);//Result is not Null
            Assert.AreSame(controllerResultForExternalTrue[2].Text.Trim(), "11234.003.001");

            filter = "Deliverable1";
            var controllerResultForFilter = DropDownController.GetWBSByDeliverableNMOP(deliverableId, mopId, isExternal, filter, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResultForExternalTrue);//Result is not Null
            Assert.AreSame(controllerResultForExternalTrue[0].Text.Trim(), "11234.001.002");
            #endregion

        }

        [TestMethod]
        public void GetWBSByTow_Test()
        {
            //Set Data
            List<DropDownViewModel> lstdropDownViewModel = new List<DropDownViewModel>();
            DropDownViewModel drpDownModel = new DropDownViewModel()
            {

            };
            List<WBS_Deliverables> lstWBS = new List<WBS_Deliverables>();
            WBS_Deliverables wbs1 = new WBS_Deliverables()
            {
                TypeOfWorkId = 542,
                WBSElementId = 17,
                FullWBSNumber = "11234.001.002",
                DeliverableId = 1,
                DeliverableName = "Deliverable1"
            };
            WBS_Deliverables wbs2 = new WBS_Deliverables()
            {
                TypeOfWorkId = 2345,
                WBSElementId = 2,
                FullWBSNumber = "12456.001.002",
                DeliverableId = 2,
                DeliverableName = "Deliverable2"
            };
            WBS_Deliverables wbs3 = new WBS_Deliverables()
            {
                TypeOfWorkId = 3456,
                WBSElementId = 3,
                FullWBSNumber = "11234.003.001",
                DeliverableId = 3,
                DeliverableName = "Deliverable3"
            };
            lstWBS.Add(wbs1);
            lstWBS.Add(wbs2);
            lstWBS.Add(wbs3);

            DropDownViewModel vm1 = new DropDownViewModel()
            {
                Text = "11234.001.002",
                Value = "17",
                Id = 17
            };
            DropDownViewModel vm2 = new DropDownViewModel()
            {
                Text = "12456.001.002",
                Value = "2",
                Id = 18
            };
            DropDownViewModel vm3 = new DropDownViewModel()
            {
                Text = "11234.003.001",
                Value = "3",
                Id = 19
            };
            lstdropDownViewModel.Add(vm1);
            lstdropDownViewModel.Add(vm2);
            lstdropDownViewModel.Add(vm3);

            //Inputs 
            bool isExternal = false;
            string filter = "";
            int mrmUserid = 1234;
            string networkLogin = "Disney ABC";
            int towId = 542;

            #region Mock
            //Mock

            //FinanceService Service Mock
            mockFinanceService.Setup(x => x.GetWBSByTow(It.IsAny<int>(), It.IsAny<bool>())).Returns(lstWBS);
            //iWBS_DeliverablesRepository Mock
            mockWBS_DeliverablesRepository.Setup(x => x.GetWBSByTow(It.IsAny<int>(), It.IsAny<bool>())).Returns(lstWBS);


            var financeService = new FinanceServiceMock(
             _iWBS_DeliverablesRepository: mockWBS_DeliverablesRepository.Object);
            //Finance Controller Mock
            var DropDownController = new DropDownControllerMock(financeService: mockFinanceService.Object);

            #endregion

            //Assertions
            mockFinanceService.Verify();
            mockWBS_DeliverablesRepository.Verify();

            var serviceResult = financeService.GetWBSByTow(towId, isExternal);


            #region "Assertion of GetWBSByTow"
            Assert.IsNotNull(serviceResult);//Result is not Null
            Assert.AreEqual(serviceResult, lstWBS);//Asserting the expected return object with dummy data
            Assert.AreEqual(serviceResult.Count, 3);//Assert matching the return data with my input

            var controllerResult = DropDownController.GetWBSByTow(towId, isExternal, filter, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResult);//Result is not Null
            Assert.AreEqual(controllerResult.Count, 3);
            Assert.AreEqual(controllerResult[0].Id, vm1.Id);

            filter = "Deliverable2";
            var controllerResultForFilter = DropDownController.GetWBSByTow(towId, isExternal, filter, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResultForFilter);//Result is not Null
            Assert.AreSame(controllerResultForFilter[0].Text.Trim(), "12456.001.002");
            #endregion

        }


        [TestMethod]
        public void GetMopByWBS_Test()
        {
            //Set Data
            List<DropDownViewModel> lstdropDownViewModel = new List<DropDownViewModel>();
            DropDownViewModel drpDownModel = new DropDownViewModel()
            {

            };
            List<WBS_Deliverables> lstWBS = new List<WBS_Deliverables>();
            WBS_Deliverables wbs1 = new WBS_Deliverables()
            {
                TypeOfWorkId = 542,
                WBSElementId = 17,
                FullWBSNumber = "11234.001.002",
                ProductionMethodTypeId = 1,
                ProductionMethodTypeName = "Post House"
            };
            WBS_Deliverables wbs2 = new WBS_Deliverables()
            {
                TypeOfWorkId = 2345,
                WBSElementId = 21,
                FullWBSNumber = "12456.001.002",
                ProductionMethodTypeId = 2,
                ProductionMethodTypeName = "Contract Request"
            };
            WBS_Deliverables wbs3 = new WBS_Deliverables()
            {
                TypeOfWorkId = 3456,
                WBSElementId = 31,
                FullWBSNumber = "11234.003.001",
                ProductionMethodTypeId = 3,
                ProductionMethodTypeName = "Miscellaneous"
            };
            lstWBS.Add(wbs1);
            lstWBS.Add(wbs2);
            lstWBS.Add(wbs3);

            DropDownViewModel vm1 = new DropDownViewModel()
            {
                Text = "Post House",
                Value = "17",
                Id = 17
            };
            DropDownViewModel vm2 = new DropDownViewModel()
            {
                Text = "Contract Request",
                Value = "21",
                Id = 21
            };
            DropDownViewModel vm3 = new DropDownViewModel()
            {
                Text = "Miscellaneous",
                Value = "31",
                Id = 17
            };
            lstdropDownViewModel.Add(vm1);
            lstdropDownViewModel.Add(vm2);
            lstdropDownViewModel.Add(vm3);

            //Inputs 
            bool isExternal = false;
            string filter = "";
            int mrmUserid = 1234;
            string networkLogin = "Disney ABC";
            int wbsId = 31;

            #region Mock
            //Mock

            //FinanceService Service Mock
            mockFinanceService.Setup(x => x.GetMopByWBS(It.IsAny<int>(), It.IsAny<bool>())).Returns(lstWBS);
            //iWBS_DeliverablesRepository Mock
            mockWBS_DeliverablesRepository.Setup(x => x.GetMopByWBS(It.IsAny<int>(), It.IsAny<bool>())).Returns(lstWBS);


            var financeService = new FinanceServiceMock(
             _iWBS_DeliverablesRepository: mockWBS_DeliverablesRepository.Object);
            //Finance Controller Mock
            var DropDownController = new DropDownControllerMock(financeService: mockFinanceService.Object);

            #endregion

            //Assertions
            mockFinanceService.Verify();
            mockWBS_DeliverablesRepository.Verify();

            var serviceResult = financeService.GetMopByWBS(wbsId, isExternal);


            #region "Assertion of GetWBSByTow"
            Assert.IsNotNull(serviceResult);//Result is not Null
            Assert.AreEqual(serviceResult, lstWBS);//Asserting the expected return object with dummy data
            Assert.AreEqual(serviceResult.Count, 3);//Assert matching the return data with my input
            var finalList = serviceResult.Where(x => x.WBSElementId == wbsId).ToList();
            Assert.AreEqual(finalList.Count, 1);

            var controllerResult = DropDownController.GetMopByWBS(wbsId, isExternal, filter, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResult);//Result is not Null
            Assert.AreEqual(controllerResult.Count, 3);
            Assert.AreEqual(controllerResult[0].Text, "Post House");
            #endregion

        }

        [TestMethod]
        public void GetDeliverableNameByMopNWBS_Test()
        {
            //Set Data
            List<DropDownViewModel> lstdropDownViewModel = new List<DropDownViewModel>();
            DropDownViewModel drpDownModel = new DropDownViewModel()
            {

            };
            List<WBS_Deliverables> lstWBS = new List<WBS_Deliverables>();
            WBS_Deliverables wbs1 = new WBS_Deliverables()
            {
                TypeOfWorkId = 542,
                WBSElementId = 17,
                FullWBSNumber = "11234.001.002",
                ProductionMethodTypeId = 1,
                ProductionMethodTypeName = "Post House",
                DeliverableId = 1,
                DeliverableName = "Deliverable1"
            };
            WBS_Deliverables wbs2 = new WBS_Deliverables()
            {
                TypeOfWorkId = 2345,
                WBSElementId = 21,
                FullWBSNumber = "12456.001.002",
                ProductionMethodTypeId = 2,
                ProductionMethodTypeName = "Contract Request",
                DeliverableId = 2,
                DeliverableName = "Deliverable2"
            };
            WBS_Deliverables wbs3 = new WBS_Deliverables()
            {
                TypeOfWorkId = 3456,
                WBSElementId = 31,
                FullWBSNumber = "11234.003.001",
                ProductionMethodTypeId = 3,
                ProductionMethodTypeName = "Miscellaneous",
                DeliverableId = 3,
                DeliverableName = "Deliverable3"
            };
            lstWBS.Add(wbs1);
            lstWBS.Add(wbs2);
            lstWBS.Add(wbs3);

            DropDownViewModel vm1 = new DropDownViewModel()
            {
                Text = "Deliverable1",
                Value = "1",
                Id = 1
            };
            DropDownViewModel vm2 = new DropDownViewModel()
            {
                Text = "Deliverable2",
                Value = "2",
                Id = 2
            };
            DropDownViewModel vm3 = new DropDownViewModel()
            {
                Text = "Deliverable3",
                Value = "3",
                Id = 3
            };
            lstdropDownViewModel.Add(vm1);
            lstdropDownViewModel.Add(vm2);
            lstdropDownViewModel.Add(vm3);

            //Inputs 
            bool isExternal = false;
            string filter = "";
            int mrmUserid = 1234;
            string networkLogin = "Disney ABC";
            int wbsId = 31;
            int mopId = 1;
            #region Mock
            //Mock

            //FinanceService Service Mock
            mockFinanceService.Setup(x => x.GetDeliverableNameByMopNWBS(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(lstWBS);
            //iWBS_DeliverablesRepository Mock
            mockWBS_DeliverablesRepository.Setup(x => x.GetDeliverableNameByMopNWBS(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(lstWBS);


            var financeService = new FinanceServiceMock(
             _iWBS_DeliverablesRepository: mockWBS_DeliverablesRepository.Object);
            //Finance Controller Mock
            var DropDownController = new DropDownControllerMock(financeService: mockFinanceService.Object);

            #endregion

            //Assertions
            mockFinanceService.Verify();
            mockWBS_DeliverablesRepository.Verify();

            var serviceResult = financeService.GetDeliverableNameByMopNWBS(wbsId, mopId, isExternal);


            #region "Assertion of GetWBSByTow"
            Assert.IsNotNull(serviceResult);//Result is not Null
            Assert.AreEqual(serviceResult, lstWBS);//Asserting the expected return object with dummy data
            Assert.AreEqual(serviceResult.Count, 3);//Assert matching the return data with my input

            var controllerResult = DropDownController.GetDeliverableNameByMopNWBS(wbsId, mopId, isExternal, filter, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResult);//Result is not Null
            Assert.AreEqual(controllerResult.Count, 3);
            Assert.AreEqual(controllerResult[0].Text, "Deliverable1");

            filter = "Deliverable2";
            var controllerResultForFilter = DropDownController.GetDeliverableNameByMopNWBS(wbsId, mopId, isExternal, filter, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResultForFilter);//Result is not Null
            Assert.AreSame(controllerResultForFilter[0].Text.Trim(), "Deliverable2");
            #endregion

        }

        [TestMethod]
        public void GetDeliverableByWbs_Test()
        {
            //Set Data
            List<DropDownViewModel> lstdropDownViewModel = new List<DropDownViewModel>();
            DropDownViewModel drpDownModel = new DropDownViewModel()
            {

            };
            List<WBS_Deliverables> lstWBS = new List<WBS_Deliverables>();
            WBS_Deliverables wbs1 = new WBS_Deliverables()
            {
                WBSElementId = 17,
                FullWBSNumber = "11234.001.002",
                DeliverableId = 1,
                DeliverableName = "Deliverable1",
                SAPVendorId = 68,
                DeliverableBudgetId = 123567

            };
            WBS_Deliverables wbs2 = new WBS_Deliverables()
            {
                WBSElementId = 21,
                FullWBSNumber = "12456.001.002",
                ProductionMethodTypeId = 2,
                ProductionMethodTypeName = "Contract Request",
                DeliverableId = 2,
                DeliverableName = "Deliverable2",
                SAPVendorId = 72,
                DeliverableBudgetId = 123586
            };
            WBS_Deliverables wbs3 = new WBS_Deliverables()
            {
                TypeOfWorkId = 3456,
                WBSElementId = 31,
                FullWBSNumber = "11234.003.001",
                ProductionMethodTypeId = 3,
                ProductionMethodTypeName = "Miscellaneous",
                DeliverableId = 3,
                DeliverableName = "Deliverable3",
                SAPVendorId = 74,
                DeliverableBudgetId = 123521
            };
            lstWBS.Add(wbs1);
            lstWBS.Add(wbs2);
            lstWBS.Add(wbs3);

            DropDownViewModel vm1 = new DropDownViewModel()
            {
                Text = "Deliverable1",
                Value = "1",
                Id = 1,
                Description = "1" + "|" + "11234.001.002" + "|" + "Deliverable1" + "|" + "123567"
            };
            DropDownViewModel vm2 = new DropDownViewModel()
            {
                Text = "Deliverable2",
                Value = "2",
                Id = 2,
                Description = "2" + "|" + "12456.001.002" + "|" + "Deliverable2" + "|" + "123586"
            };
            DropDownViewModel vm3 = new DropDownViewModel()
            {
                Text = "Deliverable3",
                Value = "3",
                Id = 3,
                Description = "3" + "|" + "11234.003.001" + "|" + "Deliverable3" + "|" + "123521"
            };
            lstdropDownViewModel.Add(vm1);
            lstdropDownViewModel.Add(vm2);
            lstdropDownViewModel.Add(vm3);

            //Inputs 
            bool isExternal = false;
            string filter = "";
            int mrmUserid = 1234;
            string networkLogin = "Disney ABC";
            int wbsId = 31;
            int SAPVendorId = 74;
            #region Mock
            //Mock

            //FinanceService Service Mock
            mockFinanceService.Setup(x => x.GetDeliverableByWbs(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(lstWBS);
            //iWBS_DeliverablesRepository Mock
            mockWBS_DeliverablesRepository.Setup(x => x.GetDeliverableByWbs(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(lstWBS);


            var financeService = new FinanceServiceMock(
             _iWBS_DeliverablesRepository: mockWBS_DeliverablesRepository.Object);
            //Finance Controller Mock
            var DropDownController = new DropDownControllerMock(financeService: mockFinanceService.Object);

            #endregion

            //Assertions
            mockFinanceService.Verify();
            mockWBS_DeliverablesRepository.Verify();

            var serviceResult = financeService.GetDeliverableByWbs(wbsId, SAPVendorId, isExternal);


            #region "Assertion of GetWBSByTow"
            Assert.IsNotNull(serviceResult);//Result is not Null
            Assert.AreEqual(serviceResult, lstWBS);//Asserting the expected return object with dummy data
            Assert.AreEqual(serviceResult.Count, 3);//Assert matching the return data with my input

            var controllerResult = DropDownController.GetDeliverableByWbs(isExternal, wbsId, SAPVendorId, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResult);//Result is not Null
            Assert.AreEqual(controllerResult.Count, 3);
            Assert.AreEqual(controllerResult[0].Text, "1( Deliverable1 )");
            Assert.AreEqual(controllerResult[0].Description, "1|11234.001.002|Deliverable1|123567");

            SAPVendorId = 17;
            wbsId = 68;
            var controllerResult1 = DropDownController.GetDeliverableByWbs(isExternal, wbsId, SAPVendorId, mrmUserid, networkLogin);
            Assert.IsNotNull(controllerResult1);//Result is not Null
            Assert.AreEqual(controllerResult1[1].Text, "2( Deliverable2 )");
            Assert.AreEqual(controllerResult1[1].Description, "2|12456.001.002|Deliverable2|123586");
            #endregion
        }

    }
}
