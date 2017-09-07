using System;
using System.Collections.Generic;
using System.Linq;
using Disney.MRM.DANG.Core.Contracts;
using Disney.MRM.DANG.Service.Contracts;
using Disney.MRM.DANG.Interface;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Disney.MRM.DANG.API.Test.MockObject.Service;
using Moq;
using Disney.MRM.DANG.Model;
using Disney.MRM.DANG.Service.Implementations;
using Disney.MRM.DANG.API.Controllers;
using Disney.MRM.DANG.API.ManualMapper;
using Disney.MRM.DANG.ViewModel;
using Disney.MRM.DANG.API.Test.MockObject.Controller;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Http.Results;
using Kendo.Mvc.UI;


namespace Disney.MRM.DANG.API.Test.Controllers
{
    [TestClass]
    public class ContractRequestControllerTest
    {
        #region Constants
        const int MRM_USER_ID = 540;
        const string NETWORK_LOGIN = "swna\\TestLogin";
        #endregion
        private Moq.MockRepository mockRepository;
        private Moq.Mock<IContractRequestService> mockcontractrequestService;
        private Moq.Mock<IProjectService> mockcontractprojectservice;
        private Moq.Mock<IBudgetService> mockbudgetService;
        private Moq.Mock<IDeliverableRepository> mockdeliverablerepository;
        private Moq.Mock<IMasterVendorViewRepository> mockVendorViewRepository;
        private Moq.Mock<IDeliverable_VendorRepository> mockDeliverableVendorRepository;
        private Moq.Mock<IVendorRepository> mockVendorRepository;
        private Moq.Mock<IUserRepository> mockUserRepository;
        private Moq.Mock<IDeliverableUserTitleMrmUserRepository> mockDeliverableUserTitleMrmUserRepository;        

        [TestInitialize]
        public void ClassInit()
        {
            mockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
            mockcontractrequestService = mockRepository.Create<IContractRequestService>();
            mockcontractprojectservice = mockRepository.Create<IProjectService>();
            mockbudgetService = mockRepository.Create<IBudgetService>();
            mockdeliverablerepository = mockRepository.Create<IDeliverableRepository>();
            mockVendorViewRepository = mockRepository.Create<IMasterVendorViewRepository>();
            mockDeliverableVendorRepository = mockRepository.Create<IDeliverable_VendorRepository>();
            mockVendorRepository = mockRepository.Create<IVendorRepository>();
            mockDeliverableUserTitleMrmUserRepository = mockRepository.Create<IDeliverableUserTitleMrmUserRepository>();
            mockUserRepository = mockRepository.Create<IUserRepository>();
        }

        [TestMethod]

        public void ContractRequestsGrid_Tests()
        {
            #region Data            
            Deliverable deliverable1 = new Deliverable()
            {
                Id=1403256,
                CreatedBy=556,
                Name="Test",
                ProducingDepartmentId=1               
            };
            Department dept1 = new Department()
            {
                Id=1,
                Code= "PROG"
            };
            DeliverableGroup deliverablegroup1 = new DeliverableGroup()
            {
                Id=1,
                Code="CR"
            };
            DeliverableType deliverabletype1 = new DeliverableType()
            {
                Name="CR",
                Id=1
            };
            DeliverableStatus status = new DeliverableStatus()
            {
                Name = "Draft",
                Id = 1
            };
            DeliverableBudget delbudget = new DeliverableBudget()
            {
                CreatedBy=556,
                EstimateCompleteAmount=100,
                ActualAmount=500,
                Id=285,
                MasterVendorId = 61
            };
            MasterVendor msvendor = new MasterVendor()
            {
                Id=61,
                CreatedBy=556
            };

            string SAPVendorName = "SAP Vendor Name";
            Vendor vendors = new Vendor()
            {
                Id=61,
                Name = SAPVendorName
            };
            msvendor.Vendor = vendors;
            delbudget.MasterVendor = msvendor;
            InvoiceLine invoice1 = new InvoiceLine()
            {
                Id=1,
                Amount=100
            };
            List<InvoiceLine> invoicelist = new List<InvoiceLine>();
            invoicelist.Add(invoice1);
            delbudget.InvoiceLine = invoicelist;
            ActualsReconciliation arc = new ActualsReconciliation()
            {
                ActualAmount = 100,
                CreatedBy = 556,
                DeliverableBudgetId = 285
            };
            List<ActualsReconciliation> arclist = new List<ActualsReconciliation>();
            arclist.Add(arc);
            delbudget.ActualsReconciliation = arclist;
            List<DeliverableBudget> delbudgetlist = new List<DeliverableBudget>();
            delbudgetlist.Add(delbudget);
            DeliverableDate deldate = new DeliverableDate()
            {
                Id=1,
                DeliverableId=1403256
            };
            DeliverableDateType deldatetype = new DeliverableDateType()
            {
                Id=1,
                Code= "DEL"
            };
            deldate.DeliverableDateType = deldatetype;
            List<DeliverableDate> deldatelist = new List<DeliverableDate>();
            deldatelist.Add(deldate);
            ContractRequest ctrreq = new ContractRequest()
            {
                ContractRequestProject="CR",
                CreatedBy=556
            };
            deliverable1.DeliverableGroup = deliverablegroup1;
            deliverable1.Department = dept1;
            deliverable1.DeliverableType = deliverabletype1;
            deliverable1.DeliverableStatus = status;
            deliverable1.DeliverableDate = deldatelist;
            deliverable1.ContractRequest = ctrreq;
            deliverable1.DeliverableBudget = delbudgetlist;
            List<Deliverable> deliverablelist = new List<Deliverable>();
            deliverablelist.Add(deliverable1);
            List<CRDeliverableViewModel> viewModel = new List<CRDeliverableViewModel>();
            #endregion

            #region Mock
            mockdeliverablerepository.Setup(x => x.GetDeliverablesByDeliverableGroup(It.IsAny<int>())).Returns(deliverablelist);
            mockbudgetService.Setup(x => x.GetDeliverablesByDeliverableGroup(It.IsAny<int>())).Returns(deliverablelist);

            #endregion
            var budgetservice = new BudgetServiceMock(_deliverableRepository:mockdeliverablerepository.Object);
            var contractservice = new ContractRequestControllerMock(budgetservice: mockbudgetService.Object);
            
            var results = budgetservice.GetDeliverablesByDeliverableGroup(MRM_USER_ID);
            viewModel= ContractRequestMapper.CRDeliverableMapper(deliverablelist);
           

            #region Assets
            Assert.IsNotNull(results);
            Assert.IsNotNull(viewModel);
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual(viewModel[0].Vendor, SAPVendorName);
            #endregion

        }

        [TestMethod]

        public void ContractRequestsGrid_NoVendorTests()
        {
            #region Data
            Deliverable deliverable1 = new Deliverable()
            {
                Id = 1403256,
                CreatedBy = 556,
                Name = "Test",
                ProducingDepartmentId = 1
            };
            Department dept1 = new Department()
            {
                Id = 1,
                Code = "PROG"
            };
            DeliverableGroup deliverablegroup1 = new DeliverableGroup()
            {
                Id = 1,
                Code = "CR"
            };
            DeliverableType deliverabletype1 = new DeliverableType()
            {
                Name = "CR",
                Id = 1
            };
            DeliverableStatus status = new DeliverableStatus()
            {
                Name = "Draft",
                Id = 1
            };
            DeliverableBudget delbudgetForOtherName = new DeliverableBudget()
            {
                CreatedBy = 556,
                EstimateCompleteAmount = 100,
                ActualAmount = 500,
                Id = 285,
                MasterVendorId = 61
            };

            string otherName = "Master Vendor Other Name";
            MasterVendor msvendor = new MasterVendor()
            {
                Id = 61,
                CreatedBy = 556,
                OtherName = otherName

            };

            delbudgetForOtherName.MasterVendor = msvendor;
            InvoiceLine invoice1 = new InvoiceLine()
            {
                Id = 1,
                Amount = 100
            };
            List<InvoiceLine> invoicelist = new List<InvoiceLine>();
            invoicelist.Add(invoice1);
            delbudgetForOtherName.InvoiceLine = invoicelist;
            ActualsReconciliation arc = new ActualsReconciliation()
            {
                ActualAmount = 100,
                CreatedBy = 556,
                DeliverableBudgetId = 285
            };
            List<ActualsReconciliation> arclist = new List<ActualsReconciliation>();
            arclist.Add(arc);
            delbudgetForOtherName.ActualsReconciliation = arclist;
            List<DeliverableBudget> delbudgetlist = new List<DeliverableBudget>();
            delbudgetlist.Add(delbudgetForOtherName);
            DeliverableDate deldate = new DeliverableDate()
            {
                Id = 1,
                DeliverableId = 1403256
            };
            DeliverableDateType deldatetype = new DeliverableDateType()
            {
                Id = 1,
                Code = "DEL"
            };
            deldate.DeliverableDateType = deldatetype;
            List<DeliverableDate> deldatelist = new List<DeliverableDate>();
            deldatelist.Add(deldate);
            ContractRequest ctrreq = new ContractRequest()
            {
                ContractRequestProject = "CR",
                CreatedBy = 556
            };
            deliverable1.DeliverableGroup = deliverablegroup1;
            deliverable1.Department = dept1;
            deliverable1.DeliverableType = deliverabletype1;
            deliverable1.DeliverableStatus = status;
            deliverable1.DeliverableDate = deldatelist;
            deliverable1.ContractRequest = ctrreq;
            deliverable1.DeliverableBudget = delbudgetlist;
            List<Deliverable> deliverablelist = new List<Deliverable>();
            deliverablelist.Add(deliverable1);
            List<CRDeliverableViewModel> viewModel = new List<CRDeliverableViewModel>();
            #endregion

            #region Mock
            mockdeliverablerepository.Setup(x => x.GetDeliverablesByDeliverableGroup(It.IsAny<int>())).Returns(deliverablelist);
            mockbudgetService.Setup(x => x.GetDeliverablesByDeliverableGroup(It.IsAny<int>())).Returns(deliverablelist);

            #endregion
            var budgetservice = new BudgetServiceMock(_deliverableRepository: mockdeliverablerepository.Object);
            var contractservice = new ContractRequestControllerMock(budgetservice: mockbudgetService.Object);

            var results = budgetservice.GetDeliverablesByDeliverableGroup(MRM_USER_ID);
            viewModel = ContractRequestMapper.CRDeliverableMapper(deliverablelist);


            #region Assets
            Assert.IsNotNull(results);
            Assert.IsNotNull(viewModel);
            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual(viewModel[0].Vendor, otherName);
            #endregion

        }

        [TestMethod]

        public void ContractRequestDeliverableGrid_Tests()
        {
            #region Data
            int Crid = 1;
            List<DeliverableViewModel> viewModel = new List<DeliverableViewModel>();
            DeliverableBudgetSummary budgetsumm = new DeliverableBudgetSummary()
            {
                BudgetTypeId=1,
                BudgetTypeName="ON Air",
                CreatedBy=540,
                ProducingDepartmentName="CR",
                DEL_Date = DateTime.UtcNow,
                DeliverableStatusName="Draft",
                DeliverableId=1435624,
                DeliverableTypeName="TestCR",
                MarketingGroupChannelId=1,
                MarketingGroupChannelName="Disney"
            };
            List<DeliverableBudgetSummary> budgetsummlist = new List<DeliverableBudgetSummary>();
            budgetsummlist.Add(budgetsumm);
            #endregion

            #region Mock
            mockdeliverablerepository.Setup(x => x.GetAllDeliverablesForCR(It.IsAny<int>(), It.IsAny<int>())).Returns(budgetsummlist.AsQueryable());
            mockbudgetService.Setup(x => x.GetAllDeliverablesForCR(It.IsAny<int>(), It.IsAny<int>())).Returns(budgetsummlist.AsQueryable());
            #endregion
            var budgetservice = new BudgetServiceMock(_deliverableRepository: mockdeliverablerepository.Object);
            var contractservice = new ContractRequestControllerMock(budgetservice: mockbudgetService.Object);

            viewModel = ContractRequestMapper.DeliverableViewModelMapper(budgetsummlist);
            var results= budgetservice.GetAllDeliverablesForCR(Crid,MRM_USER_ID);

            #region Assets
            Assert.IsFalse(results==null);
            Assert.IsNotNull(viewModel);
            Assert.IsTrue(results.ElementAt(0).MarketingGroupChannelId == 1);
            Assert.IsTrue(viewModel.Count > 0);
            #endregion

        }

        #region MRM-501
        [TestMethod]
        public void GetCRVendors_Tests()
        {
            #region Variables
            var deliverableId = 101;
            #endregion
            #region Data SetUp

            #region MasterVendor
            var masterVendorsWithSAPNo = new List<MasterVendor1>()
            {
                new MasterVendor1
                {
                    Id = 1,
                    SAPVendorId = 1
                },
                new MasterVendor1
                {
                    Id = 2,
                    SAPVendorId = 2
                },
                new MasterVendor1
                {
                    Id = 3,
                    SAPVendorId = 3
                },
                new MasterVendor1
                {
                    Id = 4,
                    SAPVendorId = 4
                },
                new MasterVendor1
                {
                    Id = 5,
                    SAPVendorId = 5
                },
            };

            var masterVendorsWithoutSAPNo = new List<MasterVendor1>()
            {
                new MasterVendor1
                {
                    Id = 6,
                    SAPVendorId = null,
                    OtherName = "No SAP Number",
                    PhoneNumber = "2154519099",
                    Address = "1 North Front Street"
                }
            };
            #endregion

            #region Deliverable MasterVendor
            var lstDeliverableMasterVendor = new List<Deliverable_MasterVendor>()
            {
                new Deliverable_MasterVendor
                {
                    Id = 1,
                    DeliverableId = 101,
                    AwardedContractFlag = true,
                    RespondedFlag = true,
                    MasterVendorId = 2
                },
                new Deliverable_MasterVendor
                {
                    Id = 2,
                    DeliverableId = 101,
                    AwardedContractFlag = true,
                    RespondedFlag = true,
                    MasterVendorId = 3
                },
                new Deliverable_MasterVendor
                {
                    Id = 3,
                    DeliverableId = 101,
                    AwardedContractFlag = true,
                    RespondedFlag = true,
                    MasterVendorId = 5
                }
            };
            #endregion

            #region Vendor
            var lstVendor = new List<Vendor>()
            {
                new Vendor
                {
                    Name = "Vendor 2",                    
                    City = "Pasadena",                    
                    FederalId = "VEN2",
                    Id = 2,
                    PhoneNumber = "9876543211",
                    SAPVendorNumber= "SAPVen2"
                },
                new Vendor
                {
                    Name = "Vendor 3",
                    City = "Pasadena",
                    FederalId = "VEN3",
                    Id = 3,
                    PhoneNumber = "9876543212",
                    SAPVendorNumber= "SAPVen3"
                },
                new Vendor
                {
                    Name = "Vendor 5",
                    City = "Pasadena",
                    FederalId = "VEN5",
                    Id = 5,
                    PhoneNumber = "9876543215",
                    SAPVendorNumber= "SAPVen5"
                }
            };
            #endregion

            #region Deliverable_UserTitle_MRMUser
            List<Deliverable_UserTitle_MRMUser> lstDeliverableUsertitleMrmuser = new List<Deliverable_UserTitle_MRMUser>();
            lstDeliverableUsertitleMrmuser.Add(new Deliverable_UserTitle_MRMUser { Id = 1, IsPrimaryFlag = true, MRMUserId = 1, NotifyFlag = true });
            lstDeliverableUsertitleMrmuser.Add(new Deliverable_UserTitle_MRMUser { Id = 2, IsPrimaryFlag = true, MRMUserId = 2, NotifyFlag = true });
            lstDeliverableUsertitleMrmuser.Add(new Deliverable_UserTitle_MRMUser { Id = 3, IsPrimaryFlag = true, MRMUserId = 3, NotifyFlag = true });
            #endregion
            #endregion

            #region Mock
            mockVendorViewRepository.SetupSequence(x => x.GetMany(It.IsAny<Expression<Func<MasterVendor1, bool>>>()))
                                    .Returns(masterVendorsWithSAPNo.AsEnumerable())
                                    .Returns(masterVendorsWithoutSAPNo.AsEnumerable());

            mockVendorViewRepository.Setup(x => x.GetSingle(It.IsAny<Expression<Func<MasterVendor1, bool>>>())).Returns(new MasterVendor1
            {
                Id = 1,
                SAPVendorId = 1
            });

            mockDeliverableVendorRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<Deliverable_MasterVendor, bool>>>())).Returns(lstDeliverableMasterVendor);

            mockDeliverableUserTitleMrmUserRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<Deliverable_UserTitle_MRMUser, bool>>>())).Returns(lstDeliverableUsertitleMrmuser);

            mockVendorRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<Vendor, bool>>>())).Returns(lstVendor);
            #endregion

            #region Services
            var deliverableService2 = new DeliverableServiceV2Mock(
                masterVendorViewRepository: mockVendorViewRepository.Object,
                deliverable_VendorRepository: mockDeliverableVendorRepository.Object,
                deliverableUserTitleMrmUserRepository: mockDeliverableUserTitleMrmUserRepository.Object,
                vendorRepository: mockVendorRepository.Object);
            #endregion

            #region Asserts
            var results = deliverableService2.GetCRVendors(new List<int> { 2, 3, 5 }, deliverableId).ToList();
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 4);
            Assert.IsTrue(results[results.Count-1].SapVendorNumber == null);
            #endregion

        }
        #endregion
        #region MRM-1037
        [TestMethod]
        public void GetContactsUsers_Tests()
        {
            #region Variables            
            var contactIds = new List<int> { 1,2,3};
            var masterVendorId = 1;
            #endregion
            
            #region Data SetUp            
            
            #region User
            var lstUser = new List<MRMUser>()
            {
                new MRMUser
                {
                    Id = 1,
                    FirstName = "User1",
                    LastName = "Last1",
                    MasterVendorId = 1,
                    EmailAddress = "User1@email.com"
                },
                new MRMUser
                {
                    Id = 2,
                    FirstName = "User2",
                    LastName = "Last2",
                    MasterVendorId = 1,
                    EmailAddress = "User2@email.com"
                },
                new MRMUser
                {
                    Id = 3,
                    FirstName = "User3",
                    LastName = "Last3",
                    MasterVendorId = 1,
                    EmailAddress = "User3@email.com"
                }
            };
            #endregion
            
            #endregion

            #region Mock
            mockUserRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<MRMUser, bool>>>())).Returns(lstUser.AsEnumerable());            
            
            #endregion

            #region Services
            var userService  = new UserServiceMock(
                _userRepository : mockUserRepository.Object);
            #endregion

            #region Asserts
            var results = userService.GetContactsUsers(contactIds, masterVendorId).ToList();
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 3);
            #endregion

        }

        [TestMethod]
        public void GetVendorContacts_Tests()
        {
            #region Variables            
            var contactIds = new List<int> { 1, 2, 3 };            
            #endregion

            #region Data SetUp            

            #region User
            var lstUser = new List<MRMUser>()
            {
                new MRMUser
                {
                    Id = 1,
                    FirstName = "User1",
                    LastName = "Last1",                
                    EmailAddress = "User1@email.com",
                    PhoneNumber = "1234567",
                    UserName = "swna\\user1"
                },
                new MRMUser
                {
                    Id = 2,
                    FirstName = "User2",
                    LastName = "Last2",                    
                    EmailAddress = "User2@email.com",
                    PhoneNumber = "7654321",
                    UserName = "swna\\user2"
                },
                new MRMUser
                {
                    Id = 3,
                    FirstName = "User3",
                    LastName = "Last3",
                    EmailAddress = "User3@email.com",
                    PhoneNumber = "9876543",
                    UserName = "swna\\user3"
                },
            };
            #endregion

            #endregion

            #region Mock
            mockUserRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<MRMUser, bool>>>())).Returns(lstUser.AsEnumerable());

            #endregion

            #region Services
            var userService = new UserServiceMock(
                _userRepository: mockUserRepository.Object);
            #endregion

            #region Asserts
            var results = userService.GetVendorContacts(contactIds).ToList();
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 3);
            #endregion

        }

        [TestMethod]
        public void GetContactUser_Tests()
        {
            #region Variables            
            var contactId = 1;          
            #endregion

            #region Data SetUp            

            #region User

            var user = new MRMUser()
            {
                Id = 1,
                FirstName = "User1",
                LastName = "Last1",
                EmailAddress = "User1@email.com",
                PhoneNumber = "1234567",
                UserName = "swna\\user1"
            };

            #endregion

            #endregion

            #region Mock
            mockUserRepository.Setup(x => x.GetSingle(It.IsAny<Expression<Func<MRMUser, bool>>>())).Returns(user);
            mockUserRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);

            #endregion

            #region Services
            var userService = new UserServiceMock(
                _userRepository: mockUserRepository.Object);            

            #endregion

            #region Asserts
            var results = userService.GetById(contactId);
            
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Id == 1);

            #endregion

        }
        #endregion

    }
}
