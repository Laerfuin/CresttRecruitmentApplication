namespace CresttRecruitmentApplication.Domain.Repositories.Interfaces
{
    public interface IEmployeeUtilityRepository
    {
        bool CheckIfPeselNumberIsTaken(string value);

        int GetFreeID();
    }
}