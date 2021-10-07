using CresttRecruitmentApplication.Domain.Models.Employee;
using CresttRecruitmentApplication.Domain.Repositories.FakeDataStores;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Domain.Repositories
{
    internal class EmployeeReadRepository : IEmployeeReadRepository
    {
        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await Task.FromResult(FakeEmployeeStore.Employees);
        }

        public async Task<Employee> GetById(EmployeeId id)
        {
            return await Task.FromResult(FakeEmployeeStore.Employees.FirstOrDefault(a => a.Id == id));
        }
    }
}