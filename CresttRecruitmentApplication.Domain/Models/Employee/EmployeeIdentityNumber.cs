using CresttRecruitmentApplication.Domain.Core;
using System;
using System.Linq;

namespace CresttRecruitmentApplication.Domain.Models.Employee
{
    public class EmployeeIdentityNumber : GenericValueObjectWithValidation<string>
    {
        public EmployeeIdentityNumber(string value) : base(value)
        {
        }

        public EmployeeIdentityNumber(int value) : base(GenerateID(value))
        {
        }

        protected override void Validate(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value.Length != 8)
                throw new ArgumentException($"Value {value} is out of range");

            if (value.Any(a => !char.IsDigit(a)))
                throw new ArgumentException($"Value {value} is incorrect");
        }

        private static string GenerateID(int value) // TODO CR zwróciłbym uwagę na nieupublicznianie niepotrzebnie metod
        {
            if (value < 1 || value > 99999999)
                throw new ArgumentOutOfRangeException($"Value {value} is out of range"); // TODO CR brakuje sprawdzenia, że value < 1

            return $"{value,8:D8}";
        }

        public int GetAsNumber() => int.Parse(Value);
    }
}