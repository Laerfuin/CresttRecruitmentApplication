using CresttRecruitmentApplication.Domain.Models.Employee;
using MediatR;

namespace CresttRecruitmentApplication.Application.Commands
{
    public class ModifyEmployeeCommand : IRequest
    {
        public EmployeeId Id { get; }
        public EmployeeName Name { get; }
        public EmployeeGender Gender { get; }
        public EmployeeLastName LastName { get; }
        public EmployeeDateOfBirth DateOfBirth { get; }
        public EmployeePeselNumber PeselNumber { get; }

        public ModifyEmployeeCommand(
            EmployeeId id,
            EmployeeName name,
            EmployeeGender gender,
            EmployeeLastName lastName,
            EmployeeDateOfBirth dateOfBirth,
            EmployeePeselNumber peselNumber)
        {
            Id = id;
            Name = name;
            Gender = gender;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PeselNumber = peselNumber;
        }
    }
}