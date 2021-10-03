using System;

namespace CresttRecruitmentApplication.Domain.Models.Employee
{
    public class Employee
    {
        public Employee(Guid key,
                        EmployeeID id,
                        EmployeePeselNumber pesel,
                        EmployeeDateOfBirth dateOfBirth,
                        EmployeeLastName lastName,
                        EmployeeName name,
                        EmployeeGender gender)
        {
            Key = key;
            ID = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Pesel = pesel ?? throw new ArgumentNullException(nameof(pesel));
            Gender = gender ?? throw new ArgumentNullException(nameof(gender));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            DateOfBirth = dateOfBirth ?? throw new ArgumentNullException(nameof(dateOfBirth));
        }

        public Guid Key { get; }
        public EmployeeID ID { get; }
        public EmployeeGender Gender { get; }
        public EmployeeName Name { get; }
        public EmployeePeselNumber Pesel { get; }
        public EmployeeLastName LastName { get; }
        public EmployeeDateOfBirth DateOfBirth { get; }
    }
}