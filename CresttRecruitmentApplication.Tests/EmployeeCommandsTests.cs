using CresttRecruitmentApplication.Application.Commands;
using CresttRecruitmentApplication.Application.Dtos;
using CresttRecruitmentApplication.Application.QueryHandlers;
using CresttRecruitmentApplication.Domain.Builders.Implementation;
using CresttRecruitmentApplication.Domain.Enums;
using CresttRecruitmentApplication.Domain.Models.Employee;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using CresttRecruitmentApplication.Domain.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Tests
{
    [TestFixture]
    [Parallelizable]
    public class EmployeeCommandsTests
    {
        private Mock<IEmployeeWriteRepository> _employeeRepository;

        [SetUp]
        public void Setup()
        {
            _employeeRepository = new Mock<IEmployeeWriteRepository>();
        }

        [Test]
        public void DeleteEmployeeHandler_WithCorrectKey_ShouldPass()
        {
            var randomGuid = Guid.NewGuid();

            _employeeRepository
                .Setup(a => a.Delete(randomGuid))
                .Returns(Task.CompletedTask);

            var handler = new DeleteEmployeeHandler(_employeeRepository.Object);

            Assert.DoesNotThrowAsync(
                async () => await handler.Handle(new DeleteEmployeeCommand(randomGuid), new CancellationToken()));
        }

        [Test]
        public void DeleteEmployeeHandler_WithIncorrectKey_ShouldThrowAnException()
        {
            var randomGuid = Guid.NewGuid();

            _employeeRepository
                .Setup(a => a.Delete(randomGuid))
                .Throws(new ArgumentNullException());

            var handler = new DeleteEmployeeHandler(_employeeRepository.Object);

            Assert.ThrowsAsync(
                typeof(ArgumentNullException),
                async () => await handler.Handle(new DeleteEmployeeCommand(randomGuid), new CancellationToken()));
        }

        [Test]
        public void CreateEmployeeHandler_WithCorrectValues_ShouldPass()
        {
            var mockedUtilityService = new Mock<IEmployeeUtilityService>();
            var command = new CreateEmployeeCommand(new CreateEmployeeDto
            {
                Name = "Jan",
                LastName = "Kowalski",
                DateOfBirth = DateTime.UtcNow,
                Gender = (byte)GenderType.Male,
                Pesel = "12345678912"
            });

            mockedUtilityService
                .Setup(a => a.CheckIfPeselNumberIsTaken(new EmployeePesel(command.Values.Pesel)))
                .Returns(false);

            mockedUtilityService
                .Setup(a => a.GetFreeID())
                .Returns(1);

            var builder = new EmployeeBuilder(mockedUtilityService.Object);

            builder
                .SetDateOfBirth(command.Values.DateOfBirth)
                .SetGender((GenderType)command.Values.Gender)
                .SetLastName(command.Values.LastName)
                .SetName(command.Values.Name)
                .SetPesel(command.Values.Pesel);

            var preparedEmployee = builder.ToNewEmployee();

            _employeeRepository
                .Setup(a => a.Create(preparedEmployee))
                .Returns(Task.CompletedTask);

            var handler = new CreateEmployeeHandler(_employeeRepository.Object, builder);

            Assert.DoesNotThrowAsync(async () => await handler.Handle(command, new CancellationToken()));
        }

        [Test]
        public void CreateEmployeeHandler_WithNoValues_ShouldThrowAnException()
        {
            var command = new CreateEmployeeCommand(new CreateEmployeeDto());
            var handler = new CreateEmployeeHandler(_employeeRepository.Object, new EmployeeBuilder(null));

            Assert.ThrowsAsync(
                typeof(ArgumentNullException),
                async () => await handler.Handle(command, new CancellationToken()));
        }

        [Test]
        public void CreateEmployeeHandler_WithIncorrectValues_ShouldThrowAnException()
        {
            var command = new CreateEmployeeCommand(new CreateEmployeeDto { Name = "" });
            var handler = new CreateEmployeeHandler(_employeeRepository.Object, new EmployeeBuilder(null));

            Assert.ThrowsAsync(
                typeof(ArgumentException),
                async () => await handler.Handle(command, new CancellationToken()));
        }

        [Test]
        public void ModifyEmployeeHandler_WithIncorrectKey_ShouldThrowAnException()
        {
            var randomGuid = Guid.NewGuid();
            var readRepository = new Mock<IEmployeeReadRepository>();

            readRepository
                .Setup(a => a.GetById(randomGuid))
                .Throws(new ArgumentNullException());

            var command = new ModifyEmployeeCommand(new ExtendedEmployeeDto());
            var handler = new ModifyEmployeeHandler(new EmployeeBuilder(null), readRepository.Object, _employeeRepository.Object);

            Assert.ThrowsAsync(
                typeof(ArgumentNullException),
                async () => await handler.Handle(command, new CancellationToken()));
        }

        [Test]
        public void ModifyEmployeeHandler_WithNoValues_ShouldThrowAnException()
        {
            var readRepository = new Mock<IEmployeeReadRepository>();
            var command = new ModifyEmployeeCommand(new ExtendedEmployeeDto { Key = Guid.NewGuid().ToString() });
            var handler = new ModifyEmployeeHandler(new EmployeeBuilder(null), readRepository.Object, _employeeRepository.Object);

            Assert.ThrowsAsync(
                typeof(ArgumentNullException),
                async () => await handler.Handle(command, new CancellationToken()));
        }

        [Test]
        public void ModifyEmployeeHandler_WithIncorrectValues_ShouldThrowAnException()
        {
            var readRepository = new Mock<IEmployeeReadRepository>();
            var command = new ModifyEmployeeCommand(new ExtendedEmployeeDto { Key = Guid.NewGuid().ToString(), Name = "" });
            var handler = new ModifyEmployeeHandler(new EmployeeBuilder(null), readRepository.Object, _employeeRepository.Object);

            Assert.ThrowsAsync(
                typeof(ArgumentException),
                async () => await handler.Handle(command, new CancellationToken()));
        }

        [Test]
        public void ModifyEmployeeHandler_WithCorrectValues_ShouldPass()
        {
            var readRepository = new Mock<IEmployeeReadRepository>();
            var mockedUtilityService = new Mock<IEmployeeUtilityService>();
            var command = new ModifyEmployeeCommand(new ExtendedEmployeeDto
            {
                Name = "Jan",
                LastName = "Kowalski",
                DateOfBirth = DateTime.UtcNow,
                Gender = (byte)GenderType.Male,
                Pesel = "12345678912"
            });

            mockedUtilityService
                .Setup(a => a.CheckIfPeselNumberIsTaken(new EmployeePesel(command.Values.Pesel)))
                .Returns(false);

            mockedUtilityService
                .Setup(a => a.GetFreeID())
                .Returns(1);

            var builder = new EmployeeBuilder(mockedUtilityService.Object);

            builder
                .SetDateOfBirth(command.Values.DateOfBirth)
                .SetGender((GenderType)command.Values.Gender)
                .SetLastName(command.Values.LastName)
                .SetName(command.Values.Name)
                .SetPesel(command.Values.Pesel);

            var preparedEmployee = builder.ToNewEmployee();

            command.Values.Key = preparedEmployee.Key.ToString();

            _employeeRepository
                .Setup(a => a.Modify(preparedEmployee))
                .Returns(Task.CompletedTask);

            readRepository
                .Setup(a => a.GetById(preparedEmployee.Key))
                .Returns(Task.FromResult(preparedEmployee));

            var handler = new ModifyEmployeeHandler(
                builder,
                readRepository.Object,
                _employeeRepository.Object);

            Assert.DoesNotThrowAsync(async () => await handler.Handle(command, new CancellationToken()));
        }
    }
}