using CresttRecruitmentApplication.Domain.Models.Employee;
using System.Collections.Generic;

namespace CresttRecruitmentApplication.Domain.Repositories.FakeDataStores
{
    internal class FakeEmployeeStore
    {
        internal static List<Employee> Employees = new();
    }
}