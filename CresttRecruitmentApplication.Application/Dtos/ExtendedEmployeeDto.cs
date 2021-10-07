using System;

namespace CresttRecruitmentApplication.Application.Dtos
{
    public class ExtendedEmployeeDto : CreateEmployeeDto
    {
        public string IdentityNumber { get; set; }
        public Guid Id { get; set; }
    }
}