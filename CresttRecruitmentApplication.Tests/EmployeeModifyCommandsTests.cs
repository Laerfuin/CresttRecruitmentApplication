//using CresttRecruitmentApplication.Application.Commands;
//using CresttRecruitmentApplication.Application.Dtos;
//using CresttRecruitmentApplication.Application.QueryHandlers;
//using CresttRecruitmentApplication.Domain.Enums;
//using CresttRecruitmentApplication.Domain.Models.Employee;
//using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
//using CresttRecruitmentApplication.Tests.Extensions;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace CresttRecruitmentApplication.Tests
//{
//    [TestFixture]
//    [Parallelizable]
//    public class EmployeeModifyCommandsTests
//    {
//        private Mock<IEmployeeReadRepository> _employeeReadRepository;
//        private Mock<IEmployeeWriteRepository> _employeeWriteRepository;
//        private Mock<IEmployeeUtilityRepository> _employeeUtilityRepository;

//        [SetUp]
//        public void Setup()
//        {
//            _employeeReadRepository = new Mock<IEmployeeReadRepository>();
//            _employeeWriteRepository = new Mock<IEmployeeWriteRepository>();
//            _employeeUtilityRepository = new Mock<IEmployeeUtilityRepository>();
//        }

//        [Test]
//        public void ModifyEmployeeHandler_WithIncorrectKey_ShouldThrowAnException()
//        {
//            _employeeReadRepository
//                .Setup(a => a.GetById(It.IsAny<Guid>()))
//                .Throws(new ArgumentNullException());

//            var command = new ModifyEmployeeCommand(new ExtendedEmployeeDto());
//            var handler = new ModifyEmployeeHandler(
//                _employeeReadRepository.Object,
//                _employeeWriteRepository.Object,
//                _employeeUtilityRepository.Object);

//            Assert.ThrowsAsync(
//                typeof(ArgumentNullException),
//                async () => await handler.Handle(command, new CancellationToken()));
//        }

//        private ModifyEmployeeCommand PrepareExistingEmployeeCommand()
//        {
//            var command = new ModifyEmployeeCommand(new ExtendedEmployeeDto
//            {
//                Name = "Jan",
//                LastName = "Kowalski",
//                DateOfBirth = DateTime.UtcNow,
//                Gender = (byte)GenderType.Male,
//                Pesel = "12345678912"
//            });

//            var id = new EmployeeIdentityNumber(1);
//            var name = new EmployeeName(command.Values.Name);
//            var peselNumber = new EmployeePeselNumber(command.Values.Pesel);
//            var lastName = new EmployeeLastName(command.Values.LastName);
//            var dateOfBirth = new EmployeeDateOfBirth(command.Values.DateOfBirth);
//            var gender = new EmployeeGender((GenderType)command.Values.Gender);

//            var preparedEmployee = new Employee(
//                Guid.NewGuid(),
//                id,
//                peselNumber,
//                dateOfBirth,
//                lastName,
//                name,
//                gender);

//            _employeeReadRepository
//                .Setup(a => a.GetById(preparedEmployee.Id))
//                .Returns(Task.FromResult(preparedEmployee));

//            command.Values.Id = preparedEmployee.Id.ToString();

//            return command;
//        }

//        [Test]
//        public void ModifyEmployeeHandler_WithCorrectValues_ShouldPass()
//        {
//            _employeeUtilityRepository.SetPeselCheckResult(false);

//            var command = PrepareExistingEmployeeCommand();

//            _employeeWriteRepository
//                .Setup(a => a.Update(It.IsAny<Employee>()))
//                .Returns(Task.CompletedTask);

//            var handler = new ModifyEmployeeHandler(
//                _employeeReadRepository.Object,
//                _employeeWriteRepository.Object,
//                _employeeUtilityRepository.Object);

//            Assert.DoesNotThrowAsync(async () => await handler.Handle(command, new CancellationToken()));

//            _employeeWriteRepository.Verify(a => a.Update(It.IsAny<Employee>()));
//        }

//        [Test]
//        public void ModifyEmployeeHandler_WithNoValues_ShouldThrowAnException()
//        {
//            var command = PrepareExistingEmployeeCommand();

//            var handler = new ModifyEmployeeHandler(
//                _employeeReadRepository.Object,
//                _employeeWriteRepository.Object,
//                _employeeUtilityRepository.Object);

//            command.Values.Name = null;

//            Assert.ThrowsAsync(
//                typeof(ArgumentNullException),
//                async () => await handler.Handle(command, new CancellationToken()));
//        }

//        [Test]
//        public void ModifyEmployeeHandler_WithIncorrectValues_ShouldThrowAnException()
//        {
//            var command = PrepareExistingEmployeeCommand();

//            var handler = new ModifyEmployeeHandler(
//                _employeeReadRepository.Object,
//                _employeeWriteRepository.Object,
//                _employeeUtilityRepository.Object);

//            command.Values.Name = "";

//            Assert.ThrowsAsync(
//                typeof(ArgumentException),
//                async () => await handler.Handle(command, new CancellationToken()));
//        }

//        [Test]
//        public void ModifyEmployeeHandler_WithTakenPeselNumber_ShouldThrowAnException()
//        {
//            var command = PrepareExistingEmployeeCommand();

//            command.Values.Pesel = "11111222233";

//            _employeeUtilityRepository.SetPeselCheckResult(true);

//            var handler = new ModifyEmployeeHandler(
//                _employeeReadRepository.Object,
//                _employeeWriteRepository.Object,
//                _employeeUtilityRepository.Object);

//            Assert.ThrowsAsync(
//                typeof(ArgumentException),
//                async () => await handler.Handle(command, new CancellationToken()));
//        }
//    }
//}