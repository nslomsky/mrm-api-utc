using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disney.MRM.DANG.Core.Contracts;
using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.Service.Implementations;
using Disney.MRM.DANG.Model;

namespace Disney.MRM.DANG.API.Test.MockObject.Service
{
    public class UserServiceMock : UserService
    {
        public UserServiceMock
            (IUnitOfWork unitOfWork=null, IUserRepository _userRepository = null, IChannelRepository channelRepository=null, IUserChannelRepository userChannelRepository=null,
             IModuleRepository moduleRepository=null, ICompanyRepository companyRepository=null,IRoleRepository roleRepository=null,
             IDepartmentRepository departmentRepository=null, ILogService loggerService=null,IUserRoleModuleRepository userRoleModuleRepository=null,
             IUserTitleRepository userTitleRepository=null,IDepartmentDepartmentTypeRepository departmentDepartmentTypeRepository=null,
             IMRMUserDepartmentTypeRepository userDepartmentTypeRepository=null) :base(
                 unitOfWork, _userRepository, channelRepository,userChannelRepository,moduleRepository,companyRepository,roleRepository,departmentRepository,loggerService,
                 userRoleModuleRepository,userTitleRepository,departmentDepartmentTypeRepository,userDepartmentTypeRepository)
        {

        }
    }
}
