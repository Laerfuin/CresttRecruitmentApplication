using CresttRecruitmentApplication.Domain.Core;
using System;

namespace CresttRecruitmentApplication.Domain.Models.Employee
{
    public class EmployeeLastName : GenericValueObject<string>
    {
        public EmployeeLastName(string value) : base(value)
        {
        }

        protected override void Validate(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value.Length < 1 || value.Length > 50)
                throw new ArgumentException($"Value {value} is out of range");
        }
    }
}