using CresttRecruitmentApplication.Application.Exceptions;
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
            var id = new EmployeeIdentityNumber(1);
            var name = new EmployeeName("Jan");
            var peselNumber = new EmployeePeselNumber("12345678912");
            var lastName = new EmployeeLastName("Kowalski");
            var dateOfBirth = new EmployeeDateOfBirth(DateTime.UtcNow);
            var gender = new EmployeeGender(GenderType.Female);

            var model = new Employee(
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

            var existingEmployeeKey = fakeEmployeeStore.First().Id;

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
            Assert.AreEqual(employees.First().Id, existingEmployeeKey.Value);
        }

        [Test]
        public async Task GetEmployeeByKeyHandler_WithCorrectKey_ShouldPass()
        {
            AdditionalSetUp();

            var existingEmployeeId = fakeEmployeeStore.First().Id;

            _employeeRepository
                .Setup(a => a.GetById(existingEmployeeId))
                .Returns(Task.Run(() =>
                {
                    return fakeEmployeeStore.FirstOrDefault(a => a.Id == existingEmployeeId);
                }));

            var handler = new GetEmployeeByIdHandler(_employeeRepository.Object);

            var employee = await handler.Handle(
                new GetEmployeeByIdQuery(existingEmployeeId),
                new CancellationToken());

            Assert.NotNull(employee);
            Assert.AreEqual(employee.Id, existingEmployeeId.Value);
        }

        [Test]
        public void GetEmployeeByIdHandler_WithIncorrectId_ShouldFail()
        {
            var incorrectId = new EmployeeId(new Guid());

            _employeeRepository
                .Setup(a => a.GetById(incorrectId))
                .Returns(Task.Run(() =>
                {
                    return fakeEmployeeStore.FirstOrDefault(a => a.Id == incorrectId);
                }));

            var handler = new GetEmployeeByIdHandler(_employeeRepository.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
                new GetEmployeeByIdQuery(incorrectId),
                new CancellationToken()));
        }
    }
}