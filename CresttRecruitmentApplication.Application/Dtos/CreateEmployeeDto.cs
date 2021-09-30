using System;

namespace CresttRecruitmentApplication.Application.Dtos
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string Pesel { get; set; }
        public byte Gender { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}