using CresttRecruitmentApplication.Domain.Models.Employee;
using CresttRecruitmentApplication.Domain.Repositories.FakeDataStores;
using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Domain.Repositories
{
    internal class EmployeeWriteRepository : IEmployeeWriteRepository
    {
        public async Task Insert(Employee value)
        {
            await Task.Run(() =>
            {
                FakeEmployeeStore.Employees.Add(value);
            });
        }

        public async Task Delete(Employee value)
        {
            await Task.Run(() =>
            {
                FakeEmployeeStore.Employees.Remove(value);
            });
        }
    }
}