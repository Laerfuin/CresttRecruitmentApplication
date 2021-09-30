using CresttRecruitmentApplication.Domain.Enums;
using CresttRecruitmentApplication.Domain.Models.Employee;
using System;

namespace CresttRecruitmentApplication.Domain.Builders.Interfaces
{
    public interface IEmployeeBuilder
    {
        IEmployeeBuilder SetDateOfBirth(DateTime value);

        IEmployeeBuilder SetGender(GenderType value);

        IEmployeeBuilder SetLastName(string value);

        IEmployeeBuilder SetName(string value);

        IEmployeeBuilder SetPesel(string value);

        Employee ToModifiedEmployee(Guid key, EmployeeID id);

        Employee ToNewEmployee();
    }
}