using CresttRecruitmentApplication.Application.Commands;
using CresttRecruitmentApplication.Application.QueryHandlers;
using CresttRecruitmentApplication.Domain.Enums;
using CresttRecruitmentApplication.Domain.Models.Employee;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
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
            var takenPeselNumber = new EmployeePeselNumber("12345678912");

            _employeeUtilityRepository
                .Setup(a => a.CheckIfPeselNumberIsTaken(takenPeselNumber))
                .Returns(true);

            var command = new CreateEmployeeCommand(null, null, null, null, takenPeselNumber);
            var handler = new CreateEmployeeHandler(
                _employeeWriteRepository.Object,
                _employeeUtilityRepository.Object);

            var ex = Assert.ThrowsAsync(
                typeof(ArgumentException),
                async () => await handler.Handle(command, new CancellationToken()));

            Assert.AreEqual(ex.Message, $"Pesel {takenPeselNumber.Value} is taken");

            _employeeUtilityRepository.Verify(a => a.CheckIfPeselNumberIsTaken(takenPeselNumber));
        }

        [Test]
        public void CreateEmployeeHandler_WithCorrectValues_ShouldPass()
        {
            var command = new CreateEmployeeCommand(
                new EmployeeName("Jan"),
                new EmployeeGender(GenderType.Male),
                new EmployeeLastName("Kowalski"),
                new EmployeeDateOfBirth(new DateTime(2000, 10, 10)),
                new EmployeePeselNumber("12345678912"));

            _employeeUtilityRepository
                .Setup(a => a.CheckIfPeselNumberIsTaken(command.PeselNumber))
                .Returns(false);
            _employeeUtilityRepository
                .Setup(a => a.GetHighestTakenIdentityNumber())
                .Returns(new EmployeeIdentityNumber(1));
            _employeeWriteRepository
                .Setup(a => a.Insert(It.IsAny<Employee>()))
                .Returns(Task.CompletedTask);

            var handler = new CreateEmployeeHandler(
                _employeeWriteRepository.Object,
                _employeeUtilityRepository.Object);

            Assert.DoesNotThrowAsync(async () => await handler.Handle(command, new CancellationToken()));

            _employeeWriteRepository.Verify(a =>
               a.Insert(It.Is<Employee>(e => 
                    e.Name.Equals(command.Name) 
                    && e.LastName.Equals(command.LastName)
                    && e.Gender.Equals(command.Gender)
                    && e.DateOfBirth.Equals(command.DateOfBirth)
                    && e.PeselNumber.Equals(command.PeselNumber)
                   )));
        }

        [Test]
        public void CreateEmployeeHandler_WithNoValues_ShouldThrowAnException()
        {
            var command = new CreateEmployeeCommand(null, null, null, null, null);
            var handler = new CreateEmployeeHandler(
                _employeeWriteRepository.Object,
                _employeeUtilityRepository.Object);

            _employeeUtilityRepository
                .Setup(a => a.CheckIfPeselNumberIsTaken(command.PeselNumber))
                .Returns(false);
            _employeeUtilityRepository
                .Setup(a => a.GetHighestTakenIdentityNumber())
                .Returns(new EmployeeIdentityNumber(1));

            var ex = Assert.ThrowsAsync(
                typeof(ArgumentNullException),
                async () => await handler.Handle(command, new CancellationToken()));

            Assert.AreEqual((ex as ArgumentNullException).ParamName, "name");
        }
    }
}