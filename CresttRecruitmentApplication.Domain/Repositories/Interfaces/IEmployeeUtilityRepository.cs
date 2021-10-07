using CresttRecruitmentApplication.Domain.Models.Employee;

namespace CresttRecruitmentApplication.Domain.Repositories.Interfaces
{
    public interface IEmployeeUtilityRepository
    {
        bool CheckIfPeselNumberIsTaken(EmployeePeselNumber value);

        EmployeeIdentityNumber GetHighestTakenIdentityNumber();
    }
}