using CresttRecruitmentApplication.Domain.Models.Employee;
using CresttRecruitmentApplication.Domain.Repositories.FakeDataStores;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using System.Linq;

namespace CresttRecruitmentApplication.Domain.Repositories
{
    internal class EmployeeUtilityRepository : IEmployeeUtilityRepository
    {
        public bool CheckIfPeselNumberIsTaken(EmployeePeselNumber value)
        {
            return FakeEmployeeStore.Employees.Any(a => a.PeselNumber.Equals(value));
        }

        public EmployeeIdentityNumber GetHighestTakenIdentityNumber()
        {
            var highestValue = FakeEmployeeStore.Employees.Any()
                ? FakeEmployeeStore.Employees.Max(a => int.Parse(a.IdentityNumber.Value))
                : 1;

            return new EmployeeIdentityNumber(highestValue);
        }
    }
}