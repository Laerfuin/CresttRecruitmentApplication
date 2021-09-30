using CresttRecruitmentApplication.Application.Queries;
using CresttRecruitmentApplication.Application.QueryHandlers;
using CresttRecruitmentApplication.Domain.Builders.Implementation;
using CresttRecruitmentApplication.Domain.Enums;
using CresttRecruitmentApplication.Domain.Models.Employee;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using CresttRecruitmentApplication.Domain.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Tests
{
    [TestFixture]
    [Parallelizable]
    public class EmployeeQueriesTests
    {
        private Mock<IEmployeeReadRepository> _employeeRepository;
        private readonly List<Employee> fakeEmployeeStore = new();
        private Guid existingEmployeeKey;

        [SetUp]
        public void Setup()
        {
            _employeeRepository = new Mock<IEmployeeReadRepository>();

            var utilityServiceMock = new Mock<IEmployeeUtilityService>();

            utilityServiceMock.Setup(a => a.GetFreeID()).Returns(1);

            var employeeBuilder = new EmployeeBuilder(utilityServiceMock.Object);

            employeeBuilder
                .SetDateOfBirth(DateTime.UtcNow)
                .SetGender(GenderType.Female)
                .SetLastName("Kowalski")
                .SetName("Jan")
                .SetPesel("12345678912");

            var createdEmployee = employeeBuilder.ToNewEmployee();

            fakeEmployeeStore.Add(createdEmployee);

            existingEmployeeKey = createdEmployee.Key;
        }

        [Test]
        public async Task GetAllEmployeeHandler_ShouldReturnOneRecord()
        {
            _employeeRepository
                .Setup(a => a.GetAll())
                .Returns(Task.Run(() =>
                {
                    return fakeEmployeeStore.AsEnumerable();
                }));

            var handler = new GetAllEmployeesHandler(_employeeRepository.Object);

            var employees = await handler.Handle(
                new GetAllEmployeesQuery(),
                new CancellationToken());

            Assert.AreEqual(employees.Count(), 1);
            Assert.AreEqual(employees.First().Key.ToString(), existingEmployeeKey.ToString());
        }

        [Test]
        public async Task GetAllEmployeeHandler_WithCorrectKey_ShouldPass()
        {
            _employeeRepository
                .Setup(a => a.GetById(existingEmployeeKey))
                .Returns(Task.Run(() =>
                {
                    return fakeEmployeeStore.FirstOrDefault(a => a.Key == existingEmployeeKey);
                }));

            var handler = new GetEmployeeByKeyHandler(_employeeRepository.Object);

            var employee = await handler.Handle(
                new GetEmployeeByKeyQuery(existingEmployeeKey),
                new CancellationToken());

            Assert.NotNull(employee);
            Assert.AreEqual(employee.Key.ToString(), existingEmployeeKey.ToString());
        }

        [Test]
        public void GetAllEmployeeHandler_WithIncorrectKey_ShouldFail()
        {
            var incorrectKey = new Guid();

            _employeeRepository
                .Setup(a => a.GetById(incorrectKey))
                .Returns(Task.Run(() =>
                {
                    return fakeEmployeeStore.FirstOrDefault(a => a.Key == incorrectKey);
                }));

            var handler = new GetEmployeeByKeyHandler(_employeeRepository.Object);

            Assert.ThrowsAsync<NullReferenceException>(async () => await handler.Handle(
                new GetEmployeeByKeyQuery(incorrectKey),
                new CancellationToken()));
        }
    }
}