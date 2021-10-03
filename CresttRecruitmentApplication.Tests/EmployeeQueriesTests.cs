using CresttRecruitmentApplication.Application.Queries;
using CresttRecruitmentApplication.Application.QueryHandlers;
using CresttRecruitmentApplication.Domain.Enums;
using CresttRecruitmentApplication.Domain.Models.Employee;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
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

        [SetUp]
        public void Setup()
        {
            _employeeRepository = new Mock<IEmployeeReadRepository>();
        }

        private void AdditionalSetUp()
        {
            var id = new EmployeeID(1);
            var name = new EmployeeName("Jan");
            var peselNumber = new EmployeePeselNumber("12345678912");
            var lastName = new EmployeeLastName("Kowalski");
            var dateOfBirth = new EmployeeDateOfBirth(DateTime.UtcNow);
            var gender = new EmployeeGender(GenderType.Female);

            var model = new Employee(
                Guid.NewGuid(),
                id,
                peselNumber,
                dateOfBirth,
                lastName,
                name,
                gender);

            fakeEmployeeStore.Add(model);
        }

        [Test]
        public async Task GetAllEmployeeHandler_ShouldReturnOneRecord()
        {
            AdditionalSetUp();

            var existingEmployeeKey = fakeEmployeeStore.First().Key;

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
        public async Task GetEmployeeByKeyHandler_WithCorrectKey_ShouldPass()
        {
            AdditionalSetUp();

            var existingEmployeeKey = fakeEmployeeStore.First().Key;

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
        public void GetEmployeeByKeyHandler_WithIncorrectKey_ShouldFail()
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