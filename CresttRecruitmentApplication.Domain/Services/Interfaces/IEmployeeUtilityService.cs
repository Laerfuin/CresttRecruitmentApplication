using CresttRecruitmentApplication.Domain.Models.Employee;

namespace CresttRecruitmentApplication.Domain.Services.Interfaces
{
    public interface IEmployeeUtilityService
    {
        bool CheckIfPeselNumberIsTaken(EmployeePesel value);

        int GetFreeID();
    }
}