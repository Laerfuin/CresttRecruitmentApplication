using CresttRecruitmentApplication.Domain.Core;
using CresttRecruitmentApplication.Domain.Enums;

namespace CresttRecruitmentApplication.Domain.Models.Employee
{
    public class EmployeeGender : GenericEnumValueObject<GenderType>
    {
        public EmployeeGender(GenderType value) : base(value)
        {
        }
    }
}