using System;
using System.Collections.Generic;
using Disney.MRM.DANG.API.Contracts;
using Disney.MRM.DANG.API.Managers;
using Disney.MRM.DANG.API.Managers.Deliverable;
using Disney.MRM.DANG.DataAccess;
using Disney.MRM.DANG.Repository;
using Disney.MRM.DANG.Interface;
using Disney.MRM.DANG.API.Managers.Documents;
using Disney.MRM.DANG.Service.Contracts;
using Disney.MRM.DANG.Model;
using Disney.MRM.DANG.MongoDbRepository.Contracts;
using Disney.MRM.DANG.Service.Implementations;
using Disney.MRM.DANG.Core;
using Disney.MRM.DANG.MongoDbModel;
using Disney.MRM.DANG.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Linq.Expressions;
using Moq;

namespace Disney.MRM.DANG.API.Test.Controllers
{
    [TestClass]
    public class DocumentControllerTest
    {
        private Moq.MockRepository mockRepository;
        private Moq.Mock<IDocumentRepository> mockDocumentRepository;
        private Moq.Mock<IDocumentsService> mockDocumentService;
        private Moq.Mock<IDocumentAccessTypeRepository> mockDocumentAccessTypeRepository;
        private Moq.Mock<IDocument_MRMUserRepository> mockMRMUserRepository;
        private Moq.Mock<IUserRepository> mockUserRepository;
        private Moq.Mock<IDeliverableDocumentRepository> mockDeliverableDocumentRepository;
        private Moq.Mock<IUnitOfWork> mockUnitOfWork;
        private Moq.Mock<IUserService> mockUserService;
        private Moq.Mock<IDropDownListService> mockDropDownListService;
        private Moq.Mock<IContractRequest_MasterVendor_DocumentRepository> mockContractRequestMasterVendorDocumentRepository;

        [TestInitialize]
        public void ClassInit()
        {
            mockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };
            mockDocumentRepository = mockRepository.Create<IDocumentRepository>();
            mockDocumentService = mockRepository.Create<IDocumentsService>();
            mockDocumentAccessTypeRepository = mockRepository.Create<IDocumentAccessTypeRepository>();
            mockMRMUserRepository = mockRepository.Create<IDocument_MRMUserRepository>();
            mockUserRepository = mockRepository.Create<IUserRepository>();
            mockDeliverableDocumentRepository = mockRepository.Create<IDeliverableDocumentRepository>();
            mockUnitOfWork = mockRepository.Create<IUnitOfWork>();
            mockUserService = mockRepository.Create<IUserService>();
            mockDropDownListService = mockRepository.Create<IDropDownListService>();
            mockContractRequestMasterVendorDocumentRepository = mockRepository.Create<IContractRequest_MasterVendor_DocumentRepository>();
        }

        #region DELETE UNIT TESTS
        [TestMethod]
        public void DeleteDocumentById_ShouldReturnExceptionUserPemissionToDeleteDenied()
        {

            #region Arrange

            var nonExistentDocId = -99;
            bool exceptionCaught = false;

            MRMUser testMRMUser = new MRMUser()
            {
                Id = 1,
                FirstName = "TestFirst",
                LastName = "TestLast"
            };


            mockUserRepository.Setup(user => user.GetById(It.IsAny<int>())).Returns((MRMUser)null);
            mockDocumentRepository.Setup(doc => doc.GetSingle(It.IsAny<Expression<Func<Document, bool>>>())).Returns(
           (Expression<Func<Document, bool>> expr) => null);
            #endregion

            #region Act
            try
            {
                var docService = new DocumentsService(mockUnitOfWork.Object, null, mockDocumentRepository.Object, mockUserRepository.Object,
                                      mockDocumentAccessTypeRepository.Object, mockMRMUserRepository.Object, mockDeliverableDocumentRepository.Object, mockContractRequestMasterVendorDocumentRepository.Object);

                docService.DeleteDocumentById(nonExistentDocId);
            }
            catch
            {
                exceptionCaught = true;
            }

            #endregion


            //Assert
            Assert.IsTrue(exceptionCaught);
        }

        [TestMethod]
        public void DeleteDocumentById_ShouldReturnExceptionUserPemissionDeniedIsActiveFlagFalse()
        {
            #region Arrange

            var docId = 1;
            bool exceptionCaught = false;

            MRMUser testMRMUser = new MRMUser()
            {
                Id = 1,
                FirstName = "TestFirst",
                LastName = "TestLast"
            };

            Document testDocument = new Document()
            {
                Id = 1,
                Name = "TestDocumentName",
                OthersCanDeleteFlag = true,
                DocumentAccessTypeId = 4, //User Only
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName",
                Description = "TestDescription",
                IsActiveFlag = false
            };

            mockUserRepository.Setup(user => user.GetById(It.IsAny<int>())).Returns((MRMUser)null);
            mockDocumentRepository.Setup(doc => doc.GetSingle(It.IsAny<Expression<Func<Document, bool>>>())).Returns(
           (Expression<Func<Document, bool>> expr) => testDocument);
            #endregion

            #region Act
            try
            {
                var docService = new DocumentsService(mockUnitOfWork.Object, null, mockDocumentRepository.Object, mockUserRepository.Object,
                                      mockDocumentAccessTypeRepository.Object, mockMRMUserRepository.Object, mockDeliverableDocumentRepository.Object, mockContractRequestMasterVendorDocumentRepository.Object);

                docService.DeleteDocumentById(docId);
            }
            catch
            {
                exceptionCaught = true;
            }

            #endregion


            //Assert
            Assert.IsTrue(exceptionCaught);
        }

        [TestMethod]
        public void DeleteDocumentById_ShouldReturnExceptionUserPemissionDeniedOthersCanDeleteFlagFalse()
        {
            #region Arrange

            var docId = 1;
            bool exceptionCaught = false;

            MRMUser testMRMUser = new MRMUser()
            {
                Id = 1,
                FirstName = "TestFirst",
                LastName = "TestLast"
            };

            Document testDocument = new Document()
            {
                Id = 1,
                Name = "TestDocumentName",
                OthersCanDeleteFlag = false,
                DocumentAccessTypeId = 4, //User Only
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName",
                Description = "TestDescription",
                IsActiveFlag = true,
                OwnerMRMUserId = 99
            };

            mockUserRepository.Setup(user => user.GetById(It.IsAny<int>())).Returns((MRMUser)null);
            mockDocumentRepository.Setup(doc => doc.GetSingle(It.IsAny<Expression<Func<Document, bool>>>())).Returns(
           (Expression<Func<Document, bool>> expr) => testDocument);
            #endregion

            #region Act
            try
            {
                var docService = new DocumentsService(mockUnitOfWork.Object, null, mockDocumentRepository.Object, mockUserRepository.Object,
                                      mockDocumentAccessTypeRepository.Object, mockMRMUserRepository.Object, mockDeliverableDocumentRepository.Object, mockContractRequestMasterVendorDocumentRepository.Object);

                docService.DeleteDocumentById(docId);
            }
            catch
            {
                exceptionCaught = true;
            }

            #endregion


            //Assert
            Assert.IsTrue(exceptionCaught);
        }

        [TestMethod]
        public void DeleteDocumentById_ShouldReturnExceptionUserPemissionDeniedDocAccessTypeIdDeptMismatch()
        {
            #region Arrange

            var docId = 1;
            bool exceptionCaught = false;

            MRMUser testMRMUser = new MRMUser()
            {
                Id = 1,
                FirstName = "TestFirst",
                LastName = "TestLast",
                DepartmentId = 19//Finance
            };

            DocumentAccessType testDocAccessType = new DocumentAccessType()
            {
                Code = Constants.DocumentAccessType.Department,
                Name = "Department"
            };

            Document testDocument = new Document()
            {
                Id = 1,
                Name = "TestDocumentName",
                OthersCanDeleteFlag = true,
                DocumentAccessTypeId = 1, //Department
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName",
                Description = "TestDescription",
                IsActiveFlag = true,
                OwnerMRMUserId = 99,
                DocumentAccessType = testDocAccessType
            };

            mockUserRepository.Setup(user => user.GetById(It.IsAny<int>())).Returns((MRMUser)null);
            mockDocumentRepository.Setup(doc => doc.GetSingle(It.IsAny<Expression<Func<Document, bool>>>())).Returns(
           (Expression<Func<Document, bool>> expr) => testDocument);
            #endregion

            #region Act
            try
            {
                var docService = new DocumentsService(mockUnitOfWork.Object, null, mockDocumentRepository.Object, mockUserRepository.Object,
                                      mockDocumentAccessTypeRepository.Object, mockMRMUserRepository.Object, mockDeliverableDocumentRepository.Object, mockContractRequestMasterVendorDocumentRepository.Object);

                docService.DeleteDocumentById(docId);
            }
            catch
            {
                exceptionCaught = true;
            }

            #endregion


            //Assert
            Assert.IsTrue(exceptionCaught);
        }

        [TestMethod]
        public void DeleteDocumentById_ShouldSuccessfullyDelete()
        {
            #region Arrange

            var docId = 1;
            bool exceptionCaught = false;

            MRMUser testMRMUser = new MRMUser()
            {
                Id = 1,
                FirstName = "TestFirst",
                LastName = "TestLast",
                DepartmentId = 84//MRM Team

            };

            DocumentAccessType testDocAccessType = new DocumentAccessType()
            {
                Code = Constants.DocumentAccessType.Department,
                Name = "Department"
            };

            Document testDocument = new Document()
            {
                Id = 1,
                Name = "TestDocumentName",
                OthersCanDeleteFlag = true,
                DocumentAccessTypeId = 1, //Department
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName",
                FileId = Guid.Empty,
                Description = "TestDescription",
                IsActiveFlag = true,
                OwnerMRMUserId = 99,
                DocumentAccessType = testDocAccessType
            };

            mockUserRepository.Setup(user => user.GetById(It.IsAny<long>())).Returns(testMRMUser);

            mockDocumentRepository.Setup(doc => doc.GetSingle(It.IsAny<Expression<Func<Document, bool>>>())).Returns(
           (Expression<Func<Document, bool>> expr) => testDocument);

            mockDocumentRepository.Setup(doc => doc.Update(It.IsAny<Document>()));

            mockDeliverableDocumentRepository.Setup(mongo => mongo.Delete(It.IsAny<Guid>()));

            mockUnitOfWork.Setup(uow => uow.Commit());
            #endregion

            #region Act
            try
            {
                var docService = new DocumentsService(mockUnitOfWork.Object, null, mockDocumentRepository.Object, mockUserRepository.Object,
                                      mockDocumentAccessTypeRepository.Object, mockMRMUserRepository.Object, mockDeliverableDocumentRepository.Object, mockContractRequestMasterVendorDocumentRepository.Object);
                docService.DeleteDocumentById(docId);
            }
            catch (Exception ex)
            {
                exceptionCaught = true;
            }

            #endregion


            //Assert
            Assert.IsFalse(exceptionCaught);
        }
        #endregion

        #region SAVE UNIT TEST

        [TestMethod]
        public void SaveDocumentUpdate_FailedDueToIsActiveFalse()
        {

            bool exceptionCaught = false;
            //Arrange

            MRMUser testMRMUser = new MRMUser()
            {
                Id = 1,
                FirstName = "TestFirst",
                LastName = "TestLast",
                DepartmentId = 84//MRM Team

            };

            Document testDocument = new Document()
            {
                Id = 1,
                Name = "TestDocumentName",
                OthersCanDeleteFlag = true,
                DocumentAccessTypeId = 4, //User Only
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName",
                Description = "TestDescription",
                IsActiveFlag = false
            };

            List<Document_MRMUser> testDocMRMUsers = new List<Document_MRMUser>();

            Document_MRMUser testDocMRMUser = new Document_MRMUser()
            {
                MRMUserId = 0
            };

            testDocMRMUsers.Add(testDocMRMUser);

            DocumentAccessType testDocAccessType = new DocumentAccessType()
            {
                Code = Constants.DocumentAccessType.Department
            };

            Document testExistingDocument = new Document()
            {
                Id = 1,
                Name = "TestDocumentName",
                OthersCanDeleteFlag = true,
                DocumentAccessTypeId = 4, //User Only
                DocumentAccessType = testDocAccessType,
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName",
                Description = "TestDescriptionChange",
                IsActiveFlag = false,
                Document_MRMUser = testDocMRMUsers,
                OwnerMRMUserId = 0
            };

            mockUserRepository.Setup(user => user.GetById(It.IsAny<long>())).Returns(testMRMUser);
            mockDocumentRepository.Setup(doc => doc.GetSingle(It.IsAny<Expression<Func<Document, bool>>>())).Returns(
           (Expression<Func<Document, bool>> expr) => testExistingDocument);
            try
            {
                var docService = new DocumentsService(mockUnitOfWork.Object, null, mockDocumentRepository.Object, mockUserRepository.Object,
                                   mockDocumentAccessTypeRepository.Object, mockMRMUserRepository.Object, mockDeliverableDocumentRepository.Object, mockContractRequestMasterVendorDocumentRepository.Object);

                var result = docService.Save(testDocument, null);
            }
            catch (Exception ex)
            {
                exceptionCaught = true;
            }

            //Assert
            Assert.IsTrue(exceptionCaught);

        }

        [TestMethod]
        public void SaveDocumentUpdate_FailedDueToDocAccessTypeMismatchDept()
        {

            bool exceptionCaught = false;
            //Arrange

            MRMUser testMRMUser = new MRMUser()
            {
                Id = 1,
                FirstName = "TestFirst",
                LastName = "TestLast",
                DepartmentId = 84//MRM Team

            };

            Document testDocument = new Document()
            {
                Id = 1,
                Name = "TestDocumentName",
                OthersCanDeleteFlag = true,
                DocumentAccessTypeId = 4, //User Only
                DepartmentId = 74,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName",
                Description = "TestDescription",
                IsActiveFlag = true
            };

            List<Document_MRMUser> testDocMRMUsers = new List<Document_MRMUser>();

            Document_MRMUser testDocMRMUser = new Document_MRMUser()
            {
                MRMUserId = 0
            };

            testDocMRMUsers.Add(testDocMRMUser);

            DocumentAccessType testDocAccessType = new DocumentAccessType()
            {
                Code = Constants.DocumentAccessType.Department
            };

            Document testExistingDocument = new Document()
            {
                Id = 1,
                Name = "TestDocumentName",
                OthersCanDeleteFlag = true,
                DocumentAccessTypeId = 4, //User Only
                DocumentAccessType = testDocAccessType,
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName",
                Description = "TestDescriptionChange",
                IsActiveFlag = false,
                Document_MRMUser = testDocMRMUsers,
                OwnerMRMUserId = 0
            };

            mockUserRepository.Setup(user => user.GetById(It.IsAny<long>())).Returns(testMRMUser);
            mockDocumentRepository.Setup(doc => doc.GetSingle(It.IsAny<Expression<Func<Document, bool>>>())).Returns(
           (Expression<Func<Document, bool>> expr) => testExistingDocument);
            try
            {
                var docService = new DocumentsService(mockUnitOfWork.Object, null, mockDocumentRepository.Object, mockUserRepository.Object,
                                   mockDocumentAccessTypeRepository.Object, mockMRMUserRepository.Object, mockDeliverableDocumentRepository.Object, mockContractRequestMasterVendorDocumentRepository.Object);

                var result = docService.Save(testDocument, null);
            }
            catch (Exception ex)
            {
                exceptionCaught = true;
            }

            //Assert
            Assert.IsTrue(exceptionCaught);

        }

        [TestMethod]
        public void SaveDocumentUpdate_Success()
        {

            bool exceptionCaught = false;
            int result = 0;
            //Arrange

            MRMUser testMRMUser = new MRMUser()
            {
                Id = 1,
                FirstName = "TestFirst",
                LastName = "TestLast",
                DepartmentId = 84//MRM Team

            };

            Document testDocument = new Document()
            {
                Id = 1,
                Name = "TestDocumentName",
                OthersCanDeleteFlag = true,
                DocumentAccessTypeId = 4, //User Only
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName",
                Description = "TestDescription",
                IsActiveFlag = true
            };

            List<Document_MRMUser> testDocMRMUsers = new List<Document_MRMUser>();

            Document_MRMUser testDocMRMUser = new Document_MRMUser()
            {
                MRMUserId = 0
            };

            testDocMRMUsers.Add(testDocMRMUser);

            DocumentAccessType testDocAccessType = new DocumentAccessType()
            {
                Code = Constants.DocumentAccessType.Department
            };

            Document testExistingDocument = new Document()
            {
                Id = 1,
                Name = "TestDocumentName",
                OthersCanDeleteFlag = true,
                DocumentAccessTypeId = 4, //User Only
                DocumentAccessType = testDocAccessType,
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName",
                Description = "TestDescriptionChange",
                IsActiveFlag = true,
                Document_MRMUser = testDocMRMUsers,
                OwnerMRMUserId = 0
            };

            mockUserRepository.Setup(user => user.GetById(It.IsAny<long>())).Returns(testMRMUser);
            mockDocumentRepository.Setup(doc => doc.GetSingle(It.IsAny<Expression<Func<Document, bool>>>())).Returns(
           (Expression<Func<Document, bool>> expr) => testExistingDocument);

            mockDocumentRepository.Setup(doc => doc.Update(It.IsAny<Document>()));

            mockUnitOfWork.Setup(uow => uow.Commit());

            try
            {
                var docService = new DocumentsService(mockUnitOfWork.Object, null, mockDocumentRepository.Object, mockUserRepository.Object,
                                   mockDocumentAccessTypeRepository.Object, mockMRMUserRepository.Object, mockDeliverableDocumentRepository.Object, mockContractRequestMasterVendorDocumentRepository.Object);

                result = docService.Save(testDocument, null);
            }
            catch (Exception ex)
            {
                exceptionCaught = true;
            }

            //Assert
            Assert.IsFalse(exceptionCaught);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void SaveDocumentInsert_Success()
        {

            bool exceptionCaught = false;
            int result = 0;
            //Arrange

            byte[] fileData = { 0x32, 0x00, 0x1E, 0x00 };

            DeliverableDocument testDeliverableDocument = new DeliverableDocument()
            {
                DeliverableId = 1,
                FileData = fileData,
                FileName = "NewFileName",
                FileSize = 1000,
                FileType = "MongoFileType",
                CreateDate = DateTime.Now,
                UserId = 0,
                UserName = "networklogin",
                IsActive = true,
                Id = Guid.Empty
            };

            MRMUser testMRMUser = new MRMUser()
            {
                Id = 1,
                FirstName = "TestFirst",
                LastName = "TestLast",
                DepartmentId = 84//MRM Team

            };

            Document testDocument = new Document()
            {
                Id = 1,
                Name = "TestDocumentName",
                OthersCanDeleteFlag = true,
                DocumentAccessTypeId = 4, //User Only
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName",
                Description = "TestDescription",
                IsActiveFlag = true,
                FileId = null
            };

            List<Document_MRMUser> testDocMRMUsers = new List<Document_MRMUser>();

            Document_MRMUser testDocMRMUser = new Document_MRMUser()
            {
                MRMUserId = 0
            };

            testDocMRMUsers.Add(testDocMRMUser);

            DocumentAccessType testDocAccessType = new DocumentAccessType()
            {
                Code = Constants.DocumentAccessType.Department
            };

            mockUserRepository.Setup(user => user.GetById(It.IsAny<long>())).Returns(testMRMUser);
            mockDocumentRepository.Setup(doc => doc.GetSingle(It.IsAny<Expression<Func<Document, bool>>>())).Returns(
           (Expression<Func<Document, bool>> expr) => null);

            mockDeliverableDocumentRepository.Setup(dd => dd.Add(It.IsAny<DeliverableDocument>())).Returns(true);

            mockDocumentRepository.Setup(doc => doc.Add(It.IsAny<Document>())).Returns(testDocument);

            mockUnitOfWork.Setup(uow => uow.Commit());

            try
            {
                var docService = new DocumentsService(mockUnitOfWork.Object, null, mockDocumentRepository.Object, mockUserRepository.Object,
                                   mockDocumentAccessTypeRepository.Object, mockMRMUserRepository.Object, mockDeliverableDocumentRepository.Object, mockContractRequestMasterVendorDocumentRepository.Object);

                result = docService.Save(testDocument, testDeliverableDocument);
            }
            catch (Exception ex)
            {
                exceptionCaught = true;
            }

            //Assert
            Assert.IsFalse(exceptionCaught);
            Assert.IsTrue(testDocument.FileId == Guid.Empty);

        }

        [TestMethod]
        public void SaveDocumentInsert_FailFileExtTooLong()
        {

            bool exceptionCaught = false;
            string errorMsg = string.Empty;
            string fileExtPartialErrorMsg = "25 chars";

            //Arrange
            DocumentUploadViewModel testDocViewModel = new DocumentUploadViewModel()
            {
                FileExtension = "12345678901234567890123456",// > 25 chars
                DeliverableId = 1,
                DocumentAccessTypeId = 1//DEPT
            };

            //Act
            try
            {
                var docManager = new DocumentsManager(mockDocumentService.Object, mockUserService.Object, mockDropDownListService.Object);

                docManager.Save(testDocViewModel);
            }
            catch (Exception ex)
            {
                exceptionCaught = true;
                errorMsg = ex.Message;
            }

            //Assert
            Assert.IsTrue(exceptionCaught);
            Assert.IsTrue(errorMsg.Contains(fileExtPartialErrorMsg));
        }

        [TestMethod]
        public void SaveDocumentInsert_FailDeliverableIDDoesNotExist()
        {

            bool exceptionCaught = false;
            string errorMsg = string.Empty;
            string deliverableIdPartialErrorMsg = "Deliverable Id";

            //Arrange
            DocumentUploadViewModel testDocViewModel = new DocumentUploadViewModel()
            {
                FileExtension = "1234567890123456789012345",// > 25 chars
                DocumentAccessTypeId = 1//DEPT
            };

            //Act
            try
            {
                var docManager = new DocumentsManager(mockDocumentService.Object, mockUserService.Object, mockDropDownListService.Object);

                docManager.Save(testDocViewModel);
            }
            catch (Exception ex)
            {
                exceptionCaught = true;
                errorMsg = ex.Message;
            }

            //Assert
            Assert.IsTrue(exceptionCaught);
            Assert.IsTrue(errorMsg.Contains(deliverableIdPartialErrorMsg));
        }

        #endregion

        #region GET DOCUMENT TESTS
        [TestMethod]
        public void GetDocumentsByDeliverableId_ShouldReturnGeneralAccessDocument()
        {

            //Arrange

            int testDeliverableId = 1;

            MRMUser testMRMUser = new MRMUser()
            {
                Id = 1,
                FirstName = "TestFirst",
                LastName = "TestLast",
                DepartmentId = 19//Finance

            };

            Document testDocument1 = new Document()
            {
                Id = 1,
                Name = "TestDocumentName1",
                OthersCanDeleteFlag = true,
                DocumentAccessType = new DocumentAccessType() { Code = Constants.DocumentAccessType.Department },
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName2",
                Description = "TestDescription",
                IsActiveFlag = true,
                FileId = null,
                OwnerMRMUserId = 2
            };

            Document testDocument2 = new Document()
            {
                Id = 2,
                Name = "TestDocumentName2",
                OthersCanDeleteFlag = true,
                DocumentAccessType = new DocumentAccessType() { Code = Constants.DocumentAccessType.GeneralAccess },
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName2",
                Description = "TestDescription",
                IsActiveFlag = true,
                FileId = null,
                OwnerMRMUserId = 2
            };

            //Build Document list
            List<Document> testDocumentList = new List<Document> { testDocument1, testDocument2 };

            IQueryable<Document> testDocumentQueryable = testDocumentList.AsQueryable();

            mockUserRepository.Setup(user => user.GetById(It.IsAny<long>())).Returns(testMRMUser);
            mockDocumentRepository.Setup(doc => doc.GetAll()).Returns(testDocumentQueryable);

            //Act


            var docService = new DocumentsService(mockUnitOfWork.Object, null, mockDocumentRepository.Object, mockUserRepository.Object,
                                   mockDocumentAccessTypeRepository.Object, mockMRMUserRepository.Object, mockDeliverableDocumentRepository.Object, mockContractRequestMasterVendorDocumentRepository.Object);

            var result = docService.GetDocumentsByDeliverableId(testDeliverableId).ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.FirstOrDefault().DocumentAccessType.Code == Constants.DocumentAccessType.GeneralAccess);

        }

        [TestMethod]
        public void GetDocumentsByDeliverableId_ShouldReturnAll()
        {

            //Arrange

            int testDeliverableId = 1;

            MRMUser testMRMUser = new MRMUser()
            {
                Id = 1,
                FirstName = "TestFirst",
                LastName = "TestLast",
                DepartmentId = 84//MRM Team

            };

            Document testDocument1 = new Document()
            {
                Id = 1,
                Name = "TestDocumentName1",
                OthersCanDeleteFlag = true,
                DocumentAccessType = new DocumentAccessType() { Code = Constants.DocumentAccessType.Department },
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName2",
                Description = "TestDescription",
                IsActiveFlag = true,
                FileId = null,
                OwnerMRMUserId = 2
            };

            Document testDocument2 = new Document()
            {
                Id = 2,
                Name = "TestDocumentName2",
                OthersCanDeleteFlag = true,
                DocumentAccessType = new DocumentAccessType() { Code = Constants.DocumentAccessType.GeneralAccess },
                DepartmentId = 19,//Finance
                DeliverableId = 1,
                FileName = "TestFileName2",
                Description = "TestDescription",
                IsActiveFlag = true,
                FileId = null,
                OwnerMRMUserId = 2
            };

            //Build Document list
            List<Document> testDocumentList = new List<Document> { testDocument1, testDocument2 };

            IQueryable<Document> testDocumentQueryable = testDocumentList.AsQueryable();

            mockUserRepository.Setup(user => user.GetById(It.IsAny<long>())).Returns(testMRMUser);
            mockDocumentRepository.Setup(doc => doc.GetAll()).Returns(testDocumentQueryable);

            //Act


            var docService = new DocumentsService(mockUnitOfWork.Object, null, mockDocumentRepository.Object, mockUserRepository.Object,
                                   mockDocumentAccessTypeRepository.Object, mockMRMUserRepository.Object, mockDeliverableDocumentRepository.Object, mockContractRequestMasterVendorDocumentRepository.Object);

            var result = docService.GetDocumentsByDeliverableId(testDeliverableId).ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2);

        }

        [TestMethod]
        public void GetDocumentsByDeliverableId_ShouldReturnTestFileName1()
        {

            //Arrange

            int testDeliverableId = 1;

            MRMUser testMRMUser = new MRMUser()
            {
                Id = 1,
                FirstName = "TestFirst",
                LastName = "TestLast",
                DepartmentId = 84//MRM Team

            };

            Document testDocument1 = new Document()
            {
                Id = 1,
                Name = "TestDocumentName1",
                OthersCanDeleteFlag = true,
                DocumentAccessType = new DocumentAccessType() { Code = Constants.DocumentAccessType.Department },
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName1",
                Description = "TestDescription",
                IsActiveFlag = true,
                FileId = null,
                OwnerMRMUserId = 2
            };

            //Build Document list
            List<Document> testDocumentList = new List<Document> { testDocument1 };

            IQueryable<Document> testDocumentQueryable = testDocumentList.AsQueryable();

            mockUserRepository.Setup(user => user.GetById(It.IsAny<long>())).Returns(testMRMUser);
            mockDocumentRepository.Setup(doc => doc.GetAll()).Returns(testDocumentQueryable);

            //Act
            var docService = new DocumentsService(mockUnitOfWork.Object, null, mockDocumentRepository.Object, mockUserRepository.Object,
                                   mockDocumentAccessTypeRepository.Object, mockMRMUserRepository.Object, mockDeliverableDocumentRepository.Object, mockContractRequestMasterVendorDocumentRepository.Object);

            docService.UserId = testDocument1.OwnerMRMUserId;

            var result = docService.GetDocumentsByDeliverableId(testDeliverableId).ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1);

        }

        [TestMethod]
        public void GetDocumentsByDeliverableId_ShouldReturnThreeDocuments()
        {

            //Arrange
            #region Data Setup
            int testDeliverableId = 1;
            string testDocDescription = "WillReturn";

            MRMUser testMRMUser = new MRMUser()
            {
                Id = 1,
                FirstName = "TestFirst",
                LastName = "TestLast",
                DepartmentId = 84//MRM Team

            };

            Document testDocument1 = new Document()
            {
                Id = 1,
                Name = "TestDocumentName1",
                OthersCanDeleteFlag = true,
                DocumentAccessType = new DocumentAccessType() { Code = Constants.DocumentAccessType.Department },
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName1",
                Description = "WillReturn",
                IsActiveFlag = true,
                FileId = null,
                OwnerMRMUserId = 2
            };

            Document testDocument2 = new Document()
            {
                Id = 2,
                Name = "TestDocumentName2",
                OthersCanDeleteFlag = true,
                DocumentAccessType = new DocumentAccessType() { Code = Constants.DocumentAccessType.GeneralAccess },
                DepartmentId = 19,//Finance
                DeliverableId = 1,
                FileName = "TestFileName2",
                Description = "WillReturn",
                IsActiveFlag = true,
                FileId = null,
                OwnerMRMUserId = 2
            };

            Document testDocument3 = new Document()
            {
                Id = 2,
                Name = "TestDocumentName3",
                OthersCanDeleteFlag = true,
                DocumentAccessType = null,
                DepartmentId = 19,//Finance
                DeliverableId = 1,
                FileName = "TestFileName3",
                Description = "WillReturn",
                IsActiveFlag = true,
                FileId = null,
                OwnerMRMUserId = 2
            };

            List<Document_MRMUser> testDocMRMUsers = new List<Document_MRMUser>();

            Document testDocument4 = new Document()
            {
                Id = 2,
                Name = "TestDocumentName4",
                OthersCanDeleteFlag = true,
                DocumentAccessType = new DocumentAccessType() { Code = Constants.DocumentAccessType.SpecificPeople },
                DepartmentId = 19,//Finance
                DeliverableId = 1,
                FileName = "TestFileName4",
                Description = "WillNotReturn",
                IsActiveFlag = true,
                FileId = null,
                OwnerMRMUserId = 3,
                Document_MRMUser = testDocMRMUsers,//Will not return, no users
            };

            Document testDocument5 = new Document()
            {
                Id = 2,
                Name = "TestDocumentName5",
                OthersCanDeleteFlag = true,
                DocumentAccessType = null,
                DepartmentId = 19,//Finance
                DeliverableId = 1,
                FileName = "TestFileName5",
                Description = "WillNotReturn",
                IsActiveFlag = false, //Will not return. Only Active docs
                FileId = null,
                OwnerMRMUserId = 2
            };

            Document testDocument6 = new Document()
            {
                Id = 2,
                Name = "TestDocumentName5",
                OthersCanDeleteFlag = true,
                DocumentAccessType = null,
                DepartmentId = 19,//Finance
                DeliverableId = 2,//Will not return, not matching deliverableId being passed in 
                FileName = "TestFileName5",
                Description = "WillNotReturn",
                IsActiveFlag = false,
                FileId = null,
                OwnerMRMUserId = 2
            };
            #endregion

            //Build Document list
            List<Document> testDocumentList = new List<Document> { testDocument1, testDocument2, testDocument3, testDocument4, testDocument5, testDocument6 };

            IQueryable<Document> testDocumentQueryable = testDocumentList.AsQueryable();

            mockUserRepository.Setup(user => user.GetById(It.IsAny<long>())).Returns(testMRMUser);
            mockDocumentRepository.Setup(doc => doc.GetAll()).Returns(testDocumentQueryable);

            //Act
            var docService = new DocumentsService(mockUnitOfWork.Object, null, mockDocumentRepository.Object, mockUserRepository.Object,
                                   mockDocumentAccessTypeRepository.Object, mockMRMUserRepository.Object, mockDeliverableDocumentRepository.Object, mockContractRequestMasterVendorDocumentRepository.Object);

            docService.UserId = 2;

            var result = docService.GetDocumentsByDeliverableId(testDeliverableId).ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.TrueForAll(doc => doc.Description.Equals(testDocDescription)));

        }
        [TestMethod]
        public void GetDocumentsByDeliverableId_ShouldReturnSpecificUserDocument()
        {

            //Arrange

            int testDeliverableId = 1;

            MRMUser testMRMUser = new MRMUser()
            {
                Id = 1,
                FirstName = "TestFirst",
                LastName = "TestLast",
                DepartmentId = 19//Finance

            };

            Document testDocument1 = new Document()
            {
                Id = 1,
                Name = "TestDocumentName1",
                OthersCanDeleteFlag = true,
                DocumentAccessType = new DocumentAccessType() { Code = Constants.DocumentAccessType.Department },
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName2",
                Description = "TestDescription",
                IsActiveFlag = true,
                FileId = null,
                OwnerMRMUserId = 2
            };

            Document testDocument2 = new Document()
            {
                Id = 2,
                Name = "TestDocumentName2",
                OthersCanDeleteFlag = true,
                DocumentAccessType = new DocumentAccessType() { Code = Constants.DocumentAccessType.GeneralAccess },
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestFileName2",
                Description = "TestDescription",
                IsActiveFlag = true,
                FileId = null,
                OwnerMRMUserId = 2
            };

            Document_MRMUser testSPDocUser = new Document_MRMUser()
            {
                MRMUserId = 10
            };

            List<Document_MRMUser> testSPDocUserList = new List<Document_MRMUser> { testSPDocUser };

            Document testSpecificPeople = new Document()
            {
                Id = 3,
                Name = "TestSP",
                OthersCanDeleteFlag = true,
                DocumentAccessType = new DocumentAccessType() { Code = Constants.DocumentAccessType.SpecificPeople },
                DepartmentId = 84,//MRM Team
                DeliverableId = 1,
                FileName = "TestSP",
                Description = "TestSPDescription",
                IsActiveFlag = true,
                FileId = null,
                OwnerMRMUserId = 2,
                Document_MRMUser = testSPDocUserList
            };

            //Build Document list
            List<Document> testDocumentList = new List<Document> { testDocument1, testDocument2, testSpecificPeople };

            IQueryable<Document> testDocumentQueryable = testDocumentList.AsQueryable();

            mockUserRepository.Setup(user => user.GetById(It.IsAny<long>())).Returns(testMRMUser);
            mockDocumentRepository.Setup(doc => doc.GetAll()).Returns(testDocumentQueryable);

            //Act


            var docService = new DocumentsService(mockUnitOfWork.Object, null, mockDocumentRepository.Object, mockUserRepository.Object,
                                   mockDocumentAccessTypeRepository.Object, mockMRMUserRepository.Object, mockDeliverableDocumentRepository.Object, mockContractRequestMasterVendorDocumentRepository.Object);
            docService.UserId = 10;

            var result = docService.GetDocumentsByDeliverableId(testDeliverableId).ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2);
            var specificPeopleDoc = result.Where(x => x.Id == 3).ToList();
            Assert.IsTrue(specificPeopleDoc.Count() > 0);
        }
        #endregion
    }



}