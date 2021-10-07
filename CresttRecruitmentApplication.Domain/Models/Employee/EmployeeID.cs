using CresttRecruitmentApplication.Domain.Core;
using System;
using System.Linq;

namespace CresttRecruitmentApplication.Domain.Models.Employee
{
    public class EmployeeId : GenericValueObject<Guid>
    {
        public EmployeeId() : base(Guid.NewGuid())
        {
        }

        public EmployeeId(Guid value) : base(value)
        {
        }
    }
}