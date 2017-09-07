using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disney.MRM.DANG.Service.Implementations;
using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.Service.Contracts;
using Disney.MRM.DANG.Repository;
using Disney.MRM.DANG.API.Managers.BudgetPlanner;
using Disney.MRM.DANG.API.Contracts;
using AutoMapper;


namespace Disney.MRM.DANG.API.Test.MockObject.Manager
{
    class BudgetPlannerTowManagerMock : BudgetPlannerTowManager
    {
        public BudgetPlannerTowManagerMock(IBudgetPlanTowService budgetPlanTowService = null,
                                           IBudgetService budgetService = null,
                                           IMappingEngine mapper = null)
            : base(budgetPlanTowService,
                   budgetService,
                   mapper) { }
    }
}
