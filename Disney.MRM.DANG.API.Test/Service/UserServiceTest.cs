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
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;



namespace Disney.MRM.DANG.API.Test.Service
{
    [TestClass]
    public class UserServiceTest
    {
        private Moq.MockRepository mockRepository;
        private Moq.Mock<IUserRepository> mockuserRepository;
        private Moq.Mock<IUserChannelRepository> mockuserchannelRepository;
        private Moq.Mock<IUserRoleModuleRepository> mockuserrolemoduleRepository;
        private Moq.Mock<IMRMUserDepartmentTypeRepository> mockuserdepartmenttypeRepository;
        private Moq.Mock<IUnitOfWork> mockunitofwork;
       

        [TestInitialize]
        public void ClassInit()
        {
            mockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
            mockuserRepository = mockRepository.Create<IUserRepository>();
            mockuserchannelRepository = mockRepository.Create<IUserChannelRepository>();
            mockuserrolemoduleRepository = mockRepository.Create<IUserRoleModuleRepository>();
            mockuserdepartmenttypeRepository = mockRepository.Create<IMRMUserDepartmentTypeRepository>();            
            mockunitofwork = mockRepository.Create<IUnitOfWork>();
        }
        [TestMethod]
        public void SaveTest()//MRM-964
        {
            #region datasetup
            MRMUser user = new MRMUser()
            {
                CreatedBy=556,
                Id=581,
                UserName="Test",
                DepartmentId=85,
                IsActiveFlag=true,
                LastUpdatedBy=556,
                IsAdministratorFlag=true,
                MasterVendorId=12,
                PhoneNumber="9562345685",
                UserTitleId=5,
                CreatedDateTime=DateTime.Now,
                LastUpdatedDateTime=DateTime.Now                
            };
            MRMUser_Channel userchannel = new MRMUser_Channel()
            {
                Id=1,
                MRMUserId=556,
                CreatedBy=556,
                ChannelId=2,
                LastUpdatedBy=556,
                CreatedDateTime=DateTime.Now,
                LastUpdatedDateTime=DateTime.Now
            };
            List<MRMUser_Channel> userchannellist = new List<MRMUser_Channel>();
            userchannellist.Add(userchannel);
            MRMUser_UserRole_Module userrole = new MRMUser_UserRole_Module()
            {
                Id=1,
                CreatedBy=556,
                CreatedDateTime=DateTime.Now,
                LastUpdatedBy=556,
                ModuleId=1,
                MRMUserId=556

            };
            List<MRMUser_UserRole_Module> userrolemodule = new List<MRMUser_UserRole_Module>();
            userrolemodule.Add(userrole);
            MRMUser_DepartmentType userdepartmenttype = new MRMUser_DepartmentType()
            {
                Id=1,
                CreatedBy=556,
                CreatedDateTime=DateTime.Now,
                DepartmentTypeId=1,
                LastUpdatedBy=556,
                MRMUserId=556,
                LastUpdatedDateTime=DateTime.Now

            };
            List<MRMUser_DepartmentType> userdepartmenttypelist = new List<MRMUser_DepartmentType>();
            userdepartmenttypelist.Add(userdepartmenttype);

            user.MRMUser_Channel = userchannellist;
            user.MRMUser_DepartmentType = userdepartmenttypelist;
            user.MRMUser_UserRole_Module = userrolemodule;
            #endregion

            #region mocking
            mockuserRepository.Setup(i => i.GetById(It.IsAny<long>())).Returns(user);            
            mockuserRepository.Setup(i => i.GetSingle(It.IsAny<Expression<Func<MRMUser, bool>>>())).Returns(user);
            mockuserchannelRepository.Setup(i => i.GetMany(It.IsAny<Expression<Func<MRMUser_Channel, bool>>>())).Returns(userchannellist);
            mockuserchannelRepository.Setup(i => i.Delete(It.IsAny<MRMUser_Channel>()));
            mockuserrolemoduleRepository.Setup(i => i.GetMany(It.IsAny<Expression<Func<MRMUser_UserRole_Module, bool>>>())).Returns(userrolemodule);
            mockuserrolemoduleRepository.Setup(i => i.Delete(It.IsAny<MRMUser_UserRole_Module>()));
            mockuserdepartmenttypeRepository.Setup(i => i.GetMany(It.IsAny<Expression<Func<MRMUser_DepartmentType, bool>>>())).Returns(userdepartmenttypelist);
            mockuserdepartmenttypeRepository.Setup(i => i.Delete(It.IsAny<MRMUser_DepartmentType>()));
            mockuserRepository.Setup(i => i.Update(It.IsAny<MRMUser>()));
            mockuserRepository.Setup(i => i.Add(It.IsAny<MRMUser>())).Returns(user);
            mockunitofwork.Setup(i => i.Commit());
            #endregion

            #region service
            var userservice = new UserServiceMock(unitOfWork: mockunitofwork.Object, _userRepository: mockuserRepository.Object, userChannelRepository: mockuserchannelRepository.Object,
                                               userRoleModuleRepository: mockuserrolemoduleRepository.Object, userDepartmentTypeRepository: mockuserdepartmenttypeRepository.Object);

            var result = userservice.Save(user, 556);

            #endregion

            #region Assets
            Assert.IsNotNull(result);
            Assert.IsTrue(userdepartmenttypelist.Count == 1);
            Assert.IsTrue(result.UserName.Equals("Test"));
            #endregion
        }
    }
}
