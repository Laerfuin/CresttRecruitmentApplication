using CresttRecruitmentApplication.Domain.Enums;
using System;

namespace CresttRecruitmentApplication.Application.Dtos
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string PeselNumber { get; set; }
        public GenderType Gender { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}