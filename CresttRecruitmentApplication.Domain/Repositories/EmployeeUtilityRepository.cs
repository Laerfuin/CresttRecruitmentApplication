using CresttRecruitmentApplication.Domain.Repositories.FakeDataStores;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using System.Linq;

namespace CresttRecruitmentApplication.Domain.Repositories
{
    internal class EmployeeUtilityRepository : IEmployeeUtilityRepository
    {
        public bool CheckIfPeselNumberIsTaken(string value)
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