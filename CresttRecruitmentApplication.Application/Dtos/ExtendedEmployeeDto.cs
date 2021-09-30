namespace CresttRecruitmentApplication.Application.Dtos
{
    public class ExtendedEmployeeDto : CreateEmployeeDto
    {
        public string ID { get; set; }
        public string Key { get; set; }
    }
}