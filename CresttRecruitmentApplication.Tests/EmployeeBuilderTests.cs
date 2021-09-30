using CresttRecruitmentApplication.Domain.Builders.Implementation;
using CresttRecruitmentApplication.Domain.Builders.Interfaces;
using CresttRecruitmentApplication.Domain.Enums;
using CresttRecruitmentApplication.Domain.Models.Employee;
using CresttRecruitmentApplication.Domain.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace CresttRecruitmentApplication.Tests
{
    [TestFixture]
    public class EmployeeBuilderTests
    {
        private IEmployeeBuilder _builder;
        private Mock<IEmployeeUtilityService> _employeeUtilityServiceMock;

        [SetUp]
        public void Setup()
        {
            _employeeUtilityServiceMock = new Mock<IEmployeeUtilityService>();
            _builder = new EmployeeBuilder(_employeeUtilityServiceMock.Object);
        }

        [Test]
        public void Builder_CreateEmployeeWithMissingValues_ShouldThrowException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => _builder.ToNewEmployee());
        }

        [Test]
        public void Builder_SetEmployeePesel_ShouldThrowException()
        {
            var peselNumber = "12345678912";

            _employeeUtilityServiceMock
                .Setup(a => a.CheckIfPeselNumberIsTaken(new EmployeePesel(peselNumber)))
                .Returns(true);

            Assert.Throws(typeof(ArgumentException), () => _builder.SetPesel(peselNumber));
        }

        [Test]
        public void Builder_SetEmployeePesel_ShouldPass()
        {
            var peselNumber = "12345678912";

            _employeeUtilityServiceMock
                .Setup(a => a.CheckIfPeselNumberIsTaken(new EmployeePesel(peselNumber)))
                .Returns(false);

            Assert.DoesNotThrow(() => _builder.SetPesel(peselNumber));
        }

        [Test]
        public void Builder_SetCreateEmployee_ShouldPass()
        {
            _employeeUtilityServiceMock
                .Setup(a => a.GetFreeID())
                .Returns(7);

            _builder
                .SetDateOfBirth(DateTime.UtcNow)
                .SetGender(GenderType.Female)
                .SetLastName("Kowalski")
                .SetName("Jan")
                .SetPesel("12345678912");

            Assert.DoesNotThrow(() => _builder.ToNewEmployee());

            var createdEmployee = _builder.ToNewEmployee();

            Assert.AreEqual(createdEmployee.ID.Value, "00000007");
        }
    }
}