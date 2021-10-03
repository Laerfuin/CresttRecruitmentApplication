using CresttRecruitmentApplication.Domain.Models.Employee;
using CresttRecruitmentApplication.Domain.Repositories.FakeDataStores;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using System;
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

        public async Task<Employee> GetById(Guid key)
        {
            return await Task.FromResult(FakeEmployeeStore.Employees.FirstOrDefault(a => a.Key == key));
        }
    }
}