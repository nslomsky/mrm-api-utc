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
using Disney.MRM.DANG.ViewModel.Admin;
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
    public class AdminControllerTest
    {
        #region private variables
        private Moq.MockRepository mockRepository;        
        private Moq.Mock<IAdminService> mockadminService;
        private Moq.Mock<IMediaOutletCategoryRepository> mockmediaoutletcategRepository;
        private Moq.Mock<IUnitOfWork> mockunitofwork;
        private Moq.Mock<IGLAccountMediaOutletCategoryRepository> mockGLAccountMediaOutletCategoryRepository;
        private Moq.Mock<ITypeOfWorkCategoryRepository> mockTypeOfWorkCategoryRepository;
        private Moq.Mock<IMediaOutletRepository> mockImediaoutletRepository;
        private Moq.Mock<IVendorRepository> vendorrepositorymock;
        private Moq.Mock<IDeliverableGroupRepository> deliverablegrouprepository;
        private Moq.Mock<IUserTitleRepository> mockusertitlerepository;

        private Moq.Mock<IMasterVendorRepository> mastervendorrepositorymock;
        private Moq.Mock<IWBSElementRepository> wbsElementRepositorymock;
        private Moq.Mock<ITypeOfWorkRepository> towRepositorymock;

        private Moq.Mock<IChannelDeliverableGroupUserTitleRepository> mockChdelgpusertitleRepository;

        #endregion
        [TestInitialize]
        public void ClassInit()
        {
            mockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };            
            mockadminService = mockRepository.Create<IAdminService>();
            mockmediaoutletcategRepository = mockRepository.Create<IMediaOutletCategoryRepository>();
            mockunitofwork = mockRepository.Create<IUnitOfWork>();
            mockGLAccountMediaOutletCategoryRepository = mockRepository.Create<IGLAccountMediaOutletCategoryRepository>();
            mockTypeOfWorkCategoryRepository = mockRepository.Create<ITypeOfWorkCategoryRepository>();
            mockImediaoutletRepository= mockRepository.Create<IMediaOutletRepository>();
            vendorrepositorymock = mockRepository.Create<IVendorRepository>();
            deliverablegrouprepository= mockRepository.Create<IDeliverableGroupRepository>();
            mockusertitlerepository= mockRepository.Create<IUserTitleRepository>();
            mastervendorrepositorymock= mockRepository.Create<IMasterVendorRepository>();

            wbsElementRepositorymock = mockRepository.Create < IWBSElementRepository>();
            towRepositorymock = mockRepository.Create<ITypeOfWorkRepository>();

            mockChdelgpusertitleRepository = mockRepository.Create<IChannelDeliverableGroupUserTitleRepository>();

        }
        [TestMethod]
        public void GetMediaOutletCategory_Test()
        {
            #region Datasetup
            string Exception = null;
            List<MediaOutletCategoryVM> result = new List<MediaOutletCategoryVM>();
            MediaOutletCategory mediaoutletcateg = new MediaOutletCategory()
            {
                Code = "SCL",
                Id = 16,
                Name = "Social"
            };
            List<MediaOutletCategory> medialist = new List<MediaOutletCategory>();
            medialist.Add(mediaoutletcateg);
            Mapper.CreateMap<List<MediaOutletCategory>, List<MediaOutletCategoryVM>>();
            #endregion
            #region  Mocking
            mockadminService.Setup(a => a.GetMediaOutletCategory(It.IsAny<int>())).Returns(medialist);
            mockmediaoutletcategRepository.Setup(a => a.GetAll()).Returns(medialist.AsQueryable());

            #endregion
            //ACT
            var AdminService = new AdminServiceMock(MediaOutletCategoryRepository: mockmediaoutletcategRepository.Object);
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
            var serviceresult = AdminService.GetMediaOutletCategory(540);
            result = AdminController.GetMediaOutletCategory(540, "//SWNA/TestLogin");
            try
            {
                var AdminController1 = new AdminControllerMock();
                var result1 = AdminController1.GetMediaOutletCategory(1, null);
            }
            catch (Exception ex)
            {
                Exception = ex.Message;
            }
            //Assert

            Assert.IsNotNull(serviceresult);
            Assert.IsNotNull(Exception);
            Assert.AreEqual("Processing of the HTTP request resulted in an exception. Please see the HTTP response returned by the 'Response' property of this exception for details.", Exception);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void AddMediaOutletCategory_Test()
        {
            #region Datasetup
            List<MediaOutletCategoryVM> result = new List<MediaOutletCategoryVM>();
            GLAccount_MediaOutletCategory glaccmedia = new GLAccount_MediaOutletCategory()
            {
                Id=12,
                MediaOutletCategoryId=17,
                GLAccountId=32,
                CreatedBy=1
            };
            List<GLAccount_MediaOutletCategory> glaccmedialist = new List<GLAccount_MediaOutletCategory>();
            glaccmedialist.Add(glaccmedia);
            MediaOutletCategoryVM mediacategvm = new MediaOutletCategoryVM()
            {
                Id=17,
                Code="DM",
                Name="DirectMail"                
            };
            List<MediaOutletCategoryVM> mediacategvmlist = new List<MediaOutletCategoryVM>();
            mediacategvmlist.Add(mediacategvm);
            MediaOutletCategory mediaoutletcateg = new MediaOutletCategory()
            {
                Code = "SCL",
                Id = 16,
                Name = "Social",
                GLAccount_MediaOutletCategory = glaccmedialist
            };
            List<MediaOutletCategory> medialist = new List<MediaOutletCategory>();
            medialist.Add(mediaoutletcateg);
            Mapper.CreateMap<List<MediaOutletCategoryVM>, List<MediaOutletCategory>>();
            Mapper.CreateMap<List<MediaOutletCategory>, List<MediaOutletCategoryVM>>();
            #endregion
            #region  Mocking
            mockadminService.Setup(a => a.AddMediaOutletCategory(It.IsAny<List<MediaOutletCategory>>(), It.IsAny<int>())).Returns(medialist);
            mockmediaoutletcategRepository.Setup(a => a.AddMediaOutletCategory(It.IsAny<MediaOutletCategory>(), It.IsAny<List<GLAccount_MediaOutletCategory>>())).Returns(mediaoutletcateg);
            mockunitofwork.Setup(x => x.Commit());
            #endregion
            //ACT
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
            result = AdminController.AddMediaOutletCategory(mediacategvmlist, 540, "//SWNA/TestLogin");
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void UpdateMediaOutletCategory_Test()
        {
            #region Datasetup
            
            GLAccount_MediaOutletCategory glaccmedia = new GLAccount_MediaOutletCategory()
            {
                Id = 12,
                MediaOutletCategoryId = 17,
                GLAccountId = 32,
                CreatedBy = 1
            };
            List<GLAccount_MediaOutletCategory> glaccmedialist = new List<GLAccount_MediaOutletCategory>();
            glaccmedialist.Add(glaccmedia);
            MediaOutletCategoryVM mediacategvm = new MediaOutletCategoryVM()
            {
                Id = 17,
                Code = "DM",
                Name = "DirectMail"
            };
            List<MediaOutletCategoryVM> mediacategvmlist = new List<MediaOutletCategoryVM>();
            mediacategvmlist.Add(mediacategvm);
            MediaOutletCategory mediaoutletcateg = new MediaOutletCategory()
            {
                Code = "SCL",
                Id = 16,
                Name = "Social",
                GLAccount_MediaOutletCategory = glaccmedialist
            };
            List<MediaOutletCategory> medialist = new List<MediaOutletCategory>();
            medialist.Add(mediaoutletcateg);
            Mapper.CreateMap<List<MediaOutletCategoryVM>, List<MediaOutletCategory>>();
            #endregion
            #region  Mocking
            mockadminService.Setup(a => a.UpdateMediaOutletCategory(It.IsAny<List<MediaOutletCategory>>(), It.IsAny<int>())).Returns(true);
            mockGLAccountMediaOutletCategoryRepository.Setup(a => a.GetMany(It.IsAny<Expression<Func<GLAccount_MediaOutletCategory, bool>>>())).Returns(glaccmedialist);
            mockGLAccountMediaOutletCategoryRepository.Setup(a => a.Delete(It.IsAny<Expression<Func<GLAccount_MediaOutletCategory, bool>>>()));
            mockmediaoutletcategRepository.Setup(a => a.UpdateMediaOutletCategory(It.IsAny<MediaOutletCategory>(), It.IsAny<List<GLAccount_MediaOutletCategory>>())).Returns(true);
            mockunitofwork.Setup(x => x.Commit());
            #endregion
            //ACT
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
           var result = AdminController.UpdateMediaOutletCategory(mediacategvmlist, 540, "//SWNA/TestLogin");
            //Assert
            Assert.IsTrue(result==true);
        }

        [TestMethod]
        public void GetTypeOfWorkCategoryDetails_Test()
        {
            #region Datasetup
            TypeOfWorkCategoryViewModel result = new TypeOfWorkCategoryViewModel();
            List<TypeOfWorkCategory> serviceResult = new List<TypeOfWorkCategory>();
            TypeOfWorkCategory objTypeOfWorkCategory = new TypeOfWorkCategory()
            {
                Code = "M",
                Name = "Acquired"
            };
            TypeOfWorkCategoryViewModel objTypeOfWorkCategoryVM = new TypeOfWorkCategoryViewModel()
            {
                Code = "MOV-A",
                Name = "Acquired"
            };
            List<TypeOfWorkCategory> lstTypeOfWorkCategory = new List<TypeOfWorkCategory>();

            lstTypeOfWorkCategory.Add(objTypeOfWorkCategory);
            #endregion
            #region  Mocking
            mockadminService.Setup(a => a.GetTypeOfWorkCategoryDetails(It.IsAny<TypeOfWorkCategoryViewModel>())).Returns(lstTypeOfWorkCategory.AsQueryable());
            mockTypeOfWorkCategoryRepository.Setup(a => a.GetTypeOfWorkCategory(It.IsAny<TypeOfWorkCategory>())).Returns(lstTypeOfWorkCategory.AsQueryable());

            #endregion
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
            var AdminService = new AdminServiceMock(TOWCategoryRepository: mockTypeOfWorkCategoryRepository.Object);

            result = AdminController.GetTypeOfWorkCategoryDetails(objTypeOfWorkCategoryVM, 570, "//SWNA/TestLogin");
            serviceResult = AdminService.GetTypeOfWorkCategoryDetails(objTypeOfWorkCategoryVM).ToList();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(serviceResult);
            Assert.IsTrue(result.TypeOfWork.Count > 0);
            Assert.IsTrue(serviceResult.Count>0);
        }

        [TestMethod]
        public void CreateTypeOfWorkCategoryDetails_Test()
        {
            #region Datasetup
            TypeOfWorkCategoryViewModel result = new TypeOfWorkCategoryViewModel();
            List<TypeOfWorkCategory> serviceResult = new List<TypeOfWorkCategory>();
            int mrmUserId = 570;
            TypeOfWorkCategory objTypeOfWorkCategory = new TypeOfWorkCategory()
            {
                Code = "MOV-A",
                Name = "Acquired"
            };
            TypeOfWorkCategoryViewModel objTypeOfWorkCategoryVM = new TypeOfWorkCategoryViewModel()
            {
                Code = "MOV-A",
                Name = "Acquired"
            };
            #endregion
            #region  Mocking
            mockadminService.Setup(a => a.CreateTypeOfWorkCategory(It.IsAny<TypeOfWorkCategoryViewModel>(),mrmUserId)).Returns(objTypeOfWorkCategory.Id);
            mockTypeOfWorkCategoryRepository.Setup(a => a.CreateTypeOfWorkCategory(It.IsAny<TypeOfWorkCategory>(), mrmUserId)).Returns(objTypeOfWorkCategoryVM.Id);

            #endregion
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
            var AdminService = new AdminServiceMock(TOWCategoryRepository: mockTypeOfWorkCategoryRepository.Object);

            result = AdminController.CreateTypeOfWorkCategoryDetails(objTypeOfWorkCategoryVM,mrmUserId);
            objTypeOfWorkCategory.Id = AdminService.CreateTypeOfWorkCategory(objTypeOfWorkCategoryVM, mrmUserId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Selected == true);
            Assert.IsNotNull(objTypeOfWorkCategory.Id);
        }

        [TestMethod]
        public void GetMediaOutlet_Test()
        {
            #region DataSetup
           
            MediaOutletCategory mediacateg = new MediaOutletCategory()
            {
                Id = 21,
                Name = "National TV - Syndication",
                Code="NTVB"                

            };
            List<MediaOutletCategory> mediacateglist = new List<MediaOutletCategory>();
            mediacateglist.Add(mediacateg);
            MediaOutlet media = new MediaOutlet()
            {
                Id = 65,
                Name = "Katie",
                Code = "KATIE",
                Description = "",
                IsActiveFlag = true,
                MediaOutletCategoryId = 21,
                MediaOutletCategory = mediacateg
            };
            List<MediaOutlet> medialist = new List<MediaOutlet>();
            medialist.Add(media);
            DropDownViewModel dropdnmod = new DropDownViewModel()
            {
                Id = 21,
                Text = "National TV - Syndication",
                Code= "NTVB",
                Value= "21"
            };
            List<DropDownViewModel> dopdnmodlist = new List<DropDownViewModel>();
            dopdnmodlist.Add(dropdnmod);
            List<int> ids = new List<int>();
            ids.Add(1);
            MediaOutletVM mediavm = new MediaOutletVM()
            {

                Id = 65,
                Name = "Katie",
                Code = "KATIE",
                Description = "",
                IsActiveFlag = true,
                MediaOutletCategory = dopdnmodlist,
                ExistingIds= ids,
                CreatedBy=567,
                LastUpdatedBy=567

            };
            List<MediaOutletVM> mediavmlist = new List<MediaOutletVM>();
            mediavmlist.Add(mediavm);
            
            #endregion
            #region Mocking
            mockadminService.Setup(a => a.GetMediaOutlet(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).Returns(medialist);
            mockImediaoutletRepository.Setup(a => a.GetMany(It.IsAny<Expression<Func<MediaOutlet, bool>>>())).Returns(medialist);
            #endregion
            //Act
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
            var AdminService = new AdminServiceMock(TOWCategoryRepository: mockTypeOfWorkCategoryRepository.Object,mediaOutletRepository:mockImediaoutletRepository.Object);
            var result = AdminController.GetMediaOutlet(21, media.Name, 496, "swna\\NANDR006");
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void AddMediaOutlet_Test()
        {
            #region DataSetup
            MediaOutletCategory mediacateg = new MediaOutletCategory()
            {
                Id = 21,
                Name = "National TV - Syndication",
                Code = "NTVB"

            };
            List<MediaOutletCategory> mediacateglist = new List<MediaOutletCategory>();
            mediacateglist.Add(mediacateg);
            MediaOutlet media = new MediaOutlet()
            {
                Id=1,
                Name = "Test",
                Code = "TEST",
                IsActiveFlag = true,
                MediaOutletCategoryId = 21,
                MediaOutletCategory = mediacateg
            };
            List<MediaOutlet> medialist = new List<MediaOutlet>();
            medialist.Add(media);            
            DropDownViewModel dropdnmod = new DropDownViewModel()
            {
                Id = 21,
                Text = "National TV - Syndication"
            };
            List<DropDownViewModel> dopdnmodlist = new List<DropDownViewModel>();
            dopdnmodlist.Add(dropdnmod);
            MediaOutletVM mediavm = new MediaOutletVM()
            {

                Name = "Test",
                Code = "TEST",
                IsActiveFlag = true,
                MediaOutletCategory= dopdnmodlist

            };
            List<MediaOutletVM> mediavmlist = new List<MediaOutletVM>();
            mediavmlist.Add(mediavm);
            Mapper.CreateMap<List<MediaOutlet>, List<MediaOutletVM>>();
            Mapper.CreateMap<DropDownViewModel, MediaOutlet>();
            Mapper.CreateMap<List<DropDownViewModel>, List<MediaOutlet>>();
            #endregion
            #region Mocking
            mockadminService.Setup(a => a.AddMediaOutlet(It.IsAny<List<MediaOutlet>>(), It.IsAny<int>())).Returns(medialist);
            mockImediaoutletRepository.Setup(a => a.Add(It.IsAny<MediaOutlet>())).Returns(media);
            #endregion
            //Act
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
            var AdminService = new AdminServiceMock(TOWCategoryRepository: mockTypeOfWorkCategoryRepository.Object, mediaOutletRepository: mockImediaoutletRepository.Object);
            var result = AdminController.AddMediaOutlet(mediavmlist, 570, "//SWNA/TestLogin");
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void UpdateMediaOutlet_Test()
        {
            #region DataSetup
            MediaOutlet media = new MediaOutlet()
            {

                Name = "Test",
                Code = "TEST",
                IsActiveFlag = true,
                MediaOutletCategoryId = 21
            };
            List<MediaOutlet> medialist = new List<MediaOutlet>();
            medialist.Add(media);
            MediaOutletVM mediavm = new MediaOutletVM()
            {

                Name = "Test",
                Code = "TEST",
                IsActiveFlag = true,

            };
            List<MediaOutletVM> mediavmlist = new List<MediaOutletVM>();
            mediavmlist.Add(mediavm);
            #endregion
            #region Mocking
            mockadminService.Setup(a => a.UpdateMediaOutlet(It.IsAny<MediaOutletVM>(), It.IsAny<int>())).Returns(true);
            mockImediaoutletRepository.Setup(a => a.GetById(It.IsAny<long>())).Returns(media);
            mockImediaoutletRepository.Setup(a => a.Update(It.IsAny<MediaOutlet>()));
            mockImediaoutletRepository.Setup(a => a.DeleteMediaOutlet(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            mockImediaoutletRepository.Setup(a => a.GetMany(It.IsAny<Expression<Func<MediaOutlet, bool>>>())).Returns(medialist);
            mockImediaoutletRepository.Setup(a => a.Add(It.IsAny<MediaOutlet>())).Returns(media);
            mockunitofwork.Setup(x => x.Commit());
            #endregion
            //Act
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
            var AdminService = new AdminServiceMock(TOWCategoryRepository: mockTypeOfWorkCategoryRepository.Object, mediaOutletRepository: mockImediaoutletRepository.Object);
            var result = AdminController.UpdateMediaOutlet(mediavmlist, 570, "//SWNA/TestLogin");
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void isDulicateFieldinMediaOutlet_Test()
        {
            #region DataSetup
            MediaOutlet media = new MediaOutlet()
            {

                Name = "Test",
                Code = "TEST",
                IsActiveFlag = true,
                MediaOutletCategoryId = 21
            };
            List<MediaOutlet> medialist = new List<MediaOutlet>();
            medialist.Add(media);
            #endregion
            #region Mocking
            mockadminService.Setup(a => a.isDulicateFieldinMediaOutlet(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            mockImediaoutletRepository.Setup(a => a.GetMany(It.IsAny<Expression<Func<MediaOutlet, bool>>>())).Returns(medialist);
            #endregion
            //Act
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
            var AdminService = new AdminServiceMock(TOWCategoryRepository: mockTypeOfWorkCategoryRepository.Object, mediaOutletRepository: mockImediaoutletRepository.Object);
            var result = AdminController.isDulicateFieldinMediaOutlet("Code", "KATIE", 570, "//SWNA/TestLogin");
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetVendorAdminDetails_Test()
        {
            #region Datasetup
            List<VendorAdminViewModel> returnResult = new List<VendorAdminViewModel>();
            Channel ch = new Channel()
            {
                Id = 1,
                Name = "DisneyChannel"
            };
            List<Channel> chlist = new List<Channel>();
            chlist.Add(ch);
            Channel_DeliverableGroup_MasterVendor cdmsvend = new Channel_DeliverableGroup_MasterVendor()
            {
                ChannelId=1,
                Channel=ch
            };
            
            List<Channel_DeliverableGroup_MasterVendor> cdmsvendlist = new List<Channel_DeliverableGroup_MasterVendor>();
            cdmsvendlist.Add(cdmsvend);
            MasterVendor msvend = new MasterVendor()
            {
                Id=1,
                Channel_DeliverableGroup_MasterVendor= cdmsvendlist,
                SAPVendorId=200
            };
            
            VendorAdminModel vm = new VendorAdminModel()
            {
                Id = 1,
                Name = "233 W 49TH STREET REALTY",
                SAPVendorNumber = "1000360116",
                FederalId = "",
                Address = "",
                PhoneNumber = "",
                BudgetGroups = chlist
            };
            List<VendorAdminModel> vmlist = new List<VendorAdminModel>();
            vmlist.Add(vm);
            Mapper.CreateMap<List<VendorAdminModel>, List<VendorAdminViewModel>>();
            #endregion
            #region Mocking
            mockadminService.Setup(a => a.GetVendorAdminDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(vmlist);
            vendorrepositorymock.Setup(a => a.GetVendorAdminDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(vmlist);
            #endregion
            //Act
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
            var AdminService = new AdminServiceMock(TOWCategoryRepository: mockTypeOfWorkCategoryRepository.Object, mediaOutletRepository: mockImediaoutletRepository.Object, vendor:vendorrepositorymock.Object);
            var result = AdminController.GetVendorAdminDetails("233 W 49TH STREET REALTY", "1000360116", "", "//SWNA/TestLogin");
            //Assert
            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void AddVendorAdmin_Test()
        {
            #region Datasetup

            Channel ch = new Channel()
            {
                Id = 1,
                Name = "DisneyChannel"
            };
            List<Channel> chlist = new List<Channel>();
            chlist.Add(ch);
            VendorAdminViewModel vm = new VendorAdminViewModel()
            {
                Id = 1,
                Name = "233 W 49TH STREET REALTY",
                SAPVendorNumber = "1000360116",
                FederalId = "",
                Address = "test",
                PhoneNumber = "956412345"

            };
            List<VendorAdminViewModel> vmlist = new List<VendorAdminViewModel>();
            vmlist.Add(vm);
            Channel_DeliverableGroup_MasterVendor cdmsvend = new Channel_DeliverableGroup_MasterVendor()
            {
                ChannelId = 1,
                Channel = ch
            };

            List<Channel_DeliverableGroup_MasterVendor> cdmsvendlist = new List<Channel_DeliverableGroup_MasterVendor>();
            cdmsvendlist.Add(cdmsvend);
            MasterVendor msvend = new MasterVendor()
            {
                Id = 1,
                Channel_DeliverableGroup_MasterVendor = cdmsvendlist,
                SAPVendorId = 200,
                CreatedBy = 567,
                LastUpdatedBy = 567,
                CreatedDateTime = DateTime.Now,
                LastUpdatedDateTime = DateTime.Now,
                Address = "test",
                PhoneNumber = "9856412345",
                OtherName = "Test"
            };
            Vendor vend = new Vendor()
            {
                Id = 50,
                Name = "Test",
                SAPVendorNumber = "1000360116",
                CreatedBy = 567,
                LastUpdatedBy = 567,
                CreatedDateTime = DateTime.Now,
                LastUpdatedDateTime = DateTime.Now,
                PhoneNumber = ""
            };
            DeliverableGroup delgp = new DeliverableGroup()
            {
                Id = 1,
                Name = "Test"
            };
            List<DeliverableGroup> delgplist = new List<DeliverableGroup>();
            delgplist.Add(delgp);
            UserTitle usertt = new UserTitle()
            {
                Code = "WP",
                Id = 15,
                Name = "WriterProducer"
            };
            Mapper.CreateMap<VendorAdminViewModel, MasterVendor>();
            Mapper.CreateMap<VendorAdminViewModel, Vendor>();
            #endregion
            #region Mocking
            mockadminService.Setup(a => a.AddVendorAdmin(It.IsAny<Vendor>(), It.IsAny<int>())).Returns(1);
            mockadminService.Setup(a => a.AddMasterVendor(It.IsAny<MasterVendor>(), It.IsAny<int>(), It.IsAny<int>())).Returns(1);
            vendorrepositorymock.Setup(a => a.Add(It.IsAny<Vendor>())).Returns(vend);
            deliverablegrouprepository.Setup(a => a.GetAll()).Returns(delgplist.AsQueryable());
            mockusertitlerepository.Setup(a => a.GetSingle(It.IsAny<Expression<Func<UserTitle, bool>>>())).Returns(usertt);
            mastervendorrepositorymock.Setup(a => a.Add(It.IsAny<MasterVendor>())).Returns(msvend);
            mockunitofwork.Setup(x => x.Commit());
            #endregion
            //Act
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
            var AdminService = new AdminServiceMock(TOWCategoryRepository: mockTypeOfWorkCategoryRepository.Object, deliverableGroupRepository: deliverablegrouprepository.Object, masterVendorRepository: mastervendorrepositorymock.Object, mediaOutletRepository: mockImediaoutletRepository.Object, vendor: vendorrepositorymock.Object, unitOfWork: mockunitofwork.Object, userTitle: mockusertitlerepository.Object);
            var serviceresult = AdminService.AddVendorAdmin(vend, 540);
            var servresult = AdminService.AddMasterVendor(msvend, 0, 540);
            var result = AdminController.AddVendorAdmin(vmlist, 540, "//SWNA/TestLogin");

            //Assert
            Assert.IsNotNull(serviceresult);
            Assert.IsNotNull(servresult);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void UpdateVendorAdmin_Test()
        {
            #region Datasetup

            Channel ch = new Channel()
            {
                Id = 1,
                Name = "DisneyChannel"
            };
            List<Channel> chlist = new List<Channel>();
            chlist.Add(ch);
            VendorAdminViewModel vm = new VendorAdminViewModel()
            {
                Id = 1,
                Name = "233 W 49TH STREET REALTY",
                SAPVendorNumber = "1000360116",
                FederalId = "",
                Address = "",
                PhoneNumber = ""

            };
            List<VendorAdminViewModel> vmlist = new List<VendorAdminViewModel>();
            vmlist.Add(vm);
            Channel_DeliverableGroup_MasterVendor cdmsvend = new Channel_DeliverableGroup_MasterVendor()
            {
                ChannelId = 1,
                Channel = ch
            };

            List<Channel_DeliverableGroup_MasterVendor> cdmsvendlist = new List<Channel_DeliverableGroup_MasterVendor>();
            cdmsvendlist.Add(cdmsvend);
            MasterVendor msvend = new MasterVendor()
            {
                Id = 1,
                Channel_DeliverableGroup_MasterVendor = cdmsvendlist,
                SAPVendorId = 200,
                CreatedBy = 567,
                LastUpdatedBy = 567,
                CreatedDateTime = DateTime.Now,
                LastUpdatedDateTime = DateTime.Now,
                Address = "",
                PhoneNumber = ""
            };
            Vendor vend = new Vendor()
            {
                Id = 50,
                Name = "Test",
                SAPVendorNumber = "1000360116",
                CreatedBy = 567,
                LastUpdatedBy = 567,
                CreatedDateTime = DateTime.Now,
                LastUpdatedDateTime = DateTime.Now,
                PhoneNumber = ""
            };
            DeliverableGroup delgp = new DeliverableGroup()
            {
                Id = 1,
                Name = "Test"
            };
            List<DeliverableGroup> delgplist = new List<DeliverableGroup>();
            delgplist.Add(delgp);
            UserTitle usertt = new UserTitle()
            {
                Code = "Test",
                Id = 15,
                Name = "Test"
            };
            Mapper.CreateMap<VendorAdminViewModel, MasterVendor>();
            Mapper.CreateMap<VendorAdminViewModel, Vendor>();
            #endregion
            #region Mocking
            mockadminService.Setup(a => a.UpdateVendor(It.IsAny<Vendor>(), It.IsAny<int>())).Returns(1);
            mockadminService.Setup(a => a.UpdateMasterVendor(It.IsAny<MasterVendor>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            vendorrepositorymock.Setup(a => a.GetSingle(It.IsAny<Expression<Func<Vendor, bool>>>())).Returns(vend);
            vendorrepositorymock.Setup(a => a.Update(It.IsAny<Vendor>()));
            deliverablegrouprepository.Setup(a => a.GetAll()).Returns(delgplist.AsQueryable());
            mockusertitlerepository.Setup(a => a.GetSingle(It.IsAny<Expression<Func<UserTitle, bool>>>())).Returns(usertt);
            mastervendorrepositorymock.Setup(a => a.GetSingle(It.IsAny<Expression<Func<MasterVendor, bool>>>())).Returns(msvend);
            vendorrepositorymock.Setup(a => a.DeleteCDMasVenRec(It.IsAny<int>())).Returns(true);
            mastervendorrepositorymock.Setup(a => a.Update(It.IsAny<MasterVendor>()));
            mockunitofwork.Setup(x => x.Commit());
            #endregion
            //Act
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
            var AdminService = new AdminServiceMock(TOWCategoryRepository: mockTypeOfWorkCategoryRepository.Object, deliverableGroupRepository: deliverablegrouprepository.Object, masterVendorRepository: mastervendorrepositorymock.Object, mediaOutletRepository: mockImediaoutletRepository.Object, vendor: vendorrepositorymock.Object, unitOfWork: mockunitofwork.Object);
            var serviceresult = AdminService.UpdateVendor(vend, 540);
            var result = AdminController.UpdateVendorAdmin(vmlist, 540, "//SWNA/TestLogin");
            //Assert
            Assert.IsNotNull(result);

        }



        #region MRM-1815
        [TestMethod]
        public void CheckForDuplicateChargeCode_Test()
        {
            #region Datasetup

            var wbs =  new WBSElement
                {
                    Id = 1,
                    ChargeCodeTypeId = 2,
                    CompanyId = 1,
                    FullWBSNumber = "WBS-1",
                    ExternalWBSFlag = true,
                    ExternalBusinessAreaId = 1,
                    ExternalDefaultGLAccountId = 1,
                    ExternalDefaultCostCenterId = 1,
                    TypeOfWorkId = 1,
                    BusinessArea = new BusinessArea
                    {
                        Id = 1,
                        Name = "BU1",
                        CompanyId = 1,
                        IsActiveFlag = true,
                        Description = "BU1"
                    },
                    Company = new Company
                    {
                        Id = 1,
                        IsActiveFlag = true,
                        Name = "Company1",
                        Description = "Company1",
                        Code = "100"
                    },
                    CostCenter = new CostCenter
                    {
                        Id = 1,
                        Name = "CostCenter1",
                        CostCenterCode = "001",
                        Description = "CostCenter1"
                    },
                    GLAccount = new GLAccount
                    {
                        Id = 1,
                        Description = "GLAccount1",
                        Number = "001"
                    },
                    ChargeCodeType = new ChargeCodeType
                    {
                        Id = 2,
                        Name = "External WBS"
                    },
                    TypeOfWork = new TypeOfWork
                    {
                        Id = 1,
                        Name = "TOW1",
                        Description = "TOW1",
                        FiscalYear = 2017
                    }

                };
            #endregion

            #region Mocking
            wbsElementRepositorymock.Setup(a => a.GetSingle(It.IsAny<Expression<Func<WBSElement, bool>>>())).Returns(wbs);
            var adminService = new AdminServiceMock(TOWCategoryRepository: mockTypeOfWorkCategoryRepository.Object,
               deliverableGroupRepository: deliverablegrouprepository.Object,
               masterVendorRepository: mastervendorrepositorymock.Object,
               mediaOutletRepository: mockImediaoutletRepository.Object,
               vendor: vendorrepositorymock.Object, wbsElementRepository: wbsElementRepositorymock.Object);
            #endregion

            #region Act

            var  result = adminService.CheckForDuplicateChargeCode(null, "WBS-1");

            #endregion

            #region  Assert
            Assert.IsTrue(result != null);            
            #endregion

        }


        [TestMethod]
        public void GetExternalChargeCodes_Test()
        {
            #region Datasetup

            var wbsList = new List<WBSElement>();
            wbsList.Add(
                new WBSElement
                {
                    Id = 1,
                    ChargeCodeTypeId = 2,
                    CompanyId = 1,
                    FullWBSNumber = "WBS-1",
                    ExternalWBSFlag = true,
                    ExternalBusinessAreaId = 1,
                    ExternalDefaultGLAccountId = 1,
                    ExternalDefaultCostCenterId = 1,
                    TypeOfWorkId = 1,
                    BusinessArea = new BusinessArea
                    {
                        Id = 1,
                        Name = "BU1",
                        CompanyId = 1,
                        IsActiveFlag = true,
                        Description = "BU1"
                    },
                    Company = new Company
                    {
                        Id = 1,
                        IsActiveFlag = true,
                        Name = "Company1",
                        Description = "Company1",
                        Code = "100"
                    },
                    CostCenter = new CostCenter
                    {
                        Id = 1,
                        Name = "CostCenter1",
                        CostCenterCode = "001",
                        Description = "CostCenter1"
                    },
                    GLAccount = new GLAccount
                    {
                        Id = 1,
                        Description = "GLAccount1",
                        Number = "001"
                    },
                    ChargeCodeType = new ChargeCodeType
                    {
                        Id = 2,
                        Name = "External WBS"
                    },
                    TypeOfWork = new TypeOfWork
                    {
                        Id = 1,
                        Name = "TOW1",
                        Description = "TOW1",
                        FiscalYear = 2017
                    }

                });

            wbsList.Add(
                new WBSElement
                {
                    Id = 2,
                    ChargeCodeTypeId = 2,
                    CompanyId = 2,
                    FullWBSNumber = "WBS-2",
                    ExternalWBSFlag = true,
                    ExternalBusinessAreaId = 2,
                    ExternalDefaultGLAccountId = 2,
                    ExternalDefaultCostCenterId = 2,
                    TypeOfWorkId = 1,
                    BusinessArea = new BusinessArea
                    {
                        Id = 2,
                        Name = "BU2",
                        CompanyId = 2,
                        IsActiveFlag = true,
                        Description = "BU2"
                    },
                    Company = new Company
                    {
                        Id = 2,
                        IsActiveFlag = true,
                        Name = "Company2",
                        Description = "Company2",
                        Code = "200"
                    },
                    CostCenter = new CostCenter
                    {
                        Id = 2,
                        Name = "CostCenter2",
                        CostCenterCode = "002",
                        Description = "CostCenter2"
                    },
                    GLAccount = new GLAccount
                    {
                        Id = 2,
                        Description = "GLAccount2",
                        Number = "002"
                    },
                    ChargeCodeType = new ChargeCodeType
                    {
                        Id = 3,
                        Name = "GLAccount"
                    },
                    TypeOfWork = new TypeOfWork
                    {
                        Id = 1,
                        Name = "TOW1",
                        Description = "TOW1",
                        FiscalYear = 2017
                    }

                });

            wbsList.Add(
                new WBSElement
                {
                    Id = 3,
                    ChargeCodeTypeId = 3,
                    CompanyId = 3,
                    FullWBSNumber = "WBS-3",
                    ExternalWBSFlag = true,
                    ExternalBusinessAreaId = 3,
                    ExternalDefaultGLAccountId = 3,
                    ExternalDefaultCostCenterId = 3,
                    TypeOfWorkId = 3,
                    BusinessArea = new BusinessArea
                    {
                        Id = 3,
                        Name = "BU3",
                        CompanyId = 3,
                        IsActiveFlag = true,
                        Description = "BU3"
                    },
                    Company = new Company
                    {
                        Id = 3,
                        IsActiveFlag = true,
                        Name = "Company3",
                        Description = "Company3",
                        Code = "300"
                    },

                    CostCenter = new CostCenter
                    {
                        Id = 3,
                        Name = "CostCenter3",
                        CostCenterCode = "003",
                        Description = "CostCenter3"
                    },
                    GLAccount = new GLAccount
                    {
                        Id = 3,
                        Description = "GLAccount3",
                        Number = "003"
                    },
                    ChargeCodeType = new ChargeCodeType
                    {
                        Id = 3,
                        Name = "GLAccount"
                    },
                    TypeOfWork = new TypeOfWork
                    {
                        Id = 3,
                        Name = "TOW3",
                        Description = "TOW3",
                        FiscalYear = 2017
                    }

                });



            #endregion

            #region Mocking
            wbsElementRepositorymock.Setup(a => a.GetMany(It.IsAny<Expression<Func<WBSElement, bool>>>())).Returns(wbsList);
            var adminService = new AdminServiceMock(TOWCategoryRepository: mockTypeOfWorkCategoryRepository.Object,
               deliverableGroupRepository: deliverablegrouprepository.Object,
               masterVendorRepository: mastervendorrepositorymock.Object,
               mediaOutletRepository: mockImediaoutletRepository.Object,
               vendor: vendorrepositorymock.Object, wbsElementRepository: wbsElementRepositorymock.Object);
            #endregion

            #region Act
            var lst = adminService.GetExternalChargeCodes();
            #endregion

            #region Assert
            Assert.IsTrue(lst.Any());
            #endregion

        }


        [TestMethod]
        public void SearchExternalChargeCodes_Test()
        {
            #region Datasetup

            var wbsList = new List<WBSElement>();
            wbsList.Add(
                new WBSElement
                {
                    Id = 1,                 
                    ChargeCodeTypeId = 2,
                    CompanyId = 1,
                    FullWBSNumber = "WBS-1", 
                    ExternalWBSFlag = true,
                    ExternalBusinessAreaId = 1,
                    ExternalDefaultGLAccountId = 1,
                    ExternalDefaultCostCenterId = 1,
                    TypeOfWorkId = 1,
                    BusinessArea = new BusinessArea
                    {
                        Id = 1,
                        Name = "BU1",
                        CompanyId = 1,
                        IsActiveFlag = true,
                        Description = "BU1"
                    },
                    Company = new Company
                    {
                        Id = 1,
                        IsActiveFlag = true,
                        Name = "Company1",
                        Description = "Company1",
                        Code = "100"
                    },
                    CostCenter = new CostCenter
                    {
                        Id = 1,
                        Name = "CostCenter1",
                        CostCenterCode = "001",
                        Description = "CostCenter1"                        
                    },
                    GLAccount = new GLAccount {
                        Id = 1,
                        Description = "GLAccount1",
                        Number = "001"
                    },
                    ChargeCodeType = new ChargeCodeType
                    {
                        Id = 2,
                        Name = "External WBS"
                    },
                    TypeOfWork = new TypeOfWork
                    {
                        Id = 1,
                        Name = "TOW1",
                        Description = "TOW1",
                        FiscalYear = 2017
                    }

                });

            wbsList.Add(
                new WBSElement
                {
                    Id = 2,                    
                    ChargeCodeTypeId = 2,
                    CompanyId = 2,
                    FullWBSNumber = "WBS-2",
                    ExternalWBSFlag = true,
                    ExternalBusinessAreaId = 2,
                    ExternalDefaultGLAccountId = 2,
                    ExternalDefaultCostCenterId = 2,
                    TypeOfWorkId = 1,
                    BusinessArea = new BusinessArea
                    {
                        Id = 2,
                        Name = "BU2",
                        CompanyId = 2,
                        IsActiveFlag = true,
                        Description = "BU2"
                    },
                    Company = new Company
                    {
                        Id = 2,
                        IsActiveFlag = true,
                        Name = "Company2",
                        Description = "Company2",
                        Code = "200"
                    },
                    CostCenter = new CostCenter
                    {
                        Id = 2,
                        Name = "CostCenter2",
                        CostCenterCode = "002",
                        Description = "CostCenter2"
                    },
                    GLAccount = new GLAccount
                    {
                        Id = 2,
                        Description = "GLAccount2",
                        Number = "002"
                    },
                    ChargeCodeType = new ChargeCodeType
                    {
                        Id = 3,
                        Name = "GLAccount"
                    },
                    TypeOfWork = new TypeOfWork
                    {
                        Id = 1,
                        Name = "TOW1",
                        Description = "TOW1",
                        FiscalYear = 2017
                    }

                });

            wbsList.Add(
                new WBSElement
                {
                    Id = 3,
                    ChargeCodeTypeId = 3,
                    CompanyId = 3,
                    FullWBSNumber = "WBS-3",
                    ExternalWBSFlag = true,
                    ExternalBusinessAreaId = 3,
                    ExternalDefaultGLAccountId = 3,
                    ExternalDefaultCostCenterId = 3,
                    TypeOfWorkId = 3,
                    BusinessArea = new BusinessArea
                    {
                        Id = 3,
                        Name = "BU3",
                        CompanyId = 3,
                        IsActiveFlag = true,
                        Description = "BU3"
                    },
                    Company = new Company
                    {
                        Id = 3,
                        IsActiveFlag = true,
                        Name = "Company3",
                        Description = "Company3",
                        Code = "300"
                    },
                    
                    CostCenter = new CostCenter
                    {
                        Id = 3,
                        Name = "CostCenter3",
                        CostCenterCode = "003",
                        Description = "CostCenter3"
                    },
                    GLAccount = new GLAccount
                    {
                        Id = 3,
                        Description = "GLAccount3",
                        Number = "003"
                    },
                    ChargeCodeType = new ChargeCodeType
                    {
                        Id = 3,
                        Name = "GLAccount"
                    },
                    TypeOfWork = new TypeOfWork
                    {
                        Id = 3,
                        Name = "TOW3",
                        Description = "TOW3",
                        FiscalYear = 2017
                    }

                });

            var searchModel = new ExternalChargeCodeModel
            {
                FiscalYear = 2017
            };

            #endregion

            #region Mocking
            wbsElementRepositorymock.Setup(a => a.GetMany(It.IsAny<Expression<Func<WBSElement, bool>>>())).Returns(wbsList);
            var adminService = new AdminServiceMock(TOWCategoryRepository: mockTypeOfWorkCategoryRepository.Object, 
                deliverableGroupRepository: deliverablegrouprepository.Object,
                masterVendorRepository: mastervendorrepositorymock.Object,
                mediaOutletRepository: mockImediaoutletRepository.Object, 
                vendor: vendorrepositorymock.Object,wbsElementRepository:wbsElementRepositorymock.Object);
            #endregion

            #region Act            
            var lst = adminService.SearchExternalChargeCodes(searchModel);
            #endregion

            #region Result
            
            Assert.IsTrue(lst.Any());

            #endregion

        }

        [TestMethod]
        public void AddExternalChargeCodes_Test()
        {
            #region Datasetup

            var modelList = new List<ExternalChargeCodeModel>();

            modelList.Add(new ExternalChargeCodeModel {
                ChargeCodeId = 0,
                BusinessAreaId = 1,
                BusinessArea = "BU1",
                ChargeCode = "WBS-1",
                ChargeCodeType = "EXT",
                ChargeCodeTypeId = 2,
                CompanyCode = "CU1",
                CompanyId = 1,
                CostCenter = "Center1",
                CostCenterId = 1,
                FiscalYear = 2016,
                FiscalYearId = 2016,
                GLAccount = "Account1",
                GLAccountId = 1,
                Name = "TOW-1"
            });

            var tow = new MRM.DANG.Model.TypeOfWork
            {
                Id = 1,
                Name = "TOW-1",
                FiscalYear = 2016
            };
            
            var wbs = new WBSElement
                {
                    Id = 1,
                    ChargeCodeTypeId = 2,
                    CompanyId = 1,
                    FullWBSNumber = "WBS-1",
                    ExternalWBSFlag = true,
                    ExternalBusinessAreaId = 1,
                    ExternalDefaultGLAccountId = 1,
                    ExternalDefaultCostCenterId = 1,
                    TypeOfWorkId = 1,
                    BusinessArea = new BusinessArea
                    {
                        Id = 1,
                        Name = "BU1",
                        CompanyId = 1,
                        IsActiveFlag = true,
                        Description = "BU1"
                    },
                    Company = new Company
                    {
                        Id = 1,
                        IsActiveFlag = true,
                        Name = "Company1",
                        Description = "Company1",
                        Code = "100"
                    },
                    CostCenter = new CostCenter
                    {
                        Id = 1,
                        Name = "CostCenter1",
                        CostCenterCode = "001",
                        Description = "CostCenter1"
                    },
                    GLAccount = new GLAccount
                    {
                        Id = 1,
                        Description = "GLAccount1",
                        Number = "001"
                    },
                    ChargeCodeType = new ChargeCodeType
                    {
                        Id = 2,
                        Name = "External WBS"
                    },
                    TypeOfWork = new TypeOfWork
                    {
                        Id = 1,
                        Name = "TOW1",
                        Description = "TOW1",
                        FiscalYear = 2017
                    }

                };

            #endregion

            #region Mocking
            wbsElementRepositorymock.Setup(a => a.Add(It.IsAny<WBSElement>())).Returns(wbs);
            towRepositorymock.Setup(a => a.Get(It.IsAny<Expression<Func<TypeOfWork, bool>>>())).Returns(tow);
            towRepositorymock.Setup(a => a.Add(It.IsAny<TypeOfWork>())).Returns(tow);
            mockunitofwork.Setup(x => x.Commit());

            var adminService = new AdminServiceMock(TOWCategoryRepository: mockTypeOfWorkCategoryRepository.Object,
                deliverableGroupRepository: deliverablegrouprepository.Object,
                masterVendorRepository: mastervendorrepositorymock.Object,
                mediaOutletRepository: mockImediaoutletRepository.Object,
                vendor: vendorrepositorymock.Object, wbsElementRepository: wbsElementRepositorymock.Object,
                towRepository: towRepositorymock.Object,
                unitOfWork: mockunitofwork.Object
                );
            #endregion

            #region Act
            var result = adminService.AddExternalChargeCode(modelList, 500);
            #endregion

            #region Assert
            Assert.IsTrue(result.Any());
            #endregion

        }

        [TestMethod]
        public void UpdateExternalChargeCodes_Test()
        {
            #region Datasetup

            var modelList = new List<ExternalChargeCodeModel>();

            modelList.Add(new ExternalChargeCodeModel
            {
                ChargeCodeId = 0,
                BusinessAreaId = 1,
                BusinessArea = "BU1",
                ChargeCode = "WBS-1",
                ChargeCodeType = "EXT",
                ChargeCodeTypeId = 2,
                CompanyCode = "CU1",
                CompanyId = 1,
                CostCenter = "Center1",
                CostCenterId = 1,
                FiscalYear = 2016,
                FiscalYearId = 2016,
                GLAccount = "Account1",
                GLAccountId = 1,
                Name = "TOW-1"
            });

            var tow = new MRM.DANG.Model.TypeOfWork
            {
                Id = 1,
                Name = "TOW-1",
                FiscalYear = 2016
            };

            var wbs = new WBSElement
            {
                Id = 1,
                ChargeCodeTypeId = 2,
                CompanyId = 1,
                FullWBSNumber = "WBS-1",
                ExternalWBSFlag = true,
                ExternalBusinessAreaId = 1,
                ExternalDefaultGLAccountId = 1,
                ExternalDefaultCostCenterId = 1,
                TypeOfWorkId = 1,
                BusinessArea = new BusinessArea
                {
                    Id = 1,
                    Name = "BU1",
                    CompanyId = 1,
                    IsActiveFlag = true,
                    Description = "BU1"
                },
                Company = new Company
                {
                    Id = 1,
                    IsActiveFlag = true,
                    Name = "Company1",
                    Description = "Company1",
                    Code = "100"
                },
                CostCenter = new CostCenter
                {
                    Id = 1,
                    Name = "CostCenter1",
                    CostCenterCode = "001",
                    Description = "CostCenter1"
                },
                GLAccount = new GLAccount
                {
                    Id = 1,
                    Description = "GLAccount1",
                    Number = "001"
                },
                ChargeCodeType = new ChargeCodeType
                {
                    Id = 2,
                    Name = "External WBS"
                },
                TypeOfWork = new TypeOfWork
                {
                    Id = 1,
                    Name = "TOW1",
                    Description = "TOW1",
                    FiscalYear = 2017
                }

            };

            #endregion

            #region Mocking
            wbsElementRepositorymock.Setup(a => a.Update(It.IsAny<WBSElement>()));
            wbsElementRepositorymock.Setup(a => a.GetById(It.IsAny<long>())).Returns(wbs);

            towRepositorymock.Setup(a => a.Get(It.IsAny<Expression<Func<TypeOfWork, bool>>>())).Returns(tow);
            towRepositorymock.Setup(a => a.Add(It.IsAny<TypeOfWork>())).Returns(tow);
            mockunitofwork.Setup(x => x.Commit());

            var adminService = new AdminServiceMock(TOWCategoryRepository: mockTypeOfWorkCategoryRepository.Object,
                deliverableGroupRepository: deliverablegrouprepository.Object,
                masterVendorRepository: mastervendorrepositorymock.Object,
                mediaOutletRepository: mockImediaoutletRepository.Object,
                vendor: vendorrepositorymock.Object, wbsElementRepository: wbsElementRepositorymock.Object,
                towRepository: towRepositorymock.Object,
                unitOfWork: mockunitofwork.Object
                );
            #endregion

            #region Act
            var result = adminService.UpdateExternalChargeCode(modelList, 500);
            #endregion

            #region Assert
            Assert.IsTrue(result.Any());
            #endregion

        }

        #endregion


        #region Marketing Group Team Test --- MRM-1857

        [TestMethod]
        public void GetMarketingGroupTeams_Test()
        {
            #region Datasetup
            List<MarketingGroupTeamAdminVM> returnResult = new List<MarketingGroupTeamAdminVM>();
            List<Channel_DeliverableGroup_UserTitle> serviceresult = new List<Channel_DeliverableGroup_UserTitle>();
            Channel ch = new Channel()
            {
                Name = "Disney Channel"
            };
            DeliverableGroup dg = new DeliverableGroup()
            {
                Name = "Contract Request"
            };
            UserTitle usertit = new UserTitle()
            {
                Name = "Marketing Manager"
            };
            MRMUser mrmusers = new MRMUser()
            {
                Id = 582
            };
            Channel_DeliverableGroup_UserTitle channeldeligroupusertitle = new Channel_DeliverableGroup_UserTitle()
            {
                MRMUser = mrmusers,
                UserTitle = usertit,
                DeliverableGroup = dg,
                Channel = ch,
                ChannelId = 1,
                DeliverableGroupId = 1,
                UserTitleId = 9,
                Id = 245,
                CreatedBy = 524,
                LastUpdatedBy = 524
            };
            List<Channel_DeliverableGroup_UserTitle> usertitlelist = new List<Channel_DeliverableGroup_UserTitle>();
            usertitlelist.Add(channeldeligroupusertitle);
            Mapper.CreateMap<List<Channel_DeliverableGroup_UserTitle>, List<MarketingGroupTeamAdminVM>>();
            #endregion
            #region Mocking
            mockadminService.Setup(a => a.GetMarketingGroupTeams(It.IsAny<int>())).Returns(usertitlelist);
            mockChdelgpusertitleRepository.Setup(a => a.GetAll()).Returns(usertitlelist.AsQueryable());
            #endregion
            //Act
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
            var AdminService = new AdminServiceMock(channelDeliverableGroupUserTitleRepository: mockChdelgpusertitleRepository.Object);
            returnResult = AdminController.GetMarketingGroupTeams(582, "ABC");
            serviceresult = AdminService.GetMarketingGroupTeams(582);
            //Assert
            Assert.IsNotNull(returnResult);
            Assert.IsTrue(serviceresult.Count == 1);
        }
        [TestMethod]
        public void AddMarketingGroupTeams_Test()
        {
            #region Datasetup
            Channel_DeliverableGroup_UserTitle channeldeligroupusertitle = new Channel_DeliverableGroup_UserTitle()
            {
                ChannelId = 1,
                DeliverableGroupId = 3,
                UserTitleId = 4,
                Id = 184,
                CreatedBy = 567,
                CreatedDateTime = DateTime.UtcNow,
                LastUpdatedBy = 567,
                LastUpdatedDateTime = DateTime.UtcNow,
                MRMUserId = 567
            };
            List<Channel_DeliverableGroup_UserTitle> usertitlelist = new List<Channel_DeliverableGroup_UserTitle>();
            usertitlelist.Add(channeldeligroupusertitle);
            MarketingGroupTeamAdminVM marketinggpteamadmin = new MarketingGroupTeamAdminVM()
            {
                CreatedBy = 540,
                CreatedDateTime = DateTime.UtcNow,
                LastUpdatedBy = 540,
                LastUpdatedDateTime = DateTime.UtcNow
            };
            List<MarketingGroupTeamAdminVM> marketinggpteamadminlist = new List<MarketingGroupTeamAdminVM>();
            marketinggpteamadminlist.Add(marketinggpteamadmin);
            Mapper.CreateMap<List<MarketingGroupTeamAdminVM>, List<Channel_DeliverableGroup_UserTitle>>();
            #endregion
            #region Mocking
            mockadminService.Setup(a => a.AddMarketingGroupTeams(It.IsAny<List<Channel_DeliverableGroup_UserTitle>>(), It.IsAny<int>())).Returns(true);
            mockunitofwork.Setup(a => a.StartOwnTransactionWithinContext(It.IsAny<List<Channel_DeliverableGroup_UserTitle>>(), It.IsAny<string>(), It.IsAny<int>())).Returns(true);
            #endregion
            //Act
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
            var AdminService = new AdminServiceMock(channelDeliverableGroupUserTitleRepository: mockChdelgpusertitleRepository.Object, unitOfWork: mockunitofwork.Object);
            bool serviceresult = AdminService.AddMarketingGroupTeams(usertitlelist, 567);
            bool result = AdminController.AddMarketingGroupTeams(marketinggpteamadminlist, 540, "//SWNA/TestLogin");
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(serviceresult);
        }
        [TestMethod]
        public void UpdateMarketingGroupTeams_Test()
        {
            #region Datasetup
            Channel_DeliverableGroup_UserTitle channeldeligroupusertitle = new Channel_DeliverableGroup_UserTitle()
            {
                ChannelId = 1,
                DeliverableGroupId = 3,
                UserTitleId = 4,
                Id = 184,
                CreatedBy = 567,
                CreatedDateTime = DateTime.UtcNow,
                LastUpdatedBy = 567,
                LastUpdatedDateTime = DateTime.UtcNow,
                MRMUserId = 567
            };
            List<Channel_DeliverableGroup_UserTitle> usertitlelist = new List<Channel_DeliverableGroup_UserTitle>();
            usertitlelist.Add(channeldeligroupusertitle);
            MarketingGroupTeamAdminVM marketinggpteamadmin = new MarketingGroupTeamAdminVM()
            {
                CreatedBy = 540,
                CreatedDateTime = DateTime.UtcNow,
                LastUpdatedBy = 540,
                LastUpdatedDateTime = DateTime.UtcNow
            };
            List<MarketingGroupTeamAdminVM> marketinggpteamadminlist = new List<MarketingGroupTeamAdminVM>();
            marketinggpteamadminlist.Add(marketinggpteamadmin);
            Mapper.CreateMap<List<MarketingGroupTeamAdminVM>, List<Channel_DeliverableGroup_UserTitle>>();
            #endregion
            #region Mocking
            mockadminService.Setup(a => a.UpdateMarketingGroupTeams(It.IsAny<List<Channel_DeliverableGroup_UserTitle>>(), It.IsAny<int>())).Returns(true);
            mockunitofwork.Setup(a => a.StartOwnTransactionWithinContext(It.IsAny<List<Channel_DeliverableGroup_UserTitle>>(), It.IsAny<string>(), It.IsAny<int>())).Returns(true);
            #endregion
            //Act
            var AdminController = new AdminControllerMock(adminService: mockadminService.Object);
            var AdminService = new AdminServiceMock(channelDeliverableGroupUserTitleRepository: mockChdelgpusertitleRepository.Object, unitOfWork: mockunitofwork.Object);
            bool serviceresult = AdminService.UpdateMarketingGroupTeams(usertitlelist, 567);
            bool result = AdminController.UpdateMarketingGroupTeams(marketinggpteamadminlist, 540, "//SWNA/TestLogin");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(serviceresult);
        }

        #endregion
    }
}
