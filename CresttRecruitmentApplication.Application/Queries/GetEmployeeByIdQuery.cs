using CresttRecruitmentApplication.Application.Dtos;
using CresttRecruitmentApplication.Domain.Models.Employee;
using MediatR;

namespace CresttRecruitmentApplication.Application.Queries
{
    public class GetEmployeeByIdQuery : IRequest<ExtendedEmployeeDto>
    {
        public EmployeeId Id { get; }

        public GetEmployeeByIdQuery(EmployeeId id)
        {
            Id = id;
        }
    }
}