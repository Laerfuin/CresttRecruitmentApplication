using CresttRecruitmentApplication.Application.Dtos;
using MediatR;

namespace CresttRecruitmentApplication.Application.Commands
{
    public class ModifyEmployeeCommand : IRequest
    {
        public ExtendedEmployeeDto Values { get; }

        public ModifyEmployeeCommand(ExtendedEmployeeDto values)
        {
            Values = values;
        }
    }
}