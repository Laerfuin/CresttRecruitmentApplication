using CresttRecruitmentApplication.Domain.Models.Employee;
using System;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Domain.Repositories.Interfaces
{
    public interface IEmployeeWriteRepository
    {
        Task Create(Employee value);

        Task Modify(Employee value);

        Task Delete(Guid key);
    }
}