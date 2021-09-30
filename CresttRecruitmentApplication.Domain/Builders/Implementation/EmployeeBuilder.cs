using CresttRecruitmentApplication.Domain.Builders.Interfaces;
using CresttRecruitmentApplication.Domain.Enums;
using CresttRecruitmentApplication.Domain.Models.Employee;
using CresttRecruitmentApplication.Domain.Services.Interfaces;
using System;

namespace CresttRecruitmentApplication.Domain.Builders.Implementation
{
    public class EmployeeBuilder : IEmployeeBuilder
    {
        private readonly IEmployeeUtilityService _utilityService;

        public EmployeeBuilder(IEmployeeUtilityService utilityService)
        {
            _utilityService = utilityService;
        }

        private EmployeeGender gender;
        private EmployeeName name;
        private EmployeePesel pesel;
        private EmployeeLastName lastName;
        private EmployeeDateOfBirth dateOfBirth;

        private Employee result;

        public Employee ToNewEmployee()
        {
            if (result != null)
                return result;

            var firstFreeID = GenerateID(_utilityService.GetFreeID());

            result = new Employee(
                Guid.NewGuid(),
                new EmployeeID(firstFreeID),
                pesel,
                dateOfBirth,
                lastName,
                name,
                gender);

            return result;
        }

        public Employee ToModifiedEmployee(Guid key, EmployeeID id)
        {
            if (result != null)
                return result;

            result = new Employee(
                key,
                id,
                pesel,
                dateOfBirth,
                lastName,
                name,
                gender);

            return result;
        }

        private static string GenerateID(int value) => $"{value,8:D8}";

        public IEmployeeBuilder SetName(string value)
        {
            name = new EmployeeName(value);

            return this;
        }

        public IEmployeeBuilder SetGender(GenderType value)
        {
            gender = new EmployeeGender(value);

            return this;
        }

        public IEmployeeBuilder SetPesel(string value)
        {
            var parsedValue = new EmployeePesel(value);

            if (_utilityService.CheckIfPeselNumberIsTaken(parsedValue))
                throw new ArgumentException($"Pesel {value} is taken");

            pesel = parsedValue;

            return this;
        }

        public IEmployeeBuilder SetLastName(string value)
        {
            lastName = new EmployeeLastName(value);

            return this;
        }

        public IEmployeeBuilder SetDateOfBirth(DateTime value)
        {
            dateOfBirth = new EmployeeDateOfBirth(value);

            return this;
        }
    }
}