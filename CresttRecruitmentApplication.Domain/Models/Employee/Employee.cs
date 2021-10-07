using System;

namespace CresttRecruitmentApplication.Domain.Models.Employee
{
    public class Employee
    {
        public Employee(
            EmployeeIdentityNumber identityNumber,
            EmployeePeselNumber peselNumber,
            EmployeeDateOfBirth dateOfBirth,
            EmployeeLastName lastName,
            EmployeeName name,
            EmployeeGender gender)
        {
            Id = new EmployeeId();
            IdentityNumber = identityNumber ?? throw new ArgumentNullException(nameof(identityNumber));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PeselNumber = peselNumber ?? throw new ArgumentNullException(nameof(peselNumber));
            Gender = gender ?? throw new ArgumentNullException(nameof(gender));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            DateOfBirth = dateOfBirth ?? throw new ArgumentNullException(nameof(dateOfBirth));
        }

        public EmployeeId Id { get; }    //TODO CR to oczywiście kwestia konwencji, ale Id to z reguły jest klucz nadawany przez system (np. Guid). Nie znam konwencji, które mówi czym jest "Key".
        public EmployeeIdentityNumber IdentityNumber { get; }
        public EmployeeGender Gender { get; private set; }
        public EmployeeName Name { get; private set; }
        public EmployeePeselNumber PeselNumber { get; private set; }
        public EmployeeLastName LastName { get; private set; }
        public EmployeeDateOfBirth DateOfBirth { get; private set; }

        public void Update(
            EmployeeGender gender = null,
            EmployeeName name = null,
            EmployeePeselNumber peselNumber = null,
            EmployeeLastName lastName = null,
            EmployeeDateOfBirth dateOfBirth = null)
        {
            if (gender != null) Gender = gender;
            if (name != null) Name = name;
            if (peselNumber != null) PeselNumber = peselNumber;
            if (lastName != null) LastName = lastName;
            if (dateOfBirth != null) DateOfBirth = dateOfBirth;
        }
        //TODO CR Bardzo ważne!!!:
        // w tej klasie wszystko pięknie, natomiast brakuje mi metody "Update"/"Modify" do aktualizowania obiektu pracownika. W DDD takie podejście jest podstawą.
    }
}