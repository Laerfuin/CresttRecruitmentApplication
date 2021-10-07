//using CresttRecruitmentApplication.Application.Commands;
//using CresttRecruitmentApplication.Application.QueryHandlers;
//using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace CresttRecruitmentApplication.Tests
//{
//    [TestFixture]
//    [Parallelizable]
//    public class EmployeeDeleteCommandTests
//    {
//        private Mock<IEmployeeWriteRepository> _employeeWriteRepository;

//        [SetUp]
//        public void Setup()
//        {
//            _employeeWriteRepository = new Mock<IEmployeeWriteRepository>();
//        }

//        [Test]
//        public void DeleteEmployeeHandler_WithCorrectKey_ShouldPass()
//        {
//            var randomGuid = Guid.NewGuid();

//            _employeeWriteRepository
//                .Setup(a => a.Delete(randomGuid))
//                .Returns(Task.CompletedTask);

//            var handler = new DeleteEmployeeHandler(_employeeWriteRepository.Object);

//            Assert.DoesNotThrowAsync(
//                async () => await handler.Handle(new DeleteEmployeeCommand(randomGuid), new CancellationToken()));

//            _employeeWriteRepository.Verify(a => a.Delete(randomGuid));
//        }

//        [Test]
//        public void DeleteEmployeeHandler_WithIncorrectKey_ShouldThrowAnException()
//        {
//            var randomGuid = Guid.NewGuid();

//            _employeeWriteRepository
//                .Setup(a => a.Delete(randomGuid))
//                .Throws(new ArgumentNullException());

//            var handler = new DeleteEmployeeHandler(_employeeWriteRepository.Object);

//            Assert.ThrowsAsync(
//                typeof(ArgumentNullException),
//                async () => await handler.Handle(new DeleteEmployeeCommand(randomGuid), new CancellationToken()));

//            _employeeWriteRepository.Verify(a => a.Delete(randomGuid));
//        }
//    }
//}