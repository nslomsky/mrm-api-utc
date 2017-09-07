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
    public class UserControllerTest
    {
        #region Constants
        const int MRM_USER_ID = 524;
        const string NETWORK_LOGIN = "swna\\8testbot";
        #endregion

        #region private variables
        private Moq.MockRepository mockRepository;
        private Moq.Mock<IUserRepository> mockuserRepository;
        private Moq.Mock<IUserService> mockuserService;
        private Moq.Mock<IChannelRepository> mockchannelRepository;
        private Moq.Mock<IModuleRepository> mockmoduleRepository;
        private Moq.Mock<IMRMUserDepartmentTypeRepository> mockuserdepartmenttypeRepository;
        private Moq.Mock<IDepartmentRepository> mockdepartmentRepository;
        private Moq.Mock<IUnitOfWork> mockunitofwork;
        private Moq.Mock<IRoleRepository> mockroleRepository;
        private Moq.Mock<IUserTitleRepository> mockusertitleRepository;
        #endregion
        [TestInitialize]
        public void ClassInit()
        {
            mockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
            mockuserRepository = mockRepository.Create<IUserRepository>();
            mockuserService = mockRepository.Create<IUserService>();
            mockchannelRepository = mockRepository.Create<IChannelRepository>();
            mockmoduleRepository = mockRepository.Create<IModuleRepository>();
            mockuserdepartmenttypeRepository = mockRepository.Create<IMRMUserDepartmentTypeRepository>();
            mockdepartmentRepository = mockRepository.Create<IDepartmentRepository>();
            mockunitofwork = mockRepository.Create<IUnitOfWork>();
            mockroleRepository = mockRepository.Create<IRoleRepository>();
            mockusertitleRepository = mockRepository.Create<IUserTitleRepository>();

        }
        [TestMethod]
        public void GetAdminDataByDepartmentId_Test()
        {
            #region Datasetup
            Department dept = new Department()
            {
                Id = 84,
                Name = "MRM Team",
                Code = "MRM"
            };
            List<Department> deptlist = new List<Department>();
            deptlist.Add(dept);
            MRMUser users = new MRMUser()
            {
                Id = 567,
                FirstName = "Ramya",
                CreatedBy = 532,
                Department = dept
            };
            List<MRMUser> userslist = new List<MRMUser>();
            userslist.Add(users);
            Channel channel1 = new Channel()
            {
                Id = 1,
                Name = "Disney Channel",
                Code = "DC",
                CreatedBy = 1,
                LastUpdatedBy = 1,
                IsMarketingGroupFlag = true,
                IsActiveFlag = true
            };
            List<Channel> channellist = new List<Channel>();
            channellist.Add(channel1);
            Module module1 = new Module()
            {
                Id = 10,
                Code = "USER",
                IsAdminModule = true,
                Name = "User Admin"
            };
            List<Module> modulelist = new List<Module>();
            modulelist.Add(module1);
            UserRole roles = new UserRole()
            {
                Id = 1,
                Name = "None",
                LastUpdatedBy = 567
            };
            List<UserRole> roleslist = new List<UserRole>();
            roleslist.Add(roles);
            UserTitle titles = new UserTitle()
            {
                Id = 1,
                Name = "Test"
            };
            List<UserTitle> titleslist = new List<UserTitle>();
            titleslist.Add(titles);
            #endregion
            #region mocking
            mockuserService.Setup(a => a.GetAllDepartments()).Returns(deptlist);
            mockdepartmentRepository.Setup(a => a.GetAll()).Returns(deptlist.AsQueryable());
            mockuserService.Setup(a => a.AllUsers()).Returns(userslist);
            mockuserRepository.Setup(a => a.GetMany(It.IsAny<Expression<Func<MRMUser, bool>>>())).Returns(userslist);
            mockuserService.Setup(a => a.GetAllChannels()).Returns(channellist);
            mockchannelRepository.Setup(a => a.GetAll()).Returns(channellist.AsQueryable());
            mockuserService.Setup(a => a.GetAllModules()).Returns(modulelist);
            mockmoduleRepository.Setup(a => a.GetAll()).Returns(modulelist.AsQueryable());
            mockuserService.Setup(a => a.GetAllRoles()).Returns(roleslist);
            mockroleRepository.Setup(a => a.GetAll()).Returns(roleslist.AsQueryable());
            mockuserService.Setup(a => a.GetAllTitles()).Returns(titleslist);
            mockusertitleRepository.Setup(a => a.GetAll()).Returns(titleslist.AsQueryable());

            #endregion
            //Act
            var UserController = new UserControllerMock(userService: mockuserService.Object);
            var result = UserController.AllUsers(true, MRM_USER_ID, NETWORK_LOGIN);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.AllChannels.Count == 1);
            Assert.IsFalse(result.AllModules.Count > 0);

        }
        [TestMethod]
        public void GetAdminModules_Test()
        {
            #region Datasetup           
            Module module1 = new Module()
            {
                Id = 15,
                Code = "VEND",
                IsAdminModule = true,
                Name = "Vendor Admin"
            };
            List<Module> modulelist = new List<Module>();
            modulelist.Add(module1);
            #endregion
            #region Mocking
            mockuserService.Setup(a => a.GetAllModules()).Returns(modulelist);
            #endregion
            //ACT
            var UserController = new UserControllerMock(userService: mockuserService.Object);
            IEnumerable<DropDownViewModel> result = UserController.GetAdminModules(MRM_USER_ID, NETWORK_LOGIN);
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void SaveChanges_Test()
        {
            #region DataSetup
            UserViewModel users = new UserViewModel()
            {
                DepartmentId=92,               
                FirstName="test",
                LastName="mrm",
                Id= 581,
                IsActive=true,
                
            };
            List<UserViewModel> userslist = new List<UserViewModel>();
            userslist.Add(users);
            #endregion
            #region Mocking
            mockuserService.Setup(a => a.savechanges(It.IsAny<List<UserViewModel>>())).Returns(true);
            mockuserRepository.Setup(a => a.UpdateMRMuser(It.IsAny<List<UserViewModel>>())).Returns(true);
            #endregion
            //Act
            var UserController = new UserControllerMock(userService: mockuserService.Object);
            bool result = UserController.SaveChanges(userslist, MRM_USER_ID, NETWORK_LOGIN);
            
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result == true);
        }
    }
}
