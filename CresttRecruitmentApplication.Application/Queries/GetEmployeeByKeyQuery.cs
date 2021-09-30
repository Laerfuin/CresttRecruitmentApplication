using CresttRecruitmentApplication.Application.Dtos;
using MediatR;
using System;

namespace CresttRecruitmentApplication.Application.Queries
{
    public class GetEmployeeByKeyQuery : IRequest<ExtendedEmployeeDto>
    {
        public Guid Key { get; }

        public GetEmployeeByKeyQuery(Guid id)
        {
            Key = id;
        }
    }
}