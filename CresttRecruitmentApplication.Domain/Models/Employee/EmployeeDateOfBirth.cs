using CresttRecruitmentApplication.Domain.Core;
using System;

namespace CresttRecruitmentApplication.Domain.Models.Employee
{
    public class EmployeeDateOfBirth : GenericValueObject<DateTime>
    {
        public EmployeeDateOfBirth(DateTime value) : base(value)
        {
        }

        protected override void Validate(DateTime value)
        {
        }
    }
}