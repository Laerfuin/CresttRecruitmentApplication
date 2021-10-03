using CresttRecruitmentApplication.Application.Commands;
using CresttRecruitmentApplication.Application.Dtos;
using CresttRecruitmentApplication.Application.QueryHandlers;
using CresttRecruitmentApplication.Domain.Enums;
using CresttRecruitmentApplication.Domain.Models.Employee;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using CresttRecruitmentApplication.Tests.Extensions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Tests
{
    [TestFixture]
    [Parallelizable]
    public class EmployeeCreateCommandsTests
    {
        private Mock<IEmployeeWriteRepository> _employeeWriteRepository;
        private Mock<IEmployeeUtilityRepository> _employeeUtilityRepository;

        [SetUp]
        public void Setup()
        {
            _employeeWriteRepository = new Mock<IEmployeeWriteRepository>();
            _employeeUtilityRepository = new Mock<IEmployeeUtilityRepository>();
        }

        [Test]
        public void CreateEmployeeHandler_WithTakenPeselNumber_ShouldThrowAnException()
        {
            _employeeUtilityRepository.SetPeselCheckResult(true);

            var command = new CreateEmployeeCommand(new CreateEmployeeDto());
            var handler = new CreateEmployeeHandler(
                _employeeWriteRepository.Object,
                _employeeUtilityRepository.Object);

            Assert.ThrowsAsync(
                typeof(ArgumentException),
                async () => await handler.Handle(command, new CancellationToken()));

            _employeeUtilityRepository.Verify(a => a.CheckIfPeselNumberIsTaken(It.IsAny<string>()));
        }

        [Test]
        public void CreateEmployeeHandler_WithCorrectValues_ShouldPass()
        {
            var command = new CreateEmployeeCommand(new CreateEmployeeDto
            {
                Name = "Jan",
                LastName = "Kowalski",
                DateOfBirth = DateTime.UtcNow,
                Gender = (byte)GenderType.Male,
                Pesel = "12345678912"
            });

            _employeeUtilityRepository.SetPeselCheckResult(false);
            _employeeUtilityRepository.SetGetIdResult(1);

            _employeeWriteRepository
                .Setup(a => a.Create(It.IsAny<Employee>()))
                .Returns(Task.CompletedTask);

            var handler = new CreateEmployeeHandler(
                _employeeWriteRepository.Object,
                _employeeUtilityRepository.Object);

            Assert.DoesNotThrowAsync(async () => await handler.Handle(command, new CancellationToken()));

            _employeeWriteRepository.Verify(a => a.Create(It.IsAny<Employee>()));
        }

        [Test]
        public void CreateEmployeeHandler_WithNoValues_ShouldThrowAnException()
        {
            var command = new CreateEmployeeCommand(new CreateEmployeeDto());
            var handler = new CreateEmployeeHandler(
                _employeeWriteRepository.Object,
                _employeeUtilityRepository.Object);

            _employeeUtilityRepository.SetPeselCheckResult(false);
            _employeeUtilityRepository.SetGetIdResult(1);

            Assert.ThrowsAsync(
                typeof(ArgumentNullException),
                async () => await handler.Handle(command, new CancellationToken()));
        }

        [Test]
        public void CreateEmployeeHandler_WithMissingValues_ShouldThrowAnException()
        {
            var command = new CreateEmployeeCommand(new CreateEmployeeDto { Name = "" });
            var handler = new CreateEmployeeHandler(
                _employeeWriteRepository.Object,
                _employeeUtilityRepository.Object);

            _employeeUtilityRepository.SetPeselCheckResult(false);
            _employeeUtilityRepository.SetGetIdResult(1);

            Assert.ThrowsAsync(
                typeof(ArgumentException),
                async () => await handler.Handle(command, new CancellationToken()));
        }
    }
}