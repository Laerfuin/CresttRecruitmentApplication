using CresttRecruitmentApplication.Domain.Models.Employee;
using CresttRecruitmentApplication.Domain.Repositories.FakeDataStores;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Domain.Repositories
{
    internal class EmployeeWriteRepository : IEmployeeWriteRepository
    {
        public async Task Create(Employee value)
        {
            await Task.Run(() =>
            {
                FakeEmployeeStore.Employees.Add(value);
            });
        }

        public async Task Delete(Guid key)
        {
            await Task.Run(() =>
            {
                FakeEmployeeStore.Employees.Remove(FakeEmployeeStore.Employees.First(a => a.Key == key));
            });
        }

        public async Task Modify(Employee value)
        {
            await Task.Run(() =>
            {
                var record = FakeEmployeeStore.Employees.First(a => a.Key == value.Key);

                record = value;
            });
        }
    }
}