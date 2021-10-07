//using CresttRecruitmentApplication.Domain.Repositories.Interfaces;
//using Moq;

//namespace CresttRecruitmentApplication.Tests.Extensions
//{
//    internal static class EmployeeUtilityRepositoryMockExtensions
//    {
//        internal static void SetPeselCheckResult(this Mock<IEmployeeUtilityRepository> target, bool result)
//        {
//            target
//                .Setup(a => a.CheckIfPeselNumberIsTaken(It.IsAny<string>()))
//                .Returns(result);

//            // TODO CR zamiast takiego podejścia, napisałbym bez extension method, bezpośrednio w teście, żeby nie zaciemniać obrazu:
//            // target.Setup(a => a.CheckIfPeselNumberIsTaken("123456789")).Returns(true);
//            // takie podejście jest lepsze, bo: jednocześnie konfiguruje odpowiedź dla zadanego konkretnego wejścia i jednocześnie upewnia się, że nie została wywołana metoda ze złym argumentem
//        }

//        internal static void SetGetIdResult(this Mock<IEmployeeUtilityRepository> target, int result)
//        {
//            target
//                .Setup(a => a.GetFreeIdentityNumber())
//                .Returns(result);
//            // TODO CR też bym zaimplementował bezpośrednio w teście
//            // Tłumaczę to podejście tym, że wtedy testy dużo łatwiej czytać. A wcale się tak nie rozrastają.
//        }
//    }
//}