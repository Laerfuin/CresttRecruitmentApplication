using CresttRecruitmentApplication.Application.Dtos;
using MediatR;
using System;

namespace CresttRecruitmentApplication.Application.Commands
{
    public class CreateEmployeeCommand : IRequest<Guid>
    {
        public CreateEmployeeDto Values { get; }

        public CreateEmployeeCommand(CreateEmployeeDto values)
        {
            Values = values;
        }
    }
}