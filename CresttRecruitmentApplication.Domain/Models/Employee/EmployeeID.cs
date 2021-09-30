using CresttRecruitmentApplication.Domain.Core;
using System;
using System.Linq;

namespace CresttRecruitmentApplication.Domain.Models.Employee
{
    public class EmployeeID : GenericValueObject<string>
    {
        public EmployeeID(string value) : base(value)
        {
        }

        protected override void Validate(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value.Length != 8)
                throw new ArgumentException($"Value {value} is out of range");

            if (value.Any(a => !char.IsDigit(a)))
                throw new ArgumentException($"Value {value} is incorrect");
        }
    }
}