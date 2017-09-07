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
using Disney.MRM.DANG.API.Managers.BudgetPlanner;
using Disney.MRM.DANG.API.Test.MockObject.Manager;
using Disney.MRM.DANG.ViewModel.BudgetPlanner;
using Disney.MRM.DANG.API.AutoMapper;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Linq.Expressions;
using Disney.MRM.DANG.API.Test.MockObject.Service;
using Moq;


namespace Disney.MRM.DANG.API.Test.Controllers
{
    [TestClass]
    public class BudgetPlannerControllerTests
    {
        private Moq.MockRepository mockRepository;
        private Moq.Mock<IBudgetPlanTowService> mockBudgetPlanTowService;
        private Moq.Mock<IBudgetService> mockBudgetService;

        //IMappingEngine allows mocking of AutoMapper.Mapper 
        private Moq.Mock<IMappingEngine> mockMappingEngine;
        
        [TestInitialize]
        public void ClassInit()
        {
            mockRepository = new MockRepository(MockBehavior.Loose) { DefaultValue = DefaultValue.Mock };
            mockBudgetPlanTowService = mockRepository.Create<IBudgetPlanTowService>();
            mockBudgetService = mockRepository.Create<IBudgetService>();
            mockMappingEngine = mockRepository.Create<IMappingEngine>();
        }

       [TestMethod]
       public void GetBudgetPlannerTowHeader_ShouldIncludePreviousFiscalYearName()
        {
            #region Data Setup

            const int planId = 10;

            var budgetPlanTypeOfWork = new BudgetPlanTypeOfWork();
            budgetPlanTypeOfWork.Id = 10;
            budgetPlanTypeOfWork.PreviousFYTypeOfWorkId = 1234;
            var listBudgetPlanTypeOfWork = new List<BudgetPlanTypeOfWork>();
            listBudgetPlanTypeOfWork.Add(budgetPlanTypeOfWork);
            var testQueryableBudgetPlanTypeOfWork = listBudgetPlanTypeOfWork.AsQueryable();

            var tow = new TypeOfWork
            {
                Id = 1234,
                Name = "MyTestTOW"
            };

            var testTOWs = new List<TypeOfWork>();
            testTOWs.Add(tow);
            var testQueryableTOWs = testTOWs.AsQueryable();

            var vmPlannerTowHeader = new PlannerTowHeaderViewModel
            {
                Id = 10,
                PreviousFYTypeOfWorkId = 1234,
                PreviousFYTypeOfWorkName = null
            };

           var testListPlannerTowHeaderViewModel = new List<PlannerTowHeaderViewModel>();
           testListPlannerTowHeaderViewModel.Add(vmPlannerTowHeader);

           var testQueryableTypeOfWorkCategories = new List<TypeOfWorkCategory>().AsQueryable();
           var testQueryableBudgetPlanBudgetTypes = new List<BudgetPlanBudgetType>().AsQueryable();
           var testQueryableRatings = new List<Rating>().AsQueryable();

           #endregion

           #region Mocking

           mockBudgetPlanTowService.Setup(x => x.GetBudgetPlannerTowHeader(planId)).Returns(testQueryableBudgetPlanTypeOfWork);
           mockBudgetPlanTowService.Setup(x => x.GetTypeOfWorkCategory()).Returns(testQueryableTypeOfWorkCategories);
           mockBudgetPlanTowService.Setup(x => x.GetBudgetTypes()).Returns(testQueryableBudgetPlanBudgetTypes);
           mockBudgetPlanTowService.Setup(x => x.GetRatings()).Returns(testQueryableRatings);

           mockBudgetService.Setup(x => x.GetTOWS()).Returns(testTOWs.AsQueryable());

           mockMappingEngine.Setup(x => x.Map<IQueryable<BudgetPlanTypeOfWork>, List<PlannerTowHeaderViewModel>>(It.IsAny<IQueryable<BudgetPlanTypeOfWork>>()))
                            .Returns(testListPlannerTowHeaderViewModel);

           var budgetPlannerTowManager = new BudgetPlannerTowManagerMock(budgetPlanTowService: mockBudgetPlanTowService.Object,
                                                                         budgetService: mockBudgetService.Object,
                                                                         mapper: mockMappingEngine.Object);
           #endregion

           //Act
           var vmBudgetPlan = budgetPlannerTowManager.GetBudgetPlannerTowHeader(planId);

           //Assert
           Assert.IsTrue(vmBudgetPlan.HeaderList.FirstOrDefault().PreviousFYTypeOfWorkName == testTOWs.FirstOrDefault().Name);
       }
    }
}
