using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
using Moq;

namespace CresttRecruitmentApplication.Tests.Extensions
{
    internal static class EmployeeUtilityRepositoryMockExtensions
    {
        internal static void SetPeselCheckResult(this Mock<IEmployeeUtilityRepository> target, bool result)
        {
            target
                .Setup(a => a.CheckIfPeselNumberIsTaken(It.IsAny<string>()))
                .Returns(result);
        }

        internal static void SetGetIdResult(this Mock<IEmployeeUtilityRepository> target, int result)
        {
            target
                .Setup(a => a.GetFreeID())
                .Returns(result);
        }
    }
}