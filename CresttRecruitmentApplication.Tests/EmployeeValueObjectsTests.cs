using CresttRecruitmentApplication.Domain.Enums;
using CresttRecruitmentApplication.Domain.Models.Employee;
using NUnit.Framework;
using System;

namespace CresttRecruitmentApplication.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class EmployeeValueObjectsTests
    {
        [Test]
        public void EmployeeID_CreateWithCorrectStringValue_ShouldPass()
        {
            Assert.DoesNotThrow(() => new EmployeeIdentityNumber("00000001"));
        }

        [Test]
        public void EmployeeID_CreateWithNullStringValue_ShouldFail()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new EmployeeIdentityNumber(null));
        }

        [Test]
        public void EmployeeID_CreateWithWrongSizedStringValue_ShouldFail()
        {
            Assert.Throws(typeof(ArgumentException), () => new EmployeeIdentityNumber("000000000000000000000001"));
        }

        [Test]
        public void EmployeeID_CreateWithLetters_ShouldFail()
        {
            Assert.Throws(typeof(ArgumentException), () => new EmployeeIdentityNumber("0dsd0001"));
        }

        [Test]
        public void EmployeeID_CreateWithCorrectNumericValue_ShouldPass()
        {
            Assert.DoesNotThrow(() => new EmployeeIdentityNumber(763));
        }

        [Test]
        public void EmployeeID_CreateWithWrongSizedNumericValue_ShouldFail()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => new EmployeeIdentityNumber(199999999));
        }

        [Test]
        public void EmployeeGender_CreateWithCorrectValue_ShouldPass()
        {
            Assert.DoesNotThrow(() => new EmployeeGender(GenderType.Male));
        }

        [Test]
        public void EmployeeGender_CreateWithIncorrectValue_ShouldFail()
        {
            Assert.Throws(typeof(ArgumentException), () => new EmployeeGender(0));
        }

        [Test]
        public void EmployeePeselNumber_CreateWithCorrectValue_ShouldPass()
        {
            Assert.DoesNotThrow(() => new EmployeePeselNumber("12345678912"));
        }

        [Test]
        public void EmployeePeselNumber_CreateWithNullValue_ShouldFail()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new EmployeePeselNumber(null));
        }

        [Test]
        public void EmployeePeselNumber_CreateWithWrongSizedValue_ShouldFail()
        {
            Assert.Throws(typeof(ArgumentException), () => new EmployeePeselNumber("234234"));
        }

        [Test]
        public void EmployeePeselNumber_CreateWithLetters_ShouldFail()
        {
            Assert.Throws(typeof(ArgumentException), () => new EmployeePeselNumber("563816dg4hw"));
        }

        [Test]
        public void EmployeeName_CreateWithCorrectValue_ShouldPass()
        {
            var correctValue = "Jan";

            Assert.DoesNotThrow(() => new EmployeeName(correctValue));
        }

        [Test]
        public void Employee_CreateEmployeeWithMissingValues_ShouldThrowAnException()
        {
            Assert.Throws(
                typeof(ArgumentNullException),
                () => new Employee(null, null, null, null, null, null));
        }

        [Test]
        public void EmployeeName_CreateWithNullValue_ShouldFail()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new EmployeeName(null));
        }

        [Test]
        public void EmployeeName_CreateWithWrongSizedValue_ShouldFail()
        {
            var tooShortName = "";
            var tooLongName = "namenamenamenamenamenamenamenamenamenamenamenamename" +
                "namenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamename";

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => new EmployeeName(tooShortName));
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => new EmployeeName(tooLongName));
        }

        [Test]
        public void EmployeeLastName_CreateWithCorrectValue_ShouldPass()
        {
            var correctValue = "Kowalski";

            Assert.DoesNotThrow(() => new EmployeeLastName(correctValue));
        }

        [Test]
        public void EmployeeLastName_CreateWithNullValue_ShouldFail()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new EmployeeLastName(null));
        }

        [Test]
        public void EmployeeLastName_CreateWithWrongSizedValue_ShouldFail()
        {
            var tooShortLastName = "";
            var tooLongLastName = "namenamenamenamenamenamenamenamenamenamenamenamename" +
                "namenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamename";

            Assert.Throws(typeof(ArgumentException), () => new EmployeeIdentityNumber(tooShortLastName));
            Assert.Throws(typeof(ArgumentException), () => new EmployeeIdentityNumber(tooLongLastName));
        }

        [Test]
        public void EmployeeDateOfBirth_CreateWithCorrectValue_ShouldPass()
        {
            Assert.DoesNotThrow(() => new EmployeeDateOfBirth(DateTime.Now));
        }
    }
}