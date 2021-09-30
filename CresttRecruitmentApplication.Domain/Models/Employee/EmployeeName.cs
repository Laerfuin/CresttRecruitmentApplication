using CresttRecruitmentApplication.Domain.Core;
using System;

namespace CresttRecruitmentApplication.Domain.Models.Employee
{
    public class EmployeeName : GenericValueObject<string>
    {
        public EmployeeName(string value) : base(value)
        {
        }

        protected override void Validate(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value.Length < 1 || value.Length > 25)
                throw new ArgumentException($"Value {value} is out of range");
        }
    }
}