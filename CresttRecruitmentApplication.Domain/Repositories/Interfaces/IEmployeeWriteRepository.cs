using CresttRecruitmentApplication.Domain.Models.Employee;
using System.Threading.Tasks;

namespace CresttRecruitmentApplication.Domain.Repositories.Interfaces
{
    public interface IEmployeeWriteRepository
    {
        Task Insert(Employee value);

        Task Delete(Employee value);
    }
}