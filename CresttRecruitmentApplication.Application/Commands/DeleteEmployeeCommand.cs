using CresttRecruitmentApplication.Domain.Models.Employee;
using MediatR;

namespace CresttRecruitmentApplication.Application.Commands
{
    public class DeleteEmployeeCommand : IRequest
    {
        public EmployeeId Id { get; }

        public DeleteEmployeeCommand(EmployeeId id)
        {
            Id = id;
        }
    }
}