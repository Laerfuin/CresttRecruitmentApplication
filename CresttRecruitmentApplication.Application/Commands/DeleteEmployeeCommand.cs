using MediatR;
using System;

namespace CresttRecruitmentApplication.Application.Commands
{
    public class DeleteEmployeeCommand : IRequest
    {
        public Guid Key { get; }

        public DeleteEmployeeCommand(Guid key)
        {
            Key = key;
        }
    }
}