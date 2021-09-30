using CresttRecruitmentApplication.Domain.Models.Employee;
using CresttRecruitmentApplication.Domain.Repositories.FakeDataStores;
using CresttRecruitmentApplication.Domain.Services.Interfaces;
using System.Linq;

namespace CresttRecruitmentApplication.Domain.Services.Implementation
{
    public class EmployeeUtilityService : IEmployeeUtilityService
    {
        public bool CheckIfPeselNumberIsTaken(EmployeePesel value)
        {
            return FakeEmployeeStore.Employees.Any(a => a.Pesel.Equals(value));
        }

        public int GetFreeID()
        {
            return FakeEmployeeStore.Employees.Any()
                ? FakeEmployeeStore.Employees.Max(a => int.Parse(a.ID.Value)) + 1
                : 1;
        }
    }
}