using CresttRecruitmentApplication.Domain.Core;
using System;
using System.Linq;

namespace CresttRecruitmentApplication.Domain.Models.Employee
{
    public class EmployeePeselNumber : GenericValueObjectWithValidation<string>
    {
        public EmployeePeselNumber(string value) : base(value)
        {
        }

        protected override void Validate(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value.Length != 11)
                throw new ArgumentException($"Value {value} is out of range");

            if (value.Any(a => !char.IsDigit(a)))
                throw new ArgumentException($"Value {value} is incorrect");
        }
    }
}