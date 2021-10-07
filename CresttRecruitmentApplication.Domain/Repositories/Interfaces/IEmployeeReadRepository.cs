using CresttRecruitmentApplication.Domain.Models.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Domain.Repositories.Interfaces
{
    public interface IEmployeeReadRepository
    {
        Task<IEnumerable<Employee>> GetAll();

        Task<Employee> GetById(EmployeeId id);
    }
}