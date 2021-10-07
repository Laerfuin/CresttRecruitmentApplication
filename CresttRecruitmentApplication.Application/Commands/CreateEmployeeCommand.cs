using CresttRecruitmentApplication.Domain.Models.Employee;
using MediatR;

namespace CresttRecruitmentApplication.Application.Commands
{
    public class CreateEmployeeCommand : IRequest
    {
        public EmployeeName Name { get; }
        public EmployeeGender Gender { get; }
        public EmployeeLastName LastName { get; }
        public EmployeeDateOfBirth DateOfBirth { get; }
        public EmployeePeselNumber PeselNumber { get; }

        public CreateEmployeeCommand(
            EmployeeName name,
            EmployeeGender gender,
            EmployeeLastName lastName,
            EmployeeDateOfBirth dateOfBirth,
            EmployeePeselNumber peselNumber)
        {
            Name = name;
            Gender = gender;
            LastName = lastName; 
            DateOfBirth = dateOfBirth;
            PeselNumber = peselNumber;
        }
    }
}